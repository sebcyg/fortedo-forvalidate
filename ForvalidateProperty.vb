Imports System.Text
Imports System.Reflection

Public Class ForvalidateProperty
    Public Const PROPERTY_NAME_MARKER = _
        "$propertyName$"
    Public Const DEFAULT_STRING_FORMAT = _
        "{" & PROPERTY_NAME_MARKER & "}"
    Public Const NO_PROPERTY_NAME_MARKER_EXCEPTION_MESSAGE = _
        "There is no property name marker in the specified string format."

    Private _name As String
    Public Property Name() As String
        Get
            Return _name
        End Get
        Private Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Private _parent As ForvalidateProperty
    Public Property Parent() As ForvalidateProperty
        Get
            Return _parent
        End Get
        Set(ByVal value As ForvalidateProperty)
            _parent = value
        End Set
    End Property

    Private _translations As Dictionary(Of String, String)
    Public ReadOnly Property Translations() As Dictionary(Of String, String)
        Get
            If _translations Is Nothing Then
                _translations = New Dictionary(Of String, String)
            End If
            Return _translations
        End Get
    End Property

    Private _stringFormat As String
    Public Property StringFormat() As String
        Get
            If _stringFormat = Nothing Then
                Return DEFAULT_STRING_FORMAT
            End If
            Return _stringFormat
        End Get
        Set(ByVal value As String)
            If value.Contains(PROPERTY_NAME_MARKER) Then
                _stringFormat = value
            Else
                Throw New ArgumentException(NO_PROPERTY_NAME_MARKER_EXCEPTION_MESSAGE)
            End If
        End Set
    End Property

    Default ReadOnly Property Item(ByVal lang As String) As String
        Get
            Dim sb As New StringBuilder
            If Parent IsNot Nothing Then
                sb.AppendFormat("{0}.", Parent(lang))
            End If
            If lang <> Nothing AndAlso Translations.ContainsKey(lang) Then
                sb.Append(Translations(lang))
            Else
                sb.Append(Name)
            End If
            Dim stringFormat = Me.StringFormat.Replace(PROPERTY_NAME_MARKER, "{0}")
            Return String.Format(stringFormat, sb.ToString())
        End Get
    End Property

    Private _value As Object
    Public Property Value() As Object
        Get
            Return _value
        End Get
        Private Set(ByVal value As Object)
            _value = value
        End Set
    End Property

    Public Sub RefreshValue(ByVal obj As Object)
        Dim type = obj.GetType()
        Dim prop = type.GetProperty(Me.Name, BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.DeclaredOnly)
        Me.Value = prop.GetValue(obj, Nothing)
    End Sub

    Public Sub New(ByVal name As String)
        Me.Name = name
    End Sub

End Class
