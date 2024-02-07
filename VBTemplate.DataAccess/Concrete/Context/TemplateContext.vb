Imports Microsoft.EntityFrameworkCore
Imports VBTemplate.Entity

Public Class TemplateContext
    Inherits DbContext

    Public Sub New()
    End Sub

    Public Sub New(ByVal options As DbContextOptions(Of TemplateContext))
        MyBase.New(options)
    End Sub

    Public Overridable Property Product As DbSet(Of Product)
    Public Overridable Property User As DbSet(Of User)

    Protected Overrides Sub OnConfiguring(optionsBuilder As DbContextOptionsBuilder)
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-R04PVQ3; Initial Catalog= VBTemplateDB; Integrated Security=True; TrustServerCertificate = true; ")
    End Sub

End Class
