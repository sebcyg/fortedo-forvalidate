Imports System.Globalization

Public Class ValidatorBase(Of TObject)
    Private _rules As New List(Of ValidationRule(Of TObject))
    Private _language As String = CultureInfo.CurrentCulture.TwoLetterISOLanguageName

    ''' <summary>
    ''' Adds new rule to selected property.
    ''' </summary>
    ''' <param name="propertyName">Name of a property the rule will be assigned to. it must be the name of the name of object property used in source code.</param>
    ''' <returns>Added property - it starts the fluent interface and conditions chaining.</returns>
    ''' <remarks></remarks>
    Public Function AddRule(ByVal propertyName As String) As ValidationRule(Of TObject)
        Dim rule = New ValidationRule(Of TObject)(propertyName)
        _rules.Add(rule)
        Return rule
    End Function

    ''' <summary>
    ''' Sets language for current instance of the validator.
    ''' Language settings are used to determine property names provided in validation result messages.
    ''' </summary>
    ''' <param name="language">Two-letter ISO language name.</param>
    ''' <remarks></remarks>
    Public Sub SetLang(ByVal language As String)
        _language = language
    End Sub

    ''' <summary>
    ''' Validates entire object and returns validation result for all properties with rule set configured inside the validator.
    ''' </summary>
    ''' <param name="obj">Object being validated</param>
    ''' <returns>Validation result for entire object</returns>
    ''' <remarks></remarks>
    Public Function Validate(ByVal obj As TObject) As ValidationResult
        Return Validate(obj, Nothing)
    End Function

    ''' <summary>
    ''' Validates entire object and returns validation result for all properties with rule set configured inside the validator.
    ''' Each property name in validation result messages is transformed by provided function.
    ''' </summary>
    ''' <param name="obj">Object being validated</param>
    ''' <param name="propertyNameFunc">Function tranforming each property name in validation result messages.</param>
    ''' <returns>Validation result for entire object</returns>
    ''' <remarks>If propertyNameFunc is null (Nothing), the method is equivalent to the one with only obj parameter.</remarks>
    Public Function Validate(ByVal obj As TObject, ByVal propertyNameFunc As Func(Of String, String)) As ValidationResult
        Dim result = New ValidationResult
        For Each rule As ValidationRule(Of TObject) In _rules
            result.Combine(rule.Validate(obj, _language, propertyNameFunc))
        Next
        Return result
    End Function

    ''' <summary>
    ''' Validates selected property of an object and rturn validation result if there is any rule set configured for it inside the validator.
    ''' </summary>
    ''' <param name="obj">Object which property is validated</param>
    ''' <param name="propertyName">Name of the property being validated</param>
    ''' <returns>Validation result for selected property</returns>
    ''' <remarks>If there is no result set configured for selected property, result IsValid property will be set to True.</remarks>
    Public Function ValidateProperty(ByVal obj As TObject, ByVal propertyName As String) As ValidationResult
        Return ValidateProperty(obj, propertyName, Nothing)
    End Function

    ''' <summary>
    ''' Validates selected property of an object and rturn validation result if there is any rule set configured for it inside the validator.
    ''' Each property name in validation result messages is transformed by provided function.
    ''' </summary>
    ''' <param name="obj">Object which property is validated</param>
    ''' <param name="propertyName">Name of the property being validated</param>
    ''' <param name="propertyNameFunc">Function tranforming each property name in validation result messages.</param>
    ''' <returns>Validation result for selected property</returns>
    ''' <remarks>
    ''' If there is no result set configured for selected property, result IsValid property will be set to True.
    ''' If propertyNameFunc is null (Nothing), the method is equivalent to the one without only propertyNameFunc parameter.
    ''' </remarks>
    Public Function ValidateProperty(ByVal obj As TObject, ByVal propertyName As String, ByVal propertyNameFunc As Func(Of String, String)) As ValidationResult
        Dim result = New ValidationResult
        For Each rule As ValidationRule(Of TObject) In _rules.Where(Function(r) r.PropertyName = propertyName)
            result.Combine(rule.Validate(obj, _language, propertyNameFunc))
        Next
        Return result
    End Function

End Class