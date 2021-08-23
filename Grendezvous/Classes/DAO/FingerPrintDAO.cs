using GestionProduit.Classes;
using SecuGen.FDxSDKPro.Windows;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grendezvous.Classes.Beans;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Grendezvous.Forms;
using System.Windows.Forms;

namespace Grendezvous.Classes.DAO
{
    class FingerPrintDAO
    {
        private bool test;
        private int error;
        static byte[] photo;
        private static FingerPrintDAO _fingerDao;
        private SqlConnection con = ConnexionDb.getConnexion();
        SGFingerPrintManager _frm;

        public static FingerPrintDAO getInstance()
        {
            if (_fingerDao == null)
                _fingerDao = new FingerPrintDAO();
            return _fingerDao;
        }
        public bool Test { get => test; set => test = value; }
        public int Error { get => error; set => error = value; }

        private static Byte[] convertImageToBinary(Image image)
        {
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            photo = ms.ToArray();
            return photo;
        }

        private static Image ConvertBinaryToImage(Byte[] image)
        {
            MemoryStream ms = new MemoryStream(image);
            return Image.FromStream(ms);
        }

        bool verify(Byte[] image)
        {
            this.Test = false;
            SGFPMSecurityLevel secLevel;
            secLevel = SGFPMSecurityLevel.NORMAL;

             this.Error =FingerPrint.getInstance().Frm.MatchTemplate(FingerPrint.getInstance().FingerTest, image, secLevel, ref this.test);
            if (this.Error == (Int32)SGFPMError.ERROR_NONE)
            {
                if (this.Test == false)
                { this.Test = true; }
            }
            else
            {
                MessageBox.Show("Acunne correspondance dans le sytème.","Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return this.Test;
        }
        public void testFinger(SGFingerPrintManager sfpm)
        {
      
            try
            {
                test = false;
                FingerPrint.getInstance().Frm = sfpm;
                _frm = sfpm;
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "test_eleve";
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        FingerPrint.getInstance().FingerTest = (Byte[]) dr["empreinte"];
                        if (verify(FingerPrint.getInstance().FingerTest))
                        {
                            //new FrmDEL (dr["nom"], dr["postnom"], dr[prenom"], this.ConvertBinaryToImage[byte[]) dr["photo_eleve"]));
                            // new FingerForm( dr["nom"].ToString()).ShowDialog();
                          MessageBox.Show(dr["nom"].ToString());
                            break;

                            //break;
                        }
                        else
                        {
                            new Speek().speek("Aucune correspondance dans le système.");
                            break;
                        }
                    }
                    dr.Close();
                    cmd.Dispose();
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
        }

        public void presence(SGFingerPrintManager sfpm)
        {
            try
            {
                if (this.con.State == ConnectionState.Closed)
                    this.con.Open();
                using (IDbCommand cmd = this.con.CreateCommand())
                {
                    cmd.CommandText = "test_eleve";
                    cmd.CommandType = CommandType.StoredProcedure;
                    IDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        FingerPrint.getInstance().FindFinger = (Byte[])dr["empreinte"];
                        if (verify(FingerPrint.getInstance().FindFinger))
                        {
                            //Appel de la fonction pour faire la presence;
                        }
                        else
                        {
                            new Speek().speek("Aucune connrespondance dans le système.");
                        }
                    }
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }
        }

        public static String DisplayError(int iError)
        {
            string text = "";

            switch (iError)
            {
                case 0:                             //SGFDX_ERROR_NONE				= 0,
                    text = "Error none";
                    break;

                case 1:                             //SGFDX_ERROR_CREATION_FAILED	= 1,
                    text = "Can not create object";
                    break;

                case 2:                             //   SGFDX_ERROR_FUNCTION_FAILED	= 2,
                    text = "Function Failed";
                    break;

                case 3:                             //   SGFDX_ERROR_INVALID_PARAM	= 3,
                    text = "Invalid Parameter";
                    break;

                case 4:                          //   SGFDX_ERROR_NOT_USED			= 4,
                    text = "Not used function";
                    break;

                case 5:                                //SGFDX_ERROR_DLLLOAD_FAILED	= 5,
                    text = "Can not create object";
                    break;

                case 6:                                //SGFDX_ERROR_DLLLOAD_FAILED_DRV	= 6,
                    text = "Can not load device driver";
                    break;
                case 7:                                //SGFDX_ERROR_DLLLOAD_FAILED_ALGO = 7,
                    text = "Can not load sgfpamx.dll";
                    break;

                case 51:                //SGFDX_ERROR_SYSLOAD_FAILED	   = 51,	// system file load fail
                    text = "Can not load driver kernel file";
                    break;

                case 52:                //SGFDX_ERROR_INITIALIZE_FAILED  = 52,   // chip initialize fail
                    text = "Failed to initialize the device";
                    break;

                case 53:                //SGFDX_ERROR_LINE_DROPPED		   = 53,   // image data drop
                    text = "Data transmission is not good";
                    break;

                case 54:                //SGFDX_ERROR_TIME_OUT			   = 54,   // getliveimage timeout error
                    text = "Time out";
                    break;

                case 55:                //SGFDX_ERROR_DEVICE_NOT_FOUND	= 55,   // device not found
                    text = "Device not found";
                    break;

                case 56:                //SGFDX_ERROR_DRVLOAD_FAILED	   = 56,   // dll file load fail
                    text = "Can not load driver file";
                    break;

                case 57:                //SGFDX_ERROR_WRONG_IMAGE		   = 57,   // wrong image
                    text = "Wrong Image";
                    break;

                case 58:                //SGFDX_ERROR_LACK_OF_BANDWIDTH  = 58,   // USB Bandwith Lack Error
                    text = "Lack of USB Bandwith";
                    break;

                case 59:                //SGFDX_ERROR_DEV_ALREADY_OPEN	= 59,   // Device Exclusive access Error
                    text = "Device is already opened";
                    break;

                case 60:                //SGFDX_ERROR_GETSN_FAILED		   = 60,   // Fail to get Device Serial Number
                    text = "Device serial number error";
                    break;

                case 61:                //SGFDX_ERROR_UNSUPPORTED_DEV		   = 61,   // Unsupported device
                    text = "Unsupported device";
                    break;

                // Extract & Verification error
                case 101:                //SGFDX_ERROR_FEAT_NUMBER		= 101, // utoo small number of minutiae
                    text = "The number of minutiae is too small";
                    break;

                case 102:                //SGFDX_ERROR_INVALID_TEMPLATE_TYPE		= 102, // wrong template type
                    text = "Template is invalid";
                    break;

                case 103:                //SGFDX_ERROR_INVALID_TEMPLATE1		= 103, // wrong template type
                    text = "1st template is invalid";
                    break;

                case 104:                //SGFDX_ERROR_INVALID_TEMPLATE2		= 104, // vwrong template type
                    text = "2nd template is invalid";
                    break;

                case 105:                //SGFDX_ERROR_EXTRACT_FAIL		= 105, // extraction fail
                    text = "Minutiae extraction failed";
                    break;

                case 106:                //SGFDX_ERROR_MATCH_FAIL		= 106, // matching  fail
                    text = "Matching failed";
                    break;

            }
            return text;
        }
    }
}
