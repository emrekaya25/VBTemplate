Imports FluentValidation
Imports VBTemplate.Entity

Public Class ProductValidator
    Inherits AbstractValidator(Of ProductDTORequest)
    Public Sub New()
        RuleFor(Function(x) x.Name).NotEmpty().WithMessage("İsim boş olamaz")
        RuleFor(Function(x) x.Name).MinimumLength(2).WithMessage("Ürün ismi 2 karakterden büyük olmalıdır.")
    End Sub
End Class
