Namespace Conditions
    Public Class TestPropCondition(Of TObject, TPropertyType) : Implements ICondition(Of TObject)
        Private _validateFunc As Func(Of TPropertyType, Boolean)

        Public Function Validate(ByVal obj As TObject, ByVal validatedProperty As ForvalidateProperty) As ForvalidateResult _
            Implements ICondition(Of TObject).Validate
            If _validateFunc(validatedProperty.Value) Then
                Return New ForvalidateResult
            Else
                Return New ForvalidateResult(validatedProperty.Name, "Właściwość $propertyName$ jest nieprawidłowa.")
            End If
        End Function

        Public Sub New(ByVal func As Func(Of TPropertyType, Boolean))
            _validateFunc = func
        End Sub

    End Class
End Namespace