Imports AutoMapper
Imports VBTemplate.Entity

Public Class ProductDTORequestMapper
    Inherits Profile
    Public Sub New()
        CreateMap(Of Product, ProductDTORequest)()
        CreateMap(Of ProductDTORequest, Product)()
    End Sub

End Class
