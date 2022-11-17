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
using XManager.ImageProcessingData;

namespace XManager.Forms
{
    public partial class ProcessingTimeForm : Form
    {
        public Action CloseEventDelegate; // 폼 종료 시
        public ProcessingTimeForm()
        {
            InitializeComponent();
        }

        public void ProcessingUpdate()
        {
            lbxProcessingTime.Items.Clear();
            foreach (ProcessingTime item in CStatus.Instance().ProcessingTime)
            {
                string message = "[" + item.Type + "] : " + item.Time + "ms";
                lbxProcessingTime.Items.Add(message);
            }
        }

        private void ProcessingTimeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CloseEventDelegate != null)
                CloseEventDelegate();
        }

        private void ProcessingTimeForm_Load(object sender, EventArgs e)
        {
            this.Text = LangResource.ProcessingTime;
        }
    }
}
