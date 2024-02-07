Imports System.Linq.Expressions
Imports VBTemplate.Entity

Public Interface IProductService
    Inherits IGenericService(Of ProductDTORequest, ProductDTOResponse)

End Interface
