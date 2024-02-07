Imports Microsoft.AspNetCore.Mvc.Filters

Public Class ValidationFilter
    Inherits Attribute
    Implements IAsyncActionFilter
    Private _validatorType As Type

    Public Sub New(validatorType As Type)
        _validatorType = validatorType
    End Sub

    Public Async Function OnActionExecutionAsync(context As ActionExecutingContext, [next] As ActionExecutionDelegate) As Task Implements IAsyncActionFilter.OnActionExecutionAsync
        ValidationHelper.Validate(_validatorType, context.ActionArguments.Values.ToArray())
        Dim executedContext = Await [next]()
    End Function
End Class
