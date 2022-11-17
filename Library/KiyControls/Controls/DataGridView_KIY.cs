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
    /// 썸네일 표시에 사용하기 위해 DataGridView를 확장시킨 컨트롤
    /// </summary>
    public partial class DataGridView_KIY : DataGridView
    {
        //private readonly int CAPTIONHEIGHT = 21;
        //private readonly int BORDERWIDTH = 2;

        /// <summary>
        /// 수평 스크롤바의 크기
        /// </summary>
        public Size HorizontalScrollBarSize { get { return HorizontalScrollBar.Size; } }

        /// <summary>
        /// 수직 스크롤바의 크기
        /// </summary>
        public Size VerticalScrollBarSize { get { return VerticalScrollBar.Size; } }


        /// <summary>
        /// 생성자
        /// </summary>
        public DataGridView_KIY()
        {
            InitializeComponent();

            HorizontalScrollBar.Visible = true;
            HorizontalScrollBar.VisibleChanged += new EventHandler(ShowScrollBars);
        }


        /// <summary>
        /// 수평 스크롤바의 Visible값이 바뀔때마다 호출
        /// 수평 스크롤바의 크기및 위치를 설정한다
        /// </summary>
        /// <param name="sender">VisibleChanged의 sender</param>
        /// <param name="e">VisibleChanged의 e</param>
        private void ShowScrollBars(object sender, EventArgs e)
        {
            if (!HorizontalScrollBar.Visible)
            {
                int width = this.ClientRectangle.Width;
                int height = this.HorizontalScrollBar.Height;

                HorizontalScrollBar.Location = 
                    new Point(0, ClientRectangle.Height - height);

                HorizontalScrollBar.Size = new Size(width, height);
                HorizontalScrollBar.Show();
            }

            //if (!VerticalScrollBar.Visible)
            //{
            //    int width = VerticalScrollBar.Width;

            //    VerticalScrollBar.Location =
            //    new Point(ClientRectangle.Width - width - BORDERWIDTH, CAPTIONHEIGHT);

            //    VerticalScrollBar.Size =
            //    new Size(width, ClientRectangle.Height - CAPTIONHEIGHT - BORDERWIDTH);

            //    VerticalScrollBar.Show();
            //}
        }
    }
}
