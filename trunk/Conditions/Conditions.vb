Imports System.Runtime.CompilerServices

Namespace Conditions

    Public Module Conditions
        <Extension()> _
        Public Function Test(Of TObject)(ByVal rule As ValidationRule(Of TObject), ByVal func As Func(Of TObject, Boolean)) As ValidationRule(Of TObject)
            Dim condition = New TestCondition(Of TObject)(func)
            rule.AddCondition(condition)
            Return rule
        End Function

        <Extension()> _
        Public Function Length(Of TObject)(ByVal rule As ValidationRule(Of TObject), ByVal minLength As Int32, ByVal maxLength As Int32) As ValidationRule(Of TObject)
            Dim condition = New LengthCondition(Of TObject)(minLength, maxLength)
            rule.AddCondition(condition)
            Return rule
        End Function

        <Extension()> _
        Public Function NotNull(Of TObject)(ByVal rule As ValidationRule(Of TObject)) As ValidationRule(Of TObject)
            Dim condition = New NotNullCondition(Of TObject)()
            rule.AddCondition(condition)
            Return rule
        End Function

        <Extension()> _
        Public Function NotEmpty(Of TObject)(ByVal rule As ValidationRule(Of TObject)) As ValidationRule(Of TObject)
            Dim condition = New NotEmptyCondition(Of TObject)()
            rule.AddCondition(condition)
            Return rule
        End Function

        <Extension()> _
        Public Function Matches(Of TObject)(ByVal rule As ValidationRule(Of TObject), ByVal pattern As String) As ValidationRule(Of TObject)
            Dim condition = New MatchesCondition(Of TObject)(pattern)
            rule.AddCondition(condition)
            Return rule
        End Function

        <Extension()> _
        Public Function Validates(Of TObject, TNestedObject)(ByVal rule As ValidationRule(Of TObject), ByVal validator As ValidatorBase(Of TNestedObject)) As ValidationRule(Of TObject)
            Dim condition = New ValidatesCondition(Of TObject, TNestedObject)(validator)
            rule.AddCondition(condition)
            Return rule
        End Function
    End Module
End Namespace