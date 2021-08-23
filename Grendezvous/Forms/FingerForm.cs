using Grendezvous.Classes.Beans;
using Grendezvous.Classes.DAO;
using SecuGen.FDxSDKPro.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grendezvous.Forms
{
    public partial class FingerForm : Form
    {
        public FingerForm()
        {
            InitializeComponent();
        }

        public FingerForm(String photo)
        {
            InitializeComponent();
            this.lblInfo.Text = photo;
        }

        ////Secugen variables
        SGFingerPrintManager _frm;
        SGFPMDeviceName _deviceName;
        Int32 _deviceId;
        Int32 _error;
        Int32 _mImageWidth;
        Int32 _mImageHeight;
        int _type, _number, _pouce;
        private byte[] _mRegMin1;
        private byte[] _mRegMin2;
        private byte[] _mVerify;
        private Byte[] _photo;
        bool _matched1, isPresence;


        #region FONCTIONS
        void initSecugen()
        {
            try
            {
                _mRegMin1 = new Byte[400];
                _mRegMin2 = new Byte[400];
                _mVerify = new Byte[400];
                _deviceName = SGFPMDeviceName.DEV_AUTO;
                _deviceId = Convert.ToInt32(SGFPMPortAddr.USB_AUTO_DETECT);
                labelDevice.Text = "Lecteur : " + _deviceName.ToString();
                _error = _frm.Init(_deviceName);
                _error = _frm.OpenDevice(_deviceId);

                if (_error == Convert.ToInt32(SGFPMError.ERROR_NONE))
                {
                    GetBtn_Click();
                    labelDevice.Text = "Lecteur connecté avec succèss.";
                    _frm.EnableAutoOnEvent(true, (int)this.Handle);
                }
                else
                {
                    labelDevice.Text = "Veuillez brancher un lecteur d'empreinte.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        void getImage()
        {
            try
            {
                _matched1 = false;
                Int32 elapTime = Environment.TickCount;
                Byte[] fpImage;
                labelDevice.Text = "Lecteur : " + _deviceName.ToString();
                fpImage = new Byte[_mImageWidth * _mImageHeight];
                _error = _frm.GetImage(fpImage);
                if (_error == (Int32)SGFPMError.ERROR_NONE)
                {
                    elapTime = Environment.TickCount - elapTime;
                    if (_type == 1)
                    {
                        if (_number == 1)
                        {
                            DrawImage(fpImage, pictureBox1);
                            _error = _frm.CreateTemplate(fpImage, _mRegMin1);
                            if (_error == (Int32)SGFPMError.ERROR_NONE)
                            {
                                _number = 1;
                            }
                            else
                                labError.Text = FingerPrintDAO.DisplayError(_error);
                        }
                    }
                    else
                    {
                        DrawImage(fpImage, pictureBox1);
                        _error = _frm.CreateTemplate(fpImage, _mRegMin2);
                        if (_error == (Int32)SGFPMError.ERROR_NONE)
                            FingerPrint.getInstance().FingerTest = _mRegMin2;
                        else
                            labError.Text = FingerPrintDAO.DisplayError(_error);
                        //if (isPresence)
                        //    new FingerPrintDAO().presence(_frm);
                        //else
                        //    new FingerPrintDAO().testFinger(_frm);
                    }
                    //labError.Text = FingerPrintDAO.DisplayError(_error);
                    label1.Text = "Image capturée : " + elapTime + " millisecondes";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetBtn_Click()
        {
            SGFPMDeviceInfoParam pInfo = new SGFPMDeviceInfoParam();
            Int32 iError = _frm.GetDeviceInfo(pInfo);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                _mImageWidth = pInfo.ImageWidth;
                _mImageHeight = pInfo.ImageHeight;
            }
        }

        private void FingerForm_Load(object sender, EventArgs e)
        {
            _frm = new SGFingerPrintManager();
            initSecugen();
        }

        private void DrawImage(Byte[] imgData, PictureBox picBox)
        {
            int colorval;
            Bitmap bmp = new Bitmap(_mImageWidth, _mImageHeight);
            picBox.Image = (Image)bmp;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    colorval = (int)imgData[(j * _mImageWidth) + i];
                    bmp.SetPixel(i, j, Color.FromArgb(colorval, colorval, colorval));
                }
            }
            picBox.Refresh();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnEmpr_Click(object sender, EventArgs e)
        {
            //    verify(true);
        }

        //private Byte[] convertImageToBinary(Image image)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //    _photo = ms.ToArray();
        //    return _photo;
        //}



        //protected override void WndProc(ref Message message)
        //{
        //    if (message.Msg == (int)SGFPMMessages.DEV_AUTOONEVENT)
        //    {
        //        if (message.WParam.ToInt32() == (Int32)SGFPMAutoOnEvent.FINGER_ON)
        //        {
        //            label1.Text = "Scanning Image....";
        //            getImage();
        //        }
        //        else if (message.WParam.ToInt32() == (Int32)SGFPMAutoOnEvent.FINGER_OFF)
        //        {
        //            lblInfo.Text = "Empreinte capturée avec succès !!!";

        //        }  
        //    }
        //    base.WndProc(ref message);
        //}

        //void verify(bool veri)
        //{
        //    _matched1 = false;
        //    SGFPMSecurityLevel secu_level;

        //    secu_level = (SGFPMSecurityLevel)7;

        //    _error = _frm.MatchTemplate(_mRegMin1, _mRegMin2, secu_level, ref _matched1);
        //    if (_error == (Int32)SGFPMError.ERROR_NONE)
        //    {
        //        if (_matched1)
        //        {
        //            //lblresult.Text = "Verification Success";
        //            //lblresult.ForeColor = Color.Green;
        //            FingerPrint.getInstance().Finger = pictureBox1.Image;
        //            if (veri)
        //            {
        //                if (_pouce == 1)
        //                    FingerPrint.getInstance().FindFinger = _mRegMin1;
        //                //else
        //                //    FingerPrint.getInstance().FindFinger = _mRegMin1;
        //                this.DialogResult = DialogResult.Yes;
        //            }
        //        }
        //        else
        //        {
        //            //lblresult.Text = "Verification Failed";
        //            //lblresult.ForeColor = Color.Red;
        //        }

        //    }
        //    else
        //        labError.Text = FingerPrintDAO.DisplayError(_error);
        //}

        #endregion
    }
}
