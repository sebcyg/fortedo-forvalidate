Imports System.Windows.Data
Imports System.Windows.Controls

Namespace Wpf

    Public Class GenericValidationRule
        Inherits ValidationRule

        Public Overrides Function Validate(ByVal value As Object, ByVal cultureInfo As System.Globalization.CultureInfo) As ValidationResult
            Dim bindingExp As BindingExpression = value
            Dim proxy = ValidationProxy.GetProxy(bindingExp.DataItem)
            If proxy IsNot Nothing Then
                Dim propertyPath = bindingExp.ParentBinding.Path.Path
                'bug what if binding is used in several bindingexpression and in one is valid but not in the other
                proxy.ClearPropertyException(propertyPath)
                proxy.RemoveInvalidatedBindingExpression(bindingExp)
                Dim result = proxy.Validate(propertyPath)
                If result.IsValid Then
                    Return System.Windows.Controls.ValidationResult.ValidResult
                Else
                    proxy.AddInvalidatedBindingExpression(bindingExp)
                    Return New System.Windows.Controls.ValidationResult(False, result.ToString())
                End If
            Else
                Return System.Windows.Controls.ValidationResult.ValidResult
            End If
        End Function

        Public Sub New()
            MyBase.New(ValidationStep.UpdatedValue, True)
        End Sub
    End Class
End Namespace