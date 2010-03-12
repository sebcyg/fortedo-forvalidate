Namespace Conditions
    Public Class TestPropCondition(Of TObject, TPropertyType) : Implements ICondition(Of TObject)
        Private _validateFunc As Func(Of TPropertyType, Boolean)

        Public Function Validate(ByVal obj As TObject, ByVal propertyValue As Object, ByVal propertyName As String) As ValidationResult _
            Implements ICondition(Of TObject).Validate
            If _validateFunc(propertyValue) Then
                Return New ValidationResult
            Else
                Return New ValidationResult(propertyName, "Właściwość $propertyName$ jest nieprawidłowa.")
            End If
        End Function

        Public Sub New(ByVal func As Func(Of TPropertyType, Boolean))
            _validateFunc = func
        End Sub

    End Class
End Namespace