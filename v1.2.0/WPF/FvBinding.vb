Imports System.Windows.Data
Imports System.Windows.Controls
Imports System.Windows
Imports System.Threading

Namespace Wpf

    ''' <summary>
    ''' Binding implementation providing support for Forvalidate integration with WPF (especially MVVM/MVP)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FvBinding
        Inherits Binding

        Private _exceptionMessage As String
        Public Property ExceptionMessage() As String
            Get
                Return _exceptionMessage
            End Get
            Set(ByVal value As String)
                _exceptionMessage = value
            End Set
        End Property

        ''' <summary>
        ''' Initializes new instance of this class. UpdateSourceTrigger is set to PropertyChanged and TargetNullValue is set to String.Empty.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            MyBase.New()
            Me.ValidationRules.Add(New ExceptionValidationRule)
            Me.ValidationRules.Add(New FvWpfValidationRule)
            Me.UpdateSourceTrigger = Windows.Data.UpdateSourceTrigger.PropertyChanged
            Me.TargetNullValue = String.Empty
            Me.UpdateSourceExceptionFilter = New UpdateSourceExceptionFilterCallback(AddressOf UpdateSourceExceptionFilterHandler)
        End Sub

        ''' <summary>
        ''' Initializes new instance of this class and set its Path property. UpdateSourceTrigger is set to PropertyChanged and TargetNullValue is set to String.Empty.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New(ByVal path As String)
            Me.New()
            Me.Path = New PropertyPath(path)
        End Sub

        Private Function UpdateSourceExceptionFilterHandler(ByVal bindExpression As Object, ByVal exception As Exception) As Object
            Dim bindingExp As BindingExpression = bindExpression
            Dim proxy = FvProxy.GetProxy(bindingExp.DataItem)
            Dim message As String
            message = If(Me.ExceptionMessage, exception.Message)
            If proxy IsNot Nothing Then
                proxy.SetPropertyException(bindingExp.ParentBinding.Path.Path, message)
                proxy.AddInvalidatedBindingExpression(bindingExp)
            End If

            Return message
        End Function
    End Class

    Public Class ErrorsToStringConverter
        Implements IValueConverter

        Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
            Dim validationErrors = CType(value, IList(Of System.Windows.Controls.ValidationError))
            Return If(validationErrors.Count = 0, "", validationErrors(0).ErrorContent)
        End Function

        Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
            Throw New Exception("The method or operation is not implemented.")
        End Function
    End Class
End Namespace