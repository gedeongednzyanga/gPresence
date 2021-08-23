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
    public partial class MaladeClasse : UserControl
    {
        private int iduser = 0;
        public MaladeClasse()
        {
            InitializeComponent();
        }

        public MaladeClasse(int user)
        {
            InitializeComponent();
            this.iduser = user;
        }


        #region FONCTION

        void loadMalade(PresencesDAO presences)
        {
            gridPresencecle.DataSource = presences.getAll(2);
            
        }

        void loadClasses()
        {
            combClasse.Items.Clear();
            new ClasseDAO().loadCombo(combClasse, "select_classe", "designation_cl");
            
        }

        void filter()
        {
            gridPresencecle.DataSource = new PresencesDAO().filter(bunifuDatepicker1.Value, combClasse.Text.Trim(), 2);
        }

        #endregion

        private void AllRfait_Load(object sender, EventArgs e)
        {
            loadMalade(new PresencesDAO());
            loadClasses();
        }

        private void combClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter();
        }
    }
}
