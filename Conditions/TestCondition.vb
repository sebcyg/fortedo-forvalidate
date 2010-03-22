﻿Namespace Conditions
    Public Class TestCondition(Of TObject) : Implements ICondition(Of TObject)
        Private _validateFunc As Func(Of TObject, Boolean)

        Public Function Validate(ByVal obj As TObject, ByVal validatedProperty As ForvalidateProperty) As ForvalidateResult _
            Implements ICondition(Of TObject).Validate
            If _validateFunc(obj) Then
                Return New ForvalidateResult
            Else
                Return New ForvalidateResult(validatedProperty.Name, "Właściwość $propertyName$ jest nieprawidłowa.")
            End If
        End Function

        Public Sub New(ByVal func As Func(Of TObject, Boolean))
            _validateFunc = func
        End Sub

    End Class
End Namespace