Imports System.Runtime.CompilerServices

Namespace Wpf

    Public Module FvProxyExtensions
        <Extension()> _
        Public Function GetProxy(ByVal target As Object) As FvProxy
            Return FvProxy.GetProxy(target)
        End Function

        <Extension()> _
        Public Function SetProxy(ByVal target As Object, ByVal validator As FvValidatorBase) As FvProxy
            Return FvProxy.SetProxy(target, validator)
        End Function

        <Extension()> _
        Public Function SetProxy(Of TValidator As {FvValidatorBase, New})(ByVal target As Object) As FvProxy
            Return FvProxy.SetProxy(target, FvProxy.GetValidatorInstance(Of TValidator)())
        End Function

        <Extension()> _
        Public Sub RemoveProxy(ByVal target As Object)
            FvProxy.RemoveProxy(target)
        End Sub

        <Extension()> _
        Public Function ForceValidation(ByVal target As Object) As Boolean
            Dim proxy = FvProxy.GetProxy(target)
            If proxy Is Nothing Then
                Return False
            Else
                proxy.ForceValidation()
                Return True
            End If

        End Function
    End Module
End Namespace