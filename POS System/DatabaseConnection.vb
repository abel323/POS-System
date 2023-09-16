Imports System.Data.OleDb
Module DatabaseConnection
    Dim conn As New OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0; Data source=D:/Access Databases/Point Of Sales.accdb")

    Public Function connect() As OleDbConnection
        Return conn
    End Function
End Module
