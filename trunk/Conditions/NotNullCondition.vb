Namespace Conditions

    Public Class NotNullCondition(Of TObject) : Implements ICondition(Of TObject)

        Public Function Validate(ByVal obj As TObject, ByVal validatedProperty As ForvalidateProperty) As ValidationResult _
            Implements ICondition(Of TObject).Validate
            If validatedProperty.Value Is Nothing Then
                Return New ValidationResult(validatedProperty.Name, "Właściwość $propertyName$ nie może być pusta.")
            Else
                Return New ValidationResult
            End If
        End Function

    End Class
End Namespace