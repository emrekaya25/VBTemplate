Imports Microsoft.AspNetCore.Authorization
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.Configuration
Imports Microsoft.IdentityModel.Tokens
Imports System.Configuration
Imports System.IdentityModel.Tokens.Jwt
Imports System.Security.Claims
Imports System.Text
Imports VBTemplate.Business
Imports VBTemplate.Core
Imports VBTemplate.Entity

<ApiController>
<Route("[action]")>
<AllowAnonymous>
Public Class AccountController
    Inherits Controller

    Private ReadOnly _userService As UserManager
    Private ReadOnly _configuration As IConfiguration

    Public Sub New(userService As IUserService, configuration As IConfiguration)
        _userService = userService
        _configuration = configuration
    End Sub

    <HttpPost("/Login")>
    Public Async Function Login(loggedUser As LoginDTORequest) As Task(Of IActionResult)

        Dim user = Await _userService.GetLoggedUser(Function(e) e.Email = loggedUser.Email)

        If user Is Nothing Then

            Return Ok((Result(Of LoginDTOResponse).Error(message:="Kullanıcı adı geçersiz.")))

        ElseIf (VerifyWithBCrypt.VerifyPassword(user.Password, loggedUser.Password)) Then

            Dim key = Encoding.UTF8.GetBytes(_configuration.GetValue(Of String)("AppSettings:JWTKey"))

            Dim userclaims As New List(Of Claim)

            userclaims.Add(New Claim("userEmail", user.Name))
            userclaims.Add(New Claim("userName", user.Surname))
            userclaims.Add(New Claim("userPassword", user.Password))

            Dim jwt As New JwtSecurityToken(
            expires:=DateTime.Now.AddDays(15),
            notBefore:=DateTime.Now,
            claims:=userclaims,
            issuer:="EmreKaya",
            signingCredentials:=New SigningCredentials(New SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            )

            Dim tokenHandler As New JwtSecurityTokenHandler

            Dim token = tokenHandler.WriteToken(jwt)

            Dim loginDTOResponse As New LoginDTOResponse

            loginDTOResponse.Name = user.Name
            loginDTOResponse.Surname = user.Surname
            loginDTOResponse.Email = user.Email
            loginDTOResponse.Token = token


            Return Ok(Result(Of LoginDTOResponse).SuccessWithData(loginDTOResponse))

        Else
            Return Ok((Result(Of LoginDTOResponse).Error(message:="Kullanıcı adı veya şifre geçersiz.")))

        End If







    End Function




End Class
