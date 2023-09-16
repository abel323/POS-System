Imports System.Data.OleDb
Public Class frmCategory
    Dim cmd As New OleDbCommand
    Dim sql As String
    Private Sub frmCategory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim DA As New OleDbDataAdapter
        Dim DT As New DataTable
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                sql = "SELECT * FROM Category"
                cmd.Connection = connect()
                cmd.CommandText = sql
                DA.SelectCommand = cmd
                DA.Fill(DT)
                dgvCategory.DataSource = DT
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim DA As New OleDbDataAdapter
        Dim DT As New DataTable
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                If txtCategoryName.Text.Length = 0 Then
                    MsgBox("Catagory Name Is Required!", vbCritical)
                Else
                    sql = "INSERT INTO Category(Category_Name) VALUES('" & txtCategoryName.Text & "')"
                    cmd.Connection = connect()
                    cmd.CommandText = sql
                    If cmd.ExecuteNonQuery > 0 Then
                        MsgBox("Category Added Successfuly!", vbInformation)
                        txtCategoryName.Clear()
                        txtCategoryID.Clear()
                        sql = "SELECT * FROM Category"
                        cmd.CommandText = sql
                        DA.SelectCommand = cmd
                        DA.Fill(DT)
                        dgvCategory.DataSource = DT
                    Else
                        MsgBox("Unable To Add Category!", vbCritical)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
    End Sub

    Private Sub dgvCategory_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvCategory.CellContentClick
        If e.RowIndex > -1 Then
            txtCategoryID.Text = dgvCategory.CurrentRow.Cells(0).Value
            txtCategoryName.Text = dgvCategory.CurrentRow.Cells(1).Value
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        Dim DA As New OleDbDataAdapter
        Dim DT As New DataTable
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                If txtCategoryID.Text.Length = 0 Then
                    MsgBox("Please Select Category From The Table!", vbCritical)
                Else
                    sql = "DELETE FROM Category WHERE ID=" & CInt(txtCategoryID.Text)
                    cmd.Connection = connect()
                    cmd.CommandText = sql
                    If cmd.ExecuteNonQuery > 0 Then
                        MsgBox("Category Deleted Successfully!", vbInformation)
                        txtCategoryID.Clear()
                        txtCategoryName.Clear()
                        sql = "SELECT * FROM Category"
                        cmd.CommandText = sql
                        DA.SelectCommand = cmd
                        DA.Fill(DT)
                        dgvCategory.DataSource = DT
                    Else
                        MsgBox("Unable To Delete Category Data!", vbCritical)
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
                If txtCategoryID.Text.Length = 0 Then
                    MsgBox("Please Select Category From The Table!", vbCritical)
                Else
                    sql = "UPDATE Category SET Category_Name='" & txtCategoryName.Text & "' WHERE ID=" & CInt(txtCategoryID.Text)
                    cmd.Connection = connect()
                    cmd.CommandText = sql
                    If cmd.ExecuteNonQuery > 0 Then
                        MsgBox("Category Edited Successfully!", vbInformation)
                        txtCategoryID.Clear()
                        txtCategoryName.Clear()
                        sql = "SELECT * FROM Category"
                        cmd.CommandText = sql
                        DA.SelectCommand = cmd
                        DA.Fill(DT)
                        dgvCategory.DataSource = DT
                    Else
                        MsgBox("Unable To Edit Category Data!", vbCritical)
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