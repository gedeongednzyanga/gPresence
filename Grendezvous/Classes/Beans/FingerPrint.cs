using SecuGen.FDxSDKPro.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grendezvous.Classes.Beans
{
    class FingerPrint
    {
        private Image finger;
        private byte[] fingerTest;
        private byte[] findFinger = new Byte[400];
        private SGFingerPrintManager _sfpm;
        private static FingerPrint _fp;

        public Image Finger { get => finger; set => finger = value; }
        public byte[] FingerTest { get => fingerTest; set => fingerTest = value; }
        public SGFingerPrintManager Frm { get => _sfpm; set => _sfpm = value; }
        public byte[] FindFinger { get => findFinger; set => findFinger = value; }

        public static FingerPrint getInstance()
        {
            if (_fp == null)
                _fp = new FingerPrint();
            return _fp;
        }

    }
}
