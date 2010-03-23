Public Class FvProperty
    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Private Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _translation As String
    Public Property Translation() As String
        Get
            If _translation Is Nothing Then
                Return _name
            End If
            Return _translation
        End Get
        Private Set(ByVal value As String)
            _translation = value
        End Set
    End Property

    Public Sub New(ByVal name As String, ByVal translation As String)
        Me.Name = name
        Me.Translation = translation
    End Sub
End Class
