Namespace Conditions


    Public Class LengthCondition(Of TObject) : Implements ICondition(Of TObject)
        Private _minLength As Int32
        Private _maxLength As Int32

        Public Function Validate(ByVal obj As TObject, ByVal validatedProperty As ForvalidateProperty) As ValidationResult _
            Implements ICondition(Of TObject).Validate
            Dim value = TryCast(validatedProperty.Value, String)
            If value Is Nothing Then
                Return New ValidationResult(validatedProperty.Name, "Właściwość $propertyName$ jest nieprawidłowa.", ValidationErrorSource.Rule)
            Else
                If value.Length >= _minLength And value.Length <= _maxLength Then
                    Return New ValidationResult
                Else
                    Return New ValidationResult(validatedProperty.Name, _
                                                String.Format("Właściwość $propertyName$ musi mieć długość {0}-{1} znaków.", _minLength, _maxLength), ValidationErrorSource.Rule)
                End If
            End If
        End Function

        Public Sub New(ByVal minLength As Int32, ByVal maxLength As Int32)
            _minLength = minLength
            _maxLength = maxLength
        End Sub

    End Class
End Namespace