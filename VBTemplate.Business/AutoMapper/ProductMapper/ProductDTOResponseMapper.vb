Imports AutoMapper
Imports VBTemplate.Entity

Public Class ProductDTOResponseMapper
    Inherits Profile
    Public Sub New()
        CreateMap(Of Product, ProductDTOResponse)()
        CreateMap(Of ProductDTOResponse, Product)()
    End Sub
End Class
