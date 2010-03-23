Imports System.Runtime.CompilerServices
Imports Fortedo.ForValidate.Conditions

Public Module FvConditionExtensions
    <Extension()> _
    Public Function NotEqual(ByVal rule As FvConditionRule, ByVal value As Object) As FvConditionRule
        Dim condition As New NotEqualCondition(value)
        rule.Conditions.Add(condition)
        Return rule
    End Function

    <Extension()> _
    Public Function Length(ByVal rule As FvConditionRule, ByVal min As Integer, ByVal max As Integer) As FvConditionRule
        Dim condition As New LengthCondition(min, max)
        rule.Conditions.Add(condition)
        Return rule
    End Function

    <Extension()> _
    Public Function Matches(ByVal rule As FvConditionRule, ByVal pattern As String) As FvConditionRule
        Dim condition As New MatchesCondition(pattern)
        rule.Conditions.Add(condition)
        Return rule
    End Function

    <Extension()> _
    Public Function NotEmpty(ByVal rule As FvConditionRule) As FvConditionRule
        Dim condition As New NotEmptyCondition()
        rule.Conditions.Add(condition)
        Return rule
    End Function

    <Extension()> _
    Public Function NotNull(ByVal rule As FvConditionRule) As FvConditionRule
        Dim condition As New NotNullCondition()
        rule.Conditions.Add(condition)
        Return rule
    End Function

    <Extension()> _
    Public Function Test(Of TObject)(ByVal rule As FvConditionRule, ByVal func As Func(Of TObject, Boolean)) As FvConditionRule
        Dim condition As New TestCondition(Of TObject)(func)
        rule.Conditions.Add(condition)
        Return rule
    End Function

    <Extension()> _
    Public Function Ensure(Of TProperty)(ByVal rule As FvConditionRule, ByVal func As Func(Of TProperty, Boolean)) As FvConditionRule
        Dim condition As New EnsureCondition(Of TProperty)(func)
        rule.Conditions.Add(condition)
        Return rule
    End Function
End Module
