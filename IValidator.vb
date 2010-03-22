Public Interface IValidator
    Function Validate(ByVal obj As Object) As ForvalidateResult
    Function Validate(ByVal obj As Object, ByVal propertyName As String) As ForvalidateResult
End Interface
