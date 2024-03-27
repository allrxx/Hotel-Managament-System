Imports MySql.Data.MySqlClient

Public Class Form2
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2GroupBox2.Visible = False
        Guna2GroupBox3.Visible = False

        Guna2ComboBox1.Items.Add("Aadhar Card")
        Guna2ComboBox1.Items.Add("Driving License")
        Guna2ComboBox1.Items.Add("Voter ID")
        Guna2ComboBox1.Items.Add("PAN Card")
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Guna2GroupBox2.Visible = True
        Guna2GroupBox3.Visible = False
    End Sub


    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Form3.Show()
        Me.Hide()
    End Sub
    Private Sub Guna2Button11_Click(sender As Object, e As EventArgs) Handles Guna2Button11.Click
        ' Define your connection string here
        Dim connectionString As String = "server=localhost; database=hms; uid=root; pwd=admin;"

        ' SQL query to insert customer data
        Dim query As String = "INSERT INTO customer (cusname, cusage, cusproof, cusphoneno) VALUES (@cusname, @cusage, @cusproof, @cusphoneno)"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                ' Replace Guna2TextBox and Guna2ComboBox with the actual names if they are different.
                command.Parameters.AddWithValue("@cusname", Guna2TextBox4.Text)
                command.Parameters.AddWithValue("@cusage", Guna2TextBox3.Text)
                command.Parameters.AddWithValue("@cusproof", Guna2ComboBox1.SelectedItem.ToString())
                command.Parameters.AddWithValue("@cusphoneno", Guna2TextBox5.Text)
                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Customer added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    ' Handle any errors that might have occurred
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    connection.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        Guna2GroupBox3.Visible = True
        Guna2GroupBox2.Visible = False
    End Sub
End Class