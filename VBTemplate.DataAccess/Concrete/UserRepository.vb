Imports Microsoft.EntityFrameworkCore
Imports VBTemplate.Entity

Public Class UserRepository
    Inherits Repository(Of User)
    Implements IUserRepository

    Public Sub New(context As DbContext)
        MyBase.New(context)
    End Sub
End Class
