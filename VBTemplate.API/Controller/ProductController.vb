Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VBTemplate.Business
Imports VBTemplate.Core
Imports VBTemplate.Entity

<ApiController>
<Route("[controller]/[action]")>
Public Class ProductController
    Inherits Controller

    Private ReadOnly _productService As ProductManager

    Public Sub New(productService As IProductService)
        _productService = productService
    End Sub

    <HttpGet("/GetProducts")>
    Public Async Function GetProducts() As Task(Of IActionResult)
        Dim values = Await _productService.GetAllAsync()
        Return Ok(values)
    End Function

    <HttpGet("/GetProduct/{productId}")>
    Public Async Function GetProduct(productId As Int64) As Task(Of IActionResult)
        Dim value = Await _productService.GetAsync(productId)
        Return Ok(value)
    End Function

    <HttpPost("/AddProduct")>
    <ValidationFilter(GetType(ProductValidator))>
    Public Async Function AddProduct(product As ProductDTORequest) As Task(Of IActionResult)
        Dim values = Await _productService.AddAsync(product)
        Return Ok(values)
    End Function
    <HttpPost("/UpdateProduct")>
    <ValidationFilter(GetType(ProductValidator))>
    Public Async Function UpdateProduct(product As ProductDTORequest) As Task(Of IActionResult)
        Dim values = Await _productService.UpdateAsync(product)
        Return Ok(values)
    End Function
    <HttpPost("/DeleteProduct")>
    Public Async Function DeleteProduct(product As ProductDTORequest) As Task(Of IActionResult)
        Dim values = Await _productService.DeleteAsync(product)
        Return Ok(values)
    End Function

End Class
