Public Class VerifyWithBCrypt
    Public Shared Function VerifyPassword(userPassword As String, requestPassword As String)
        Return BCrypt.Net.BCrypt.Verify(requestPassword, userPassword)
    End Function
End Class
