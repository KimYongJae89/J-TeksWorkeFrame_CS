using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Appender;
using log4net.Layout;
using log4net.Repository.Hierarchy;

namespace KiyLib.General
{
    // 참고 사이트 - https://bigenergy.tistory.com/377

    /// <summary>
    /// Log를 남기기 위한 클래스
    /// 싱글턴패턴으로 구현되어 있으며, log4net.dll을 사용한다
    /// </summary>
    public class KLog
    {
        private static volatile KLog _uniqInstance;
        private static readonly object _syncRoot = new Object();
        private ILog _log;

        /// <summary>
        /// 클래스의 유일한 인스턴스
        /// </summary>
        public static KLog Inst
        {
            get
            {
                if (_uniqInstance == null)
                {
                    lock (_syncRoot)
                    {
                        if (_uniqInstance == null)
                            _uniqInstance = new KLog();
                    }
                }

                return _uniqInstance;
            }
        }


        private KLog() { }


        /// <summary>
        /// 초기화 함수
        /// 사용시 처음 한번은 반드시 호출해야 한다
        /// Form의 Load 이벤트에 사용하기를 권장
        /// </summary>
        /// <param name="appenderType">Log를 기록할 방식, 여러 옵션을 동시에 선택 가능하다</param>
        /// <param name="maxSizeRollBackups">Log파일의 최대 개수, appenderType에 RollingFile이 포함돼 있을때만 의미가 있다</param>
        /// <param name="maximumFileSize">각 Log파일의 최대 크기, appenderType에 RollingFile이 포함돼 있을때만 의미가 있다</param>
        public void Initialize(KLogAppenderType appenderType, int maxSizeRollBackups = 10, string maximumFileSize = "1MB")
        {
            // 솔루션 이름
            string slnName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            slnName = System.IO.Path.GetFileNameWithoutExtension(slnName);
            // 실행폴더 아래에 Log폴더
            string logPath = AppDomain.CurrentDomain.BaseDirectory + $"\\Log\\{slnName}.log";

            Hierarchy hierarchy = LogManager.GetRepository() as Hierarchy;
            hierarchy.Configured = true;

            // 로그 출력 포맷
            var logPattern = new PatternLayout("%date [%thread] [%-5level] - %message%newline");

            // RollingFileAppender - 파일 로그
            if (appenderType.HasFlag(KLogAppenderType.RollingFile))
            {
                var rollAppnd = new RollingFileAppender
                {
                    Name = "RollingFileLogger",
                    File = logPath,
                    AppendToFile = true,

                    StaticLogFileName = true,
                    CountDirection = 1,
                    MaxSizeRollBackups = maxSizeRollBackups,
                    MaximumFileSize = maximumFileSize,
                    RollingStyle = RollingFileAppender.RollingMode.Date,
                    LockingModel = new FileAppender.MinimalLock(),

                    // 날짜가 변경되면 이전 로그에 붙을 접미사
                    DatePattern = "_[yyyyMMdd]\".log\"",
                    Layout = logPattern
                };

                hierarchy.Root.AddAppender(rollAppnd);
                rollAppnd.ActivateOptions(); ;
            }

            if (appenderType.HasFlag(KLogAppenderType.Console))
            {
                var csAppnd = new ConsoleAppender()
                {
                    Name = "ConsoleLogger",
                    Layout = logPattern
                };

                hierarchy.Root.AddAppender(csAppnd);
                csAppnd.ActivateOptions(); ;
            }

            hierarchy.Root.Level = log4net.Core.Level.All;

            _log = LogManager.GetLogger("logger");
        }

        /// <summary>
        /// Log를 기록한다
        /// </summary>
        /// <param name="LogMsg">Log의 내용</param>
        /// <param name="type">Log의 타입</param>
        public void Add(string LogMsg, KLogType type = KLogType.DEBUG)
        {
            switch (type)
            {
                case KLogType.DEBUG:
                    _log.Debug(LogMsg);
                    break;
                case KLogType.ERR:
                    _log.Error(LogMsg);
                    break;
                case KLogType.FATAL:
                    _log.Fatal(LogMsg);
                    break;
                case KLogType.INFO:
                    _log.Info(LogMsg);
                    break;
                case KLogType.WARN:
                    _log.Warn(LogMsg);
                    break;
            }
        }

        /// <summary>
        /// Log기록을 중지한다
        /// Form의 FormClosed 이벤트에 사용하기를 권장
        /// </summary>
        public void Close()
        {
            LogManager.Shutdown();
        }
    }
}
