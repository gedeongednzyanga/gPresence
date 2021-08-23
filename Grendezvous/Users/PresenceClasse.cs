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
    public partial class PresenceClasse : UserControl
    {
     
        public PresenceClasse()
        {
            InitializeComponent();
        }

        public PresenceClasse(int user)
        {
            InitializeComponent();
        }

        #region FONCTION

        void presenceClasse(PresencesDAO presencesDAO)
        {
            gridPresencecl.DataSource = presencesDAO.getAll(1);
        }

        void loadClasses()
        {
            combClasse.Items.Clear();
            new ClasseDAO().loadCombo(combClasse, "select_classe", "designation_cl");
      
        }
        void filter()
        {
            gridPresencecl.DataSource = new PresencesDAO().filter(bunifuDatepicker1.Value, combClasse.Text.Trim(), 1);
        }

        #endregion

        private void AllRprogramme_Load(object sender, EventArgs e)
        {
            presenceClasse(new PresencesDAO());
            loadClasses();
        }

        private void combClasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            filter();
        }
    }
}
