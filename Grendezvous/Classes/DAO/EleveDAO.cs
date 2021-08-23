using GestionProduit.Classes;
using Grendezvous.Classes.Beans;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grendezvous.Classes.DAO
{
    class EleveDAO : DAO<Eleve>
    {
        public Eleve getEleve(IDataReader dr)
        {
            Eleve eleve = new Eleve();
            eleve.Id = Convert.ToInt32(dr["id_eleve"].ToString());
            eleve.Nom = dr["nom"].ToString();
            eleve.Postnom = dr["postnom"].ToString();
            eleve.Prenom = dr["prenom"].ToString();
            eleve.Sexe = dr["sexe"].ToString();
            eleve.Classe = dr["designation_cl"].ToString();
            eleve.DateInscri =DateTime.Parse( dr["date_inscri"].ToString()).ToShortDateString();
            return eleve;
        }

        public override Eleve find(int id)
        {
            Eleve eleve = new Eleve();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "one_eleve";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, id));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        eleve.Id = Convert.ToInt32(dr["id"].ToString()); eleve.Nom = dr["nom"].ToString();
                        eleve.Postnom = dr["postnom"].ToString();
                       ;
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
            return eleve;
        }

        public override List<Eleve > getAll()
        {
            List<Eleve> liste = new List<Eleve>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "select_eleve";
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getEleve(dr));
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

        public override Eleve save(int action, Eleve obj)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "obj_eleve";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@action", 10, DbType.Int32, action));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, obj.Id));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@nom", 20, DbType.String, obj.Nom));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@postnom", 20, DbType.String, obj.Postnom));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@prenom", 20, DbType.String, obj.Prenom));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@sexe", 8, DbType.String, obj.Sexe));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@photo", int.MaxValue, DbType.Binary, obj.Profil));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@classe", 20, DbType.String, obj.Classe));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@empreinteImage", int.MaxValue, DbType.Binary, obj.EmpreinteImage));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@empreinte", int.MaxValue, DbType.Binary, obj.Empreinte));
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

        public override List<Eleve> search(string text)
        {
            List<Eleve> liste = new List<Eleve>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "search_eleve";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@nom", 50, DbType.String, text));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getEleve(dr));
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

        public void retreview(int id, PictureBox picP)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (SqlCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "select_profil_eleve";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, id));
                    IDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        Byte[] img = (Byte[])(dr[0]);
                        if (img == null)
                            picP.Image = null;
                        else
                        {
                            MemoryStream ms = new MemoryStream(img);
                            picP.Image = Image.FromStream(ms);
                        }
                    }
                    this.con.Close();
                    cmd.Dispose();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERREUR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Call_Report(string procedure, ReportViewer reportView, string path)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = procedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@ida", 10, DbType.Int32, id));
                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "EleveInscrits");
                    reportView.LocalReport.DataSources.Clear();
                    reportView.LocalReport.DataSources.Add(new ReportDataSource("EleveInscrits", ds.Tables[0]));
                    reportView.LocalReport.ReportEmbeddedResource = path;
                    reportView.RefreshReport();
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show("Error " + ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Error when Selecting data, " + ex.Message, "Selecting data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                if (this.con != null)
                {
                    if (this.con.State == ConnectionState.Open)
                        this.con.Close();
                }
            }
        }
    }
}
