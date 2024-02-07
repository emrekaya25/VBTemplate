Imports System.Linq.Expressions
Imports AutoMapper
Imports VBTemplate.Core
Imports VBTemplate.DataAccess
Imports VBTemplate.Entity

Public Class ProductManager
    Implements IProductService

    Private ReadOnly _uow As IUnitOfWork
    Private ReadOnly _mapper As IMapper

    Public Sub New(uow As IUnitOfWork, mapper As IMapper)
        _uow = uow
        _mapper = mapper
    End Sub

    Public Async Function GetAsync(Id As Long, ParamArray IncludeList() As String) As Task(Of Result(Of ProductDTOResponse)) Implements IGenericService(Of ProductDTORequest, ProductDTOResponse).GetAsync
        Dim result = New Result(Of ProductDTOResponse)
        Dim data = Await _uow.ProductRepository.GetAsync(Function(x) x.Id = Id)
        Dim mapData = _mapper.Map(Of ProductDTOResponse)(data)
        result.Data = mapData
        Return result
    End Function

    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of ProductDTORequest, Boolean)) = Nothing, Optional IncludeList() As String = Nothing) As Task(Of Result(Of IEnumerable(Of ProductDTOResponse))) Implements IGenericService(Of ProductDTORequest, ProductDTOResponse).GetAllAsync
        Dim result = New Result(Of IEnumerable(Of ProductDTOResponse))
        Dim data = Await _uow.ProductRepository.GetAllAsync()
        Dim mapData = _mapper.Map(Of List(Of ProductDTOResponse))(data)
        result.Data = mapData
        Return result
    End Function

    Public Async Function AddAsync(p As ProductDTORequest) As Task(Of Result(Of ProductDTOResponse)) Implements IGenericService(Of ProductDTORequest, ProductDTOResponse).AddAsync
        Dim entity = _mapper.Map(Of Product)(p)
        _uow.ProductRepository.Add(entity)
        Await _uow.SaveChangeAsync()
        Dim entityResponse = _mapper.Map(Of ProductDTOResponse)(entity)
        Dim result = New Result(Of ProductDTOResponse)(entityResponse, _mesaj:="Eklendi", _statusCode:=200)
        Return result
    End Function

    Public Async Function UpdateAsync(p As ProductDTORequest) As Task(Of Result(Of ProductDTOResponse)) Implements IGenericService(Of ProductDTORequest, ProductDTOResponse).UpdateAsync
        Dim entity = _mapper.Map(Of Product)(p)
        _uow.ProductRepository.Update(entity)
        Await _uow.SaveChangeAsync()
        Dim entityResponse = _mapper.Map(Of ProductDTOResponse)(entity)
        Dim result = New Result(Of ProductDTOResponse)(entityResponse, _mesaj:="Güncellendi", _statusCode:=200)
        Return Result
    End Function

#Region "Silme "
    Public Async Function DeleteAsync(p As ProductDTORequest) As Task(Of Result(Of Nullable)) Implements IGenericService(Of ProductDTORequest, ProductDTOResponse).DeleteAsync
        Dim result = New Result(Of Nullable)
        Dim exists = Await _uow.ProductRepository.AnyAsync(Function(x) x.Id = p.Id)
        If exists Then
            Throw New Exception("Ürün bulunamadı.")
        End If
        Dim product = _mapper.Map(Of Product)(p)
        _uow.ProductRepository.Delete(product)
        Await _uow.SaveChangeAsync()
        Return result
    End Function
#End Region

End Class
