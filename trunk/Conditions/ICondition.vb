Namespace Conditions
    Public Interface ICondition(Of TObject)
        Function Validate(ByVal obj As TObject, ByVal validatedProperty As ForvalidateProperty) As ValidationResult
    End Interface
End Namespace