Imports Microsoft.VisualBasic
Imports System
Imports DevExpress.Xpo



Public Class MyObject
	Inherits XPObject
	Public Sub New()
		MyBase.New()
	End Sub

	Public Sub New(ByVal session As Session)
		MyBase.New(session)
	End Sub

	Public Overrides Sub AfterConstruction()
		MyBase.AfterConstruction()
	End Sub

	Private _Parent As MyObject
	<Association("Parent-Children")> _
	Public Property Parent() As MyObject
		Get
			Return _Parent
		End Get
		Set(ByVal value As MyObject)
			SetPropertyValue("Parent", _Parent, value)
		End Set
	End Property

	Protected text_Renamed As String
	Public Property Text() As String
		Get
			Return text_Renamed
		End Get
		Set(ByVal value As String)
			SetPropertyValue(Of String)("Title", text_Renamed, value)
		End Set
	End Property

	<Association("Parent-Children")> _
	Public ReadOnly Property Children() As XPCollection(Of MyObject)
		Get
			Return GetCollection(Of MyObject)("Children")
		End Get
	End Property

	'public override string ToString() {
	'    return Title;
	'}
End Class

