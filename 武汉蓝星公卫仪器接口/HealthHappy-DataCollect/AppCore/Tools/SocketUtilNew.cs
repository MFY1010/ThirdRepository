using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace AppCore.Tools
{
    /// <summary>
    /// 网口开发辅助类
    /// </summary>
    public class SocketUtilNew
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public bool IsUdp;
        public bool isopen { get; set; } = false;
        Socket socket;
        Socket socketClient;
        Socket recSocket;
        Thread tha;

        public SocketUtilNew(string host, int port, bool isudp)
        {
            Port = port;
            Host = host;
            IsUdp = isudp;
            isopen = false;

        }

        public void Poll()
        {
            if (socket != null)
            {
                if (socket.Poll(1000, SelectMode.SelectRead))
                {
                    isopen = false;
                }
            }
        }

        public void CloseSoket()
        {
            try
            {
                if (isopen)
                {
                    socket.Close();
                    tha.Abort();
                    isopen = false;

                }

            }
            catch (Exception ex)
            {
                BaseDeal.LogError("\r\n" + ex.Message);
                throw;
            }

        }

        /// <summary>
        /// 创建服务器
        /// </summary>
        public void CreateSocket_Servre()
        {
            try
            {
                IPEndPoint ipep = null;
                if (IsUdp)
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    if (Host == "")
                    {
                        ipep = new IPEndPoint(IPAddress.Any, Port);
                    }
                    else
                    {
                        ipep = new IPEndPoint(IPAddress.Parse(Host), Port);
                    }
                }
                else
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    if (Host == "")
                    {
                        ipep = new IPEndPoint(IPAddress.Any, Port);

                    }
                    else
                    {
                        ipep = new IPEndPoint(IPAddress.Parse(Host), Port);
                    }
                }

                socket.Bind(ipep);
                socket.Listen(Port);
                tha = new Thread(Listen);
                tha.IsBackground = true;
                tha.Start(socket);

                isopen = true;
            }
            catch (Exception ex)
            {
                BaseDeal.LogError("\r\n" + ex.Message);
                throw;
            }
        }

        /// <summary>
        /// 创建客户端
        /// </summary>
        public void CreateSocket_Client()
        {
            try
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(Host), Port);
                if (IsUdp)
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                }
                else
                {
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
                socket.Connect(ipep);//尝试连接

                tha = new Thread(ReceiveData);
                tha.IsBackground = true;
                tha.Start(socket);
                isopen = true;
            }
            catch (Exception ex)
            {
                BaseDeal.LogError("\r\n" + ex.Message);
                throw;
            }
        }

        public void ReceiveData(object o)
        {
            recSocket = o as Socket;

            try
            {
                byte[] result = new byte[1024 * 1024 * 2];
                string reciveMessage = "";
                while (true)
                {
                    int receiveNumber = recSocket.Receive(result);
                    if (receiveNumber == 0)
                    {
                        //errerTime++;
                        break;
                    }
                    else
                    {
                        //获得接收数据
                        reciveMessage = Encoding.Default.GetString(result, 0, receiveNumber);
                        string s = DataReceived(new DataReceivedEventArgs(reciveMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                BaseDeal.LogError("\r\n" + ex.Message);
                throw;
            }

        }

        void Listen(object o)
        {
            try
            {

                Socket socket = o as Socket;
                while (true)
                {
                    socketClient = socket.Accept();
                    Thread th = new Thread(ReceiveData);
                    th.IsBackground = true;
                    th.Start(socketClient);
                }
            }
            catch (Exception ex)
            {
                BaseDeal.LogError("\r\n" + ex.Message);
                throw;
            }

        }

        public void SendData(string data)
        {
            byte[] byteArray = System.Text.Encoding.Default.GetBytes(data);
            if (Host == "")
                socketClient.Send(byteArray, byteArray.Length, SocketFlags.None);
            else
                recSocket.Send(byteArray, byteArray.Length, SocketFlags.None);
        }

        /// <summary>
        /// 完整协议的记录处理事件
        /// </summary>
        public event DataReceivedEventHandler DataReceived;

        public delegate string DataReceivedEventHandler(DataReceivedEventArgs e);
    }
}
