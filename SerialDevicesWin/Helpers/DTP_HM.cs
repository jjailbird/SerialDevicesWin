using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Drawing;
using System.Collections;
using System.IO;

namespace SerialDevicesWin.Helpers
{
    // Direct Thermal Printer - HWYASUNG SYSTEM - HMK, HMC
    class DTP_HM
    {
        private string PRINTER_NAME;
        private SerialPort PRINTER_PORT;
        private enum CONN_TYPE { usb, serial };
        private CONN_TYPE PRINTER_TYPE;
        public enum FontAlign { Left, Center, Right };
        public enum CharacterSet
        {
            USA, France, Germany, England, Denmark_I, Sweden, Italy, Spain_I, Japan, Norway, Denmark_II, Spain_II, Latin_America, Korea
        }
        public enum E_BARCORDE_TYPE
        {
            UPC_E   = 0x01, // n = 7
            EAN13   = 0x02, // n = 12
            EAN8    = 0x03, // n = 7 
            CODE39  = 0x04, // n >= 1
            ITF     = 0x05, // n >= 1
            CODABAR = 0x06, // n >= 1
            CODE128_A = 0x07, // 2 <= n <= 255 g
            CODE128_B = 0x08, // 2 <= n <= 255 h
            CODE128_C = 0x09, // 2 <= n <= 255 i
        }
        public enum CutMode { Full, Partial }
        public DTP_HM(string printerName)
        {
            this.PRINTER_NAME = printerName;
            this.PRINTER_TYPE = CONN_TYPE.usb;
        }

        public DTP_HM(SerialPort printerPort)
        {
            this.PRINTER_PORT = printerPort;
            this.PRINTER_TYPE = CONN_TYPE.serial;
        }

        public void SetPort(SerialPort printerPort)
        {
            this.PRINTER_PORT = printerPort;
        }

        public void ClosePort()
        {
            this.PRINTER_PORT.Close();
        }
        
        public bool IsPortOpened
        {
            get
            {
                return this.PRINTER_PORT.IsOpen;
            }
        }

        // --------------------------------------------------------------------
        public void PrintSetStatus(int No)
        {
            string sCommnad = AsciiCode.GS + (char)'a' + (char)No;
            this.PRINTER_PORT.Write(sCommnad);
        }

        public void PrintSetRealTime()
        {
            string sCommnad = AsciiCode.DLE + AsciiCode.EOT + (char)2;
            this.PRINTER_PORT.Write(sCommnad);
        }


        public void PrintLine(string sString)
        {
            string sCommand = AsciiCode.LF;
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
            {
                this.PRINTER_PORT.Write(sString);
                this.PRINTER_PORT.Write(sCommand);
            }
        }
        
        public void PrintLine(int iLine, string sString)
        {
            string sCommand = "";
            for(int i=0; i<iLine; i++)
            {
                sCommand += AsciiCode.LF;
            }
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
            {
                this.PRINTER_PORT.Write(sCommand);
            }
            PrintLine(sString);
        }

        public void PrintPage()
        {
            string sCommand = AsciiCode.ESC + AsciiCode.FF;
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
                this.PRINTER_PORT.Write(sCommand);
        }

        public void SetFontAlign(FontAlign align)
        {
            Int16 iAlign = (Int16)align;

            string sCommand = AsciiCode.ESC + (char)'a' + (char)iAlign;
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
                this.PRINTER_PORT.Write(sCommand);
        }

        public void CutPaper(CutMode mode)
        {
            Int16 iMode = (Int16)mode;

            string sCommand = AsciiCode.GS + (char)'V' + (char)iMode;
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
            {
                this.PRINTER_PORT.Write(sCommand);
            }
        }

        public void CutPaper(Int16 iLines)
        {
            string sCommand1 = "";

            for (int i = 0; i < iLines; i++)
            {
                sCommand1 += AsciiCode.LF;
            }
            //string sCommand2 = AsciiCode.ESC + (char)'i'; // Full cut
            string sCommand2 = AsciiCode.ESC + (char)'m'; // Partial cut
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
            {
                this.PRINTER_PORT.Write(sCommand1);
                // CutPaper(CutMode.Partial);
                this.PRINTER_PORT.Write(sCommand2);
            }
        }

        public void PrintLogo()
        {
            // https://stackoverflow.com/questions/14099239/printing-a-bit-map-image-to-pos-printer-via-comport-in-c-sharp
            // https://stackoverflow.com/questions/14099239/printing-a-bit-map-image-to-pos-printer-via-comport-in-c-sharp/14099717#14099717
            string logo = GetLogo(@"C:\logo.bmp");
            byte[] bytes = Encoding.ASCII.GetBytes(logo);
            string command = AsciiCode.GS + (char)'v' + (char)'0' + (char)10 + (char)10 + (char)5 + (char)5 + bytes;
            //    GS v 0 m xL xGS v 0 m xL xGS v 0 m xL xGS v 0 m xL xH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dkH yL yH d1..dk
            this.PRINTER_PORT.Write(logo);
        }

        public void SetBarcodeHeight(byte bHeight)
        {
            byte[] bytes_command = new byte[] { 0x1d, 0x68, bHeight };
            this.PRINTER_PORT.Write(bytes_command, 0, bytes_command.Length);
        }

        public void SetBarcodeHeight(int iHeight)
        {
            SetBarcodeHeight((byte)iHeight);
        }


        public void PrintBarcode(string sString, E_BARCORDE_TYPE type)
        {
            byte barcode_type;
            string barcode128_char = null;
            switch(type)
            {
                case E_BARCORDE_TYPE.CODE128_A:
                    barcode128_char = "g";
                    barcode_type = 0x07;
                    break;
                case E_BARCORDE_TYPE.CODE128_B:
                    barcode128_char = "h";
                    barcode_type = 0x07;
                    break;
                case E_BARCORDE_TYPE.CODE128_C:
                    barcode128_char = "i";
                    barcode_type = 0x07;
                    break;
                default:
                    barcode_type = (byte)type;
                    break;

            }

            if (barcode128_char != null)
            {
                sString = barcode128_char + sString;
            }

            byte[] bytes_command = new byte[] { 0x1d, 0x6b, barcode_type };
            byte[] bytes_barcode = Encoding.ASCII.GetBytes(sString + AsciiCode.NUL);
            byte[] bytes_send = Combine(bytes_command, bytes_barcode);
            this.PRINTER_PORT.Write(bytes_send, 0, bytes_send.Length);
        }

        public void PrintBarcode(string sString)
        {
            PrintBarcode(sString, E_BARCORDE_TYPE.CODE128_A);
        }
        // -----------------------------------------------------------------------

        public void PrintReset()
        {
            string sCommand = AsciiCode.ESC + (char)'@';
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
                this.PRINTER_PORT.Write(sCommand);
        }



        public void SetCharacterSet(CharacterSet iso)
        {
            string sCommand = AsciiCode.ESC + (char)'R' + (int)iso;
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
                this.PRINTER_PORT.Write(sCommand);
        }

        public void SetFontSize(string sSize)
        {
            // ex) "\x12" width: 1, height: 2
            string sCommand = AsciiCode.GS + (char)'!' + sSize;
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
                this.PRINTER_PORT.Write(sCommand);
        }

        public void SetFontSize(int xWidth, int xHeight)
        {
            int iWidth = xWidth > 0 ? (xWidth - 1) * 16 : 0;
            int iHeight = xHeight > 0 ? xHeight - 1 : 0;

            int iSize = iWidth + iHeight;

            string sCommand = AsciiCode.GS + (char)'!' + (char)iSize;
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
                this.PRINTER_PORT.Write(sCommand);
        }

        public void SetLeftMargin(string sSize)
        {
            string sCommand = AsciiCode.GS + (char)'L' + sSize;
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
                this.PRINTER_PORT.Write(sCommand);
        }


        public void SetCharacterTable()
        {
            string sCommand = AsciiCode.ESC + (char)'t' + 2;
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
                this.PRINTER_PORT.Write(sCommand);
        }

        public void AddString(string sString)
        {
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
                this.PRINTER_PORT.Write(sString);
        }
        
        public void PrintLine()
        {
            // RawPrinterHelper.SendStringToPrinter(PRINTER_NAME, sString);
            string sCommand = AsciiCode.LF;
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
                this.PRINTER_PORT.Write(sCommand);
        }

        public void PrintAdd(string sString)
        {
            string sCommand = AsciiCode.LF;
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
            {
                this.PRINTER_PORT.Write(sString);
                // this.PRINTER_PORT.Write(sCommand);
            }
        }


        public void PrintLine(string sString, Int16 iLines)
        {
            string sCommand = AsciiCode.ESC + (char)'d' + (char)iLines;
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
            {
                this.PRINTER_PORT.Write(sString);
                this.PRINTER_PORT.Write(sCommand);
            }
        }

        public void PrintFeed(string sString, Int16 iFeed)
        {
            string sCommand = AsciiCode.ESC + (char)'J' + (char)iFeed;
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
            {
                this.PRINTER_PORT.Write(sString);
                this.PRINTER_PORT.Write(sCommand);
            }
        }

        

        public void CheckStatus()
        {
            string sCommand = AsciiCode.ESC + (char)'H';
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
                this.PRINTER_PORT.Write(sCommand);
        }

        public string GetStatus()
        {
            string status = "";
            if (this.PRINTER_TYPE == CONN_TYPE.serial)
            {
                status = this.PRINTER_PORT.ReadExisting();
            }
            return status;
        }

        public byte[] Combine(byte[] a, byte[] b)
        {
            byte[] c = new byte[a.Length + b.Length];
            System.Buffer.BlockCopy(a, 0, c, 0, a.Length);
            System.Buffer.BlockCopy(b, 0, c, a.Length, b.Length);
            return c;
        }

        public byte[] Combine(params byte[][] arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;

            foreach (byte[] array in arrays)
            {
                System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }


        public string GetLogo(string imagePath)
        {
            string logo = "";
            if (!File.Exists(imagePath))
                return null;
            BitmapData data = GetBitmapData(imagePath);
            BitArray dots = data.Dots;
            byte[] width = BitConverter.GetBytes(data.Width);

            int offset = 0;
            MemoryStream stream = new MemoryStream();
            BinaryWriter bw = new BinaryWriter(stream);

            bw.Write((char)0x1B);
            bw.Write('@');

            bw.Write((char)0x1B);
            bw.Write('3');
            bw.Write((byte)24);

            while (offset < data.Height)
            {
                bw.Write((char)0x1B);
                bw.Write('*');         // bit-image mode
                bw.Write((byte)33);    // 24-dot double-density
                bw.Write(width[0]);  // width low byte
                bw.Write(width[1]);  // width high byte

                for (int x = 0; x < data.Width; ++x)
                {
                    for (int k = 0; k < 3; ++k)
                    {
                        byte slice = 0;
                        for (int b = 0; b < 8; ++b)
                        {
                            int y = (((offset / 8) + k) * 8) + b;
                            // Calculate the location of the pixel we want in the bit array.
                            // It'll be at (y * width) + x.
                            int i = (y * data.Width) + x;

                            // If the image is shorter than 24 dots, pad with zero.
                            bool v = false;
                            if (i < dots.Length)
                            {
                                v = dots[i];
                            }
                            slice |= (byte)((v ? 1 : 0) << (7 - b));
                        }

                        bw.Write(slice);
                    }
                }
                offset += 24;
                bw.Write((char)0x0A);
            }
            // Restore the line spacing to the default of 30 dots.
            bw.Write((char)0x1B);
            bw.Write('3');
            bw.Write((byte)30);

            bw.Flush();
            byte[] bytes = stream.ToArray();
            return logo + Encoding.Default.GetString(bytes);
        }

        public BitmapData GetBitmapData(string bmpFileName)
        {
            using (var bitmap = (Bitmap)Bitmap.FromFile(bmpFileName))
            {
                var threshold = 127;
                var index = 0;
                double multiplier = 570; // this depends on your printer model. for Beiyang you should use 1000
                double scale = (double)(multiplier / (double)bitmap.Width);
                int xheight = (int)(bitmap.Height * scale);
                int xwidth = (int)(bitmap.Width * scale);
                var dimensions = xwidth * xheight;
                var dots = new BitArray(dimensions);

                for (var y = 0; y < xheight; y++)
                {
                    for (var x = 0; x < xwidth; x++)
                    {
                        var _x = (int)(x / scale);
                        var _y = (int)(y / scale);
                        var color = bitmap.GetPixel(_x, _y);
                        var luminance = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                        dots[index] = (luminance < threshold);
                        index++;
                    }
                }

                return new BitmapData()
                {
                    Dots = dots,
                    Height = (int)(bitmap.Height * scale),
                    Width = (int)(bitmap.Width * scale)
                };
            }
        }

        public class BitmapData
        {
            public BitArray Dots
            {
                get;
                set;
            }

            public int Height
            {
                get;
                set;
            }

            public int Width
            {
                get;
                set;
            }
        }
    }
}
