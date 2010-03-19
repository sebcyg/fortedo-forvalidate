Imports System.Text.RegularExpressions

Namespace Conditions


    Public Class MatchesCondition(Of TObject) : Implements ICondition(Of TObject)
        Private _regex As Regex

        Public Function Validate(ByVal obj As TObject, ByVal propertyValue As Object, ByVal propertyName As String) As ValidationResult _
            Implements ICondition(Of TObject).Validate
            Dim value = TryCast(propertyValue, String)
            If value Is Nothing Then
                Return New ValidationResult
            Else
                If _regex.IsMatch(value) Then
                    Return New ValidationResult
                Else
                    Return New ValidationResult(propertyName, "Właściwość $propertyName$ nie jest zgodna ze wzorcem.")
                End If
            End If
        End Function

        Public Sub New(ByVal pattern As String)
            _regex = New Regex(pattern)
        End Sub

    End Class
End Namespace