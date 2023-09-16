Imports System.Data.OleDb
Public Class frmLogin
    Dim cmd As New OleDbCommand
    Dim sql As String
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Me.Close()
    End Sub

    Private Sub chkShowPassword_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPassword.CheckedChanged
        If chkShowPassword.Checked Then
            txtPassword.UseSystemPasswordChar = False
        Else
            txtPassword.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If cboUserType.SelectedItem.ToString = "Admin" Then
            Admin_Login(txtUserName.Text, txtPassword.Text)
        ElseIf cboUserType.SelectedItem.ToString = "Sales" Then
            Sales_Login(txtUserName.Text, txtPassword.Text)
        End If
    End Sub

    'Admin Login Function
    Private Sub Admin_Login(userName As String, password As String)
        Dim DR As OleDbDataReader
        Dim counter As Integer = 0
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                sql = "SELECT User_Name,Password FROM Admin WHERE User_Name='" & userName & "' AND Password='" & password & "'"
                cmd.Connection = connect()
                cmd.CommandText = sql
                DR = cmd.ExecuteReader()
                While DR.Read()
                    counter = counter + 1
                End While
                DR.Close()
                If counter = 1 Then
                    txtUserName.Clear()
                    txtPassword.Clear()
                    cboUserType.ResetText()
                    MsgBox("Logged In Successfully!", vbInformation)
                    frmAdminDashboard.Show()
                    Me.Hide()
                Else
                    MsgBox("Invalid Creditionals Or User Type!", vbCritical)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
    End Sub

    'Sales Person Login Handler
    Private Sub Sales_Login(userName As String, password As String)
        Dim DR As OleDbDataReader
        Dim counter As Integer = 0
        Try
            connect().Open()
            If connect().State = ConnectionState.Open Then
                sql = "SELECT User_Name,UPassword FROM Sales WHERE User_Name='" & userName & "' AND UPassword='" & password & "'"
                cmd.Connection = connect()
                cmd.CommandText = sql
                DR = cmd.ExecuteReader()
                While DR.Read()
                    counter = counter + 1
                End While
                DR.Close()
                If counter = 1 Then
                    txtUserName.Clear()
                    txtPassword.Clear()
                    cboUserType.ResetText()
                    MsgBox("Logged In Successfully!", vbInformation)
                Else
                    MsgBox("Invalid Creditionals Or User Type!", vbCritical)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message(), vbCritical)
        Finally
            connect().Close()
        End Try
    End Sub
End Class