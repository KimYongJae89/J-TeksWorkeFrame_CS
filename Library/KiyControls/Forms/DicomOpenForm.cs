using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiyControls.Forms
{
    /// <summary>
    /// dcm(dicom)이미지 포맷에 프레임이 여러장일때 사용된다
    /// 썸네일로 각각의 프레임 이미지를 보여주며, 특정 썸네일을 선택해 해당 이미지의 프레임 번호를 알아낼수 있다
    /// </summary>
    public partial class DicomOpenForm : Form
    {
        private bool _indexChangeMode;
        private List<Bitmap> _bmpList;

        /// <summary>
        /// 선택된 프레임의 Index
        /// </summary>
        public int SelectedFrameIndex { get { return thnailView.SelectedIndex; } }

        [Description("Select버튼이 눌렸을때 발생합니다"), Category("작업")]
        public event EventHandler SelectClicked;


        /// <summary>
        /// 생성자
        /// </summary>
        /// <param name="bmpList">썸네일로 표시할 Bmp의 List</param>
        public DicomOpenForm(List<Bitmap> bmpList)
        {
            InitializeComponent();
            this._bmpList = bmpList;
        }


        private void DicomOpenForm_Load(object sender, EventArgs e)
        {
            this.thnailView.SelectionChanged += ThnailView_SelectionChanged;

            if (_bmpList != null && _bmpList.Count > 0)
                AddThumbnails(_bmpList);
        }

        private void DicomOpenForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            SelectClicked?.Invoke(this, e);
        }

        private void nuUpDnIndex_ValueChanged(object sender, EventArgs e)
        {
            if (_indexChangeMode)
            {
                int idx = (int)nuUpDnIndex.Value;
                thnailView.Select(idx);
                nuUpDnIndex.Value = idx;
            }
        }

        private void ThnailView_SelectionChanged(object sender, EventArgs e)
        {
            _indexChangeMode = false;

            var obj = sender as Controls.ThumbnailView;
            var idx = obj.SelectedIndex;
            nuUpDnIndex.Value = idx;

            _indexChangeMode = true;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void AddThumbnails(List<Bitmap> bmpList)
        {
            int cnt = bmpList.Count;

            tbxNumberOfThumbnails.Text = bmpList.Count.ToString();
            nuUpDnIndex.Maximum = cnt - 1;

            thnailView.Add(bmpList);
        }
    }
}
