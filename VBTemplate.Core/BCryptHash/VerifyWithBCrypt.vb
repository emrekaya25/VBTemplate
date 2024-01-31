Public Class VerifyWithBCrypt
    Public Shared Function VerifyPassword(userPassword As String, requestPassword As String)

        Dim bool = BCrypt.Net.BCrypt.Verify(requestPassword, userPassword)
        If bool Then
            Return True
        Else
            Return False
        End If
    End Function
End Class
