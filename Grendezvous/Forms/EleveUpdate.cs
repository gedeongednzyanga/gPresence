using Grendezvous.Classes.Beans;
using Grendezvous.Classes.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grendezvous.Forms
{
    public partial class EleveUpdate : Form
    {
        private int idEleve = 0;
        public EleveUpdate()
        {
            InitializeComponent();
        }

        public EleveUpdate(int id, string nom, string postnom, string prenom, string sexe, string classe )
        {
            InitializeComponent();
            this.idEleve = id;
            this.textNom.Text = nom;
            this.textPostnom.Text = postnom;
            this.textPrenom.Text = prenom;
            if (sexe.Equals( "Homme")) radioButton1.Checked = true;
            else radioButton2.Checked = true;
            this.comboClasse.Text = classe;
      
        }

        void initilizeControls()
        {
            textPostnom.Text = "";
      
        }
        void save(bool btn, int action)
        {
            if (string.IsNullOrEmpty(textPostnom.Text.Trim()))
            {
                MessageBox.Show("Complètez tous les champs.", "Erreur...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Eleve patient = new Eleve();
                patient.Id = Convert.ToInt32(idEleve);
         
                patient.Postnom = textPostnom.Text.Trim();
                //patient.Age = Convert.ToInt32(textAge.Text.Trim());
                //patient.Adresse = textAdresse.Text.Trim();
                //patient.Telephone = textPhone.Text.Trim();
                //patient.Medicaments = textMedicament.Text.Trim();
                //patient.Jours = Convert.ToInt32(textJour.Text.Trim());
                //patient.Etat = textEtat.Text.Trim();
                if (btn)
                {
                    new EleveDAO().save(action, patient);
                }
                new Success("Patient enregistré !!!").ShowDialog();
                initilizeControls();
            }
        }

      

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            save(true, 2);
        }

      
        private void pictureBox2_Click_2(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EleveUpdate_Load(object sender, EventArgs e)
        {
            new EleveDAO().retreview(idEleve, photoEleve);
        }
    }
}
