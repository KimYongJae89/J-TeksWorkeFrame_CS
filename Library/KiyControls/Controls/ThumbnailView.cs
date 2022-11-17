using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiyControls.Controls
{
    /// <summary>
    /// DataGridView를 기반으로 썸네일 표시 컨트롤
    /// </summary>
    public partial class ThumbnailView : UserControl
    {
        private int paddingOfEachCell = 20, cellWidth, cellHeight;


        /// <summary>
        /// 각 셀간의 간격
        /// </summary>
        public int PaddingOfEachCell
        {
            get { return paddingOfEachCell; }
            set { paddingOfEachCell = value; }
        }

        /// <summary>
        /// 각 셀의 가로 길이
        /// </summary>
        public int CellWidth
        {
            get { return cellWidth; }
            set { cellWidth = value; }
        }

        /// <summary>
        /// 각 셋의 세로 길이
        /// </summary>
        public int CellHeight
        {
            get { return cellHeight; }
            set { cellHeight = value; }
        }

        /// <summary>
        /// 선택된 Index
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                if (dgv.Rows.Count > 0 && dgv.Columns.Count > 0)
                    return dgv.CurrentCell.ColumnIndex;
                else
                    return -1;
            }
        }

        /// <summary>
        /// Columns의 개수
        /// </summary>
        public int NumberOfThumbnails
        {
            get
            {
                if (dgv.Rows.Count > 0 && dgv.Columns.Count > 0)
                    return dgv.ColumnCount;
                else
                    return 0;
            }
        }


        [Description("썸네일 선택이 변경될때 발생합니다"), Category("작업")]
        public event EventHandler SelectionChanged;

        /// <summary>
        /// 생성자
        /// </summary>
        public ThumbnailView()
        {
            InitializeComponent();
        }


        /// <summary>
        /// 썸네일로 표시할 이미지들을 넣는다
        /// </summary>
        /// <param name="bmpList"></param>
        public void Add(List<Bitmap> bmpList)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();

            Bitmap orgBmp, rszBmp;
            DataGridViewImageColumn col;

            int cnt = bmpList.Count;
            for (int i = 0; i < cnt; i++)
            {
                orgBmp = bmpList[i].Clone() as Bitmap;

                col = new DataGridViewImageColumn();
                dgv.Columns.Add(col);

                if (i == 0)
                    dgv.Rows.Add();

                rszBmp = new Bitmap(orgBmp, new Size(CellWidth, cellHeight));

                dgv.Rows[0].Cells[i].Value = rszBmp;
                dgv.Rows[0].Height = cellHeight;
                dgv.Columns[i].Width = cellWidth + paddingOfEachCell;
            }

            dgv.Invalidate();
        }

        /// <summary>
        /// 특정 썸네일을 제거한다
        /// </summary>
        /// <param name="index">제거할 썸네일의 Index</param>
        /// <returns>삭제 성공 여부</returns>
        public bool RemoveAt(int index)
        {
            if (dgv.Rows.Count == 0 && dgv.Columns.Count == 0)
                return false;

            if ((dgv.Columns.Count - 1) >= index)
            {
                dgv.Columns.RemoveAt(index);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// 모든 썸네일을 제거한다
        /// </summary>
        public void Clear()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
        }

        /// <summary>
        /// 특정 썸네일을 선택한다
        /// </summary>
        /// <param name="index">선택할 썸네일의 Index</param>
        public void Select(int index)
        {
            if (dgv.Rows.Count == 0 && dgv.Columns.Count == 0)
                return;

            if ((dgv.Columns.Count - 1) >= index)
                dgv.CurrentCell = dgv.Rows[0].Cells[index];
        }


        private void ThumbnailView_Load(object sender, EventArgs e)
        {
            dgv.SelectionChanged += (dgvS, dgvE)
                => SelectionChanged?.Invoke(this, e);

            dgv.Rows.Clear();
            dgv.Columns.Clear();
            CellWidth = CellHeight = this.Height - dgv.HorizontalScrollBarSize.Height;
        }
    }
}
