Imports System.Text

Public Class FvPropertyChain
    Inherits List(Of FvProperty)

    Public ReadOnly Property OriginalPath() As String
        Get
            Dim sb As New StringBuilder
            For Each prop In Me
                If sb.Length > 0 Then
                    sb.Append(".")
                End If
                sb.AppendFormat("{0}", prop.Name)
            Next
            Return sb.ToString()
        End Get
    End Property

    Public ReadOnly Property Path() As String
        Get
            Dim sb As New StringBuilder
            For Each prop In Me
                If sb.Length > 0 Then
                    sb.Append(".")
                End If
                sb.AppendFormat("{0}", prop.Translation)
            Next
            Return sb.ToString()
        End Get
    End Property

    Public Function CheckSimilarity(ByVal chainSpecified As FvPropertyChain) As Boolean
        Dim i As Integer
        For Each prop In Me
            If chainSpecified.Count > i AndAlso prop.Name <> chainSpecified(i).Name Then
                Return False
            End If
            i += 1
        Next
        Return True
    End Function

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal source As FvPropertyChain)
        MyBase.New(source)
    End Sub

    Public Sub New(ByVal path As String)
        Me.New()
        For Each part In path.Split(".")
            Me.Add(New FvProperty(part, Nothing))
        Next
    End Sub

    Public Function CreateNested(ByVal nestedProperty As FvProperty) As FvPropertyChain
        Dim chain = New FvPropertyChain(Me)
        chain.Add(nestedProperty)
        Return chain
    End Function
End Class