Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VBTemplate.Business
Imports VBTemplate.Core
Imports VBTemplate.Entity

<ApiController>
<Route("[controller]/[action]")>
Public Class UserController
    Inherits Controller

    Private ReadOnly _userService As UserManager

    Public Sub New(userService As IUserService)
        _userService = userService
    End Sub

    <HttpGet("/GetUsers")>
    Public Async Function GetUsers() As Task(Of IActionResult)
        Dim values = Await _userService.GetAllAsync()
        Return Ok(values)
    End Function

    <HttpGet("/GetUser/{userId}")>
    Public Async Function GetProduct(userId As Int64) As Task(Of IActionResult)
        Dim value = Await _userService.GetAsync(userId)
        Return Ok(value)
    End Function

    <HttpPost("/AddUser")>
    <ValidationFilter(GetType(UserValidator))>
    Public Async Function AddProduct(user As UserDTORequest) As Task(Of IActionResult)
        Dim values = Await _userService.AddAsync(user)
        Return Ok(values)
    End Function

    <HttpPost("/UpdateUser")>
    <ValidationFilter(GetType(UserValidator))>
    Public Async Function UpdateProduct(user As UserDTORequest) As Task(Of IActionResult)
        Dim values = Await _userService.UpdateAsync(user)
        Return Ok(values)
    End Function

    <HttpPost("/DeleteUser")>
    Public Async Function DeleteProduct(user As UserDTORequest) As Task(Of IActionResult)
        Dim values = Await _userService.DeleteAsync(user)
        Return Ok(values)
    End Function

End Class

