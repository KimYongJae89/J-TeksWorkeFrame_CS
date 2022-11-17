using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using XManager.Forms;
using XManager.Util;

namespace XManager
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createNew;
            Mutex mutex = new Mutex(true, "XManager", out createNew);
            if (!createNew)
            {
                MessageBox.Show("Already Executed Program!");
                return;
            }

            try
            {
                string[] strArg = Environment.GetCommandLineArgs();

                if (strArg.Length > 1)
                {
                    if(strArg[1] == "1")
                        CStatus.AdministratorMode = true;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //잠시 잠금
                Application.Run(new BootUp());

                MainForm main = new MainForm();

                eLanguageType type = CStatus.Instance().Settings.Language;
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
               // CStatus.Instance().Settings.Load();
                Application.Run(main);
            }
            catch (Exception err)
            {
                CStatus.Instance().ErrorLogManager.WriteErrorLog(MethodBase.GetCurrentMethod().Name, err.Message);
            }
        }
    }
}
