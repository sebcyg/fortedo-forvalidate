Imports System.Globalization

Public Class ValidatorBase(Of TObject)
    Private _rules As New List(Of ValidationRule(Of TObject))
    Private _language As String = CultureInfo.CurrentCulture.TwoLetterISOLanguageName

    Public Function AddRule(ByVal propertyName As String) As ValidationRule(Of TObject)
        Dim rule = New ValidationRule(Of TObject)(propertyName)
        _rules.Add(rule)
        Return rule
    End Function

    Public Sub SetLang(ByVal language As String)
        _language = language
    End Sub

    Public Function Validate(ByVal obj As TObject) As ValidationResult
        Return Validate(obj, Nothing)
    End Function

    Public Function Validate(ByVal obj As TObject, ByVal propertyNameFunc As Func(Of String, String)) As ValidationResult
        Dim result = New ValidationResult
        For Each rule As ValidationRule(Of TObject) In _rules
            result.Combine(rule.Validate(obj, _language, propertyNameFunc))
        Next
        Return result
    End Function

    Public Function ValidateProperty(ByVal obj As TObject, ByVal propertyName As String) As ValidationResult
        Return ValidateProperty(obj, propertyName, Nothing)
    End Function


    Public Function ValidateProperty(ByVal obj As TObject, ByVal propertyName As String, ByVal propertyNameFunc As Func(Of String, String)) As ValidationResult
        Dim result = New ValidationResult
        For Each rule As ValidationRule(Of TObject) In _rules.Where(Function(r) r.PropertyName = propertyName)
            result.Combine(rule.Validate(obj, _language, propertyNameFunc))
        Next
        Return result
    End Function

End Class