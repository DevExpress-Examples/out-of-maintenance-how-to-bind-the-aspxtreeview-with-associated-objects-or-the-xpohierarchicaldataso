Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports DevExpress.Xpo

''' <summary>
''' Summary description for XpoHierarchicalDataSource
''' </summary>
Public Class XpoHierarchicalDataSource(Of T)
	Implements IHierarchicalDataSource
	Private privateSession As Session
	Public Property Session() As Session
		Get
			Return privateSession
		End Get
		Set(ByVal value As Session)
			privateSession = value
		End Set
	End Property

	Private privateParentProperty As String
	Public Property ParentProperty() As String
		Get
			Return privateParentProperty
		End Get
		Set(ByVal value As String)
			privateParentProperty = value
		End Set
	End Property
	Private privateChildrenProperty As String
	Public Property ChildrenProperty() As String
		Get
			Return privateChildrenProperty
		End Get
		Set(ByVal value As String)
			privateChildrenProperty = value
		End Set
	End Property

	Private view As XpoHierarchicalDataSourceView(Of T)

	Public Sub New()
	End Sub

	Public Event DataSourceChanged As EventHandler Implements IHierarchicalDataSource.DataSourceChanged

	Public Function GetHierarchicalView(ByVal viewPath As String) As HierarchicalDataSourceView Implements IHierarchicalDataSource.GetHierarchicalView
		If view Is Nothing Then
			view = New XpoHierarchicalDataSourceView(Of T)(Session, viewPath, ParentProperty, ChildrenProperty)
		End If

		Return view
	End Function
End Class