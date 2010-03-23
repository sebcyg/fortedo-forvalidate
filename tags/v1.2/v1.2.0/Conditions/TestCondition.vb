Namespace Conditions
    Public Class TestCondition(Of TObject)
        Inherits FvConditionBase

        Private _validateFunc As Func(Of TObject, Boolean)

        Public Sub New(ByVal func As Func(Of TObject, Boolean))
            _validateFunc = func
        End Sub

        Protected Overrides Function OnValidate(ByVal context As FvContext) As String
            If _validateFunc(context.Target) Then
                Return Nothing
            Else
                Return "Właściwość $propertyName$ jest nieprawidłowa."
            End If
        End Function
    End Class
End Namespace