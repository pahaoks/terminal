using System;

using System.Collections.Generic;
using System.Text;
using Symbol.Barcode;

namespace Bacodelib
{
    public class Scanner
    {
        private Barcode.Barcode barcode1;
        private bool enabled = false;
        private static Scanner scanner = null;

        private Scanner()
        {
            this.barcode1 = new Barcode.Barcode();
            InitBarcodeScanner(this.barcode1);
            this.barcode1.OnRead += new Barcode.Barcode.ScannerReadEventHandler(barcode1_OnRead);
        }

        public static Scanner Instance
        {
            get
            {
                if (scanner == null)
                    scanner = new Scanner();
                return scanner;
            }
        }


        private void barcode1_OnRead(object sender, Symbol.Barcode.ReaderData readerData)
        {
            if ((OnReadBarCode != null) && enabled)
            {
                string s;
                if (readerData.Type == DecoderTypes.CODE128 || readerData.Type == DecoderTypes.CODE39)
                {
                    s = readerData.Text;
                }
                else
                {
                    if (readerData.Text.Length == 12)
                        s = "0" + readerData.Text;
                    else if (readerData.Text.Length == 14)
                        s = readerData.Text.Substring(1);
                    else if (readerData.Text.Length == 13)
                        s = readerData.Text;
                    else
                        s = String.Empty;
                }
                OnReadBarCode(s, readerData.Text);
            }
        }

        public void Enable()
        {
            barcode1.EnableScanner = enabled = true;
        }

        public void Disable()
        {
            barcode1.EnableScanner = enabled = false;
        }

        public bool Enabled
        {
            get
            {
                return barcode1.EnableScanner;
            }
            set
            {
                barcode1.EnableScanner = enabled = value;
            }
        }

        public event OnReadBarCodeHandler OnReadBarCode;

        public static void InitBarcodeScanner(Barcode.Barcode scanner)
        {
            scanner.DecoderParameters.CODABAR = Barcode.DisabledEnabled.Disabled;
            scanner.DecoderParameters.CODE128 = Barcode.DisabledEnabled.Enabled; //Barcode.DisabledEnabled.Disabled;
            scanner.DecoderParameters.CODE39 = Barcode.DisabledEnabled.Disabled;
            scanner.DecoderParameters.CODE39Params.Code32Prefix = false;
            scanner.DecoderParameters.CODE39Params.Concatenation = false;
            scanner.DecoderParameters.CODE39Params.ConvertToCode32 = false;
            scanner.DecoderParameters.CODE39Params.FullAscii = false;
            scanner.DecoderParameters.CODE39Params.Redundancy = false;
            scanner.DecoderParameters.CODE39Params.ReportCheckDigit = false;
            scanner.DecoderParameters.CODE39Params.VerifyCheckDigit = false;
            scanner.DecoderParameters.CODE93 = Barcode.DisabledEnabled.Disabled;
            scanner.DecoderParameters.D2OF5 = Barcode.DisabledEnabled.Disabled;

            scanner.DecoderParameters.EAN13 = Barcode.DisabledEnabled.Enabled;
            scanner.DecoderParameters.EAN8 = Barcode.DisabledEnabled.Enabled;
            scanner.DecoderParameters.EAN8Params.ConvertToEAN13 = true;

            scanner.DecoderParameters.I2OF5 = Barcode.DisabledEnabled.Enabled;

            scanner.DecoderParameters.KOREAN_3OF5 = Barcode.DisabledEnabled.Disabled;
            scanner.DecoderParameters.MSI = Barcode.DisabledEnabled.Disabled;
            scanner.DecoderParameters.UPCA = Barcode.DisabledEnabled.Enabled;
            scanner.DecoderParameters.UPCE0 = Barcode.DisabledEnabled.Enabled;
            scanner.EnableScanner = false;
            scanner.ReaderParameters.ReaderSpecific.ContactSpecific.InitialScanTime = -1;
            scanner.ReaderParameters.ReaderSpecific.ContactSpecific.PulseDelay = -1;
            scanner.ReaderParameters.ReaderSpecific.ContactSpecific.QuietZoneRatio = -1;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.AimDuration = -1;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.AimMode = Barcode.AIM_MODE.AIM_MODE_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.AimType = Barcode.AIM_TYPE.AIM_TYPE_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.BeamTimer = -1;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.DPMMode = Barcode.DPM_MODE.DPM_MODE_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.FocusMode = Barcode.FOCUS_MODE.FOCUS_MODE_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.FocusPosition = Barcode.FOCUS_POSITION.FOCUS_POSITION_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.IlluminationMode = Barcode.ILLUMINATION_MODE.ILLUMINATION_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.ImageCaptureTimeout = -1;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.ImageCompressionTimeout = -1;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.Inverse1DMode = Barcode.INVERSE1D_MODE.INVERSE_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.LinearSecurityLevel = Barcode.LINEAR_SECURITY_LEVEL.SECURITY_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.PicklistMode = Barcode.DisabledEnabled.Undefined;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.PicklistModeEx = Barcode.PICKLIST_MODE.PICKLIST_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.PointerTimer = -1;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.PoorQuality1DMode = Barcode.DisabledEnabled.Undefined;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.VFFeedback = Barcode.VIEWFINDER_FEEDBACK.VIEWFINDER_FEEDBACK_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.VFFeedbackTime = -1;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.VFMode = Barcode.VIEWFINDER_MODE.VIEWFINDER_MODE_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.VFPosition.Bottom = 0;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.VFPosition.Left = 0;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.VFPosition.Right = 0;
            scanner.ReaderParameters.ReaderSpecific.ImagerSpecific.VFPosition.Top = 0;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.AimDuration = -1;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.AimMode = Barcode.AIM_MODE.AIM_MODE_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.AimType = Barcode.AIM_TYPE.AIM_TYPE_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.BeamTimer = -1;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.BidirRedundancy = Barcode.DisabledEnabled.Undefined;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.ControlScanLed = Barcode.DisabledEnabled.Undefined;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.DBPMode = Barcode.DBP_MODE.DBP_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.KlasseEinsEnable = Barcode.DisabledEnabled.Undefined;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.LinearSecurityLevel = Barcode.LINEAR_SECURITY_LEVEL.SECURITY_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.NarrowBeam = Barcode.DisabledEnabled.Undefined;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.PointerTimer = -1;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.RasterHeight = -1;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.RasterMode = Barcode.RASTER_MODE.RASTER_MODE_UNDEFINED;
            scanner.ReaderParameters.ReaderSpecific.LaserSpecific.ScanLedLogicLevel = Barcode.DisabledEnabled.Undefined;
            scanner.ScanParameters.BeepFrequency = 2670;
            scanner.ScanParameters.BeepTime = 200;
            scanner.ScanParameters.CodeIdType = Barcode.CodeIdTypes.None;
            scanner.ScanParameters.LedTime = 3000;
            scanner.ScanParameters.ScanType = Barcode.ScanTypes.Foreground;
            scanner.ScanParameters.WaveFile = String.Empty;
        }

        public void EnableAddBarCodes(bool enabled)
        {
            Barcode.DisabledEnabled value = enabled ? Barcode.DisabledEnabled.Enabled : Barcode.DisabledEnabled.Disabled;

            barcode1.DecoderParameters.CODABAR = value;
            barcode1.DecoderParameters.CODE128 = value;
            barcode1.DecoderParameters.CODE39 = value;
            barcode1.DecoderParameters.CODE93 = value;
            barcode1.DecoderParameters.D2OF5 = value;
            barcode1.DecoderParameters.KOREAN_3OF5 = value;
            barcode1.DecoderParameters.MSI = value;

        }






    }

    public delegate void OnReadBarCodeHandler(string Code, string OriginalCode);
}
