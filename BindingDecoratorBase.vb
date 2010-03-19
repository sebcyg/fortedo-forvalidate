Imports System
Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports System.Globalization
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Data
Imports System.Windows.Markup
Imports System.Runtime.InteropServices

''' <summary>
''' A base class for custom markup extension which provides properties
''' that can be found on regular <see cref="Binding"/> markup extension.<br/>
''' See: http://www.hardcodet.net/2008/04/wpf-custom-binding-class 
''' </summary>
<MarkupExtensionReturnType(GetType(Object))> _
Public MustInherit Class BindingDecoratorBase
    Inherits MarkupExtension
    ''' <summary>
    ''' The decorated binding class.
    ''' </summary>
    Private m_binding As New Binding()


    'check documentation of the Binding class for property information

#Region "properties"

    ''' <summary>
    ''' The decorated binding class.
    ''' </summary>
    <Browsable(False)> _
    Public Property Binding() As Binding
        Get
            Return m_binding
        End Get
        Set(ByVal value As Binding)
            m_binding = Value
        End Set
    End Property


    <DefaultValue(CType(Nothing, Object))> _
    Public Property AsyncState() As Object
        Get
            Return m_binding.AsyncState
        End Get
        Set(ByVal value As Object)
            m_binding.AsyncState = value
        End Set
    End Property

    <DefaultValue(False)> _
    Public Property BindsDirectlyToSource() As Boolean
        Get
            Return m_binding.BindsDirectlyToSource
        End Get
        Set(ByVal value As Boolean)
            m_binding.BindsDirectlyToSource = Value
        End Set
    End Property

    <DefaultValue(CType(Nothing, Object))> _
Public Property Converter() As IValueConverter
        Get
            Return m_binding.Converter
        End Get
        Set(ByVal value As IValueConverter)
            m_binding.Converter = value
        End Set
    End Property

    <DefaultValue(CType(Nothing, Object))> _
Public Property TargetNullValue() As Object
        Get
            Return m_binding.TargetNullValue
        End Get
        Set(ByVal value As Object)
            m_binding.TargetNullValue = value
        End Set
    End Property

    <TypeConverter(GetType(CultureInfoIetfLanguageTagConverter)), DefaultValue(CType(Nothing, Object))> _
Public Property ConverterCulture() As CultureInfo
        Get
            Return m_binding.ConverterCulture
        End Get
        Set(ByVal value As CultureInfo)
            m_binding.ConverterCulture = value
        End Set
    End Property

    <DefaultValue(CType(Nothing, Object))> _
Public Property ConverterParameter() As Object
        Get
            Return m_binding.ConverterParameter
        End Get
        Set(ByVal value As Object)
            m_binding.ConverterParameter = value
        End Set
    End Property

    <DefaultValue(CType(Nothing, Object))> _
Public Property ElementName() As String
        Get
            Return m_binding.ElementName
        End Get
        Set(ByVal value As String)
            m_binding.ElementName = value
        End Set
    End Property

    <DefaultValue(CType(Nothing, Object))> _
Public Property FallbackValue() As Object
        Get
            Return m_binding.FallbackValue
        End Get
        Set(ByVal value As Object)
            m_binding.FallbackValue = value
        End Set
    End Property

    <DefaultValue(False)> _
    Public Property IsAsync() As Boolean
        Get
            Return m_binding.IsAsync
        End Get
        Set(ByVal value As Boolean)
            m_binding.IsAsync = Value
        End Set
    End Property

    <DefaultValue(BindingMode.[Default])> _
    Public Property Mode() As BindingMode
        Get
            Return m_binding.Mode
        End Get
        Set(ByVal value As BindingMode)
            m_binding.Mode = Value
        End Set
    End Property

    <DefaultValue(False)> _
    Public Property NotifyOnSourceUpdated() As Boolean
        Get
            Return m_binding.NotifyOnSourceUpdated
        End Get
        Set(ByVal value As Boolean)
            m_binding.NotifyOnSourceUpdated = Value
        End Set
    End Property

    <DefaultValue(False)> _
    Public Property NotifyOnTargetUpdated() As Boolean
        Get
            Return m_binding.NotifyOnTargetUpdated
        End Get
        Set(ByVal value As Boolean)
            m_binding.NotifyOnTargetUpdated = Value
        End Set
    End Property

    <DefaultValue(False)> _
    Public Property NotifyOnValidationError() As Boolean
        Get
            Return m_binding.NotifyOnValidationError
        End Get
        Set(ByVal value As Boolean)
            m_binding.NotifyOnValidationError = Value
        End Set
    End Property

    <DefaultValue(CType(Nothing, Object))> _
Public Property Path() As PropertyPath
        Get
            Return m_binding.Path
        End Get
        Set(ByVal value As PropertyPath)
            m_binding.Path = value
        End Set
    End Property

    <DefaultValue(CType(Nothing, Object))> _
Public Property RelativeSource() As RelativeSource
        Get
            Return m_binding.RelativeSource
        End Get
        Set(ByVal value As RelativeSource)
            m_binding.RelativeSource = value
        End Set
    End Property

    <DefaultValue(CType(Nothing, Object))> _
Public Property Source() As Object
        Get
            Return m_binding.Source
        End Get
        Set(ByVal value As Object)
            m_binding.Source = value
        End Set
    End Property

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property UpdateSourceExceptionFilter() As UpdateSourceExceptionFilterCallback
        Get
            Return m_binding.UpdateSourceExceptionFilter
        End Get
        Set(ByVal value As UpdateSourceExceptionFilterCallback)
            m_binding.UpdateSourceExceptionFilter = Value
        End Set
    End Property

    <DefaultValue(UpdateSourceTrigger.[Default])> _
    Public Property UpdateSourceTrigger() As UpdateSourceTrigger
        Get
            Return m_binding.UpdateSourceTrigger
        End Get
        Set(ByVal value As UpdateSourceTrigger)
            m_binding.UpdateSourceTrigger = Value
        End Set
    End Property

    <DefaultValue(False)> _
    Public Property ValidatesOnDataErrors() As Boolean
        Get
            Return m_binding.ValidatesOnDataErrors
        End Get
        Set(ByVal value As Boolean)
            m_binding.ValidatesOnDataErrors = Value
        End Set
    End Property

    <DefaultValue(False)> _
    Public Property ValidatesOnExceptions() As Boolean
        Get
            Return m_binding.ValidatesOnExceptions
        End Get
        Set(ByVal value As Boolean)
            m_binding.ValidatesOnExceptions = Value
        End Set
    End Property

    <DefaultValue(CType(Nothing, String))> _
Public Property XPath() As String
        Get
            Return m_binding.XPath
        End Get
        Set(ByVal value As String)
            m_binding.XPath = value
        End Set
    End Property

    <DefaultValue(CType(Nothing, Object))> _
Public ReadOnly Property ValidationRules() As Collection(Of ValidationRule)
        Get
            Return m_binding.ValidationRules
        End Get
    End Property

    <DefaultValue(CType(Nothing, String))> _
Public Property StringFormat() As String
        Get
            Return m_binding.StringFormat
        End Get
        Set(ByVal value As String)
            m_binding.StringFormat = value
        End Set
    End Property

    <DefaultValue("")> _
    Public Property BindingGroupName() As String
        Get
            Return m_binding.BindingGroupName
        End Get
        Set(ByVal value As String)
            m_binding.BindingGroupName = Value
        End Set
    End Property



#End Region



    ''' <summary>
    ''' This basic implementation just sets a binding on the targeted
    ''' <see cref="DependencyObject"/> and returns the appropriate
    ''' <see cref="BindingExpressionBase"/> instance.<br/>
    ''' All this work is delegated to the decorated <see cref="Binding"/>
    ''' instance.
    ''' </summary>
    ''' <returns>
    ''' The object value to set on the property where the extension is applied. 
    ''' In case of a valid binding expression, this is a <see cref="BindingExpressionBase"/>
    ''' instance.
    ''' </returns>
    ''' <param name="provider">Object that can provide services for the markup
    ''' extension.</param>
    Public Overrides Function ProvideValue(ByVal provider As IServiceProvider) As Object
        'create a binding and associate it with the target
        Return m_binding.ProvideValue(provider)
    End Function



    ''' <summary>
    ''' Validates a service provider that was submitted to the <see cref="ProvideValue"/>
    ''' method. This method checks whether the provider is null (happens at design time),
    ''' whether it provides an <see cref="IProvideValueTarget"/> service, and whether
    ''' the service's <see cref="IProvideValueTarget.TargetObject"/> and
    ''' <see cref="IProvideValueTarget.TargetProperty"/> properties are valid
    ''' <see cref="DependencyObject"/> and <see cref="DependencyProperty"/>
    ''' instances.
    ''' </summary>
    ''' <param name="provider">The provider to be validated.</param>
    ''' <param name="target">The binding target of the binding.</param>
    ''' <param name="dp">The target property of the binding.</param>
    ''' <returns>True if the provider supports all that's needed.</returns>
    Protected Overridable Function TryGetTargetItems(ByVal provider As IServiceProvider, <Out()> ByRef target As DependencyObject, <Out()> ByRef dp As DependencyProperty) As Boolean
        target = Nothing
        dp = Nothing
        If provider Is Nothing Then
            Return False
        End If

        'create a binding and assign it to the target
        Dim service As IProvideValueTarget = DirectCast(provider.GetService(GetType(IProvideValueTarget)), IProvideValueTarget)
        If service Is Nothing Then
            Return False
        End If

        'we need dependency objects / properties
        target = TryCast(service.TargetObject, DependencyObject)
        dp = TryCast(service.TargetProperty, DependencyProperty)
        Return target IsNot Nothing AndAlso dp IsNot Nothing
    End Function

End Class
