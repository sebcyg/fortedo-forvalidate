
Public Class ForvalidateResult

    Public ReadOnly Property IsValid() As Boolean
        Get
            Return _errors Is Nothing
        End Get
    End Property

    Private _errors As List(Of ForvalidateError)
    Public ReadOnly Property Errors() As List(Of ForvalidateError)
        Get
            Return _errors
        End Get
    End Property

    Public Sub Combine(ByVal result As ForvalidateResult)
        If Not result.IsValid Then
            If _errors Is Nothing Then
                _errors = New List(Of ForvalidateError)
            End If
            _errors.AddRange(result.Errors)
        End If
    End Sub

    Public Overrides Function ToString() As String
        Return If(IsValid, Nothing, Errors(0).Message)
    End Function

    Public Sub New()
    End Sub

    Public Sub New(ByVal propertyName As String, ByVal message As String, ByVal source As ValidationErrorSource)
        _errors = New List(Of ForvalidateError)
        _errors.Add(New ForvalidateError(message, source))
    End Sub

    Public Sub New(ByVal propertyName As String, ByVal message As String)
        _errors = New List(Of ForvalidateError)
        _errors.Add(New ForvalidateError(message, ValidationErrorSource.Rule))
    End Sub
End Class