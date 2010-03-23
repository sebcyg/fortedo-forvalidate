Imports System.Windows.Data
Imports System.Windows.Controls
Imports Fortedo.ForValidate.Extensions

Namespace Wpf

    ''' <summary>
    ''' Represents a proxy to integrate Forvalidate framework with WPF.
    ''' </summary>
    ''' <remarks>Each Proxy object consists of metadata extending data model object bound to WPF UI elements.</remarks>
    Public Class ValidationProxy

        Private Shared _proxies As New List(Of ValidationProxy)

        ''' <summary>
        ''' Gets instance of proxy associated with the specified target object.
        ''' </summary>
        ''' <param name="target">The object which proxy is about to being get.</param>
        ''' <returns>The proxy associated with the specified object, if found; otherwise, null (Nothing in VB). </returns>
        ''' <remarks></remarks>
        Public Shared Function GetProxy(ByVal target As Object) As ValidationProxy
            Return _proxies.Find(Function(p) Object.ReferenceEquals(p.Target, target))
        End Function

        ''' <summary>
        ''' Sets proxy for the specified target object and adds the specified validator to metadata of the proxy.
        ''' </summary>
        ''' <param name="target">The object which proxy is about to being set.</param>
        ''' <param name="validator">The validator instance associated with object.</param>
        ''' <returns>The proxy set by the method.</returns>
        ''' <remarks>If a proxy for the specified target exists, the method does not create a new one, but changes associated validator only.</remarks>
        Public Shared Function SetProxy(ByVal target As Object, ByVal validator As FvValidatorBase) As ValidationProxy
            Dim proxy = GetProxy(target)
            If proxy IsNot Nothing Then
                proxy._validator = validator
            Else
                proxy = New ValidationProxy(target, validator)
                _proxies.Add(proxy)
            End If
            Return proxy
        End Function

        Public Shared Sub RemoveProxy(ByVal target As Object)
            Dim proxy = GetProxy(target)
            If proxy IsNot Nothing Then
                _proxies.Remove(proxy)
            End If
        End Sub

        Private _targetReference As WeakReference
        Private _validator As FvValidatorBase
        Private _propertyExceptions As New Dictionary(Of FvPropertyChain, Exception)
        Private _invalidatedBindingExpressions As New List(Of BindingExpression)

        ''' <summary>
        ''' Gets the target object of the proxy.
        ''' </summary>
        ''' <value></value>
        ''' <returns>The target object of the proxy</returns>
        ''' <remarks>target object is stored as a WeakReference to avoid memory leaks.</remarks>
        Public ReadOnly Property Target() As Object
            Get
                Return _targetReference.Target
            End Get
        End Property

        ''' <summary>
        ''' Validates the entire target object of the proxy using the validator associated with the proxy.
        ''' </summary>
        ''' <returns>Validation result containing possible errors collection.</returns>
        ''' <remarks>Returned results contains both, the exception errors (from WPF) and the validation ones (from validator).</remarks>
        Public Function Validate() As FvResult
            Dim result = _validator.Validate(Target)
            For Each item In _propertyExceptions
                result.Errors.Add(New FvError(item.Value.Message, New FvPropertyChain(item.Key)))
            Next
            Return result
        End Function

        ''' <summary>
        ''' Validates one of properties of target object specified by the property name. validation is executed using the validator associated with the proxy.
        ''' </summary>
        ''' <returns>Validation result containing possible errors collection.</returns>
        ''' <remarks>Returned results contains both, the exception errors (from WPF) and the validation ones (from validator).</remarks>
        Public Function Validate(ByVal propertyName As String) As FvResult
            Dim result = _validator.Validate(Target, propertyName)
            Dim specifiedChain = New FvPropertyChain(propertyName)
            For Each item In _propertyExceptions.Where(Function(entry) entry.Key.CheckSimilarity(specifiedChain))
                result.Errors.Add(New FvError(item.Value.Message, New FvPropertyChain(item.Key)) With {.Source = FvError.ErrorSource.Exception})
            Next
            Return result
        End Function

        Protected Sub New(ByVal target As Object, ByVal validator As FvValidatorBase)
            _targetReference = New WeakReference(target)
            _validator = validator
        End Sub

        Public Sub SetPropertyException(ByVal propertyName As String, ByVal e As Exception)
            Dim key = _propertyExceptions.Keys.FirstOrDefault(Function(p) p.Path = propertyName)
            If key Is Nothing Then
                _propertyExceptions.Add(New FvPropertyChain(propertyName), e)
            Else
                _propertyExceptions(key) = e
            End If
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
            Dim key = _propertyExceptions.Keys.FirstOrDefault(Function(p) p.Path = propertyName)
            If key IsNot Nothing Then
                _propertyExceptions.Remove(key)
            End If
        End Sub

        Private Shared _validatorInstances As New Dictionary(Of Type, FvValidatorBase)
        Public Shared Function GetValidatorInstance(Of TValidator As {FvValidatorBase, New})()
            Dim type = GetType(TValidator)
            If _validatorInstances.ContainsKey(type) Then
                Return _validatorInstances(type)
            Else
                Dim instance = New TValidator
                _validatorInstances.Add(type, instance)
                Return instance
            End If
        End Function
    End Class
End Namespace