Namespace Conditions

    Public Class NotNullCondition
        Inherits FvConditionBase

        Protected Overrides Function OnValidate(ByVal context As FvContext) As String
            If context.PropertyValue Is Nothing Then
                Return "Właściwość $propertyName$ nie może być pusta."
            End If
            Return Nothing
        End Function
    End Class
End Namespace