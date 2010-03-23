Namespace Conditions

    Public Class NotEmptyCondition
        Inherits FvConditionBase

        Protected Overrides Function OnValidate(ByVal context As FvContext) As String
            If context.PropertyValue = Nothing Then
                Return "Właściwość $propertyName$ nie może być pusta ani ustawiona na wartość domyślną."
            End If
            Return Nothing
        End Function
    End Class
End Namespace