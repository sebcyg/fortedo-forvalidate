Namespace Conditions
    Public Interface ICondition(Of TObject)
        Function Validate(ByVal obj As TObject, ByVal propertyValue As Object, ByVal propertyName As String) As ValidationResult
    End Interface
End Namespace