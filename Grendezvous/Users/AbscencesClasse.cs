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

namespace Grendezvous.Users
{
    public partial class AbscencesClasse : UserControl
    {
        private int iduser = 0;
        public AbscencesClasse()
        {
            InitializeComponent();
        }
        public AbscencesClasse(int user)
        {
            InitializeComponent();
            this.iduser = user;
        }


        #region FONCTION

        void loadAbscences(PresencesDAO rendedao)
        {
            gridPresenceAbs.DataSource = rendedao.getAll(0);
            //label3.Text = rendedao.count3(iduser).ToString();
        }

        void loadClasses()
        {
            combClasse.Items.Clear();
            new ClasseDAO().loadCombo(combClasse, "select_classe", "designation_cl");
          
        }

        void filter()
        {
            gridPresenceAbs.DataSource = new PresencesDAO().filter(bunifuDatepicker1.Value, combClasse.Text.Trim(), 0);
        }

        #endregion
        private void AllRannule_Load(object sender, EventArgs e)
        {
            loadAbscences(new PresencesDAO());
            loadClasses();
        }

        private void combClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter();
        }
    }
}
