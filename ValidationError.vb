Public Class ValidationError
    'todo add property to distinguish exception error and validation rule error
    'todo add property for setting validated property path
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