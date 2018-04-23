Imports System
Imports System.Collections.Generic
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports System.Drawing
Imports DevExpress.XtraGrid.Columns

Namespace WindowsApplication1
	Public Class LinesDrawHelper

		Private ReadOnly _View As GridView
		Public Sub New(ByVal view As GridView)
			_View = view
			view.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False
			AddHandler view.GridControl.PaintEx, AddressOf GridControl_PaintEx
		End Sub

		Private Sub GridControl_PaintEx(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.PaintExEventArgs)
			Dim vi As GridViewInfo = GetViewInfo()
			Dim pen As Pen = e.Cache.GetPen(_View.PaintAppearance.VertLine.BackColor)
			Dim lastRowIndex As Integer = vi.RowsInfo.GetLastVisibleRowIndex()
			For Each ri As GridRowInfo In vi.RowsInfo
				If ri.IsGroupRow OrElse ri.RowHandle = _View.FocusedRowHandle Then
					Continue For
				End If
				For Each column As GridColumn In _View.VisibleColumns
					Dim p1 As New Point(vi.ColumnsInfo(column).Bounds.Right - 1, ri.Bounds.Y)
					Dim bottom As Integer = If(lastRowIndex = ri.RowHandle, GetGridViewBottom(), ri.Bounds.Bottom - 2)
					Dim p2 As New Point(vi.ColumnsInfo(column).Bounds.Right - 1, bottom)
					e.Cache.DrawLine(pen, p1, p2)
				Next column

			Next ri
		End Sub

		Private Function GetViewInfo() As GridViewInfo
			Dim vi As GridViewInfo = TryCast(_View.GetViewInfo(), GridViewInfo)
			Return vi
		End Function
		Private Function GetGridViewBottom() As Integer
			Dim footerRect As Rectangle = GetViewInfo().ViewRects.Footer
			Return If(footerRect.IsEmpty, _View.ViewRect.Bottom, footerRect.Top)
		End Function
	End Class
End Namespace
