using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KiyLib.DIP
{
    /// <summary>
    /// Display구현을 위한 인터페이스
    /// 현재는 사용되지 않는다
    /// </summary>
    public interface IKDisplay
    {
        void RenderDisplay(object sender, PaintEventArgs e);

        void OnMouseDown(object sender, MouseEventArgs e);
        void OnMouseUp(object sender, MouseEventArgs e);
        void OnMouseMove(object sender, MouseEventArgs e);
        void OnMouseWheel(object sender, MouseEventArgs e);

        void OnKeyDown(object sender, KeyEventArgs e);
        void OnKeyUp(object sender, KeyEventArgs e);
    }

    /// <summary>
    /// 태그 구현을 위한 인터페이스
    /// 현재는 사용되지 않는다
    /// </summary>
    public interface ITagInfos
    {
        Dictionary<string, string> GetTagInfos();
    }
}
