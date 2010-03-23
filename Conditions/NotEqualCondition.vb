Imports Fortedo.ForValidate

Namespace Conditions
    Public Class NotEqualCondition
        Inherits FvConditionBase

        Private _equalValue As Object

        Public Sub New(ByVal value As Object)
            MyBase.New()
            _equalValue = value
        End Sub

        Protected Overrides Function OnValidate(ByVal context As FvContext) As String
            If _equalValue = context.PropertyValue Then
                Return String.Format("Właściwość $propertyName$ musi być różna od '{0}'.", _equalValue)
            End If
            Return Nothing
        End Function
    End Class
End Namespace