using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grendezvous.Classes.DAO;
using Grendezvous.Forms;
using Grendezvous.Classes.Beans;
using SecuGen.FDxSDKPro.Windows;
using System.IO;

namespace Grendezvous.Users
{
    public partial class Appel : UserControl
    {
        private int idEleve = 0;
       private int idPresence = 0;
        private int iduser = 0;
        public Appel()
        {
            InitializeComponent();
        }

        public Appel(int user)
        {
            InitializeComponent();
            this.iduser = user;
        }


        SGFingerPrintManager _FPM;
        Int32 _error;
        SGFPMDeviceName _device_name;
        Int32 _device_id;
        int type = 2;
        private byte[] m_RegMin2;
        private byte[] m_RegMin1;
        private byte[] m_Verify;
        private Int32 m_ImageWidth;
        private Int32 m_ImageHeight;
        private Byte[] photo;
        private int number, pouce;
        bool matched1, isPresence = false;


        void init()
        {
            try
            {
                m_RegMin2 = new Byte[400];
                m_RegMin1 = new Byte[400];
                m_Verify = new Byte[400];
                _device_name = SGFPMDeviceName.DEV_AUTO;
                _device_id = (Int32)(SGFPMPortAddr.USB_AUTO_DETECT);
                lbldevice.Text = "Lecteur :" + _device_name.ToString();
                _error = _FPM.Init(_device_name);
                _error = _FPM.OpenDevice(_device_id);

                if (_error == (Int32)SGFPMError.ERROR_NONE)
                {
                    GetBtn_Click();
                    lbldevice.Text = "Device connect Success";
                    _FPM.EnableAutoOnEvent(true, (int)this.Handle);
                }
                else
                {
                    //new FrmDialog("Fingerprint", "Veiller brancher le lecteur", false, 2);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == (int)SGFPMMessages.DEV_AUTOONEVENT)
            {
                if (message.WParam.ToInt32() == (Int32)SGFPMAutoOnEvent.FINGER_ON)
                {
                    lblInfo.Text = "Scanning Image....";
                    GetImage();
                }
                else if (message.WParam.ToInt32() == (Int32)SGFPMAutoOnEvent.FINGER_OFF)
                    lblInfo.Text = "Placer votre doigt";
            }
            base.WndProc(ref message);
        }

        void GetImage()
        {
            try
            {
                matched1 = false;
                Int32 elap_time;
                Byte[] fp_image;
                lbldevice.Text = "Lecteur : " + _device_name.ToString();

                elap_time = Environment.TickCount;
                fp_image = new Byte[m_ImageWidth * m_ImageHeight];

                _error = _FPM.GetImage(fp_image);

                if (_error == (Int32)SGFPMError.ERROR_NONE)
                {
                    elap_time = Environment.TickCount - elap_time;
                    if (type == 1)
                    {
                        if (number == 1)
                        {
                            DrawImage(fp_image, pictureBox1);
                            _error = _FPM.CreateTemplate(fp_image, m_RegMin1);
                            if (_error == (Int32)SGFPMError.ERROR_NONE)
                            {
                                number++;
                            }
                            else
                                lbldevice.Text = FingerPrintDAO.DisplayError(_error);
                        }
                        else
                        {
                            DrawImage(fp_image, pictureBox1);
                            _error = _FPM.CreateTemplate(fp_image, m_RegMin1);
                            if (_error == (Int32)SGFPMError.ERROR_NONE)
                            {
                                number = 1;
                            }
                            else
                                lbldevice.Text = FingerPrintDAO.DisplayError(_error);
                        }
                    }
                    else
                    {
                        DrawImage(fp_image, pictureBox1);
                        _error = _FPM.CreateTemplate(fp_image, m_RegMin1);
                        if (_error == (Int32)SGFPMError.ERROR_NONE)
                        {
                            FingerPrint.getInstance().FingerTest = m_RegMin1;
                        }
                        else
                            lbldevice.Text = FingerPrintDAO.DisplayError(_error);
                        if (isPresence)
                            FingerPrintDAO.getInstance().presence(_FPM);
                        else
                           FingerPrintDAO.getInstance().testFinger(_FPM);
                    }
                    // lbl.Text = "Image Capturer : " + elap_time + " milliseconde";
                    lblInfo.Text = "Placer votre doigt";

                }
                else
                {
                    lbldevice.Text = FingerPrintDAO.DisplayError(_error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw new Exception(ex.Message);
            }
        }
        private void GetBtn_Click()
        {
            SGFPMDeviceInfoParam pInfo = new SGFPMDeviceInfoParam();
            Int32 iError = _FPM.GetDeviceInfo(pInfo);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                m_ImageWidth = pInfo.ImageWidth;
                m_ImageHeight = pInfo.ImageHeight;
            }
        }
        private void DrawImage(Byte[] imgData, PictureBox picBox)
        {
            int colorval;
            Bitmap bmp = new Bitmap(m_ImageWidth, m_ImageHeight);
            picBox.Image = (Image)bmp;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    colorval = (int)imgData[(j * m_ImageWidth) + i];
                    bmp.SetPixel(i, j, Color.FromArgb(colorval, colorval, colorval));
                }
            }
            picBox.Refresh();
        }

        private Byte[] convertImageToBinary(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            photo = ms.ToArray();

            return photo;

        }

        private Image ConvertBinarytoImage(Byte[] photo)
        {
            try
            {
                MemoryStream ms = new MemoryStream(photo);
                return Image.FromStream(ms);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        #region FONCTIONS

        void loadClasses()
        {
            combClasse.Items.Clear();
            new ClasseDAO().loadCombo(combClasse, "select_classe", "designation_cl");
        }

        void getEleve()
        {
            int i = gridEleve.CurrentRow.Index;
            idEleve = Convert.ToInt32(gridEleve["Column1", i].Value.ToString());
            labelNom.Text = gridEleve["Column2", i].Value.ToString();
            labelPostnom.Text = gridEleve["Column3", i].Value.ToString();
            labelPrenom.Text = gridEleve["Column4", i].Value.ToString();
            labelSexe.Text = gridEleve["Column5", i].Value.ToString();
            new EleveDAO().retreview(Convert.ToInt32(gridEleve["Column1", i].Value.ToString()), photoEleveImage);

        }

        //void showReport(object obj)
        //{
        //    Reports report = obj as Reports;
        //    report.Dock = DockStyle.Fill;
        //    panel1.Controls.Clear();
        //    panel1.Controls.Add(report);
        //    panel1.Show();
        //    //lab_title.Text = "Identification du patient";
        //}

        void initComponents()
        {
            labelNom.Text = "";
            labelPostnom.Text = "";
            labelPrenom.Text = "";
            labelSexe.Text = "";
        }

        void save(bool btn, int action)
        {
            if (string.IsNullOrEmpty(labelNom.Text.Trim()) || string.IsNullOrEmpty(labelPostnom.Text.Trim()) ||
                string.IsNullOrEmpty(labelPrenom.Text.Trim()) || string.IsNullOrEmpty(labelSexe.Text.Trim()))
            {
                new Error("Pas d'élève a appelé...").ShowDialog();
            }
            else
            {
                Presence presence = new Presence();
                presence.Id = Convert.ToInt32(idPresence);
                if (radioPresence.Checked == true) { presence.Presences = 1; }   
                else if(radioAbscence.Checked == true) { presence.Presences = 0; }
                else if (radioMalade.Checked == true) { presence.Presences = 2; }
                else { presence.Presences = 0; }
                presence.Eleve = Convert.ToInt32(idEleve);
                if (btn)
                {
                    new PresencesDAO().save(action, presence);
                    new Success("Présence enregistrée !!!").ShowDialog();
                }
            }
        }

        #endregion

        private void Allrendevous_Load(object sender, EventArgs e)
        {
            initComponents();
            loadClasses();
            _FPM = new SGFingerPrintManager();
            number = 1;
            //SelectPage(type);
            init();

        }

        private void gridPatient_SelectionChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    getPatient();
            //}
            //catch (Exception ex)
            //{
            //    new Error(ex.Message);
            //}

            //Reports report = new Reports();
            //new RendevousDAO().Call_Report(iduser, report.reportViewer1, "Grendezvous.Reports.ListeRendevous.rdlc");
            //showReport(report);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (new PresencesDAO().countPresence(DateTime.Parse(DateTime.Now.ToShortDateString()), idEleve) == 1)
            {
                new Speek().speek("Présence déjà enregistrée");
                new Success("Présence déjà enregistrée !!!").ShowDialog();
                
            }
            else { save(true, 1); }
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void textsearch_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                //gridEleve.DataSource = new RendevousDAO().search(textsearch.Text.Trim(), iduser);
            }
            catch (Exception ex)
            {
                new Error(ex.Message).ShowDialog();
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Reports report = new Reports();
            //    new RendevousDAO().Call_Reportt("RENDE_P", iduser, report.reportViewer1, "Grendezvous.Reports.ListeRendevous1.rdlc");
            //    showReport(report);
            //}catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Reports report = new Reports();
            //    new RendevousDAO().Call_Reportt("RENDE_A", iduser, report.reportViewer1, "Grendezvous.Reports.ListeRendevous1.rdlc");
            //    showReport(report);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            try { 
            //{
            //    Reports report = new Reports();
            //    new RendevousDAO().Call_Reportt("RENDE_F", iduser, report.reportViewer1, "Grendezvous.Reports.ListeRendevous1.rdlc");
            //    showReport(report);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void combClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridEleve.DataSource = new ClasseDAO().triEleve(combClasse.Text.Trim());
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(combClasse.Text.Trim()))
                {
                    new Error("Sélectionner une classe...").ShowDialog();
                }else
                    gridEleve.DataSource = new ClasseDAO().searchIntriEleve(combClasse.Text.Trim(), bunifuMaterialTextbox1.Text.Trim());
            }catch(Exception ex)
            {
                MessageBox.Show("Message...", ex.Message);
            }
        }

        private void gridEleve_SelectionChanged(object sender, EventArgs e)
        {
            getEleve();
        }
    }
}
