Public Interface IValidator
    Function Validate(ByVal obj As Object) As ValidationResult
    Function Validate(ByVal obj As Object, ByVal propertyName As String) As ValidationResult
End Interface
