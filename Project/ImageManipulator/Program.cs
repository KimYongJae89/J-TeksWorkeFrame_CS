using ImageManipulator.Util;
using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageManipulator
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // License Check
            //Status.Instance().StartCheckLicense();
            //
            Status.Instance().LoadSettings();

            eLanguageType type = Status.Instance().Language;
            CultureInfo cultureInfo;
            switch (type)
            {
                case eLanguageType.English:
                    cultureInfo = new CultureInfo("en-US");
                    LangResource.Culture = cultureInfo;
                    break;
                case eLanguageType.Korea:
                default:
                    cultureInfo = new CultureInfo("ko-KR");
                    LangResource.Culture = cultureInfo;
                    break;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ImageManipulator());
        }
    }
}
