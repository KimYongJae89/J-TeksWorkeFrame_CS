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
    public partial class ImageTagViewForm : Form
    {
        public ImageTagViewForm(Dictionary<string, string> tagCodeValPair) : this()
        {
            UpdateData(tagCodeValPair);
        }

        private ImageTagViewForm()
        {
            InitializeComponent();
        }

        public void UpdateData(Dictionary<string, string> tagCodeValPair)
        {
            gridTaginfo.Rows.Clear();

            foreach (var item in tagCodeValPair)
                gridTaginfo.Rows.Add(item.Key, item.Value);
        }

        private void ImageTagViewForm_Load(object sender, EventArgs e)
        {
            this.Text = LangResource.TagView;
        }
    }
}
