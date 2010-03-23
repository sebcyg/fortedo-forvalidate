Public Class FvNestedRule
    Inherits FvRuleBase

    Protected Overrides Function OnValidate(ByVal context As FvContext) As FvResult
        Dim nestedContext = context.CreateNestedContext()
        Dim result = Validator.Validate(nestedContext)
        Return result
    End Function

    Private _validator As FvValidatorBase
    Public Property Validator() As FvValidatorBase
        Get
            Return _validator
        End Get
        Set(ByVal value As FvValidatorBase)
            _validator = value
        End Set
    End Property

    Public Sub New(ByVal propertyName As String)
        MyBase.New(propertyName)
    End Sub
End Class
