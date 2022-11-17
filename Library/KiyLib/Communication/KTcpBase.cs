using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.Communication
{
    /// <summary>
    /// Tcp 통신의 상위 클래스
    /// </summary>
    public class KTcpBase : IKNetwork, IDisposable
    {
        protected TcpClient tcp;
        protected int port;
        protected string ip;

        /// <summary>
        /// 네트워크의 연결 여부
        /// </summary>
        public bool Connected
        {
            get { return tcp.Connected; }
            private set { }
        }


        public KTcpBase()
        {
            tcp = new TcpClient();
        }


        /// <summary>
        /// TCP통신을 연결한다
        /// </summary>
        /// <param name="ip">대상 IP 주소</param>
        /// <param name="port">포트 번호</param>
        public void Connect(string ip, int port)
        {
            try
            {
                if (tcp.Connected)
                    throw new Exception("이미 연결된 상태입니다.");

                tcp.Connect(new IPEndPoint(IPAddress.Parse(ip), port));
            }
            catch (SocketException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 연결을 해제한다
        /// </summary>
        public void Disconnect()
        {
            tcp.Close();
        }

        /// <summary>
        /// 메시지를 보낸다
        /// </summary>
        /// <param name="msg">메시지 내용</param>
        public virtual void Send(string msg)
        {
            if (tcp == null)
                throw new Exception("tcp 객체가 null 입니다.");
            if (!tcp.Connected)
                throw new Exception("tcp객체가 연결되지 않았습니다.");

            byte[] sendBuf = Encoding.ASCII.GetBytes(msg);

            NetworkStream stream = tcp.GetStream();
            stream.Write(sendBuf, 0, sendBuf.Length);
        }


        // IDisposable
        /// <summary>
        /// 사용된 리소스를 해제한다
        /// </summary>
        public void Dispose()
        {
            if (tcp != null)
            {
                if (tcp.Connected)
                    tcp.Close();
            }
        }
    }
}
