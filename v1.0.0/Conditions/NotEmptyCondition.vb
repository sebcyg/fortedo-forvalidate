Namespace Conditions

    Public Class NotEmptyCondition(Of TObject) : Implements ICondition(Of TObject)

        Public Function Validate(ByVal obj As TObject, ByVal propertyValue As Object, ByVal propertyName As String) As ValidationResult _
            Implements ICondition(Of TObject).Validate
            If propertyValue = Nothing Then
                Return New ValidationResult(propertyName, "Właściwość $propertyName$ nie może być pusta ani ustawiona na wartość domyślną.")
            Else
                Return New ValidationResult
            End If
        End Function

    End Class
End Namespace