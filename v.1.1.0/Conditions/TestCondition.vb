Namespace Conditions
    Public Class TestCondition(Of TObject) : Implements ICondition(Of TObject)
        Private _validateFunc As Func(Of TObject, Boolean)

        Public Function Validate(ByVal obj As TObject, ByVal propertyValue As Object, ByVal propertyName As String) As ValidationResult _
            Implements ICondition(Of TObject).Validate
            If _validateFunc(obj) Then
                Return New ValidationResult
            Else
                Return New ValidationResult(propertyName, "Właściwość $propertyName$ jest nieprawidłowa.")
            End If
        End Function

        Public Sub New(ByVal func As Func(Of TObject, Boolean))
            _validateFunc = func
        End Sub

    End Class
End Namespace