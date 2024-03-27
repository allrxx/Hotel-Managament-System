Imports MySql.Data.MySqlClient

Public Class Form1
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        ' Connection string for MySQL database
        Dim connectionString As String = "server=localhost;user=root;password=admin;database=hms;"
        ' SQL query to check if the staff exists with the given name and password.
        Dim query As String = "SELECT COUNT(*) FROM staff WHERE staffname=@staffname AND staffpass=@staffpass"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                ' Replace Guna2TextBox1 and Guna2TextBox2 with the actual names of your text boxes if different.
                command.Parameters.AddWithValue("@staffname", Guna2TextBox1.Text)
                command.Parameters.AddWithValue("@staffpass", Guna2TextBox2.Text)
                Try
                    connection.Open()
                    Dim result As Integer = Convert.ToInt32(command.ExecuteScalar())
                    If result > 0 Then
                        ' Login is successful.
                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        ' Login failed.
                        MessageBox.Show("Login failed. Please check your username and password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Catch ex As Exception
                    ' Handle any errors that might have occurred.
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    connection.Close()
                End Try
            End Using
        End Using
    End Sub
End Class
