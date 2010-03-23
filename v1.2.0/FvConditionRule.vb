Public Class FvConditionRule
    Inherits FvRuleBase

    Private _conditions As New List(Of FvConditionBase)
    Public ReadOnly Property Conditions() As List(Of FvConditionBase)
        Get
            Return _conditions
        End Get
    End Property

    Protected Overrides Function OnValidate(ByVal context As FvContext) As FvResult
        Dim result As New FvResult
        For Each condition In Conditions
            Dim message = condition.Validate(context)
            If message IsNot Nothing Then
                result.Errors.Add(New FvError(message, context.PropertyChain))
            End If
        Next
        Return result
    End Function

    Public Sub New(ByVal propertyName As String)
        MyBase.New(propertyName)
    End Sub
End Class
