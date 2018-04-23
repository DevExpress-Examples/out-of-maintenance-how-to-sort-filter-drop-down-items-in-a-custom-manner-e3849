Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.IO
Imports System.Reflection
Imports System.Windows.Controls
Imports System.Xml.Serialization
Imports DevExpress.Xpf.Collections
Imports DevExpress.Xpf.PivotGrid
Imports DevExpress.XtraPivotGrid.Data

Namespace DXPivotGrid_CustomFilterItemsSorting
	Partial Public Class MainPage
		Inherits UserControl
		Private dataFileName As String = "nwind.xml"
		Public Sub New()
			InitializeComponent()

			' Parses an XML file and creates a collection of data items.
			Dim [assembly] As System.Reflection.Assembly = _
				System.Reflection.Assembly.GetExecutingAssembly()
			Dim stream As Stream = [assembly].GetManifestResourceStream(dataFileName)
			Dim s As New XmlSerializer(GetType(OrderData))
			Dim dataSource As Object = s.Deserialize(stream)

			' Binds a pivot grid to this collection.
			pivotGridControl1.DataSource = dataSource
		End Sub
		Private Sub pivotGridControl1_CustomFilterPopupItems(ByVal sender As Object, _
			ByVal e As PivotCustomFilterPopupItemsEventArgs)
			If rbCaptionLength.IsChecked = True Then
				ArrayList.Adapter(CType(e.Items, IList)).Sort(New FilterItemsComparer())
			End If
		End Sub
	End Class
	Public Class FilterItemsComparer
		Implements IComparer
		Private Function IComparer_Compare(ByVal x As Object, ByVal y As Object) As Integer _
			Implements IComparer.Compare
			If Not(TypeOf x Is PivotGridFilterItem) OrElse Not(TypeOf y Is PivotGridFilterItem) Then
				Return 0
			End If
			Dim item1 As PivotGridFilterItem = CType(x, PivotGridFilterItem)
			Dim item2 As PivotGridFilterItem = CType(y, PivotGridFilterItem)
			If item1.ToString().Length = item2.ToString().Length Then
				Return 0
			End If
			If item1.ToString().Length > item2.ToString().Length Then
				Return 1
			End If
			Return -1
		End Function
	End Class
End Namespace
