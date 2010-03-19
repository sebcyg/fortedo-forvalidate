Imports System.Windows.Data
Imports System.Windows.Controls

Public Class ValidationProxy

    Private Shared _proxies As New List(Of ValidationProxy)

    Public Shared Function GetProxy(ByVal target As Object) As ValidationProxy
        Return _proxies.Find(Function(p) Object.ReferenceEquals(p.Target, target))
    End Function

    Public Shared Function SetProxy(ByVal target As Object, ByVal validator As IValidator) As ValidationProxy
        Dim proxy = GetProxy(target)
        If proxy IsNot Nothing Then
            proxy._validator = validator
        Else
            proxy = New ValidationProxy(target, validator)
            _proxies.Add(proxy)
        End If
        Return proxy
    End Function

    Private _targetReference As WeakReference
    Private _validator As IValidator
    Private _propertyExceptions As New Dictionary(Of String, Exception)
    Private _invalidatedBindingExpressions As New List(Of BindingExpression)

    Public ReadOnly Property Target() As Object
        Get
            Return _targetReference.Target
        End Get
    End Property

    Public Function Validate() As ValidationResult
        Dim result = _validator.Validate(Target)
        For Each item In _propertyExceptions
            result.Combine(New ValidationResult(item.Key, item.Value.Message))
        Next
        Return result
    End Function

    Public Function Validate(ByVal propertyName As String) As ValidationResult
        Return _validator.Validate(Target, propertyName)
    End Function

    Public Sub New(ByVal target As Object, ByVal validator As IValidator)
        _targetReference = New WeakReference(target)
        _validator = validator
    End Sub

    Public Sub SetPropertyException(ByVal propertyName As String, ByVal e As Exception)
        _propertyExceptions(propertyName) = e
    End Sub

    Public Sub AddInvalidatedBindingExpression(ByVal bindingExpression As BindingExpression)
        If Not _invalidatedBindingExpressions.Contains(bindingExpression) Then
            _invalidatedBindingExpressions.Add(bindingExpression)
        End If
    End Sub

    Public Sub RemoveInvalidatedBindingExpression(ByVal bindingExpression As BindingExpression)
        _invalidatedBindingExpressions.Remove(bindingExpression)
    End Sub

    Public Sub ForceValidation()
        For Each bindingExp In _invalidatedBindingExpressions.ToList()
            bindingExp.UpdateSource()
        Next
    End Sub

    Public Sub ClearPropertyException(ByVal propertyName As String)
        _propertyExceptions.Remove(propertyName)
    End Sub
End Class
