Imports System.Runtime.CompilerServices

Public Module FvValidatorExtensions
    <Extension()> _
    Public Function AddRule(ByVal validator As FvValidatorBase, ByVal propertyName As String) As FvConditionRule
        Dim rule = New FvConditionRule(propertyName)
        validator.Rules.Add(rule)
        Return rule
    End Function

    <Extension()> _
    Public Function Validates(ByVal validator As FvValidatorBase, ByVal propertyName As String) As FvNestedRule
        Dim rule = New FvNestedRule(propertyName)
        validator.Rules.Add(rule)
        Return rule
    End Function

    <Extension()> _
    Public Function Validate(ByVal validator As FvValidatorBase, ByVal obj As Object) As FvResult
        Dim context = New FvContext(obj)
        Return validator.Validate(context)
    End Function

    <Extension()> _
    Public Function Validate(ByVal validator As FvValidatorBase, ByVal obj As Object, ByVal propertyPath As String) As FvResult
        Dim context = New FvContext(obj)
        Dim specifiedChain = New FvPropertyChain(propertyPath)
        context.FilterPropertyHandler = Function(chain) CompareChains(chain, specifiedChain)
        Return validator.Validate(context)
    End Function

    Private Function CompareChains(ByVal chainValidated As FvPropertyChain, ByVal chainSpecified As FvPropertyChain) As Boolean
        Dim i As Integer
        For Each prop In chainValidated
            If chainSpecified.Count > i AndAlso prop.Name <> chainSpecified(i).Name Then
                Return False
            End If
            i += 1
        Next
        Return True
    End Function
End Module
