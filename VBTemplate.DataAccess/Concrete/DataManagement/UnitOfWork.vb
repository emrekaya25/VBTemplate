Imports Microsoft.AspNetCore.Http
Imports Microsoft.EntityFrameworkCore
Imports VBTemplate.Entity

Public Class UnitOfWork
    Implements IUnitOfWork
    Private ReadOnly _context As TemplateContext
    Private ReadOnly _contextAccessor As IHttpContextAccessor

    Public Sub New(ByVal contextAccessor As IHttpContextAccessor, context As TemplateContext)
        _contextAccessor = contextAccessor
        _context = context

        ProductRepository = New ProductRepository(_context)
        UserRepository = New UserRepository(_context)


    End Sub

    Public ReadOnly Property ProductRepository As IProductRepository Implements IUnitOfWork.ProductRepository
    Public ReadOnly Property UserRepository As IUserRepository Implements IUnitOfWork.UserRepository


    Public Function SaveChangeAsync() As Task(Of Integer) Implements IUnitOfWork.SaveChangeAsync
        For Each item In _context.ChangeTracker.Entries(Of BaseEntity)()
            If item.State = EntityState.Added Then
                item.Entity.AddedTime = Date.Now
                item.Entity.UpdatedTime = Date.Now
                'item.Entity.AddedUser = item.Entity.AddedUser
                'item.Entity.UpdatedUser = item.Entity.UpdatedUser
                'item.Entity.AddedIPV4Address = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                'item.Entity.UpdatedIPV4Address = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                item.Entity.isActive = True

            ElseIf item.State = EntityState.Modified Then
                item.Entity.UpdatedTime = Date.Now
                'item.Entity.UpdatedUser = item.Entity.UpdatedUser
                'item.Entity.UpdatedIPV4Address = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()

                'ElseIf item.State = EntityState.Deleted Then
                '    item.Entity.isActive = False
                '    item.State = EntityState.Modified

            End If

        Next


        Return _context.SaveChangesAsync()
    End Function
End Class