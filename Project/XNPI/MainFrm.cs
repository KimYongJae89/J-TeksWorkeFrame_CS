using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XNPI.Properties;
using Language_XNPI.Properties;
using KiyControls.Controls;
using XNPI.Controls;
using KiyLib.General;

namespace XNPI
{
    public partial class MainFrm : Form
    {
        private XRay _xRayCtrl;
        private ETC _etcCtrl;
        private Device _deviceCtrl;
        private Info _infoCtrl;
        private LogView _logViewCtrl;
        private Tool _toolCtrl;
        private Snapshot _snapShotCtrl;
        private Cam _camCtrl;

        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            Initialize();
            AddControls();
        }

        private void MainFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            KLog.Inst.Close();
        }

        private void Initialize()
        {
            tblPnlOP.BackColor = Config.Inst.BackGroundClr;
            WindowState = FormWindowState.Maximized;

            LangResource.Culture = new CultureInfo("ko-KR");
            //LangResource.Culture = new CultureInfo("en-US");

            KLog.Inst.Initialize(KLogAppenderType.Console | KLogAppenderType.RollingFile);
        }

        // 64비트 UserControl 디자이너 오류 때문에 동적으로 추가
        private void AddControls()
        {
            _xRayCtrl = new XRay();
            _etcCtrl = new ETC();
            _deviceCtrl = new Device();
            _infoCtrl = new Info();
            _logViewCtrl = new LogView();
            _toolCtrl = new Tool();
            _snapShotCtrl = new Snapshot();
            _camCtrl = new Cam();

            _xRayCtrl.Dock = DockStyle.Left;
            _deviceCtrl.Dock = DockStyle.Fill;
            _etcCtrl.Dock = DockStyle.Fill;
            _infoCtrl.Dock = DockStyle.Fill;
            _logViewCtrl.Dock = DockStyle.Fill;
            _toolCtrl.Dock = DockStyle.Fill;
            _snapShotCtrl.Dock = DockStyle.Fill;
            _camCtrl.Dock = DockStyle.Fill;

            tblPnlOP.Controls.Add(_xRayCtrl, 0, 0);
            tblPnlEtcDevice.Controls.Add(_etcCtrl, 0, 0);
            tblPnlEtcDevice.Controls.Add(_deviceCtrl, 1, 0);
            tblPnlOP.Controls.Add(_infoCtrl, 0, 3);
            tblPnlOP.Controls.Add(_logViewCtrl, 0, 4);
            tblPnl.Controls.Add(_toolCtrl, 1, 0);
            tblPnl.Controls.Add(_camCtrl, 2, 0);
            pnlSnapshot.Controls.Add(_snapShotCtrl);
        }
    }
}
