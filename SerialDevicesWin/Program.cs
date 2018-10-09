using System;
using System.Windows.Forms;
using System.Text;
using System.Linq;
using SerialDevicesWin.Helpers;

namespace SerialDevicesWin
{
    static class Program
    {
        // private static HidDevice _device;
        // private const int VendorId = 5771;
        // private const int ProductId = 8452;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SerialDevicesWinMain());

        }
       

    }


}
