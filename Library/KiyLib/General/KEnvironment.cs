using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.General
{
    /// <summary>
    /// Environment(환경)정보에 관련된 클래스
    /// </summary>
    public class KEnvironment
    {
        /// <summary>
        /// 현재 OS가 64bit인지 알아낸다
        /// </summary>
        public static bool Is64Bit
        {
            get { return System.Environment.Is64BitOperatingSystem; }
            private set { }
        }

        /// <summary>
        /// 현재 Debug모드인지 알아낸다
        /// </summary>
        public static bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
            private set { }
        }
    }
}
