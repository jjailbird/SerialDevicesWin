using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
// using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using SerialDevicesWin.Helpers;
using Fleck2;
using Fleck2.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

// http://o7planning.org/en/10509/connecting-to-oracle-database-using-csharp-without-oracle-client


namespace SerialDevicesWin
{
    public partial class SerialDevicesWinMain : Form
    {
        private bool appInLoad = false;

        private string _KIOSKID = "";
        private string _local_ip = "";
        private string _global_ip = "";

        private SerialPort dtpPort = new SerialPort();
        private string dtpPortName; // = Properties.Settings.Default.dtpPortName; // Prolific USB-to-Serial Comm Port
        private DTP_HM dtpPrinter = null;

        private class PrintValues
        {
            public string Title;
            public string PatronNo;
            public string PatronName;
            public string DmStartNo;
            public string FreeCouponAmt;
            public string PrintDateSeq; 

            public PrintValues()
            {
                this.Title = "";
                this.PatronNo = "";
                this.PatronName = "";
                this.DmStartNo = "";
                this.FreeCouponAmt = "";
                this.PrintDateSeq = "";
            }
        }

        private class PrintWaitingValue
        {
            public string Title;
            public string WaitingNumber;
            public string PatientName;
            public string PatientNumber;
            public string Contents1;
            public string Contents2;
            public string PrintDateTime;
            public string Footer;

            public PrintWaitingValue()
            {
                this.Title = "";
                this.WaitingNumber = "";
                this.PatientName = "";
                this.PatientNumber = "";
                this.Contents1 = "";
                this.Contents2 = "";
                this.PrintDateTime = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                this.Footer = "";
            }
        }
        private SerialPort aisPort = new SerialPort();
        private string aisPortName; // = Properties.Settings.Default.aisPortName; // 33xx Area-Imaging Scanner

        private SerialPort msrPort = new SerialPort();
        private string msrPortName; // = Properties.Settings.Default.dtpPortName; // Prolific USB-to-Serial Comm Port


        // Web Socker Server ---------------------------------------------
        private static string _webSocketHost = "ws://localhost:8181";
        private static WebSocketServer _webSockerserver = null;
        private static List<IWebSocketConnection> _webSocketsAll = null;
      
        // ---------------------------------------------------------------

        public SerialDevicesWinMain()
        {
            InitializeComponent();

            _KIOSKID = Properties.Settings.Default.KioskID;
            _local_ip = Utils.GetIPAddress();
            // _global_ip = Utils.GetExternalIPAddress().ToString();

            this.WindowState = FormWindowState.Minimized;
            
            LoadSerialPortList();

            StartWebSocketServer();
          
        }
        
        private void SerialDevicesWinMain_Load(object sender, EventArgs e)
        {
            appInLoad = true;
            appInLoad = false;

        }

        private void lblBtnX_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        void Mouse_Move()
        {
            WinAPI.ReleaseCapture();
            WinAPI.SendMessage(this.Handle, WinAPI.WM_NCLBUTTONDOWN, WinAPI.HT_CAPTION, 0);
        }

        private void headerPanel_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse_Move();
        }

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse_Move();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("프로그램을 종료하시겠습니까?", "프로그램 종료", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                // Send2Web(new Result2Web("kiosk", "alarm", "", ResultCode.READY.ToString(), "Kiosk 프로그램이 종료되었습니다.").GetJsonString());
                //StopWebSocketServer();
                //printerPort.Close();
                //scannerPort.Close();
                Application.Exit();
            }
        }

        private void Log2TextBox(TextBox logBox, string message)
        {
            if (this.Visible)
            {
                if (this.InvokeRequired)
                {
                    Invoke(new MethodInvoker(delegate () {
                        logBox.AppendText(string.Format("RECEIVED: {0}\r\n", message));
                    }));
                }
                else
                {
                    logBox.AppendText(string.Format("RECEIVED: {0}\r\n", message));
                }
            }

            /*
            if (this.InvokeRequired)
            {
                Action action = () => {
                    logBox.AppendText(string.Format("RECEIVED: {0}\r\n", message));
                };
                logBox.Invoke(action);
            }
            else
            {
                logBox.AppendText(string.Format("RECEIVED: {0}\r\n", message));
            }
            */
        }
        // Buttons For TEST --------------------------------------------------------------------------------------
        private void btnTest_Click(object sender, EventArgs e)
        {
            
            PrintValues data = new PrintValues();
            data.Title = "GKL TEST";
            data.PatronNo = "123456";
            data.PatronName = "홍길동";
            data.FreeCouponAmt = "￦1,000,000";
            data.DmStartNo = "1234567";
            data.PrintDateSeq = "2017062300001";

            PrintToDTP(data);
        }

        private int _patronNumber = 0;
        private string _printedValue = "";
        private string _payAmount = "";

        // SerialPort Check =====================================================================================================

        private void LoadSerialPortList()
        {
            // CheckPortList();
            string[] aisPorts = SerialPort.GetPortNames();
            string[] dtpPorts = SerialPort.GetPortNames();
            string[] msrPorts = SerialPort.GetPortNames();

            // aisPortList_SelectedIndexChanged 이벤트 발생됨
            dtpPortList.DataSource = dtpPorts;
            aisPortList.DataSource = aisPorts;
            msrPortList.DataSource = msrPorts;

            dtpPortName = Properties.Settings.Default.dtpPortName ?? "";
            aisPortName = Properties.Settings.Default.aisPortName ?? "";
            msrPortName = Properties.Settings.Default.msrPortName ?? "";

            if(dtpPortName != "")
            {
                dtpInit();
                dtpPortList.SelectedItem = dtpPortName;
                dtpPortChecker_Check();
            }
            
            aisPortList.SelectedItem = aisPortName;
            msrPortList.SelectedItem = msrPortName;
                      
            aisPortChecker_Check();

        }

        // Direct Thermal Printer _______________________________________________________________________________________________
        private void dtpPortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPort = (string)dtpPortList.SelectedItem;

            //if (appInLoad == false)
            //{
            if (dtpPort.IsOpen)
            {
                if (selectedPort != dtpPortName)
                {
                    MessageBox.Show("프린터 포트를 사용중입니다. 스위치를 클릭하여 먼저 포트를 닫은 후 변경바랍니다.");
                    dtpPortList.SelectedItem = dtpPortName;
                }
            }
            else
            {
                dtpPortName = selectedPort;
                dtpPortChecker_Check();
            }
            //}

            // this.CheckPortList();
        }

        private void dtpPortChecker_Check()
        {
            if (dtpPort != null && dtpPort.IsOpen)
            {
                dtpPortChecker.BackColor = System.Drawing.Color.DodgerBlue;
            }
            else
            {
                dtpPortChecker.BackColor = System.Drawing.Color.DimGray;
            }
        }

        private void dtpPortChecker_Click(object sender, EventArgs e)
        {
            string selectedPortName = (string)dtpPortList.SelectedItem;
            dtpPortName = selectedPortName;
            if (dtpPortName != "" && (dtpPortName == aisPortName || dtpPortName == msrPortName))
            {
                MessageBox.Show("이미 사용중인 포트입니다.");
            }
            else
            {
                try
                {
                    dtpInit();
                    dtpPortChecker_Check();
                }
                catch (Exception ex)
                {
                    // MessageBox.Show(ex.Message);
                }

                Properties.Settings.Default.dtpPortName = dtpPortName;
                Properties.Settings.Default.Save();
            }

        }
        private void dtpInit()
        {

            if (dtpPrinter == null)
            {
                dtpPrinter = new DTP_HM(dtpPort);
                // dtpPrinter.PrintReset();
            }

            if (dtpPort.IsOpen)
            {
                dtpPort.Close();
            }
            else
            {
                dtpPort.PortName = dtpPortName;
                dtpPort.Encoding = Encoding.Default; // 한글 깨짐 방지
                dtpPort.BaudRate = 19200;
                dtpPort.DataBits = 8;
                dtpPort.Parity = Parity.None;
                dtpPort.StopBits = StopBits.One;

                if (dtpPort.IsOpen == false)
                {
                    dtpPort.DataReceived += new SerialDataReceivedEventHandler(SerialDataReceivedHandler4dtp);
                    try
                    {
                        dtpPort.Open();
                    }
                    catch (Exception ex)
                    {
                        Properties.Settings.Default.dtpPortName = "";
                        Properties.Settings.Default.Save();
                        MessageBox.Show(string.Format("{0}: {1}", ex.HResult.ToString(), ex.Message));
                    }
                }
            }

        }

        private class PrintResult
        {
            public string Command;
            public string Result;
            public int PatronNo;
            public string DmStatNo;

            public PrintResult()
            {
                this.Command = "PRINT_STATUS";
                this.Result = "";
                // this.PatronNo = 0;
                this.DmStatNo = "";
            }
        }

        private PrintResult _printResult = new PrintResult();


        private void SerialDataReceivedHandler4dtp(object sender, EventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            string statusCode = sp.ReadExisting();
            
            string status = "OK";
            switch (statusCode)
            {
                case "\0":
                    status = "PRINT OK";
                    break;
                case "00":
                    status = "PRINTER OK";
                    break;
                case "10":
                    status = "PAPER EMPTY";
                    break;
                case "20":
                    status = "HEAD-UP SENSOR";
                    break;
                case "40":
                    status = "CUTTER Sensor";
                    break;
                case "01":
                    status = "NEAR-END SENSOR";
                    break;
                case "02":
                    status = "PAPER JAM";
                    break;
                case "04":
                    status = "Paper out Sensor";
                    break;
                case "08":
                    status = "BM ERROR";
                    break;
                default:
                    status = "ERROR";
                    break;
            }


            if(status == "PRINT OK")
            {
                _printResult.Result = "Printed";
                Send2Web(JsonConvert.SerializeObject(_printResult));
            }
        }

        private void PrintToDTP(PrintValues print)
        {
            string sDividerPartial = "-----------------------";
            string sDividerFull = "====================================";
            
            //dtpPrinter.PrintReset();
            //dtpPrinter.SetCharacterSet(DTP_HM.CharacterSet.Korea);
            dtpPrinter.SetFontAlign(DTP_HM.FontAlign.Center);

            // dtpPrinter.SetFontSize(1, 1);
            // dtpPrinter.PrintLogo();
            
            dtpPrinter.SetFontSize(2, 2);
            dtpPrinter.PrintLine(print.Title);

            dtpPrinter.PrintLine(1, print.PatronNo);
            dtpPrinter.PrintLine(print.PatronName);

            dtpPrinter.PrintLine(1, print.FreeCouponAmt);

            dtpPrinter.PrintLine(1, "BELLOW BARCODE");
            dtpPrinter.PrintBarcode(print.DmStartNo);
            dtpPrinter.PrintLine(1, "barcarde: " + print.DmStartNo);
            dtpPrinter.PrintLine(0, print.PrintDateSeq);
            dtpPrinter.CutPaper(10);
            dtpPrinter.PrintSetStatus(1);
            dtpPrinter.PrintSetRealTime();

        }

        private void PrintWaitingNumber(PrintWaitingValue data)
        {
            dtpPrinter.SetFontAlign(DTP_HM.FontAlign.Center);
            
            dtpPrinter.SetFontSize(2, 2);
            dtpPrinter.PrintLine(data.Title);

            dtpPrinter.SetFontSize(3, 3);
            dtpPrinter.PrintLine(1, data.WaitingNumber);

            dtpPrinter.SetFontSize(2, 2);
            dtpPrinter.PrintLine(1, data.PatientName+ "님 " + data.PatientNumber);
            dtpPrinter.SetFontSize(1, 1);
            dtpPrinter.PrintLine("");

            dtpPrinter.SetBarcodeHeight(90);
            dtpPrinter.PrintBarcode(data.PatientNumber);

            dtpPrinter.SetFontSize(1, 2);
            dtpPrinter.PrintLine(1, data.Contents1);
            dtpPrinter.SetFontSize(1, 0);
            dtpPrinter.PrintLine("");
            dtpPrinter.SetFontSize(1, 2);
            dtpPrinter.PrintLine(data.Contents2);

            dtpPrinter.SetFontSize(1, 1);
            dtpPrinter.PrintLine(2, data.PrintDateTime);
            dtpPrinter.PrintLine(1, data.Footer);

            dtpPrinter.CutPaper(8);
            dtpPrinter.PrintSetStatus(1);
            dtpPrinter.PrintSetRealTime();
        }
        // ______________________________________________________________________________________________________________________

        #region ais(Area Image Scanner)
        private void aisPortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedPort = (string)aisPortList.SelectedItem;

            if (appInLoad == false)
            {
                if (aisPort.IsOpen)
                {
                    if (selectedPort != aisPortName)
                    {
                        MessageBox.Show("프린터 포트를 사용중입니다. 스위치를 클릭하여 먼저 포트를 닫은 후 변경바랍니다.");
                        aisPortList.SelectedItem = aisPortName;
                    }
                }
                else
                {
                    aisPortName = selectedPort;
                    aisPortChecker_Check();
                }
            }

            // this.CheckPortList();
        }

        private void aisPortChecker_Check()
        {
            if (aisPort != null && aisPort.IsOpen)
            {
                aisPortChecker.BackColor = System.Drawing.Color.DodgerBlue;
            }
            else
            {
                aisPortChecker.BackColor = System.Drawing.Color.DimGray;
            }
        }

        private void aisPortChecker_Click(object sender, EventArgs e)
        {
            string selectedPortName = (string)aisPortList.SelectedItem;
            if (aisPortName != "" && (aisPortName == dtpPortName || aisPortName == msrPortName))
            {
                MessageBox.Show("이미 사용중인 포트입니다.");
            }
            else
            {
                try
                {
                    aisInit();
                    aisPortChecker_Check();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                Properties.Settings.Default.aisPortName = aisPortName;
                Properties.Settings.Default.Save();
            }

        }
        private void aisInit()
        {
            if (aisPort.IsOpen)
            {
                aisPort.Close();
            }
            else
            {
                aisPort.PortName = aisPortName;
                aisPort.Encoding = Encoding.Default; // 한글 깨짐 방지
                aisPort.BaudRate = 115200;
                aisPort.DataBits = 8;
                aisPort.Parity = Parity.None;
                aisPort.StopBits = StopBits.One;

                if (aisPort.IsOpen == false)
                {
                    aisPort.DataReceived += new SerialDataReceivedEventHandler(SerialDataReceivedHandler4ais);
                    try
                    {
                        aisPort.Open();
                    }
                    catch (Exception ex)
                    {
                        Properties.Settings.Default.aisPortName = "";
                        Properties.Settings.Default.Save();
                        MessageBox.Show(string.Format("{0}: {1}", ex.HResult.ToString(), ex.Message));
                    }
                }
            }

        }
        #endregion

        private void SerialDataReceivedHandler4ais(object sender, EventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            
            string data = sp.ReadExisting();
 
            string cmdScanDeactive = AsciiCode.SYN + (char)'U' + AsciiCode.CR;
            sp.Write(cmdScanDeactive);
        }

        // ======================================================================================================================

        // 재출력 -----------------------------------------------------------------------------------------------------
        private class GetPrizeReprintResult
        {
            public string Command;

            public int PATRON_NO;
            public string PATRON_NM;
            public string LOG_DT;
            public string FREE_COUPON_AMT;
            public string PRINT_YN;
            public string DM_START_NO;

            
            public GetPrizeReprintResult()
            {
                this.Command = "";

                this.PATRON_NO = 0;
                this.PATRON_NM = "";
                this.LOG_DT = "";
                this.FREE_COUPON_AMT = "";
                this.PRINT_YN = "";
                this.DM_START_NO = "";

        }

    }
        
        // ------------------------------------------------------------------------------------------------------------


        // ======================================================================================================================

        // WebSocket Server =====================================================================================================
        private void StartWebSocketServer()
        {
            lblTitle.Text += " > " + _webSocketHost;
            // Fleck2 - Websocket Start -----------------------------------------------------------------------------------------
            // FleckLog.Level = LogLevel.Debug;
            _webSocketsAll = new List<IWebSocketConnection>();
            _webSockerserver = new WebSocketServer(_webSocketHost);
            _webSockerserver.ListenerSocket.NoDelay = true;
            _webSockerserver.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    // Console.WriteLine("Open!");
                    try { _webSocketsAll.Add(socket); }
                    catch(Exception ex)
                    {
                        Log2TextBox(txtLogBox, string.Format("_webSocketsAll.Add", ex.Message));
                    }
                    
                };
                socket.OnClose = () =>
                {
                    try
                    {
                        _webSocketsAll.Remove(socket);
                    }
                    catch (Exception ex)
                    {
                        Log2TextBox(txtLogBox, string.Format("_webSocketsAll.Remove", ex.Message));
                    }
                };
                socket.OnMessage = message =>
                {
                    try
                    {
                        Send2Device(message);
                    }
                    catch (Exception ex)
                    {
                        Log2TextBox(txtLogBox, string.Format("Send2Device", ex.Message));
                    }
                };
            });
            // ------------------------------------------------------------------------------------------------------------------

        }

        private void Send2Device(string message)
        {
            /*
            oMessage.device = "thermalPrinter"
            oMessage.command = "print_waitingNumber"
            oMessage.waitingNumber = sNumber
            */
            JObject cmd = JObject.Parse(message);
            string device = (string)cmd["device"];
            string command = (string)cmd["command"];
            
            if (device == "thermalPrinter")
            {

                if(command == "print_waitingNumber")
                {
                    PrintWaitingValue data = new PrintWaitingValue();
                    data.Title          = (string)cmd["title"];
                    data.WaitingNumber  = (string)cmd["waitingNumber"];
                    data.PatientName    = (string)cmd["patientName"];
                    data.PatientNumber  = (string)cmd["patientNumber"];
                    data.Contents1      = (string)cmd["contents1"];
                    data.Contents2      = (string)cmd["contents2"];
                    data.Footer         = (string)cmd["footer"];

                    PrintWaitingNumber(data);
                }
            }
            else
            {
                // Send2Web(JsonConvert.SerializeObject(result));

                PrintValues printData = new PrintValues();

                /*
                printData.Title = "GKL XMAS EVENT";
                printData.PatronNo = result.PATRON_NO.ToString();
                printData.PatronName = result.PATRON_NM;
                printData.DmStartNo = result.DM_START_NO;
                printData.FreeCouponAmt = result.FREE_COUPON_AMT;
                printData.PrintDateSeq = result.PRINT_DATE_SEQ;

                _printResult.PatronNo = result.PATRON_NO;
                _printResult.DmStatNo = result.DM_START_NO;

                PrintToDTP(printData);
                */

            }

        }

        private void Send2Web(string sJsonResult)
        {
            // string sJsonResult = JsonConvert.SerializeObject(combData);
            foreach (var socket in _webSocketsAll.ToList())
            {
                socket.Send(sJsonResult);
            }
        }


        private void StopWebSocketServer()
        {
            _webSockerserver.ListenerSocket.Close();
        }

        // ======================================================================================================================
    }
}
