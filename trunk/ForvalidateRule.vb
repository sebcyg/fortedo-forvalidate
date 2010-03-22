Imports System.Reflection
Imports Fortedo.ForValidate.Conditions

Public Class ForvalidateRule(Of TObject)
    Private Class ConditionItem
        Private _condition As ICondition(Of TObject)
        Public Property Condition() As ICondition(Of TObject)
            Get
                Return _condition
            End Get
            Set(ByVal value As ICondition(Of TObject))
                _condition = value
            End Set
        End Property

        Private _customMessage As String
        Public Property CustomMessage() As String
            Get
                Return _customMessage
            End Get
            Set(ByVal value As String)
                _customMessage = value
            End Set
        End Property

        Public Sub New(ByVal condition As ICondition(Of TObject))
            _condition = condition
        End Sub
    End Class

    Private _validatedProperty As ForvalidateProperty
    Public Property ValidatedProperty() As ForvalidateProperty
        Get
            Return _validatedProperty
        End Get
        Private Set(ByVal value As ForvalidateProperty)
            _validatedProperty = value
        End Set
    End Property

    Private _conditions As New List(Of ConditionItem)
    Private _lastAddedConditionItem As ConditionItem

    Public Sub AddCondition(ByVal condition As ICondition(Of TObject))
        _lastAddedConditionItem = New ConditionItem(condition)
        _conditions.Add(_lastAddedConditionItem)
    End Sub

    Public Sub New(ByVal propertyName As String)
        ValidatedProperty = New ForvalidateProperty(propertyName)
    End Sub

    Public Function Validate(ByVal obj As TObject, ByVal language As String, ByVal propertyNameFunc As Func(Of String, String)) As ForvalidateResult
        Dim result = New ForvalidateResult
        ValidatedProperty.RefreshValue(obj)

        For Each conditionItem In _conditions
            result.Combine(TransformByPropertyNameFunc(ForceCustomMessages(conditionItem.Condition.Validate(obj, ValidatedProperty), conditionItem.CustomMessage), propertyNameFunc))
        Next
        Return Translate(result, language)
    End Function

    Private Function ForceCustomMessages(ByVal result As ForvalidateResult, ByVal customMessage As String)
        If Not (result.IsValid Or String.IsNullOrEmpty(customMessage)) Then
            For Each e In result.Errors
                e.Message = customMessage
            Next
        End If
        Return result
    End Function

    Private Function TransformByPropertyNameFunc(ByVal result As ForvalidateResult, ByVal propertyNameFunc As Func(Of String, String)) As ForvalidateResult
        If Not (result.IsValid Or (propertyNameFunc Is Nothing)) Then
            For Each e In result.Errors
                e.Message = propertyNameFunc(e.Message)
            Next
        End If
        Return result
    End Function

    Private Function Translate(ByVal result As ForvalidateResult, ByVal language As String) As ForvalidateResult
        If Not result.IsValid Then
            For Each e In result.Errors
                e.Message = e.Message.Replace("$propertyName$", ValidatedProperty(language))
            Next
        End If
        Return result
    End Function

    Public Function Lang(ByVal language As String, ByVal translatedPropertyName As String) As ForvalidateRule(Of TObject)
        ValidatedProperty.Translations.Add(language, translatedPropertyName)
        Return Me
    End Function

    Public Function Msg(ByVal customMessage As String) As ForvalidateRule(Of TObject)
        If _lastAddedConditionItem IsNot Nothing Then
            _lastAddedConditionItem.CustomMessage = customMessage
        End If
        Return Me
    End Function
End Class