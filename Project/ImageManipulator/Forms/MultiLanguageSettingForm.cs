using ImageManipulator.Util;
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
    public partial class MultiLanguageSettingForm : Form
    {
        public MultiLanguageSettingForm()
        {
            InitializeComponent();
        }

        private void MultiLanguageSettingForm_Load(object sender, EventArgs e)
        {
            Status.Instance().LogManager.AddLogMessage("MultiLanguage Form Open", "");
            this.Text = LangResource.SetLanguage;
            lblLanguageMessage.Text = LangResource.LanguageMessage;
            btnApply.Text = LangResource.Apply;
            btnCancel.Text = LangResource.Cancel;

            int selectedIndex = 0;
            for (int i = 0; i < Enum.GetNames(typeof(eLanguageType)).Length; i++)
            {
                eLanguageType filtertype = (eLanguageType)i;
                cbxLanguage.Items.Add(filtertype.ToString());
                if (Status.Instance().Language == filtertype)
                    selectedIndex = i;
            }
            cbxLanguage.SelectedIndex = selectedIndex;

            this.MinimumSize = new Size(201, 130);
            this.MaximumSize = new Size(201, 130);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Status.Instance().Language = (eLanguageType)cbxLanguage.SelectedIndex;
            Status.Instance().SaveSettings();

            MessageBox.Show(LangResource.RestartMessage, LangResource.Complete, MessageBoxButtons.OK);

            Status.Instance().LogManager.AddLogMessage("Apply Language", ((eLanguageType)cbxLanguage.SelectedIndex).ToString());

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
