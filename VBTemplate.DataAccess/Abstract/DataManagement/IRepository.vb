Imports System.Linq.Expressions
Imports Microsoft.EntityFrameworkCore.ChangeTracking
Imports VBTemplate.Entity

Public Interface IRepository(Of T As BaseEntity)
    Function GetAsync(ByVal Filter As Expression(Of Func(Of T, Boolean)), ParamArray IncludeList As String()) As Task(Of T)
    Function GetAllAsync(ByVal Optional Filter As Expression(Of Func(Of T, Boolean)) = Nothing, Optional IncludeList As String() = Nothing) As Task(Of IEnumerable(Of T))
    Function AnyAsync(filter As Expression(Of Func(Of T, Boolean))) As Task(Of Boolean)
    Sub Add(entity As T)
    Sub Delete(entity As T)
    Sub Update(entity As T)
End Interface