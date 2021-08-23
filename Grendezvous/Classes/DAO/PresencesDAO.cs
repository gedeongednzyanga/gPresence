using GestionProduit.Classes;
using Grendezvous.Classes.Beans;
using Microsoft.Reporting.WinForms;
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
    class PresencesDAO : DAO<Presence>
    {
        private Presence getPresences(IDataReader dr)
        {
            Presence presence = new Presence();
            presence.Id = Convert.ToInt32(dr["id_presence"].ToString());
            presence.Nom = dr["nom"].ToString();
            presence.Postnom =dr["postnom"].ToString();
            presence.Prenom =dr["prenom"].ToString();
            presence.Sexe = dr["sexe"].ToString();
            presence.Classe = dr["designation_cl"].ToString();
            presence.Date_ = DateTime.Parse(dr["date_presence"].ToString());
            presence.Presences = Convert.ToInt32(dr["presence"].ToString());
            presence.PresenceEl = dr["presence_ltr"].ToString();
            return presence;
        }

        public override Presence find(int id)
        {
            throw new NotImplementedException();
        }

        public override List<Presence> getAll()
        {
            List<Presence> liste = new List<Presence>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "select_presence";
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getPresences(dr));
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

        public List<Presence> getAll(int id)
        {
            List<Presence> liste = new List<Presence>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "select_one_presence";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@presence", 10, DbType.Int32, id));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getPresences(dr));
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

        public List<Presence> getAll(int id, string procedure)
        {
            List<Presence> liste = new List<Presence>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = procedure;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@ida", 10, DbType.Int32, id));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getPresences(dr));
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
        public override Presence save(int action, Presence obj)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "obj_presence";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@action", 10, DbType.Int32, action));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@id", 10, DbType.Int32, obj.Id));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@presence", 10, DbType.Int32, obj.Presences));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@eleve", 10, DbType.Int32, obj.Eleve));
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

        public override List<Presence> search(string text)
        {
            List<Presence> liste = new List<Presence>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "search_in_presence";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@nom", 50, DbType.String, text));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getPresences(dr));
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

        public List<Presence> filter(DateTime date,string classe, int presence)
        {
            List<Presence> liste = new List<Presence>();
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "select_filter_presence";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@datep", 20, DbType.Date, date));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@classe", 20, DbType.String, classe));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@presence", 10, DbType.Int32, presence));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        liste.Add(getPresences(dr));
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

        public int countPresence(DateTime datepresence, int eleve)
        {
            int nbr = 0;
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "check_presence";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@dateappel", 10, DbType.Date, datepresence));
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@eleve", 10, DbType.Int32, eleve));
                    IDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        nbr = 1;
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
            return nbr;
        }

        public int count2(int ida)
        {
            int nbr = 0;
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "COUNT2";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@ida", 10, DbType.Int32, ida));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        nbr = Convert.ToInt32(dr["nbrendevous"].ToString());
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
            return nbr;
        }

        public int count3(int ida)
        {
            int nbr = 0;
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "COUNT3";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@ida", 10, DbType.Int32, ida));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        nbr = Convert.ToInt32(dr["nbrendevous"].ToString());
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
            return nbr;
        }

        public int count4(int ida)
        {
            int nbr = 0;
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "COUNT4";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@ida", 10, DbType.Int32, ida));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        nbr = Convert.ToInt32(dr["nbrendevous"].ToString());
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
            return nbr;
        }

        public void LastRendePatient(int idp, Label dateR, Label agentR)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "RENDE_Last";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@idp", 10, DbType.Int32, idp));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["date_"]== DBNull.Value)
                        {
                            dateR.Text = "Aucun rendez-vous déjà fait.";
                            agentR.Text = "Aucun docteur.";
                        }
                        else
                        {
                            dateR.Text =DateTime.Parse( dr["date_"].ToString()).ToShortDateString() + " à " + dr["heure"].ToString();
                            agentR.Text = dr["agent"].ToString();
                        } 
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

        public void TotalRendePatient(int idp, Label totalR, Label totalDoc)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "select_eleve";
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@idp", 10, DbType.Int32, idp));
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["nbre"] == DBNull.Value)
                        {
                            totalR.Text = "0 visite";
                            totalDoc.Text = "0 visite";
                        }
                        else
                        {
                            totalR.Text = dr["nbre"].ToString()+" visite(s).";
                            totalDoc.Text = dr["nbredocteur"].ToString()+" docteur(s)";
                        }
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

        public void Call_Report(int id, ReportViewer reportView, string path)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "select_presence";
                    cmd.CommandType = CommandType.StoredProcedure;
                   // cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@user", 10, DbType.Int32, id));
                    SqlDataAdapter da = new SqlDataAdapter((SqlCommand)cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "ElevePresence");
                    reportView.LocalReport.DataSources.Clear();
                    reportView.LocalReport.DataSources.Add(new ReportDataSource("ElevePresence", ds.Tables[0]));
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

        //public void Call_Reportt(string procedure, int id, ReportViewer reportView, string path)
        //{
        //    try
        //    {
        //        if (this.con.State == ConnectionState.Closed)
        //            this.con.Open();
        //        using (IDbCommand cmd = this.con.CreateCommand())
        //        {
        //            cmd.CommandText = procedure;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@ida", 10, DbType.Int32, id));
        //            SqlDataAdapter da = new SqlDataAdapter((SqlCommand)cmd);
        //            DataSet ds = new DataSet();
        //            da.Fill(ds, "DataSet_Rendevous");
        //            reportView.LocalReport.DataSources.Clear();
        //            reportView.LocalReport.DataSources.Add(new ReportDataSource("DataSet_Rendevous", ds.Tables[0]));
        //            reportView.LocalReport.ReportEmbeddedResource = path;
        //            reportView.RefreshReport();
        //        }
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        MessageBox.Show("Error " + ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show("Error when Selecting data, " + ex.Message, "Selecting data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //    finally
        //    {
        //        if (this.con != null)
        //        {
        //            if (this.con.State == ConnectionState.Open)
        //                this.con.Close();
        //        }
        //    }
        //}

        //public void Call_ReportPR(int id, ReportViewer reportView, string path)
        //{
        //    try
        //    {
        //        if (this.con.State == ConnectionState.Closed)
        //            this.con.Open();
        //        using (IDbCommand cmd = this.con.CreateCommand())
        //        {
        //            cmd.CommandText = "RENDE_Patient";
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.Add(Parametre.Instance.AjouterParametre(cmd, "@idp", 10, DbType.Int32, id));
        //            SqlDataAdapter da = new SqlDataAdapter((SqlCommand)cmd);
        //            DataSet ds = new DataSet();
        //            da.Fill(ds, "DataSet_Rendevous");
        //            reportView.LocalReport.DataSources.Clear();
        //            reportView.LocalReport.DataSources.Add(new ReportDataSource("DataSet_Rendevous", ds.Tables[0]));
        //            reportView.LocalReport.ReportEmbeddedResource = path;
        //            reportView.RefreshReport();
        //        }
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        MessageBox.Show("Error " + ex.Message, "Message...", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    catch (SqlException ex)
        //    {
        //        MessageBox.Show("Error when Selecting data, " + ex.Message, "Selecting data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //    finally
        //    {
        //        if (this.con != null)
        //        {
        //            if (this.con.State == ConnectionState.Open)
        //                this.con.Close();
        //        }
        //    }
        //}


    }
}
