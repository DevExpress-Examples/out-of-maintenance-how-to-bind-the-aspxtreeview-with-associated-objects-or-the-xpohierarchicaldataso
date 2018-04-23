Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports DevExpress.Web.ASPxTreeView
Imports System.Collections.Generic
Imports DevExpress.Web.ASPxClasses
Imports DevExpress.Xpo

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Private session As Session = XpoHelper.GetNewSession()

	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim datasource As New XpoHierarchicalDataSource(Of MyObject)() With {.Session = session, .ParentProperty = "Parent", .ChildrenProperty = "Children"}

		treeView.DataSource = datasource
		treeView.DataBind()
	End Sub


End Class




