Imports Microsoft.EntityFrameworkCore

Public Class TemplateContext
    Inherits DbContext

    Public Sub New()
    End Sub

    Public Sub New(ByVal options As DbContextOptions(Of TemplateContext))
        MyBase.New(options)
    End Sub

    Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-R04PVQ3; Initial Catalog= VBTemplateDB; Integrated Security=True; TrustServerCertificate = true; ")
    End Sub

End Class
