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
using System.IO;

namespace Grendezvous.Users
{
    public partial class Eleves : UserControl
    {
        private int idEleve = 0;
        public Eleves()
        {
            InitializeComponent();
        }

        #region FONCTIONS

        void loadPatient(EleveDAO patientdoa)
        {
            gridEleve.DataSource = patientdoa.getAll();
        }
        void getEleve()
        {
            int i = gridEleve.CurrentRow.Index;
            idEleve = Convert.ToInt32(gridEleve["Column1", i].Value.ToString());
            labnom.Text = gridEleve["Column2", i].Value.ToString();
            labpostnom.Text = gridEleve["Column3", i].Value.ToString();
            labage.Text = gridEleve["Column4", i].Value.ToString();
            labeSexe.Text = gridEleve["Column5", i].Value.ToString();
            labClasse.Text = gridEleve["Column6", i].Value.ToString();
            label6.Text = "Inscrit(e) le "+ DateTime.Parse( gridEleve["Column7", i].Value.ToString()).ToShortDateString();
            new EleveDAO().retreview(Convert.ToInt32(gridEleve["Column1", i].Value.ToString()), photoElever);
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
        void delete(int action)
        {
            Eleve eleve = new Eleve();
            eleve.Id = Convert.ToInt32(idEleve);
            eleve.Nom = labnom.Text.Trim();
            eleve.Postnom = labpostnom.Text.Trim();
            eleve.Prenom = labpostnom.Text.Trim();
            eleve.Sexe = labpostnom.Text.Trim();
            eleve.Classe = labpostnom.Text.Trim();
            eleve.Profil = convertImageToBinary(pictureBox1.Image);
            eleve.Empreinte = convertImageTobyte(pictureBox1);
            new EleveDAO().save(action, eleve);
            new Success("Elève supprimé !!!").ShowDialog();
            loadPatient(new EleveDAO());
        }

        void loadEeleve()
        {
            gridEleve.DataSource = new EleveDAO().getAll();
        }

        #endregion

        private void Patients_Load(object sender, EventArgs e)
        {
            loadEeleve();
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridEleve.DataSource = new EleveDAO().search(bunifuMaterialTextbox1.Text.Trim());
            }catch(Exception ex)
            {
                new Error(ex.Message).ShowDialog();
            }
        }

        private void gridPatient_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                getEleve();
            }catch(Exception ex)
            {
                new Error(ex.Message);
            }
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            new EleveUpdate(idEleve, labnom.Text.Trim(), labpostnom.Text.Trim(), labage.Text.Trim(),
                labeSexe.Text.Trim(), labClasse.Text.Trim()).ShowDialog();
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            delete(3);
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void labadresse_Click(object sender, EventArgs e)
        {

        }

        private void labnom_Click(object sender, EventArgs e)
        {

        }

        private void bunifuSeparator1_Load(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            //Preview report = new Preview();
            //new PresencesDAO().Call_ReportPR(idPatient, report.reportViewer1, "Grendezvous.Reports.ListeRendevous2.rdlc");
            //report.ShowDialog();
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {

            Preview report = new Preview();
            new EleveDAO().Call_Report("select_eleve", report.reportViewer1, "Grendezvous.Reports.EleveInscrits.rdlc");
            report.ShowDialog();
        }
    }
}
