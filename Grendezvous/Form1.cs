using Grendezvous.Classes.Beans;
using Grendezvous.Classes.DAO;
using Grendezvous.Forms;
using Grendezvous.Users;
using System;
using System.Windows.Forms;
using SecuGen.FDxSDKPro.Windows;

namespace Grendezvous
{
    public partial class Form1 : Form
    {
        Utilisateur utilisateur = null;
        public Form1()
        {
            InitializeComponent();
        }

        #region FONCTIONS SHOWS

        void showIdentification(object obj)
        {
            Identification identification = obj as Identification;
            identification.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(identification);
            panelParent.Show();
            lab_title.Text = "Identification de l'élève";
        }

        void showEleves(object obj)
        {
            Eleves patients = obj as Eleves;
            patients.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(patients);
            panelParent.Show();
            lab_title.Text = "Elèves Inscrits";
        }

        void showUtilisateurs(object obj)
        {
            Utilisateurs utilisateurs = obj as Utilisateurs;
            utilisateurs.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(utilisateurs);
            panelParent.Show();
            lab_title.Text = "Mon Compte";
        }

        void showPresences(object obj)
        {
            Presences presences = obj as Presences;
            presences.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(presences);
            panelParent.Show();
            lab_title.Text = "Présences";
        }

        void showAppel(object obj)
        {
            Appel allrendevous = obj as Appel;
            allrendevous.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(allrendevous);
            panelParent.Show();
            lab_title.Text = "Appel en classe";
        }
        void showPresence_classes(object obj)
        {
            PresenceClasse presenceCl = obj as PresenceClasse;
            presenceCl.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(presenceCl);
            panelParent.Show();
            lab_title.Text = "Présences";
        }
        void showAbscence_cl(object obj)
        {
            AbscencesClasse abscences = obj as AbscencesClasse;
            abscences.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(abscences);
            panelParent.Show();
            lab_title.Text = "Présences";
        }
        void showMalade_cl(object obj)
        {
            MaladeClasse malades = obj as MaladeClasse;
            malades.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(malades);
            panelParent.Show();
            lab_title.Text = "Présences";
        }

        void showSettings(object obj)
        {
            Settings allrendevous = obj as Settings;
            allrendevous.Dock = DockStyle.Fill;
            panelParent.Controls.Clear();
            panelParent.Controls.Add(allrendevous);
            panelParent.Show();
            lab_title.Text = "Configurations";
        }

        void testConnexion()
        {
            try
            {
                utilisateur = new UtilisateurDAO().Test(textUsername.Text.Trim(), textPassword.Text.Trim());
                if (string.IsNullOrEmpty(utilisateur.Username) || string.IsNullOrEmpty(utilisateur.Password))
                {
                    new Error("Champs vides...").ShowDialog();
                    utilisateur = null;
                }
                else
                {
                    new UtilisateurDAO().retreview(utilisateur.Id, gunaCirclePictureBox2);
                    new Success("Vous êtes connectés").ShowDialog();
                    new UtilisateurDAO().retreview(utilisateur.Id, profil1);
                    labcompte.Text = utilisateur.Nom;
                    showEleves(new Eleves());
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur...");
            }
        }

        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void bunifuGradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            
        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showIdentification(new Identification());
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showEleves(new Eleves());
        }

        private void bunifuFlatButton9_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showUtilisateurs(new Utilisateurs(utilisateur.Id, utilisateur.Nom, utilisateur.Username, utilisateur.Password));
        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showEleves(new Eleves());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            testConnexion();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showPresences(new Presences(utilisateur.Id));
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if( utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showAppel(new Appel(utilisateur.Id));
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            showUtilisateurs(new Utilisateurs());
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            panelParent.Controls.Clear();
            panelParent.Controls.Add(label2);
            panelParent.Controls.Add(groupBox1);
            panelParent.Controls.Add(bunifuThinButton21);
            panelParent.Show();
            utilisateur = null;
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            panelParent.Controls.Clear();
            panelParent.Controls.Add(label2);
            panelParent.Controls.Add(groupBox1);
            panelParent.Controls.Add(bunifuThinButton21);
            panelParent.Show();
            utilisateur = null;
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showPresence_classes(new PresenceClasse(utilisateur.Id));
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showAbscence_cl(new AbscencesClasse(utilisateur.Id));
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showMalade_cl(new MaladeClasse(utilisateur.Id));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (utilisateur == null)
                new Error("Connectez-vous S.V.P...").ShowDialog();
            else
                showSettings(new Settings(utilisateur.Id));
        }

        //showSettings()
    }
}
