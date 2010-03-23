﻿Imports System.Runtime.CompilerServices

Namespace Wpf

    Public Module ValidationProxyExtensions
        <Extension()> _
        Public Function GetProxy(ByVal target As Object) As ValidationProxy
            Return ValidationProxy.GetProxy(target)
        End Function

        <Extension()> _
        Public Function SetProxy(ByVal target As Object, ByVal validator As FvValidatorBase) As ValidationProxy
            Return ValidationProxy.SetProxy(target, validator)
        End Function

        <Extension()> _
        Public Function SetProxy(Of TValidator As {FvValidatorBase, New})(ByVal target As Object) As ValidationProxy
            Return ValidationProxy.SetProxy(target, ValidationProxy.GetValidatorInstance(Of TValidator)())
        End Function

        <Extension()> _
        Public Sub RemoveProxy(ByVal target As Object)
            ValidationProxy.RemoveProxy(target)
        End Sub

        <Extension()> _
        Public Function ForceValidation(ByVal target As Object) As Boolean
            Dim proxy = ValidationProxy.GetProxy(target)
            If proxy Is Nothing Then
                Return False
            Else
                proxy.ForceValidation()
                Return True
            End If

        End Function
    End Module
End Namespace