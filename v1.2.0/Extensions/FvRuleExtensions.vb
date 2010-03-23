Imports System.Runtime.CompilerServices

Namespace Extensions

    Public Module FvRuleExtensions
        <Extension()> _
        Public Sub By(Of TValidator As {FvValidatorBase, New})(ByVal rule As FvNestedRule)
            rule.Validator = New TValidator
        End Sub

        <Extension()> _
        Public Sub By(ByVal rule As FvNestedRule, ByVal validator As FvValidatorBase)
            rule.Validator = validator
        End Sub

        <Extension()> _
        Public Function WithName(Of TRule As FvRuleBase)(ByVal rule As TRule, ByVal lang As String, ByVal translatedName As String) As TRule
            rule.Translation(lang) = translatedName
            Return rule
        End Function

        <Extension()> _
        Public Function Msg(ByVal rule As FvConditionRule, ByVal lang As String, ByVal translatedMessage As String) As FvConditionRule
            'todo it should be optimised
            rule.Conditions.Last().Translation(lang) = translatedMessage
            Return rule
        End Function
    End Module
End Namespace