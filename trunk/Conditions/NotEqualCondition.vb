Namespace Conditions
    Public Class NotEqualCondition(Of TObject) : Implements ICondition(Of TObject)
        Private _equalValue As Object

        Public Function Validate(ByVal obj As TObject, ByVal validatedProperty As ForvalidateProperty) As ValidationResult _
            Implements ICondition(Of TObject).Validate
            If _equalValue = validatedProperty.Value Then
                Return New ValidationResult(validatedProperty.Name, String.Format("Właściwość $propertyName$ musi być różna od '{0}'.", _equalValue))
            Else
                Return New ValidationResult
            End If
        End Function

        Public Sub New(ByVal value As Object)
            _equalValue = value
        End Sub

    End Class
End Namespace