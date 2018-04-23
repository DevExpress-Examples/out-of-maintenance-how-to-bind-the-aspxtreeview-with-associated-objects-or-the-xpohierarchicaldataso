Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports DevExpress.Xpo
Imports System.Collections

''' <summary>
''' Summary description for ObjectCollction
''' </summary>
Public Class ObjectCollection(Of T)
	Implements IHierarchicalEnumerable
	Private children As XPCollection(Of T)
	Private parentProp As String
	Private childrenProp As String

	Public Sub New(ByVal children As XPCollection(Of T), ByVal parentProp As String, ByVal childrenProp As String)
		Me.children = children
		Me.parentProp = parentProp
		Me.childrenProp = childrenProp
	End Sub

	Public Function GetHierarchyData(ByVal enumeratedItem As Object) As IHierarchyData Implements IHierarchicalEnumerable.GetHierarchyData
		Dim obj As New ObjectHierarchyData(Of T)(children.Session, CType(enumeratedItem, T), parentProp, childrenProp)

		Return TryCast(obj, IHierarchyData)
	End Function

	Public Function GetEnumerator() As IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
		Return (TryCast(children, IEnumerable(Of T))).GetEnumerator()
	End Function
End Class