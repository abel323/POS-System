Imports System.Data.OleDb
Public Class frmReport
    Dim cmd As New OleDbCommand
    Dim sql As String

    'Function that count and return total category
    Private Function totalCategory() As Integer
        Dim counter As Integer = 0
        Dim DR As OleDbDataReader
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                sql = "SELECT * FROM Category"
                cmd.Connection = connect()
                cmd.CommandText = sql
                DR = cmd.ExecuteReader
                While DR.Read()
                    counter = counter + 1
                End While
                DR.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
        Return counter
    End Function

    'Function that counts available suppliers
    Private Function totalSuppliers() As Integer
        Dim DR As OleDbDataReader
        Dim counter As Integer = 0
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                sql = "SELECT * FROM Supplier"
                cmd.Connection = connect()
                cmd.CommandText = sql
                DR = cmd.ExecuteReader
                While DR.Read()
                    counter = counter + 1
                End While
                DR.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
        Return counter
    End Function

    'Function that counts available sales
    Private Function totalSales() As Integer
        Dim DR As OleDbDataReader
        Dim counter As Integer = 0
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                sql = "SELECT * FROM Sales"
                cmd.Connection = connect()
                cmd.CommandText = sql
                DR = cmd.ExecuteReader
                While DR.Read()
                    counter = counter + 1
                End While
                DR.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
        Return counter
    End Function

    'Function that counts total customers
    Private Function totalCustomer() As Integer
        Dim counter As Integer = 0
        Dim DR As OleDbDataReader
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                sql = "SELECT * FROM Customer"
                cmd.Connection = connect()
                cmd.CommandText = sql
                DR = cmd.ExecuteReader
                While DR.Read()
                    counter = counter + 1
                End While
                DR.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
        Return counter
    End Function

    'Function That Counts Total Products
    Private Function totalProducts() As Integer
        Dim counter As Integer = 0
        Dim DR As OleDbDataReader
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                sql = "SELECT * FROM Product WHERE Quantity_In_Stock>0"
                cmd.Connection = connect()
                cmd.CommandText = sql
                DR = cmd.ExecuteReader
                While DR.Read()
                    counter = counter + 1
                End While
                DR.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
        Return counter
    End Function
    'Function That Counts Total Sold
    Private Function totalSold() As Integer
        Dim counter As Integer = 0
        Dim DR As OleDbDataReader
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                sql = "SELECT SUM(Invoice.Total) AS TotalSold FROM Invoice"
                cmd.Connection = connect()
                cmd.CommandText = sql
                DR = cmd.ExecuteReader
                While DR.Read()
                    counter = DR.GetValue(0)
                End While
                DR.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
        Return counter
    End Function

    Private Sub frmReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblCategory.Text &= totalCategory()
        lblSupplier.Text &= totalSuppliers()
        lblSales.Text &= totalSales()
        lblCustomer.Text &= totalCustomer()
        lblProduct.Text &= totalProducts()
        lblSold.Text &= totalSold()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub
End Class