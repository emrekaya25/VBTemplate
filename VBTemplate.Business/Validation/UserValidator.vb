Imports FluentValidation
Imports VBTemplate.Entity

Public Class UserValidator
    Inherits AbstractValidator(Of UserDTORequest)
    Public Sub New()
        RuleFor(Function(x) x.Name).NotEmpty().WithMessage("Kullanıcı ismi boş olamaz.")
        RuleFor(Function(x) x.Surname).NotEmpty().WithMessage("Kullanıcı soy adı boş olamaz.")
        RuleFor(Function(x) x.Email).NotEmpty().WithMessage("Kullanıcı maili boş olamaz.")
        RuleFor(Function(x) x.Password).NotEmpty().WithMessage("Kullanıcı sifresi boş olamaz.")
        RuleFor(Function(e) e.Email).Matches("^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$").WithMessage("Mail formatı yanlış!!")
    End Sub
End Class
