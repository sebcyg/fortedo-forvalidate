Imports System.Text

Public Class FvError
    Private _message As String
    Public Property Message() As String
        Get
            Return _message
        End Get
        Private Set(ByVal value As String)
            _message = value
        End Set
    End Property

    Private _propertyChain As FvPropertyChain
    Public Property PropertyChain() As FvPropertyChain
        Get
            Return _propertyChain
        End Get
        Private Set(ByVal value As FvPropertyChain)
            _propertyChain = value
        End Set
    End Property

    Public ReadOnly Property OriginalPath() As String
        Get
            Return PropertyChain.OriginalPath
        End Get
    End Property

    Public ReadOnly Property Path() As String
        Get
            Return PropertyChain.Path
        End Get
    End Property

    Public ReadOnly Property OriginalContent()
        Get
            Return Message.Replace("$propertyName$", OriginalPath)
        End Get
    End Property

    Public ReadOnly Property Content() As String
        Get
            Return Message.Replace("$propertyName$", Path)
        End Get
    End Property

    Public Sub New(ByVal message As String, ByVal propertyChain As FvPropertyChain)
        Me.Message = message
        Me.PropertyChain = New FvPropertyChain(propertyChain)
    End Sub
End Class