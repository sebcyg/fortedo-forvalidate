Imports System.Windows.Data
Imports System.Windows.Controls
Imports Fortedo.ForValidate.Extensions

Namespace Wpf

    ''' <summary>
    ''' Represents a proxy to integrate Forvalidate framework with WPF.
    ''' </summary>
    ''' <remarks>Each Proxy object consists of metadata extending data model object bound to WPF UI elements.</remarks>
    Public Class FvProxy

        Private Shared _proxies As New List(Of FvProxy)

        ''' <summary>
        ''' Gets instance of proxy associated with the specified target object.
        ''' </summary>
        ''' <param name="target">The object which proxy is about to being get.</param>
        ''' <returns>The proxy associated with the specified object, if found; otherwise, null (Nothing in VB). </returns>
        ''' <remarks></remarks>
        Public Shared Function GetProxy(ByVal target As Object) As FvProxy
            Dim proxy = _proxies.Find(Function(p) Object.ReferenceEquals(p.Target, target))
            If proxy Is Nothing Then
                Throw New ArgumentException("There is no proxy connected to the specified target.")
            End If
            Return proxy
        End Function

        ''' <summary>
        ''' Sets proxy for the specified target object and adds the specified validator to metadata of the proxy.
        ''' </summary>
        ''' <param name="target">The object which proxy is about to being set.</param>
        ''' <returns>The proxy set by the method.</returns>
        ''' <remarks>If a proxy for the specified target exists, the method does not create a new one, but changes associated validator only.</remarks>
        Public Shared Function Connect(Of TValidator As {FvValidatorBase, New})(ByVal target As Object) As FvProxy
            Dim proxy = _proxies.Find(Function(p) Object.ReferenceEquals(p.Target, target))
            If proxy IsNot Nothing Then
                proxy._validator = New TValidator
            Else
                proxy = New FvProxy(target, New TValidator)
                _proxies.Add(proxy)
            End If
            Return proxy
        End Function

        Public Shared Sub Disconnect(ByVal target As Object)
            Dim proxy = GetProxy(target)
            If proxy IsNot Nothing Then
                _proxies.Remove(proxy)
            End If
        End Sub

        Public Shared Function Validate(ByVal target As Object) As FvResult
            Dim proxy = GetProxy(target)
            Return proxy.Validate()
        End Function

        Public Shared Function Validate(ByVal target As Object, ByVal propertyPath As String) As FvResult
            Dim proxy = GetProxy(target)
            Return proxy.Validate(propertyPath)
        End Function

        Public Shared Sub ForceValidation(ByVal target As Object)
            Dim proxy = GetProxy(target)
            For Each bindingExp In proxy._invalidatedBindingExpressions.ToList()
                bindingExp.UpdateSource()
            Next
        End Sub

        Private _targetReference As WeakReference
        Private _validator As FvValidatorBase
        Private _propertyExceptions As New List(Of FvError)
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
                result.Errors.Add(item)
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
            For Each item In _propertyExceptions.Where(Function(entry) entry.PropertyChain.CheckSimilarity(specifiedChain))
                result.Errors.Add(item)
            Next
            Return result
        End Function

        Protected Sub New(ByVal target As Object, ByVal validator As FvValidatorBase)
            _targetReference = New WeakReference(target)
            _validator = validator
        End Sub

        Public Sub SetPropertyException(ByVal propertyPath As String, ByVal message As String)
            Dim existingError = _propertyExceptions.FirstOrDefault(Function(er) er.OriginalPath = propertyPath)
            If existingError Is Nothing Then
                _propertyExceptions.Remove(existingError)
            End If
            _propertyExceptions.Add(New FvError(message, New FvPropertyChain(propertyPath)) With {.Source = FvError.ErrorSource.Exception})
        End Sub
        Public Sub ClearPropertyException(ByVal propertyPath As String)
            Dim existingError = _propertyExceptions.FirstOrDefault(Function(er) er.OriginalPath = propertyPath)
            If existingError IsNot Nothing Then
                _propertyExceptions.Remove(existingError)
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
    End Class
End Namespace