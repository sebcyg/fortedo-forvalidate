Public Class FvContext

    Private _target As Object
    Public Property Target() As Object
        Get
            Return _target
        End Get
        Private Set(ByVal value As Object)
            _target = value
        End Set
    End Property

    Private _propertyChain As FvPropertyChain
    Public Property PropertyChain() As FvPropertyChain
        Get
            If _propertyChain Is Nothing Then
                _propertyChain = New FvPropertyChain
            End If
            Return _propertyChain
        End Get
        Set(ByVal value As FvPropertyChain)
            _propertyChain = New FvPropertyChain(value)
        End Set
    End Property

    Private _propertyValue As Object
    Public Property PropertyValue() As Object
        Get
            Return _propertyValue
        End Get
        Set(ByVal value As Object)
            _propertyValue = value
        End Set
    End Property

    Private _lang As String
    Property Lang() As String
        Get
            Return _lang
        End Get
        Set(ByVal value As String)
            _lang = value
        End Set
    End Property

    Private _filterPropertyHandler As Func(Of FvPropertyChain, Boolean)
    Public Property FilterPropertyHandler() As Func(Of FvPropertyChain, Boolean)
        Get
            Return _filterPropertyHandler
        End Get
        Set(ByVal value As Func(Of FvPropertyChain, Boolean))
            _filterPropertyHandler = value
        End Set
    End Property

    Public Sub New(ByVal target As Object)
        Me.Target = target
    End Sub

    Public Sub New(ByVal baseContext As FvContext, ByVal propertyChain As FvPropertyChain, ByVal propertyValue As Object)
        Me.New(baseContext.Target)
        Me.Lang = baseContext.Lang
        Me.PropertyValue = propertyValue
        _propertyChain = propertyChain
        Me.FilterPropertyHandler = baseContext.FilterPropertyHandler
    End Sub

    Public Function CreateNestedContext() As FvContext
        Dim nestedContext As New FvContext(Me.PropertyValue) With { _
            .Lang = Me.Lang, _
            .PropertyChain = Me.PropertyChain, _
            .FilterPropertyHandler = Me.FilterPropertyHandler}
        Return nestedContext
    End Function
End Class