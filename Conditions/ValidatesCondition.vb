Namespace Conditions
    Public Class ValidatesCondition(Of TObject, TNestedObject) : Implements ICondition(Of TObject)
        Private _validator As ValidatorBase(Of TNestedObject)

        Public Function Validate(ByVal obj As TObject, ByVal validatedProperty As ForvalidateProperty) As ValidationResult _
            Implements ICondition(Of TObject).Validate
            Dim result = _validator.ValidateGeneric(validatedProperty.Value, Function(m) m.Replace("$propertyName$", "$parentPropertyName$.$propertyName$"))
            If Not result.IsValid Then
                For Each e In result.Errors
                    e.Message = e.Message.Replace("$parentPropertyName$", "$propertyName$")
                Next
            End If
            Return result
        End Function

        Public Sub New(ByVal validator As ValidatorBase(Of TNestedObject))
            _validator = validator
        End Sub

    End Class
End Namespace