Namespace Conditions
    Public Class EnsureCondition(Of TPropertyType)
        Inherits FvConditionBase

        Private _validateFunc As Func(Of TPropertyType, Boolean)

        Public Sub New(ByVal func As Func(Of TPropertyType, Boolean))
            _validateFunc = func
        End Sub

        Protected Overrides Function OnValidate(ByVal context As FvContext) As String
            If _validateFunc(context.PropertyValue) Then
                Return Nothing
            Else
                Return "Właściwość $propertyName$ jest nieprawidłowa."
            End If
        End Function
    End Class
End Namespace