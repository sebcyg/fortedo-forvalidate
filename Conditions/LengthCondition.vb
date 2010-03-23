Namespace Conditions

    Public Class LengthCondition
        Inherits FvConditionBase

        Private _minLength As Int32
        Private _maxLength As Int32

        Public Sub New(ByVal minLength As Int32, ByVal maxLength As Int32)
            _minLength = minLength
            _maxLength = maxLength
        End Sub

        Protected Overrides Function OnValidate(ByVal context As FvContext) As String
            Dim value = TryCast(context.PropertyValue, String)
            If value Is Nothing Then
                Return "Właściwość $propertyName$ jest nieprawidłowa."
            Else
                If value.Length >= _minLength And value.Length <= _maxLength Then
                    Return Nothing
                Else
                    Return String.Format("Właściwość $propertyName$ musi mieć długość {0}-{1} znaków.", _minLength, _maxLength)
                End If
            End If
        End Function
    End Class
End Namespace