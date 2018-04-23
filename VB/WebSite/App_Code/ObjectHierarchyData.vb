Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports DevExpress.Xpo
Imports DevExpress.Xpo.Metadata
Imports DevExpress.Data.Filtering

''' <summary>
''' Summary description for ObjectHierarchyData
''' </summary>
Public Class ObjectHierarchyData(Of T)
	Implements IHierarchyData
	Private session As Session

	Private obj As T
	Private parentProp As String
	Private childrenProp As String

	Private classInfo As XPClassInfo

	Public Sub New(ByVal session As Session, ByVal obj As T, ByVal parentProp As String, ByVal childrenProp As String)
		Me.session = session

		Me.obj = obj
		Me.parentProp = parentProp
		Me.childrenProp = childrenProp

		classInfo = session.GetClassInfo(GetType(T))
	End Sub

	Public Function GetChildren() As IHierarchicalEnumerable Implements IHierarchyData.GetChildren
		Dim memberInfo As XPMemberInfo = classInfo.GetMember(childrenProp)
		Dim children As XPCollection(Of T) = CType(memberInfo.GetValue(obj), XPCollection(Of T))
		' without a sotring the order of objects might differ 
		children.Sorting.Add(New SortProperty(classInfo.KeyProperty.Name, DevExpress.Xpo.DB.SortingDirection.Ascending))

		Return New ObjectCollection(Of T)(children, parentProp, childrenProp)
	End Function

	Public Function GetParent() As IHierarchyData Implements IHierarchyData.GetParent
		Dim memberInfo As XPMemberInfo = classInfo.GetMember(parentProp)
		Dim parent As T = CType(memberInfo.GetValue(obj), T)

		If parent Is Nothing Then
			Return Nothing
		End If

		Dim hierarchyData As New ObjectHierarchyData(Of T)(session, parent, parentProp, childrenProp)

		Return TryCast(hierarchyData, IHierarchyData)
	End Function

	Public ReadOnly Property HasChildren() As Boolean Implements IHierarchyData.HasChildren
		Get
			Dim memberInfo As XPMemberInfo = classInfo.GetMember(childrenProp)
			Dim children As XPCollection(Of T) = CType(memberInfo.GetValue(obj), XPCollection(Of T))

			Return (children.Count > 0)
		End Get
	End Property

	Public ReadOnly Property Item() As Object Implements IHierarchyData.Item
		Get
			Return obj
		End Get
	End Property

	Public ReadOnly Property Path() As String Implements IHierarchyData.Path
		Get
			Dim key As Object = session.GetKeyValue(obj)

			Return key.ToString()
		End Get
	End Property

	Public ReadOnly Property Type() As String Implements IHierarchyData.Type
		Get
			Return classInfo.FullName
		End Get
	End Property
End Class