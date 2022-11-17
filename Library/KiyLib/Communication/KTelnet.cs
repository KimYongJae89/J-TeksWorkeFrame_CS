using KiyLib.General;
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
    /// Telnet통신을 위한 클래스
    /// 현재 미구현 상태
    /// </summary>
    public class KTelnet : KTcpBase
    {
        public KTelnet()
        {
            this.port = 23;
        }

        public void Connect(string ip)
        {
            this.Connect(ip, this.port);
        }
    }
}
