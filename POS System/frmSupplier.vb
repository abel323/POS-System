Imports System.Data.OleDb
Public Class frmSupplier
    Dim cmd As New OleDbCommand
    Dim sql As String
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub frmSupplier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim DA As New OleDbDataAdapter
        Dim DT As New DataTable
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                sql = "SELECT * FROM Supplier"
                cmd.Connection = connect()
                cmd.CommandText = sql
                DA.SelectCommand = cmd
                DA.Fill(DT)
                dgvSupplier.DataSource = DT
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
        Dim AdminID As Integer = getAdminID(txtUserName.Text)
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                If txtUserName.Text.Length = 0 Or txtSupplierName.Text.Length = 0 Or txtSupplierPhone.Text.Length = 0 Then
                    MsgBox("One Or More Fields Are Required!")
                Else
                    sql = "INSERT INTO Supplier(Supplier_Name,Supplier_Address,Supplier_Phone,Supplier_Email,Admin)" &
                        "VALUES('" & txtSupplierName.Text & "','" & txtSupplierAddress.Text & "','" & txtSupplierPhone.Text & "','" & txtSupplierEmail.Text & "'," & AdminID & ")"
                    cmd.Connection = connect()
                    cmd.CommandText = sql
                    If cmd.ExecuteNonQuery > 0 Then
                        MsgBox("Supplier Data Added Successfully!", vbInformation)
                        txtID.Clear()
                        txtUserName.Clear()
                        txtSupplierPhone.Clear()
                        txtSupplierName.Clear()
                        txtSupplierEmail.Clear()
                        txtSupplierAddress.Clear()
                        sql = "SELECT * FROM Supplier"
                        cmd.Connection = connect()
                        cmd.CommandText = sql
                        DA.SelectCommand = cmd
                        DA.Fill(DT)
                        dgvSupplier.DataSource = DT
                    Else
                        MsgBox("Unable To Add Supplier Data!", vbCritical)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
    End Sub

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

    Private Sub dgvSupplier_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSupplier.CellContentClick
        If e.RowIndex > -1 Then
            txtID.Text = dgvSupplier.CurrentRow.Cells(0).Value
            txtSupplierName.Text = dgvSupplier.CurrentRow.Cells(1).Value
            txtSupplierAddress.Text = dgvSupplier.CurrentRow.Cells(2).Value
            txtSupplierPhone.Text = dgvSupplier.CurrentRow.Cells(3).Value
            txtSupplierEmail.Text = dgvSupplier.CurrentRow.Cells(4).Value
            txtUserName.Text = dgvSupplier.CurrentRow.Cells(5).Value
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim DA As New OleDbDataAdapter
        Dim DT As New DataTable
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                If txtID.Text.Length = 0 Then
                    MsgBox("Please Select Supplier From The Table Before Process Delete!", vbCritical)
                Else
                    sql = "DELETE FROM Supplier WHERE ID=" & Val(txtID.Text)
                    cmd.Connection = connect()
                    cmd.CommandText = sql
                    If cmd.ExecuteNonQuery > 0 Then
                        MsgBox("Supplier Deleted Successfully!", vbInformation)
                        txtID.Clear()
                        txtUserName.Clear()
                        txtSupplierPhone.Clear()
                        txtSupplierName.Clear()
                        txtSupplierEmail.Clear()
                        txtSupplierAddress.Clear()
                        sql = "SELECT * FROM Supplier"
                        cmd.Connection = connect()
                        cmd.CommandText = sql
                        DA.SelectCommand = cmd
                        DA.Fill(DT)
                        dgvSupplier.DataSource = DT
                    Else
                        MsgBox("Unable To Delete Supplier Data!", vbCritical)
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
                If txtUserName.Text.Length = 0 Or txtSupplierName.Text.Length = 0 Or txtSupplierPhone.Text.Length = 0 Or txtID.Text.Length = 0 Then
                    MsgBox("One Or More Fields Are Required!")
                Else
                    sql = "UPDATE Supplier SET Supplier_Name='" & txtSupplierName.Text & "', " &
                        " Supplier_Address='" & txtSupplierAddress.Text & "', " &
                        " Supplier_Phone='" & txtSupplierPhone.Text & "'," &
                        " Supplier_EMail='" & txtSupplierEmail.Text & "' WHERE ID=" & Val(txtID.Text)
                    cmd.Connection = connect()
                    cmd.CommandText = sql
                    If cmd.ExecuteNonQuery > 0 Then
                        MsgBox("Supplier Data Updated Successfully!", vbInformation)
                        txtID.Clear()
                        txtUserName.Clear()
                        txtSupplierPhone.Clear()
                        txtSupplierName.Clear()
                        txtSupplierEmail.Clear()
                        txtSupplierAddress.Clear()
                        sql = "SELECT * FROM Supplier"
                        cmd.Connection = connect()
                        cmd.CommandText = sql
                        DA.SelectCommand = cmd
                        DA.Fill(DT)
                        dgvSupplier.DataSource = DT
                    Else
                        MsgBox("Unable To Update Supplier Data!", vbCritical)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
    End Sub
End Class