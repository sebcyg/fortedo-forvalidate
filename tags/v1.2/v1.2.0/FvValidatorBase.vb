Imports System.Globalization

Public Class FvValidatorBase
    Private _rules As New List(Of FvRuleBase)
    Public ReadOnly Property Rules() As List(Of FvRuleBase)
        Get
            Return _rules
        End Get
    End Property

    Private _defaultLang As String
    Public Property DefaultLang() As String
        Get
            If _defaultLang Is Nothing Then
                Return CultureInfo.CurrentCulture.TwoLetterISOLanguageName
            End If
            Return _defaultLang
        End Get
        Set(ByVal value As String)
            _defaultLang = value
        End Set
    End Property

    Protected Overridable Function OnValidate(ByVal context As FvContext) As FvResult
        Dim result As New FvResult
        If context.Lang Is Nothing Then
            context.Lang = DefaultLang
        End If
        For Each r In Rules
            result.Errors.AddRange(r.Validate(context).Errors)
        Next
        Return result
    End Function

    Public Function Validate(ByVal context As FvContext) As FvResult
        Return OnValidate(context)
    End Function
End Class
