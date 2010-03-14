Public Class ValidationError
    Private _message As String
    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(ByVal value As String)
            _message = value
        End Set
    End Property

    Public Sub New(ByVal message As String)
        _message = message
    End Sub
End Class