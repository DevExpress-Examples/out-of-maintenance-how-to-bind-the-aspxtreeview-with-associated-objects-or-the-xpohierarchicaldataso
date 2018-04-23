Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering

''' <summary>
''' Summary description for XpoHierarchicalDataSourceView
''' </summary>
Public Class XpoHierarchicalDataSourceView(Of T)
	Inherits HierarchicalDataSourceView
	Private viewPath As String

	Private session As Session
	Private parentProp As String
	Private childrenProp As String

	Public Sub New(ByVal session As Session, ByVal viewPath As String, ByVal parentProp As String, ByVal childrenProp As String)
		Me.session = session
		Me.viewPath = viewPath
		Me.parentProp = parentProp
		Me.childrenProp = childrenProp
	End Sub

	Public Overrides Function [Select]() As IHierarchicalEnumerable
		Dim collection As New XPCollection(Of T)(session)
		collection.Criteria = New NullOperator(New OperandProperty(parentProp))
		' without a sotring the order of objects might differ 
		collection.Sorting.Add(New SortProperty(session.GetClassInfo(GetType(T)).KeyProperty.Name, DevExpress.Xpo.DB.SortingDirection.Ascending))

		Return New ObjectCollection(Of T)(collection, parentProp, childrenProp)
	End Function
End Class