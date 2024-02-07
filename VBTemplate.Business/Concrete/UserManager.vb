Imports System.Linq.Expressions
Imports AutoMapper
Imports VBTemplate.Core
Imports VBTemplate.DataAccess
Imports VBTemplate.Entity

Public Class UserManager
    Implements IUserService

    Private ReadOnly _uow As IUnitOfWork
    Private ReadOnly _mapper As IMapper


    Public Sub New(uow As IUnitOfWork, mapper As IMapper)
        _uow = uow
        _mapper = mapper
    End Sub

#Region "Get"
    Public Async Function GetAsync(Id As Long, ParamArray IncludeList() As String) As Task(Of Result(Of UserDTOResponse)) Implements IGenericService(Of UserDTORequest, UserDTOResponse).GetAsync
        Dim result = New Result(Of UserDTOResponse)
        Dim data = Await _uow.UserRepository.GetAsync(Function(x) x.Id = Id)
        Dim mapData = _mapper.Map(Of UserDTOResponse)(data)
        result.Data = mapData
        Return result
    End Function
#End Region

#Region "Get All"
    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of UserDTORequest, Boolean)) = Nothing, Optional IncludeList() As String = Nothing) As Task(Of Result(Of IEnumerable(Of UserDTOResponse))) Implements IGenericService(Of UserDTORequest, UserDTOResponse).GetAllAsync
        Dim result = New Result(Of IEnumerable(Of UserDTOResponse))
        Dim data = Await _uow.UserRepository.GetAllAsync()
        Dim mapData = _mapper.Map(Of List(Of UserDTOResponse))(data)
        result.Data = mapData
        Return result
    End Function
#End Region

#Region "Ekleme"
    Public Async Function AddAsync(p As UserDTORequest) As Task(Of Result(Of UserDTOResponse)) Implements IGenericService(Of UserDTORequest, UserDTOResponse).AddAsync
        Dim entity = _mapper.Map(Of User)(p)
        entity.Password = HashWithBCrypt.HashPassword(entity.Password)
        _uow.UserRepository.Add(entity)
        Await _uow.SaveChangeAsync()
        Dim entityResponse = _mapper.Map(Of UserDTOResponse)(entity)
        Dim result = New Result(Of UserDTOResponse)(entityResponse, _mesaj:="Kullanıcı Eklendi", _statusCode:=200)
        Return result
    End Function
#End Region

#Region "Güncelleme"
    Public Async Function UpdateAsync(p As UserDTORequest) As Task(Of Result(Of UserDTOResponse)) Implements IGenericService(Of UserDTORequest, UserDTOResponse).UpdateAsync
        Dim entity = _mapper.Map(Of User)(p)
        entity.Password = HashWithBCrypt.HashPassword(entity.Password)
        _uow.UserRepository.Update(entity)
        Await _uow.SaveChangeAsync()
        Dim entityResponse = _mapper.Map(Of UserDTOResponse)(entity)
        Dim result = New Result(Of UserDTOResponse)(entityResponse, _mesaj:="Kullanıcı Güncellendi", _statusCode:=200)
        Return result

    End Function
#End Region

#Region "Silme"
    Public Async Function DeleteAsync(p As UserDTORequest) As Task(Of Result(Of Nullable)) Implements IGenericService(Of UserDTORequest, UserDTOResponse).DeleteAsync
        Dim result = New Result(Of Nullable)
        Dim exists = Await _uow.UserRepository.AnyAsync(Function(x) x.Id = p.Id)
        If exists Then
            Throw New Exception("Ürün bulunamadı.")
        End If
        Dim user = _mapper.Map(Of User)(p)
        _uow.UserRepository.Delete(user)
        Await _uow.SaveChangeAsync()
        Return result
    End Function
#End Region

#Region "Login"
    Public Async Function GetLoggedUser(Filter As Expression(Of Func(Of User, Boolean)), ParamArray includeProperties() As String) As Task(Of User) Implements IUserService.GetLoggedUser
        Return Await _uow.UserRepository.GetAsync(Filter, includeProperties)
    End Function
#End Region

End Class
