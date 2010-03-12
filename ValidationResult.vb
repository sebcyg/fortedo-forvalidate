
Public Class ValidationResult

    Public ReadOnly Property IsValid() As Boolean
        Get
            Return _errors Is Nothing
        End Get
    End Property

    Private _errors As List(Of ValidationError)
    Public ReadOnly Property Errors() As List(Of ValidationError)
        Get
            Return _errors
        End Get
    End Property

    Public Sub Combine(ByVal result As ValidationResult)
        If Not result.IsValid Then
            If _errors Is Nothing Then
                _errors = New List(Of ValidationError)
            End If
            _errors.AddRange(result.Errors)
        End If
    End Sub

    Public Overrides Function ToString() As String
        Return If(IsValid, String.Empty, Errors(0).Message)
    End Function

    Public Sub New()
    End Sub

    Public Sub New(ByVal propertyName As String, ByVal message As String)
        _errors = New List(Of ValidationError)
        _errors.Add(New ValidationError(message))
    End Sub

End Class