Imports Microsoft.EntityFrameworkCore
Imports System.Linq.Expressions
Imports VBTemplate.Entity

Public Class Repository(Of T As BaseEntity)
    Implements IRepository(Of T)

    Private ReadOnly _context As DbContext
    Private ReadOnly _dbSet As DbSet(Of T)

    Public Sub New(context As DbContext)
        _context = context
        _dbSet = _context.Set(Of T)()
    End Sub

    Public Sub Add(entity As T) Implements IRepository(Of T).Add
        _dbSet.Add(entity)
    End Sub

    Public Sub Delete(entity As T) Implements IRepository(Of T).Delete
        _dbSet.Remove(entity)
    End Sub

    Public Sub Update(entity As T) Implements IRepository(Of T).Update
        _dbSet.Update(entity)
    End Sub


    Public Async Function GetAllAsync(Optional Filter As Expression(Of Func(Of T, Boolean)) = Nothing, Optional IncludeList() As String = Nothing) As Task(Of IEnumerable(Of T)) Implements IRepository(Of T).GetAllAsync

        Dim query As IQueryable(Of T) = _dbSet
        If Filter IsNot Nothing Then
            query = query.Where(Filter)
        End If
        If IncludeList IsNot Nothing Then
            For Each item In IncludeList
                query = query.Include(item)
            Next
        End If
        Return Await Task.Run(Function() query)
    End Function

    Public Async Function AnyAsync(filter As Expression(Of Func(Of T, Boolean))) As Task(Of Boolean) Implements IRepository(Of T).AnyAsync
        Dim query As IQueryable(Of T) = _dbSet
        Return Await query.AnyAsync(filter)
    End Function

    Public Async Function GetAsync(Filter As Expression(Of Func(Of T, Boolean)), ParamArray IncludeList() As String) As Task(Of T) Implements IRepository(Of T).GetAsync
        Dim query As IQueryable(Of T) = _dbSet
        If IncludeList.Any() Then
            For Each includecolumn In IncludeList
                query = _dbSet.Include(includecolumn)
            Next
        End If
        Return Await query.FirstOrDefaultAsync(Filter)
    End Function
End Class
