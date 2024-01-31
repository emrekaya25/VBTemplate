Public Class FieldValidationException
    Inherits Exception
    Sub New(messageList As List(Of String))

        MyBase.Data("FieldValidationMessage") = messageList

    End Sub
End Class
