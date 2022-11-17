using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiyControls.Controls
{
    /// <summary>
    /// Log를 표시하기 위한 컨트롤
    /// XNPI의 Log컨트롤로 사용하려고 했으나 취소돼, 실제 사용및 테스트를 하지 못했다
    /// </summary>
    public partial class LogView : UserControl
    {
        private StringBuilder _strBd;

        /// <summary>
        /// 생성자
        /// </summary>
        public LogView()
        {
            InitializeComponent();
        }

        private void LogView_Load(object sender, EventArgs e)
        {
            tbxLog.ForeColor = Color.FromArgb(192, 192, 192);

            _strBd = new StringBuilder();
        }


        /// <summary>
        /// Log로 사용할 문자열을 Log포맷형식에 맞게 변형한다
        /// </summary>
        /// <param name="logText">Log 내용</param>
        /// <param name="logCategory">Log 카테고리 (ex: SYS, ERR)</param>
        public void AppendLog(string logText, string logCategory = "")
        {
            tbxLog.AppendText(StrToLogConvert(logText, logCategory));

            ScrollMoveToBottom();
        }

        /// <summary>
        /// 현재 시간을 string형식([HH:mm:ss.fff])으로 가져온다
        /// </summary>
        /// <returns>결과 값</returns>
        private string GetCurrentTimeStr()
        {
            return DateTime.Now.ToString("[HH:mm:ss.fff]");
        }

        /// <summary>
        /// 스크롤바를 컨트롤의 맨밑으로 위치시킨다
        /// </summary>
        private void ScrollMoveToBottom()
        {
            tbxLog.SelectionStart = tbxLog.Text.Length;
            tbxLog.SelectionLength = 0;
            tbxLog.ScrollToCaret();
        }

        /// <summary>
        /// Log로 사용할 문자열을 Log포맷형식에 맞게 변형한다
        /// </summary>
        /// <param name="logText">Log 내용</param>
        /// <param name="logCategory">Log 카테고리 (ex: SYS, ERR)</param>
        /// <returns>Log 결과</returns>
        private string StrToLogConvert(string logText, string logCategory = "")
        {
            string rtLog = "";

            _strBd.Append(GetCurrentTimeStr());
            _strBd.Append(" - ");

            if (!string.IsNullOrEmpty(logCategory))
                _strBd.Append("[" + logCategory.ToUpper() + "] ");

            _strBd.Append(logText);

            rtLog = _strBd.ToString();
            _strBd.Clear();

            return rtLog;
        }
    }
}
