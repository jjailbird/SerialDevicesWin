using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialDevicesWin.Helpers
{
    class Ascii
    {

        public const char NUL = '\x00'; // null
        public const char SOH = '\x01'; // start of header
        public const char STX = '\x02'; // start of text
        public const char ETX = '\x03'; // end of text
        public const char EOT = '\x04'; // end of transmission
        public const char ENQ = '\x05'; // enquiry
        public const char ACK = '\x06'; // acknowledge
        public const char BEL = '\x07'; // bell
        public const char BS = '\x08'; // backspace
        public const char HT = '\x09'; // horizontal tab
        public const char LF = '\x0A'; // line feed
        public const char VT = '\x0B'; // vertical tab
        public const char FF = '\x0C'; // form feed
        public const char CR = '\x0D'; // enter / carriage return
        public const char SO = '\x0E'; // shift out
        public const char SI = '\x0F'; // shift in
        public const char DLE = '\x10'; // data link escape
        public const char DC1 = '\x11'; // device control 1
        public const char DC2 = '\x12'; // device control 2
        public const char DC3 = '\x13'; // device control 3
        public const char DC4 = '\x14'; // device control 4
        public const char NAK = '\x15'; // negative acknowledge
        public const char SYN = '\x16'; // synchronize
        public const char ETB = '\x17'; // end of trans.block
        public const char CAN = '\x18'; // cancel
        public const char EM = '\x19'; // end of medium
        public const char SUB = '\x1A'; // substitute
        public const char ESC = '\x1B'; // escape
        public const char FS = '\x1C'; // file separator
        public const char GS = '\x1D'; // group separator
        public const char RS = '\x1E'; // record separator
        public const char US = '\x1F'; // unit separator
        public const char DEL = '\x7F'; // delete

    }

    public static class AsciiCode
    {
        public const string NUL = "\x00"; // null
        public const string SOH = "\x01"; // start of header
        public const string STX = "\x02"; // start of text
        public const string ETX = "\x03"; // end of text
        public const string EOT = "\x04"; // end of transmission
        public const string ENQ = "\x05"; // enquiry
        public const string ACK = "\x06"; // acknowledge
        public const string BEL = "\x07"; // bell
        public const string BS = "\x08"; // backspace
        public const string HT = "\x09"; // horizontal tab
        public const string LF = "\x0A"; // line feed
        public const string VT = "\x0B"; // vertical tab
        public const string FF = "\x0C"; // form feed
        public const string CR = "\x0D"; // enter / carriage return
        public const string SO = "\x0E"; // shift out
        public const string SI = "\x0F"; // shift in
        public const string DLE = "\x10"; // data link escape
        public const string DC1 = "\x11"; // device control 1
        public const string DC2 = "\x12"; // device control 2
        public const string DC3 = "\x13"; // device control 3
        public const string DC4 = "\x14"; // device control 4
        public const string NAK = "\x15"; // negative acknowledge
        public const string SYN = "\x16"; // synchronize
        public const string ETB = "\x17"; // end of trans.block
        public const string CAN = "\x18"; // cancel
        public const string EM = "\x19"; // end of medium
        public const string SUB = "\x1A"; // substitute
        public const string ESC = "\x1B"; // escape
        public const string FS = "\x1C"; // file separator
        public const string GS = "\x1D"; // group separator
        public const string RS = "\x1E"; // record separator
        public const string US = "\x1F"; // unit separator
        public const string DEL = "\x7F"; // delete

        public static string ToHexcodes(this string sSource)
        {
            char[] charValues = sSource.ToCharArray();
            string hexOutput = "";
            foreach (char _eachChar in charValues)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(_eachChar);
                // Convert the decimal value to a hexadecimal value in string form.
                hexOutput += String.Format(@"\x{0:X}", value);
                // to make output as your eg 
                //  hexOutput +=" "+ String.Format("{0:X}", value);

            }
            return hexOutput;
        }
    }
}
