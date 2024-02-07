Imports System.Numerics
Imports Serilog

Public Class LogHelper(Of T)

    Public Sub LogInsert(entity As T, tableName As String, operationType As String, userId As Int64, mail As String)

        Log.ForContext("UserId", userId) _
            .ForContext("TableName", tableName) _
            .ForContext("OperationType", operationType) _
            .ForContext("Time", DateTime.Now) _
            .Information("{@Data}", entity)
    End Sub

    ' Diğer loglama metotları eklenebilir...

End Class
