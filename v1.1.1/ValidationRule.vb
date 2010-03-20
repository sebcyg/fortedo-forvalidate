Imports System.Reflection
Imports Fortedo.ForValidate.Conditions

Public Class ValidationRule(Of TObject)
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

    Private _propertyName As String
    Private _translations As Dictionary(Of String, String)
    Private _conditions As New List(Of ConditionItem)
    Private _lastAddedConditionItem As ConditionItem

    Public ReadOnly Property PropertyName() As String
        Get
            Return _propertyName
        End Get
    End Property

    Public Sub AddCondition(ByVal condition As ICondition(Of TObject))
        _lastAddedConditionItem = New ConditionItem(condition)
        _conditions.Add(_lastAddedConditionItem)
    End Sub

    Public Sub New(ByVal propertyName As String)
        _translations = New Dictionary(Of String, String)
        _propertyName = propertyName
    End Sub

    Public Function Validate(ByVal obj As TObject, ByVal language As String, ByVal propertyNameFunc As Func(Of String, String)) As ValidationResult
        Dim result = New ValidationResult
        Dim type = obj.GetType()
        Dim prop = type.GetProperty(PropertyName, BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.DeclaredOnly)
        Dim value = prop.GetValue(obj, Nothing)

        For Each conditionItem In _conditions
            result.Combine(TransformByPropertyNameFunc(ForceCustomMessages(conditionItem.Condition.Validate(obj, value, PropertyName), conditionItem.CustomMessage), propertyNameFunc))
        Next
        Return Translate(result, language)
    End Function

    Private Function ForceCustomMessages(ByVal result As ValidationResult, ByVal customMessage As String)
        If Not (result.IsValid Or String.IsNullOrEmpty(customMessage)) Then
            For Each e In result.Errors
                e.Message = customMessage
            Next
        End If
        Return result
    End Function

    Private Function TransformByPropertyNameFunc(ByVal result As ValidationResult, ByVal propertyNameFunc As Func(Of String, String)) As ValidationResult
        If Not (result.IsValid Or (propertyNameFunc Is Nothing)) Then
            For Each e In result.Errors
                e.Message = propertyNameFunc(e.Message)
            Next
        End If
        Return result
    End Function

    Private Function Translate(ByVal result As ValidationResult, ByVal language As String) As ValidationResult
        If Not result.IsValid Then
            For Each e In result.Errors
                e.Message = e.Message.Replace("$propertyName$", _
                                              If(_translations.ContainsKey(language), _translations(language), PropertyName))
            Next
        End If
        Return result
    End Function

    Public Function Lang(ByVal language As String, ByVal translatedPropertyName As String) As ValidationRule(Of TObject)
        _translations.Add(language, translatedPropertyName)
        Return Me
    End Function

    Public Function Msg(ByVal customMessage As String) As ValidationRule(Of TObject)
        If _lastAddedConditionItem IsNot Nothing Then
            _lastAddedConditionItem.CustomMessage = customMessage
        End If
        Return Me
    End Function
End Class