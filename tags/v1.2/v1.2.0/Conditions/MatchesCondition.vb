Imports System.Text.RegularExpressions

Namespace Conditions


    Public Class MatchesCondition
        Inherits FvConditionBase

        Private _regex As Regex

        Public Sub New(ByVal pattern As String)
            _regex = New Regex(pattern)
        End Sub

        Protected Overrides Function OnValidate(ByVal context As FvContext) As String
            Dim value = TryCast(context.PropertyValue, String)
            If value Is Nothing Then
                Return Nothing
            Else
                If _regex.IsMatch(value) Then
                    Return Nothing
                Else
                    Return "Właściwość $propertyName$ nie jest zgodna ze wzorcem."
                End If
            End If
        End Function
    End Class
End Namespace