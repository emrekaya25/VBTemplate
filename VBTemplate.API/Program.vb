Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.Identity
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports Serilog
Imports Serilog.Sinks.MSSqlServer


Module Program
    Sub Main(args As String())
        Dim builder = WebApplication.CreateBuilder(args)


        ' Add services to the container.
        builder.Services.AddControllers()

        ' Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer()
        builder.Services.AddSwaggerGen()

        Log.Logger = New LoggerConfiguration() _
    .MinimumLevel.Debug() _
    .WriteTo.File("logs/myLog-.txt", rollingInterval:=RollingInterval.Day) _
    .WriteTo.Console()
        '.WriteTo.MSSqlServer(connectionString:="Data Source=DESKTOP-R04PVQ3\SQLEXPRESS; Initial Catalog=AhlTeknolojiDB; Integrated Security=True; TrustServerCertificate=true;",
        '                    tableName:="Logs",
        '                    autoCreateSqlTable:=False,
        '                    columnOptions:=New ColumnOptions() With {
        '                        .Store = New ObjectModel.Collection(Of StandardColumn)(),
        '                        .AdditionalColumns = New ObjectModel.Collection(Of SqlColumn) From {
        '                            New SqlColumn("Id", System.Data.SqlDbType.Int, 50),
        '                            New SqlColumn("Message", System.Data.SqlDbType.NVarChar, -1),
        '                            New SqlColumn("TableName", System.Data.SqlDbType.NVarChar, 50),
        '                            New SqlColumn("UserId", System.Data.SqlDbType.BigInt),
        '                            New SqlColumn("UserMail", System.Data.SqlDbType.NVarChar, 50),
        '                            New SqlColumn("OperationType", System.Data.SqlDbType.NVarChar, 50),
        '                            New SqlColumn("Time", System.Data.SqlDbType.DateTime, 50)
        '                        }
        '                    }
        ') _
        '.CreateLogger()

        Dim app = builder.Build()

        ' Configure the HTTP request pipeline.
        If app.Environment.IsDevelopment() Then
            app.UseSwagger()
            app.UseSwaggerUI()
        End If


        app.UseHttpsRedirection()
        app.UseCors(Sub(opt) opt.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
        app.MapControllers()

        app.Run()
    End Sub
End Module