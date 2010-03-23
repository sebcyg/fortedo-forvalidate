Imports System.Reflection

Public MustInherit Class FvRuleBase

    Private _propertyName As String
    Public Property PropertyName() As String
        Get
            Return _propertyName
        End Get
        Private Set(ByVal value As String)
            _propertyName = value
        End Set
    End Property

    Private _translations As New Dictionary(Of String, String)
    Public Property Translation(ByVal lang As String) As String
        Get
            If lang Is Nothing OrElse lang.Length <> 2 Then
                Throw New ArgumentOutOfRangeException("Language name must be two-letter string.")
            End If
            If _translations.ContainsKey(lang) Then
                Return _translations(lang)
            End If
            Return Nothing
        End Get
        Set(ByVal value As String)
            If lang Is Nothing OrElse lang.Length <> 2 Then
                Throw New ArgumentOutOfRangeException("Language name must be two-letter string.")
            End If
            If value Is Nothing Then
                _translations.Remove(lang)
            Else
                _translations(lang) = value
            End If
        End Set
    End Property

    Protected MustOverride Function OnValidate(ByVal context As FvContext) As FvResult

    Public Function Validate(ByVal context As FvContext) As FvResult
        Dim chain As FvPropertyChain = context.PropertyChain.CreateNested(New FvProperty(PropertyName, Translation(context.Lang)))
        If context.FilterPropertyHandler Is Nothing OrElse context.FilterPropertyHandler(chain) Then
            Dim type = context.Target.GetType()
            Dim prop = type.GetProperty(PropertyName, BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.DeclaredOnly)
            If prop Is Nothing Then
                Throw New ArgumentException(String.Format("Property [{0}] does not exist in the specified target.", PropertyName))
            End If
            Dim newContext As New FvContext(context, chain, prop.GetValue(context.Target, Nothing))
            Dim result = OnValidate(newContext)
            Return result
        End If
        Return New FvResult
    End Function

    Public Sub New(ByVal propertyName As String)
        Me.PropertyName = propertyName
    End Sub
End Class