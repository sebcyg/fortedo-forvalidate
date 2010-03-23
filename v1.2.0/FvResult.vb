Public Class FvResult

    Private _errors As New List(Of FvError)
    Public ReadOnly Property Errors() As List(Of FvError)
        Get
            Return _errors
        End Get
    End Property

    Public ReadOnly Property IsValid() As Boolean
        Get
            Return Errors.Count = 0
        End Get
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ByVal errors As IEnumerable(Of FvError))
        Me.Errors.AddRange(errors)
    End Sub
End Class