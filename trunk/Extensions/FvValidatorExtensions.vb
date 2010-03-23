Imports System.Runtime.CompilerServices

Namespace Extensions

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
            context.FilterPropertyHandler = Function(chain) chain.CheckSimilarity(specifiedChain)
            Return validator.Validate(context)
        End Function
    End Module
End Namespace