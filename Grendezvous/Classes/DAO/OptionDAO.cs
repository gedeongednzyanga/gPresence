using GestionProduit.Classes;
using Grendezvous.Classes.Beans;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grendezvous.Classes.DAO
{
    class OptionDAO:DAO<Option>
    {
        private Option getSection(IDataReader dr)
        {
            Option option = new Option();
            option.Id = Convert.ToInt32(dr["id_user"].ToString());
            option.Designation = dr["nom"].ToString();
            option.Short_design = dr["username"].ToString();
            return option;
        }
        public override Option find(int id)
        {
            Option option = new Option();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "ONEUSER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, id));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        option.Id = Convert.ToInt32(dr["id"].ToString());
                        option.Designation = dr["nom"].ToString();
                    }
                    cmd.Dispose();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur survenue...");
            }
            finally
            {
                this.con.Close();
            }
            return option;
        }

        public override List<Option> getAll()
        {
            throw new NotImplementedException();
        }

        public override Option save(int action, Option obj)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "obj_option";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@action", 10, DbType.Int32, action));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, obj.Id));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@designation", 50, DbType.String, obj.Designation));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@design", 50, DbType.String, obj.Short_design));
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Erreur survenue...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.con.Close();
            }
            return obj;
        }

        public override List<Option> search(string text)
        {
            throw new NotImplementedException();
        }
    }
}
