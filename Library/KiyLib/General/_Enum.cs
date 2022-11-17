using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiyLib.General
{
    // Log
    /// <summary>
    /// Log를 기록할 방식, 여러 옵션을 동시에 선택 가능하다
    /// </summary>
    [Flags]
    public enum KLogAppenderType
    {
        Console = 1, RollingFile = 2
    }

    /// <summary>
    /// Log의 타입
    /// </summary>
    public enum KLogType
    {
        DEBUG, ERR, FATAL, INFO, WARN
    }
}