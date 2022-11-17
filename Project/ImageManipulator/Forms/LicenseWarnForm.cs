using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageManipulator.Forms
{
    public partial class LicenseWarnForm : Form
    {
        public LicenseWarnForm()
        {
   //         License not found.
   //please verify that the dongle is installed properly.
             InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void LicenseWarnForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void LicenseWarnForm_Load(object sender, EventArgs e)
        {
            this.Text = LangResource.Error;
            lblWarningMessage.Text = LangResource.LicenseError1 + "\n" + LangResource.LicenseError2;
            btnOk.Text = LangResource.Ok;

            Status.Instance().LogManager.AddLogMessage("License", "Error");
        }
    }
}
