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
    class ClasseDAO:DAO<Classe>
    {
        private Classe getSection(IDataReader dr)
        {
            Classe classe = new Classe();
            classe.Id = Convert.ToInt32(dr["id_user"].ToString());
            classe.Designation = dr["nom"].ToString();
            classe.Section = dr["nom"].ToString();
            classe.Option = dr["nom"].ToString();
            return classe;
        }
        public override Classe find(int id)
        {
            Classe classe = new Classe();
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
                        classe.Id = Convert.ToInt32(dr["id"].ToString());
                        classe.Designation = dr["nom"].ToString();
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
            return classe;
        }

        public override List<Classe> getAll()
        {
            throw new NotImplementedException();
        }

        public override Classe save(int action, Classe obj)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "obj_classe";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@action", 10, DbType.Int32, action));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, obj.Id));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@designation", 50, DbType.String, obj.Designation));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@section", 5, DbType.String, obj.Section));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@option", 5, DbType.String, obj.Option));
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


        public override List<Classe> search(string text)
        {
            throw new NotImplementedException();
        }

        public List<Eleve> triEleve(String classe)
        {
            List<Eleve> liste = new List<Eleve>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "tri_eleve";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@classe", 20, DbType.String, classe));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(new EleveDAO().getEleve(dr));
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
            return liste;
        }

        public List<Eleve> searchIntriEleve(String classe, String nom)
        {
            List<Eleve> liste = new List<Eleve>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "search_eleve_in_classe";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@classe", 20, DbType.String, classe));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@nom", 20, DbType.String, nom));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(new EleveDAO().getEleve(dr));
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
            return liste;
        }

        public  void loadCombo(ComboBox cmb, String storedProcedure, String field)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = storedProcedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        cmb.Items.Add(dr[field].ToString().Trim());
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
        }
    }
}
