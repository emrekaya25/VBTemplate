Imports System.Linq.Expressions
Imports VBTemplate.Entity

Public Interface IUserService
    Inherits IGenericService(Of UserDTORequest, UserDTOResponse)
    Function GetLoggedUser(Filter As Expression(Of Func(Of User, Boolean)), ParamArray ByVal includeProperties As String()) As Task(Of User)

End Interface
