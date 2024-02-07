Imports AutoMapper
Imports VBTemplate.Entity

Public Class UserDTOResponseMapper
    Inherits Profile
    Public Sub New()
        CreateMap(Of User, UserDTOResponse)()
        CreateMap(Of UserDTOResponse, User)()
    End Sub

End Class
