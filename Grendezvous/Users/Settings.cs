using Grendezvous.Classes.Beans;
using Grendezvous.Classes.DAO;
using Grendezvous.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grendezvous.Users
{
    public partial class Settings : UserControl
    {
        int iduser = 0;
        public Settings()
        {
            InitializeComponent();
        }

        #region FUNCTIONS
        void saveSection(bool btn, int action)
        {
            if (string.IsNullOrEmpty(textSectionDesignation.Text.Trim()) || string.IsNullOrEmpty(textShortSection.Text.Trim()))
            {
                MessageBox.Show("Complètez toutes les cases pour la Section.", "Erreur...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Section section = new Section();
                section.Id = Convert.ToInt32(0);
                section.Designation = textSectionDesignation.Text.Trim();
                section.Short_design = textShortSection.Text.Trim();
                if (btn)
                {
                    new SectionDAO().save(action, section);
                    textSectionDesignation.Text = "";
                    textShortSection.Text = "";
                    new Success("Section enregistrée !!!").ShowDialog();
                }

            }
        }

        void saveOption(bool btn, int action)
        {
            if (string.IsNullOrEmpty(textDesignationOption.Text.Trim()) || string.IsNullOrEmpty(textShortdesignOpt.Text.Trim()))
            {
                MessageBox.Show("Complètez toutes les cases pour l'Option.", "Erreur...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Option option = new Option();
                option.Id = Convert.ToInt32(0);
                option.Designation = textDesignationOption.Text.Trim();
                option.Short_design = textShortdesignOpt.Text.Trim();
                if (btn)
                {
                    new OptionDAO().save(action, option);
                    textDesignationOption.Text = "";
                    textShortdesignOpt.Text = "";
                    new Success("Option enregistrée !!!").ShowDialog();
                }

            }
        }

        void saveClasse(bool btn, int action)
        {
            if (string.IsNullOrEmpty(comboNiveau.Text.Trim()) || string.IsNullOrEmpty(comboBox1.Text.Trim()) || 
                string.IsNullOrEmpty(comboBox2.Text.Trim()) || string.IsNullOrEmpty(comboBox3.Text.Trim()))
            {
                MessageBox.Show("Complètez toutes les cases pour la Classe.", "Erreur...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Classe classe = new Classe();
                classe.Id = Convert.ToInt32(0);
                if (comboBox1.Text.Equals("CO")) { classe.Designation = comboNiveau.Text.Trim()+" "+comboBox1.Text.Trim()+"/"+comboBox3.Text.Trim(); }
                else { classe.Designation = comboNiveau.Text.Trim() + " " + comboBox1.Text.Trim() + comboBox2.Text.Trim() + "/" + comboBox3.Text.Trim(); }
                classe.Section = comboBox1.Text.Trim();
                classe.Option = comboBox2.Text.Trim();
                if (btn)
                {
                    new ClasseDAO().save(action, classe);
                    textDesignationOption.Text = "";
                    textShortdesignOpt.Text = "";
                    new Success("Classe enregistrée !!!").ShowDialog();
                }

            }
        }

        void loadSection()
        {
            comboBox1.Items.Clear();
            new ClasseDAO().loadCombo(comboBox1, "select_section", "short_design");
        }
        void loadOption()
        {
            comboBox2.Items.Clear();
            new ClasseDAO().loadCombo(comboBox2, "select_option", "short_desig");
        }
        #endregion

        public Settings(int user)
        {
            InitializeComponent();
            this.iduser = user;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            loadSection();
            loadOption();
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            saveSection(true, 1);
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            saveOption(true, 1);
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            saveClasse(true, 1);
        }
    }
}
