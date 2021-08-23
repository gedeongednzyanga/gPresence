using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace GestionProduit.Classes
{
    class ConnexionDb
    {
        private static SqlConnection conn;
        public static SqlConnection getConnexion()
        {
            return conn = new SqlConnection("Data Source=DESKTOP-M6JAMV0;Initial Catalog=g_presence;Integrated Security=True;User ID=sa; Password=go for it just do it");
        }
    }
}
