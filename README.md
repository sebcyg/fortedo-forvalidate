**ForValidate** is a fluent and/or declarative validation library for .Net. It can be easily integrated with existing validation models used in .Net (e.g. using IDataErrorInfo interface in WPF and Forms). It enables you to write validation
logic in a way that somewhat resembles natural language.

# Features #
 * Provides developers with a *fluent interface* (DSL) for quickly composing easily readable rules.
 * Allows using *custom* validation error messages.
 * Supports *multi-language* property names and messages.
 * Conditions *validation chaining* for one rule.
 * Easily *integrates with WPF* and data bindings.
 * Extensible set of condition validators.
 * *Declarative validation* description using attributes on properties (*feature in progress*).
 * Validates *nested complex objects* with its own validators.
 * Does not put any requirements on validated objects (no base classes, no interface implementations).
 * Validation of *entire object or selected properties* with one validator.

# Example #
## Preparing a validator ##
```vba
Imports Fortedo.ForValidate
Imports Fortedo.ForValidate.Extensions

Public Class CustomerValidator : Inherits FvValidatorBase
    Public Sub New()
        AddRule("FirstName").NotEmpty()
        AddRule("LastName").NotEmpty()
        AddRule("PeselNumber"). _
            .Matches("^([0-9]{11}|)$").Msg("en", "Property $propertyName$ should consist of 11 digits.") _
            .NotEqual("00000000000")
    End Sub
End Class
```
## Using a validator ##
```vba
Dim customer As New Customer
Dim validator As New CustomerValidator
Dim result = validator.Validate(customer)
If Not result.IsValid Then
    For Each e In result.Errors
        Console.WriteLine(e.Content)
    Next
End If
```

## Wiki ##
Further documentation on project's [Wiki](https://github.com/sebcyg/fortedo-forvalidate/wiki).

# Requirements #

 * Microsoft .Net Framework 3.0
 * Any .Net language providing generic types and extension methods (surely VB and C#)
