using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.Communication
{
    /// <summary>
    /// Modbus(모드버스) 통신을 위한 클래스
    /// 현재 미구현 상태
    /// </summary>
    public class KModbus
    {
        private static volatile KModbus uniqInstance;
        private static object syncRoot = new Object();
        private TcpClient tcp;

        private int port;
        public string IP { get; set; }

        public int Port
        {
            get { return port; }
            private set { port = value; }
        }

        private KModbus()
        {
            port = 502;
        }

        public static KModbus Instance
        {
            get
            {
                if (uniqInstance == null)
                {
                    lock (syncRoot)
                    {
                        if (uniqInstance == null)
                            uniqInstance = new KModbus();
                    }
                }

                return uniqInstance;
            }
        }

        public bool Connect(string ip)
        {
            if (tcp != null &&
                tcp.Connected)
            {
                //tcp?.Close();
                throw new Exception("이미 연결된 상태입니다.");
            }


            tcp = new TcpClient(ip, Port);

            return tcp.Connected;
        }

        public void Disconnect()
        {
            tcp?.Close();
        }
    }
}
