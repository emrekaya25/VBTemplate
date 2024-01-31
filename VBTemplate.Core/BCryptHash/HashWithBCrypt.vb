Public Class HashWithBCrypt
    Public Shared Function HashPassword(password As String)
        password = BCrypt.Net.BCrypt.HashPassword(password)
        Return password
    End Function
End Class
