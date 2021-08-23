using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grendezvous.Classes.Beans;
using Grendezvous.Classes.DAO;
using Grendezvous.Forms;
using System.IO;
using SecuGen.FDxSDKPro.Windows;

namespace Grendezvous.Users
{
    public partial class Identification : UserControl
    {
        #region VARIABLES

        ////Other variables
        private int idPatient = 0;
        private string pathPhoto = "";



        #endregion

        #region FONCTIONS_SECUGEN
        ////Secugen variables
        SGFingerPrintManager _frm;
        SGFPMDeviceName _deviceName;
        Int32 _deviceId;
        Int32 _error;
        Int32 _mImageWidth;
        Int32 _mImageHeight;
        int _type = 1, _number, _pouce = 1;
        private byte[] _mRegMin1;
        private byte[] _mRegMin2;
        private byte[] _mVerify;
        private Byte[] _photo;
        bool _matched1, isPresence;

        #endregion
        #region FONCTIONS
        void initSecugen()
        {
            try
            {
                _mRegMin1 = new Byte[400];
                _mRegMin2 = new Byte[400];
                _mVerify = new Byte[400];
                _deviceName = SGFPMDeviceName.DEV_AUTO;
                _deviceId = Convert.ToInt32(SGFPMPortAddr.USB_AUTO_DETECT);
                labelDevice.Text = "Lecteur : " + _deviceName.ToString();
                _error = _frm.Init(_deviceName);
                _error = _frm.OpenDevice(_deviceId);

                if (_error == Convert.ToInt32(SGFPMError.ERROR_NONE))
                {
                    GetBtn_Click();
                    labelDevice.Text = "Lecteur connecté avec succèss.";
                    _frm.EnableAutoOnEvent(true, (int)this.Handle);
                }
                else
                {
                    labelDevice.Text = "Veuillez brancher un lecteur d'empreinte.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        void getImage()
        {
            try
            {
                _matched1 = false;
                Int32 elapTime = Environment.TickCount;
                Byte[] fpImage;
                labelDevice.Text = "Lecteur : " + _deviceName.ToString();
                fpImage = new Byte[_mImageWidth * _mImageHeight];
                _error = _frm.GetImage(fpImage);
                if (_error == (Int32)SGFPMError.ERROR_NONE)
                {
                    elapTime = Environment.TickCount - elapTime;
                    if (_type == 1)
                    {
                        if (_number == 1)
                        {
                            DrawImage(fpImage, pictureBox1);
                            _error = _frm.CreateTemplate(fpImage, _mRegMin1);
                            if (_error == (Int32)SGFPMError.ERROR_NONE)
                            {
                                _number++;
                            }
                            else
                                labError.Text = FingerPrintDAO.DisplayError(_error);
                        }
                        else
                        {
                            DrawImage(fpImage, pictureBox1);
                            _error = _frm.CreateTemplate(fpImage, _mRegMin2);
                            if (_error == (Int32)SGFPMError.ERROR_NONE)
                            {
                                _number = 1;
                            }
                            else
                                labError.Text = FingerPrintDAO.DisplayError(_error);
                        }
                    }
                    else
                    {
                        DrawImage(fpImage, pictureBox1);
                        _error = _frm.CreateTemplate(fpImage, _mRegMin2);
                        if (_error == (Int32)SGFPMError.ERROR_NONE)
                            FingerPrint.getInstance().FingerTest = _mRegMin1;
                        else
                            labError.Text = FingerPrintDAO.DisplayError(_error);
                    }
                    labError.Text = FingerPrintDAO.DisplayError(_error);
                    label4.Text = "Image capturée : " + elapTime + " millisecondes";
          
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void verify(bool veri)
        {
            _matched1 = true;
            SGFPMSecurityLevel secu_level;

            secu_level = (SGFPMSecurityLevel)7;

            _error = _frm.MatchTemplate(_mRegMin1, _mRegMin1, secu_level, ref _matched1);
            if (_error == (Int32)SGFPMError.ERROR_NONE)
            {
                if (_matched1)
                {
                    lblresult.Text = "Verification Success";
                    lblresult.ForeColor = Color.Green;
                    FingerPrint.getInstance().Finger = pictureBox1.Image;
                    if (veri)
                    {
                        if (_pouce == 1)
                            FingerPrint.getInstance().FindFinger = _mRegMin1;
                        else
                            FingerPrint.getInstance().FindFinger = _mRegMin1;
                        MessageBox.Show("Ok");
                    }
                }
                else
                {
                    lblresult.Text = "Verification Failed";
                    lblresult.ForeColor = Color.Red;
                }
        }
            else
                MessageBox.Show(FingerPrintDAO.DisplayError(_error));
        }

        private void GetBtn_Click()
        {
            SGFPMDeviceInfoParam pInfo = new SGFPMDeviceInfoParam();
            Int32 iError = _frm.GetDeviceInfo(pInfo);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                _mImageWidth = pInfo.ImageWidth;
                _mImageHeight = pInfo.ImageHeight;
            }
        }

  

        private void DrawImage(Byte[] imgData, PictureBox picBox)
        {
            int colorval;
            Bitmap bmp = new Bitmap(_mImageWidth, _mImageHeight);
            picBox.Image = (Image)bmp;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    colorval = (int)imgData[(j * _mImageWidth) + i];
                    bmp.SetPixel(i, j, Color.FromArgb(colorval, colorval, colorval));
                }
            }
            picBox.Refresh();
        }

        private void btnEmpr_Click(object sender, EventArgs e)
        {
           // verify(true);
        }

        //private Byte[] convertImageToBinary(Image image)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //    _photo = ms.ToArray();
        //    return _photo;
        //}



        protected override void WndProc(ref Message message)
        {
            if (message.Msg == (int)SGFPMMessages.DEV_AUTOONEVENT)
            {
                if (message.WParam.ToInt32() == (Int32)SGFPMAutoOnEvent.FINGER_ON)
                {
                    label4.Text = "Scanning Image....";
                    getImage();
                }
                else if (message.WParam.ToInt32() == (Int32)SGFPMAutoOnEvent.FINGER_OFF)
                {
                    lblInfo.Text = "Empreinte capturée avec succès !!!";

                }
            }
            base.WndProc(ref message);
        }
        #endregion
        public Identification()
        {
            InitializeComponent();
        }
        #region FONCTIONS

       

        void initilizeControls()
        {
            textNom.Text = "";
            textPostnom.Text = "";
            textPrenom.Text = "";
            comboClasse.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }
        void save(bool btn, int action)
        {
            if (string.IsNullOrEmpty(textNom.Text.Trim()) || string.IsNullOrEmpty(textPostnom.Text.Trim()) || 
                string.IsNullOrEmpty(textPrenom.Text.Trim()) || string.IsNullOrEmpty(comboClasse.Text.Trim()))
            {
                MessageBox.Show("Complètez tous les champs.", "Erreur...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Eleve eleve = new Eleve();
                eleve.Id = Convert.ToInt32(idPatient);
                eleve.Nom = textNom.Text.Trim();
                eleve.Postnom = textPostnom.Text.Trim();
                eleve.Prenom = textPrenom.Text.Trim();
                if (radioButton1.Checked == true) { eleve.Sexe = radioButton1.Text.Trim(); }
                else { eleve.Sexe = radioButton2.Text.Trim(); }
                eleve.Profil = convertImageToBinary(photoEleve.Image);
                eleve.Classe = comboClasse.Text.Trim();
                eleve.EmpreinteImage = convertImageTobyte(pictureBox1);
                eleve.Empreinte = FingerPrint.getInstance().FindFinger;
                if (btn)
                {
                    new EleveDAO().save(action, eleve);
                }
                new Success("Elève enregistré !!!").ShowDialog();
                initilizeControls();
                loadEeleve();
            }
        }

        void loadClasses()
        {
            comboClasse.Items.Clear();
            new ClasseDAO().loadCombo(comboClasse, "select_classe", "designation_cl");
        }

        private Byte[] convertImageToBinary(Image image)
        {
            Byte[] photo;
            MemoryStream ms = new MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            photo = ms.ToArray();
            return photo;
        }

        private Byte[] convertImageTobyte(PictureBox pic)
        {
            MemoryStream ms = new MemoryStream();
            Bitmap bmpImage = new Bitmap(pic.Image);
            Byte[] bytImage;
            bmpImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            bytImage = ms.ToArray();
            ms.Close();
            return bytImage;
        }

        private Image ConvertBinarytoImage(byte[] photo)
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
        void loadEeleve()
        {
            bunifuCustomDataGrid1.DataSource = new EleveDAO().getAll();
        }

     
        #endregion

        private void Identification_Load(object sender, EventArgs e)
        {
            loadClasses();
            loadEeleve();

            _frm = new SGFingerPrintManager();
            initSecugen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            verify(true);
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog fd = new OpenFileDialog();
                fd.Filter = "JPG Files (*.jpg)|*.jpg| all files(*.*)|*.*";
                fd.ShowDialog();
                pathPhoto = fd.FileName.ToString();
                photoEleve.Image = Image.FromFile(pathPhoto);
            }
            catch (Exception ex)
            {
                new Error(ex.Message).ShowDialog();
            }
        }

        private void btnEmpreinte_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            save(true, 1);
        }

    }
}