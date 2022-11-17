using LibraryGlobalization.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageManipulator.Forms
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            Assembly assemObj = Assembly.GetExecutingAssembly();
            Version version = assemObj.GetName().Version; // 현재 실행되는 어셈블리..dll의 버전 가져오기

            string strMessage = "ImageManipulator\n" +"Version : " + version.ToString() + "\n" + GetCopyright() + "\n";
            lblInformation.Text = strMessage;

            this.Text = LangResource.AboutImageManipulator;
            btnClose.Text = LangResource.Ok;

            this.MinimumSize = new Size(506, 229);
            this.MaximumSize = new Size(506, 229);
        }

        private string GetCopyright()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            object[] obj = asm.GetCustomAttributes(false);
            foreach (object o in obj)
            {
                if (o.GetType() == typeof(System.Reflection.AssemblyCopyrightAttribute))
                {
                    AssemblyCopyrightAttribute aca = (AssemblyCopyrightAttribute)o;
                    return aca.Copyright;
                }
            }
            return string.Empty;
        }
    }
}
