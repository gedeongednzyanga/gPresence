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

namespace Grendezvous.Users
{
    public partial class Presences : UserControl
    {
        private int idrende = 0;
        private int idPatient = 0;
        private int iduser = 0;
        //private string port = "";

        public Presences()
        {
            InitializeComponent();
        }
        public Presences(int user)
        {
            InitializeComponent();
            this.iduser = user;
        }

        #region FONCTIONS

        void loadPresences(PresencesDAO presencesDAO)
        {
            gridPresence.DataSource = presencesDAO.getAll();
        }
        void getPatient()
        {
            int i = gridPresence.CurrentRow.Index;
            idPatient = Convert.ToInt32(gridPresence["Column1", i].Value.ToString());
            //labpatient.Text = gridPatient["Column2", i].Value.ToString()+" "+ gridPatient["Column3", i].Value.ToString();
            ////labadresse.Text = gridPatient["Column5", i].Value.ToString();
            //labphone.Text = gridPatient["Column6", i].Value.ToString();
            //labetat.Text = gridPatient["Column9", i].Value.ToString();
        }
        void loadClasses()
        {
            combClasse.Items.Clear();
            new ClasseDAO().loadCombo(combClasse, "select_classe", "designation_cl");
        } 

        void filter()
        {
            gridPresence.DataSource = new PresencesDAO().filter(bunifuDatepicker1.Value, combClasse.Text.Trim(), 5);
        }


        #endregion

        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Prendevous_Load(object sender, EventArgs e)
        {
            loadPresences(new PresencesDAO());
            loadClasses();
        }

        private void gridPatient_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
               // getPatient();
            }
            catch (Exception ex)
            {
                new Error(ex.Message);
            }
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            try
            {
                gridPresence.DataSource = new EleveDAO().search(bunifuMaterialTextbox1.Text.Trim());
            }
            catch (Exception ex)
            {
                new Error(ex.Message).ShowDialog();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
           // save(true, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Messages.instance().GetAllPorts(comboBox1);
                //if (comboBox1.Text == "")
                //    Messages.instance().SetData(Convert.ToInt32(comboBox1.Text.Trim()), 9600, 300);
                //else
                //    Messages.instance().SetData(Convert.ToInt32(comboBox1.Text.Trim()), 9600, 300);
                //Messages.instance().Test_port(label6);
            }catch(Exception ex)
            {
                new Error(ex.Message).ShowDialog();
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            //Messages.instance().Send(richTextBox1.Text.Trim(), labphone.Text.Trim(), "");
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            //richTextBox1.Text = "Bonjour "+labpatient.Text.Trim() +", votre rendez avec le docteur est fixé le "+dateTimePicker1.Text.Trim()
            //    +" à "+dateTimePicker2.Text.Trim()+". Merci et bonne suite. HealthCare Software";
        }

        private void combClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                filter();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            Preview report = new Preview();
            new PresencesDAO().Call_Report(1, report.reportViewer1, "Grendezvous.Reports.ElevePresences.rdlc");
            report.ShowDialog();
        }
    }
}
