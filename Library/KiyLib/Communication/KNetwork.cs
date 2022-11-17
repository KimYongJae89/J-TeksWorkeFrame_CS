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
    /// 네트워크 통신에 자주 사용되는 기능들을 위한 클래스
    /// </summary>
    public class KNetwork
    {
        /// <summary>
        /// 로컬 IP주소를 얻어온다
        /// </summary>
        /// <returns>결과 값</returns>
        public static IPAddress[] GetLocalIPAddress()
        {
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
                return null;

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            var addrList = host.AddressList;

            var localIPList =
                from ip in addrList
                where ip.AddressFamily == AddressFamily.InterNetwork
                select ip;

            return localIPList.ToArray();
        }
    }
}
