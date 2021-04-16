using System.Configuration;

namespace AppCore.Tools
{
    public class AppConfig
    {
        private static AppConfig instance;
        private AppConfig()
        {

        }
        public static AppConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppConfig();
                    GetConfig();
                }
                return instance;
            }
            set { instance = value; }
        }
        #region 字段
        #region 数据库连接
        public string SqlIP { get; set; }
        public string SqlName { get; set; }
        public string SqlUsername { get; set; }
        public string SqlPassword { get; set; }
        #endregion
        /// <summary>
        /// 数据对接方式 0是串口 1是网络
        /// </summary>
        public string ConnectType { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string Conn { get; set; }
        /// <summary>
        /// 仪器型号
        /// </summary>
        public string InstrumentName { get; set; }
        /// <summary>
        /// 仪器对应DLL名称
        /// </summary>
        public string InstrumentDll { get; set; }
        /// <summary>
        /// 体检项目编号 仪器所作的项目在体检系统内的项目编码
        /// </summary>
        public string GroupId { get; set; }
        #region Stock通信
        /// <summary>
        /// 本地电脑IP
        /// </summary>
        public string LocalIP { get; set; }

        /// <summary>
        /// 本地监听端口
        /// </summary>
        public string LocalPort { get; set; }

        /// <summary>
        /// 远程电脑IP
        /// </summary>
        public string ServerIP { get; set; }
        /// <summary>
        /// 远程监听端口
        /// </summary>
        public string ServerPort { get; set; }
        #endregion
        #region 串口通讯
        /// <summary>
        /// 串口号
        /// </summary>
        public string SerialPort { get; set; }
        /// <summary>
        /// 波特率
        /// </summary>
        public string BauRate { get; set; }
        /// <summary>
        /// 奇偶数据位
        /// </summary>
        public string DataBits { get; set; }
        /// <summary>
        /// 奇偶校验位
        /// </summary>
        public string Parity { get; set; }
        /// <summary>
        /// 停止位
        /// </summary>
        public string StopBit { get; set; }
        /// <summary>
        /// 是否使用UDP协议，使用1，不使用0
        /// </summary>
        public string IsUDP { get; set; }
        #endregion
        public int ResultUpdateTime { get; set; }
        #endregion

        /// <summary>
        /// 从配置文件加载配置
        /// </summary>
        private static void GetConfig()
        {
            instance.Conn = ConfigurationManager.AppSettings.Get("Conn").TrimOrNull();
            instance.SqlIP = ConfigurationManager.AppSettings.Get("SqlIP").TrimOrNull();
            instance.ConnectType = ConfigurationManager.AppSettings.Get("ConnectType").TrimOrNull();
            instance.InstrumentName = ConfigurationManager.AppSettings.Get("InstrumentName").TrimOrNull();
            instance.InstrumentDll = ConfigurationManager.AppSettings.Get("InstrumentDll").TrimOrNull();
            instance.GroupId = ConfigurationManager.AppSettings.Get("GroupId").TrimOrNull();
            instance.LocalIP = ConfigurationManager.AppSettings.Get("LocalIP").TrimOrNull();
            instance.LocalPort = ConfigurationManager.AppSettings.Get("LocalPort").TrimOrNull();
            instance.ServerIP = ConfigurationManager.AppSettings.Get("ServerIP").TrimOrNull();
            instance.ServerPort = ConfigurationManager.AppSettings.Get("ServerPort").TrimOrNull();
            instance.IsUDP = ConfigurationManager.AppSettings.Get("IsUDP").TrimOrNull();
            instance.SerialPort = ConfigurationManager.AppSettings.Get("SerialPort").TrimOrNull();
            instance.BauRate = ConfigurationManager.AppSettings.Get("BauRate").TrimOrNull();
            instance.DataBits = ConfigurationManager.AppSettings.Get("DataBits").TrimOrNull();
            instance.Parity = ConfigurationManager.AppSettings.Get("Parity").TrimOrNull();
            instance.StopBit = ConfigurationManager.AppSettings.Get("StopBit").TrimOrNull();
            int.TryParse(ConfigurationManager.AppSettings.Get("ResultUpdateTime"), out int Time);
            instance.ResultUpdateTime = Time;
        }
    }
}
