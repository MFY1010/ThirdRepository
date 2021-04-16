using AppCore;
using AppCore.Tools;
using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace HealthHappy_DataCollect.From
{
    public partial class Frm_main : Form
    {
        //创建串口、网口对象
        SerialPortUtil serial = null;
        SocketUtilNew socket = null;
        //创建定时对象
        System.Threading.Timer resultTimer = null;
        System.Threading.Timer SouceTimer = null;
        Ping p = new Ping();
        PingReply pr;
        IDataCollect MacDataCollent;
        public Frm_main()
        {
            InitializeComponent();
        }
        #region 最小化处理

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void Frm_main_Deactivate(object sender, EventArgs e)
        {
           // Hide();
        }

        /// <summary>
        /// 隐藏处理
        /// </summary>
        public void IcoHide()
        {
            this.notifyIcon1.Visible = true;
            ShowInTaskbar = false;
            Hide();
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
                this.notifyIcon1.Visible = true;
            }

        }
        DialogResult dr = DialogResult.None;
        private void Frm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (dr == DialogResult.None)
            {
                dr = MessageBox.Show("关闭程序将无法获取仪器数据，是否关闭？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
            if (dr == DialogResult.Yes)
            {
                notifyIcon1.Dispose();
                Application.Exit();
            }
            else
            {
                dr = DialogResult.None;
                e.Cancel = true;
                IcoHide();
            }
        }
        #endregion

        /// <summary>
        /// 主窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_main_Load(object sender, EventArgs e)
        {
            try
            {               
                CreateTimer();
                InitTip();
                StratCollect();
                InitIOC();
                StartupDeal();
                ShowInfo("初始化成功！");
            }
            catch (Exception ex)
            {
                BaseDeal.LogError(DateTime.Now.ToString() + "  " + ex.ToString());
                MessageBox.Show(ex.Message + "\r\n" + "程序开启失败!");
                this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.Frm_main_FormClosing);
                this.Close();
            }
        }

        /// <summary>
        /// 展示信息
        /// </summary>
        /// <param name="text"></param>
        private void ShowInfo(string text)
        {
            Tb_Info.Text += DateTime.Now.ToString("MM-dd HH:mm:ss") + "  " + text + "\r\n";
        }

        /// <summary>
        /// 创建Timer对象
        /// </summary>
        private void CreateTimer()
        {
            SouceTimer = new System.Threading.Timer(this.TiemrData, null, -1, 3000);
            timerSocket.Interval = 3000;
            timerSocket.Enabled = false;
        }

        /// <summary>
        /// 初始化端口数据
        /// </summary>
        private void InitTip()
        {
            this.Text = "仪器:" + AppConfig.Instance.InstrumentName;
            string connectName = "";
            string connectInfo = "";
            if (AppConfig.Instance.ConnectType == "0")
            {
                connectInfo = "\r\n串口号：" + AppConfig.Instance.SerialPort + "\r\n数据位：" + AppConfig.Instance.DataBits + "\r\n波特率：" +
                             AppConfig.Instance.BauRate + "\r\n停止位:" + AppConfig.Instance.StopBit + "\r\n奇偶：" +
                             AppConfig.Instance.Parity;
                connectName = "串口通信:" + connectInfo;
            }
            else if (AppConfig.Instance.ConnectType == "1")
            {
                //默认端口号不为空
                if (!string.IsNullOrWhiteSpace(AppConfig.Instance.LocalPort))
                {
                    connectInfo = "\r\n本地电脑IP：" + AppConfig.Instance.LocalIP + "\r\n本机开放端口：" + AppConfig.Instance.LocalPort;
                    connectName = "网络通信:" + connectInfo;
                }
                else
                {
                    connectInfo = "\r\n远程电脑IP：" + AppConfig.Instance.ServerIP + "\r\n远程监听端口：" + AppConfig.Instance.ServerPort;
                    connectName = "网络通信:" + connectInfo;
                }
            }
            labelConnectType.Text = connectName;
            notifyIcon1.Text = "仪器：" + AppConfig.Instance.InstrumentName;
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }

        /// <summary>
        /// 开启通讯端口
        /// </summary>
        private void StratCollect()
        {
            try
            {
                if (AppConfig.Instance.ConnectType == "0")
                {
                    OpenSerial();
                }
                else if (AppConfig.Instance.ConnectType == "1")
                {
                    OpenSocket();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 注入仪器类
        /// </summary>
        public void InitIOC()
        {
            try
            {
                var builder = new ContainerBuilder();
                string classNameByMac = AppConfig.Instance.InstrumentDll;
                string dll = classNameByMac.Split(',')[1];
                string path = BaseDeal.AppPath() + "instrument\\" + dll + ".dll";
                Assembly assembly = Assembly.LoadFile(path);
                Type t = assembly.GetType(classNameByMac.Split(',')[0]);
                //注入数据解析接口 ，获得实际解析对象
                builder.RegisterType(t).As<IDataCollect>();
                var container = builder.Build();
                MacDataCollent = container.Resolve<IDataCollect>();
                ShowInfo("成功加载" + dll + "！");
            }
            catch (Exception ex)
            {
                BaseDeal.LogError(DateTime.Now.ToString() + "  " + ex.ToString());
            }
        }

        /// <summary>
        /// 启动结果解析时服务
        /// </summary>
        private void StartupDeal()
        {
            //启动结果上传
            int interval2 = Convert.ToInt32((decimal)1000 * (AppConfig.Instance.ResultUpdateTime));
            SouceTimer.Change(0, interval2);
        }

        /// <summary>
        /// 定时解析数据
        /// </summary>
        private void TiemrData(Object state)
        {
            lock (this)
            {
                try
                {
                    string rtn = MacDataCollent.GetResultData();
                    ReturnData(rtn);

                    pr = p.Send(AppConfig.Instance.SqlIP);
                    if (pr.Status != IPStatus.Success)
                    {
                        ///托盘气泡提示
                        int tipShowMilliseconds = 1000;
                        string tipTitle = "提示";
                        string tipContent = "网络断开";
                        ToolTipIcon tipType = ToolTipIcon.Info;
                        notifyIcon1.ShowBalloonTip(tipShowMilliseconds, tipTitle, tipContent, tipType);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    BaseDeal.LogError(DateTime.Now.ToString() + "  " + ex.ToString());
                }
            }
        }

        /// <summary>
        /// 返回数据
        /// </summary>
        /// <param name="rlt"></param>
        private void ReturnData(string rlt)
        {
            try
            {
                if (!string.IsNullOrEmpty(rlt))
                {
                    if (AppConfig.Instance.ConnectType == "0")
                    {
                        serial.WriteData(rlt);
                    }
                    else
                    {
                        socket.SendData(rlt);
                    }
                    BaseDeal.FileWrite("\r\n发送：" + rlt + "\r\n");
                }
            }
            catch (Exception ex)
            {
                BaseDeal.LogError(DateTime.Now.ToString() + "  " + ex.ToString());
            }

        }

        /// <summary>
        /// 打开串口通讯
        /// </summary>
        private void OpenSerial()
        {
            try
            {
                if (serial != null)
                {
                    serial.ClosePort();
                    serial = null;
                }
                serial = new SerialPortUtil(AppConfig.Instance.SerialPort, AppConfig.Instance.BauRate, AppConfig.Instance.Parity, AppConfig.Instance.DataBits, AppConfig.Instance.StopBit);
                serial.DataReceived += serial_DataReceived;
                serial.OpenPort();
            }
            catch (Exception)
            {
                serial = null;
                throw;
            }
        }

        /// <summary>
        /// 打开网口通讯
        /// </summary>
        private void OpenSocket()
        {
            if (socket != null)
            {
                socket.CloseSoket();
                socket = null;
            }
            bool b = AppConfig.Instance.IsUDP == "1" ? true : false;
            if (!string.IsNullOrEmpty(AppConfig.Instance.LocalPort))//LocalPort不为空时，此程序作为服务端打开
            {
                socket = new SocketUtilNew(AppConfig.Instance.LocalIP, Convert.ToInt32(AppConfig.Instance.LocalPort.Trim()), b);
                socket.CreateSocket_Servre();
            }
            else
            {
                try
                {
                    socket = new SocketUtilNew(AppConfig.Instance.ServerIP, Convert.ToInt32(AppConfig.Instance.SerialPort.Trim()), b);
                    socket.CreateSocket_Client();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                timerSocket.Start();

            }
            socket.DataReceived += Socket_DataReceived;
        }

        /// <summary>
        /// 网口获取数据
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private string Socket_DataReceived(DataReceivedEventArgs e)
        {
            ShowInfo("网口成功收到数据！");
            string data = e.DataReceived;
            Decode(data);
            return "";
        }

        /// <summary>
        /// 串口获取数据
        /// </summary>
        /// <param name="e"></param>
        void serial_DataReceived(DataReceivedEventArgs e)
        {
            ShowInfo("串口成功收到数据！");
            Decode(e.DataReceived);
        }

        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="data"></param>
        private void Decode(string data)
        {
            BaseDeal.FileWrite(data);
            string rlt;
            rlt = MacDataCollent.GetResultData(data);
            ReturnData(rlt);
        }
    }
}
