﻿Imports System.Text.RegularExpressions

Namespace Conditions


    Public Class MatchesCondition(Of TObject) : Implements ICondition(Of TObject)
        Private _regex As Regex

        Public Function Validate(ByVal obj As TObject, ByVal validatedProperty As ForvalidateProperty) As ForvalidateResult _
            Implements ICondition(Of TObject).Validate
            Dim value = TryCast(validatedProperty.Value, String)
            If value Is Nothing Then
                Return New ForvalidateResult
            Else
                If _regex.IsMatch(value) Then
                    Return New ForvalidateResult
                Else
                    Return New ForvalidateResult(validatedProperty.Name, "Właściwość $propertyName$ nie jest zgodna ze wzorcem.")
                End If
            End If
        End Function

        Public Sub New(ByVal pattern As String)
            _regex = New Regex(pattern)
        End Sub

    End Class
End Namespace