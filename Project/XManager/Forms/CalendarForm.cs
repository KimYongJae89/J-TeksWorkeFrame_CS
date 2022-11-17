using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XManager.Forms
{
    public partial class CalendarForm : Form
    {
        public CalendarForm()
        {
            InitializeComponent();
        }

        private void CaldendarForm_Load(object sender, EventArgs e)
        {
            monthCalendar1.SelectionStart = DateTime.Now.AddDays(-3);
            monthCalendar1.SelectionEnd = DateTime.Now;

        }

        private void btnSelected_Click(object sender, EventArgs e)
        {
            //CStatus.Instance().CalendarStart = lblStartDate.Text.Replace("-", "");
            //CStatus.Instance().CalendarEnd = lblEndDate.Text.Replace("-", "");
            CallFormClose();
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            CallFormClose();
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            lblStartDate.Text = monthCalendar1.SelectionStart.ToShortDateString();
            lblEndDate.Text = monthCalendar1.SelectionEnd.ToShortDateString();
        }

        private delegate void callback();
        private void CallFormClose()
        {
            if (this.InvokeRequired)
            {
                callback call = CallFormClose;
                Invoke(call);
                return;
            }
            this.Close();
        }
    }
}
