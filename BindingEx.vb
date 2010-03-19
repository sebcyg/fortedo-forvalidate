Imports System.Windows.Data
Imports System.Windows.Controls
Imports System.Windows
Imports System.Threading

Public Class BindingEx
    Inherits BindingDecoratorBase

    Public Sub New()
        MyBase.New()
        Init()
    End Sub

    Public Sub New(ByVal path As String)
        Me.New()
        Me.Path = New PropertyPath(path)
    End Sub

    Private Sub Init()
        Me.ValidationRules.Add(New ExceptionValidationRule With {.ValidatesOnTargetUpdated = True})
        Me.ValidationRules.Add(New GenericValidationRule With {.ValidatesOnTargetUpdated = True})
        Me.UpdateSourceTrigger = Windows.Data.UpdateSourceTrigger.PropertyChanged
        Me.UpdateSourceExceptionFilter = New UpdateSourceExceptionFilterCallback(AddressOf UpdateSourceExceptionFilterHandler)
    End Sub

    Private Function UpdateSourceExceptionFilterHandler(ByVal bindExpression As Object, ByVal exception As Exception) As Object
        Dim bindingExp As BindingExpression = bindExpression
        Dim proxy = ValidationProxy.GetProxy(bindingExp.DataItem)
        If proxy IsNot Nothing Then
            proxy.SetPropertyException(bindingExp.ParentBinding.Path.Path, exception)
            proxy.AddInvalidatedBindingExpression(bindingExp)
        End If

        Return exception.Message
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
