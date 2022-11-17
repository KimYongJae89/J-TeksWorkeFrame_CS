using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.Communication
{
    /// <summary>
    /// 네트워크 통신을 위한 인터페이스
    /// </summary>
    public interface IKNetwork
    {
        void Connect(string ip, int port);
        void Disconnect();
        void Send(string msg);
    }
}
