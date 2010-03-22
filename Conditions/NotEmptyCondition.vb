Namespace Conditions

    Public Class NotEmptyCondition(Of TObject) : Implements ICondition(Of TObject)

        Public Function Validate(ByVal obj As TObject, ByVal validatedProperty As ForvalidateProperty) As ForvalidateResult _
            Implements ICondition(Of TObject).Validate
            If validatedProperty.Value = Nothing Then
                Return New ForvalidateResult(validatedProperty.Name, "Właściwość $propertyName$ nie może być pusta ani ustawiona na wartość domyślną.")
            Else
                Return New ForvalidateResult
            End If
        End Function

    End Class
End Namespace