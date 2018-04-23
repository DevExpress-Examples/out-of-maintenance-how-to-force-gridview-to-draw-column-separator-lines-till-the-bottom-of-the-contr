Imports Microsoft.VisualBasic
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
			AddHandler view.GridControl.Paint, AddressOf grid_Paint
		End Sub

		Private Function GetViewInfo() As GridViewInfo
			Dim vi As GridViewInfo = TryCast(_View.GetViewInfo(), GridViewInfo)
			Return vi
		End Function
		Private Function GetGridViewBottom() As Integer
			Dim footerRect As Rectangle = GetViewInfo().ViewRects.Footer
			If footerRect.IsEmpty Then
				Return _View.ViewRect.Bottom
			Else
				Return footerRect.Top
			End If
		End Function

		Private Sub grid_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)
			Dim vi As GridViewInfo = GetViewInfo()
			Dim pen As New Pen(_View.PaintAppearance.VertLine.BackColor)
			Dim lastRowIndex As Integer = vi.RowsInfo.GetLastVisibleRowIndex()
			For Each ri As GridRowInfo In vi.RowsInfo
				If ri.IsGroupRow OrElse ri.RowHandle = _View.FocusedRowHandle Then
					Continue For
				End If
				For Each column As GridColumn In _View.VisibleColumns
					Dim p1 As New Point(vi.ColumnsInfo(column).Bounds.Right - 1, ri.Bounds.Y)
					Dim bottom As Integer
					If lastRowIndex = ri.RowHandle Then
						bottom = GetGridViewBottom()
					Else
						bottom = ri.Bounds.Bottom - 2
					End If
					Dim p2 As New Point(vi.ColumnsInfo(column).Bounds.Right - 1, bottom)
					e.Graphics.DrawLine(pen, p1, p2)
				Next column

			Next ri
		End Sub
	End Class
End Namespace
