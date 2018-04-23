using System;
using System.Collections.Generic;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Drawing;
using DevExpress.XtraGrid.Columns;

namespace WindowsApplication1
{
    public class LinesDrawHelper
    {

        private readonly GridView _View;
        public LinesDrawHelper(GridView view)
        {
            _View = view;
            view.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            view.GridControl.Paint += grid_Paint;
        }

        private GridViewInfo GetViewInfo()
        {
            GridViewInfo vi = _View.GetViewInfo() as GridViewInfo;
            return vi;
        }
        private int GetGridViewBottom()
        {
            Rectangle footerRect = GetViewInfo().ViewRects.Footer;
            return footerRect.IsEmpty ? _View.ViewRect.Bottom : footerRect.Top;
        }

        void grid_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            GridViewInfo vi = GetViewInfo();
            Pen pen = new Pen(_View.PaintAppearance.VertLine.BackColor);
            int lastRowIndex = vi.RowsInfo.GetLastVisibleRowIndex();
            foreach (GridRowInfo ri in vi.RowsInfo)
            {
                if (ri.IsGroupRow || ri.RowHandle == _View.FocusedRowHandle)
                    continue;
                foreach (GridColumn column in _View.VisibleColumns)
                {
                    Point p1 = new Point(vi.ColumnsInfo[column].Bounds.Right - 1, ri.Bounds.Y);
                    int bottom =  lastRowIndex == ri.RowHandle? GetGridViewBottom() : ri.Bounds.Bottom - 2;
                    Point p2 = new Point(vi.ColumnsInfo[column].Bounds.Right - 1, bottom);
                    e.Graphics.DrawLine(pen, p1, p2);
                }

            }
        }
    }
}
