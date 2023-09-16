Imports System.Data.OleDb
Public Class frmSales
    'Dim conn As New OleDbConnection("Provider=Microsoft.Ace.OleDb.12.0; Data source=D:/Access Databases/Point Of Sales.accdb")
    Dim cmd As New OleDbCommand
    Dim sql As String
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub
    'Function that returns admin id
    Private Function getAdminID(userName As String) As Integer
        Dim DR As OleDbDataReader
        Dim ID As Integer = 0
        Try
            connect().Open()
            sql = "SELECT ID FROM Admin WHERE User_Name='" & userName & "'"
            cmd.Connection = connect()
            cmd.CommandText = sql
            DR = cmd.ExecuteReader
            While DR.Read()
                ID = DR.GetValue(0)
            End While
            DR.Close()
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
        Return ID
    End Function
    Private Sub frmSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim DA As New OleDbDataAdapter
        Dim DT As New DataTable
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                sql = "SELECT * FROM Sales"
                cmd.Connection = connect()
                cmd.CommandText = sql
                DA.SelectCommand = cmd
                DA.Fill(DT)
                dgvSales.DataSource = DT
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
    End Sub

    Private Sub dgvSales_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSales.CellContentClick
        If e.RowIndex > -1 Then
            txtID.Text = dgvSales.CurrentRow.Cells(0).Value
            txtFName.Text = dgvSales.CurrentRow.Cells(1).Value
            txtLName.Text = dgvSales.CurrentRow.Cells(2).Value
            txtMName.Text = dgvSales.CurrentRow.Cells(3).Value
            txtDOB.Value = dgvSales.CurrentRow.Cells(4).Value
            txtDOR.Value = dgvSales.CurrentRow.Cells(5).Value
            txtPNumber.Text = dgvSales.CurrentRow.Cells(6).Value
            txtEMail.Text = dgvSales.CurrentRow.Cells(7).Value
            txtUName.Text = dgvSales.CurrentRow.Cells(8).Value
            txtPassword.Text = dgvSales.CurrentRow.Cells(9).Value
        End If
    End Sub


    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim DA As New OleDbDataAdapter
        Dim DT As New DataTable
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                If txtID.Text.Length = 0 Then
                    MsgBox("Please Select Sales Person From The Table Before Deleting!", vbCritical)
                Else
                    sql = "DELETE FROM Sales WHERE ID=" & CInt(txtID.Text)
                    cmd.Connection = connect()
                    cmd.CommandText = sql
                    If cmd.ExecuteNonQuery > 0 Then
                        MsgBox("Sales Person Deleted Successfully!", vbInformation)
                        txtID.Clear()
                        txtFName.Clear()
                        txtMName.Clear()
                        txtLName.Clear()
                        txtDOB.ResetText()
                        txtDOR.ResetText()
                        txtEMail.Clear()
                        txtPNumber.Clear()
                        txtAdmin.Clear()
                        txtUName.Clear()
                        sql = "SELECT * FROM Sales"
                        cmd.CommandText = sql
                        DA.SelectCommand = cmd
                        DA.Fill(DT)
                        dgvSales.DataSource = DT
                    Else
                        MsgBox("Unable To Delete Sales!", vbCritical)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        Dim DA As New OleDbDataAdapter
        Dim DT As New DataTable
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                If txtID.Text.Length = 0 Or txtFName.Text.Length = 0 Or txtLName.Text.Length = 0 Or txtPNumber.Text.Length = 0 Or txtEMail.Text.Length = 0 Or txtUName.Text.Length = 0 Or txtPassword.Text.Length < 6 Then
                    MsgBox("One or more fields are required or doesn't meet the requirement!", vbCritical)
                Else
                    sql = "UPDATE Sales SET First_Name='" & txtFName.Text & "', " &
                        "Last_Name='" & txtLName.Text & "', " &
                        "Middle_Name='" & txtMName.Text & "', " &
                        "Date_Of_Birth='" & txtDOB.Value & "', " &
                        "Date_Of_Registration='" & txtDOR.Value & "', " &
                        "Phone_Number='" & txtPNumber.Text & "', " &
                        "EMail='" & txtEMail.Text & "', " &
                        "User_Name='" & txtUName.Text & "', " &
                        "UPassword='" & txtPassword.Text & "' " &
                        "WHERE ID=" & CInt(txtID.Text)
                    cmd.Connection = connect()
                    cmd.CommandText = sql
                    If cmd.ExecuteNonQuery > 0 Then
                        MsgBox("Sales Person Data Updated Successfully!", vbInformation)
                        txtID.Clear()
                        txtFName.Clear()
                        txtMName.Clear()
                        txtLName.Clear()
                        txtDOB.ResetText()
                        txtDOR.ResetText()
                        txtEMail.Clear()
                        txtPNumber.Clear()
                        txtAdmin.Clear()
                        txtUName.Clear()
                        sql = "SELECT * FROM Sales"
                        cmd.CommandText = sql
                        DA.SelectCommand = cmd
                        DA.Fill(DT)
                        dgvSales.DataSource = DT
                    Else
                        MsgBox("Unable To Update Sales Data!", vbCritical)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim DA As New OleDbDataAdapter
        Dim DT As New DataTable
        Dim adminID As Integer = getAdminID(txtAdmin.Text)
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                sql = "INSERT INTO Sales(First_Name,Last_Name,Middle_Name,Date_Of_Birth,Date_Of_Registration,Phone_Number,EMail,User_Name,UPassword,Admin) VALUES('" &
                    txtFName.Text & "','" & txtLName.Text & "','" & txtMName.Text & "','" & txtDOB.Text & "','" & txtDOR.Text & "','" & txtPNumber.Text & "','" & txtEMail.Text & "','" &
                    txtUName.Text & "','" & txtPassword.Text & "'," & adminID & ")"
                cmd.Connection = connect()
                cmd.CommandText = sql
                If cmd.ExecuteNonQuery > 0 Then
                    MsgBox("Sales Data Saved Successfully!", vbInformation)
                    txtID.Clear()
                    txtFName.Clear()
                    txtMName.Clear()
                    txtLName.Clear()
                    txtDOB.ResetText()
                    txtDOR.ResetText()
                    txtEMail.Clear()
                    txtPNumber.Clear()
                    txtAdmin.Clear()
                    txtUName.Clear()
                    sql = "SELECT * FROM Sales"
                    cmd.CommandText = sql
                    DA.SelectCommand = cmd
                    DA.Fill(DT)
                    dgvSales.DataSource = DT
                Else
                    MsgBox("Unable To Save Sales Data!", vbCritical)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
    End Sub
End Class