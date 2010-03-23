Public MustInherit Class FvConditionBase

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

    Protected MustOverride Function OnValidate(ByVal context As FvContext) As String

    Public Function Validate(ByVal context As FvContext) As String
        Dim message = OnValidate(context)
        If message IsNot Nothing Then
            Dim translated = Translation(context.Lang)
            If translated IsNot Nothing Then
                Return translated
            End If
            Return message
        End If
        Return Nothing
    End Function
End Class
