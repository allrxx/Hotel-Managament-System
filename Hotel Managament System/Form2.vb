Imports System.Text
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

    Private Sub Guna2Button7_Click(sender As Object, e As EventArgs) Handles Guna2Button7.Click
        ' Define your MySQL connection string
        Dim connectionString As String = "server=localhost; database=hms; uid=root; pwd=admin;"

        ' SQL query to insert into staff table
        Dim query As String = "INSERT INTO staff (staffname, staffpass) VALUES (@staffname, @staffpass)"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                ' Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@staffname", Guna2TextBox1.Text)
                command.Parameters.AddWithValue("@staffpass", Guna2TextBox2.Text)

                Try
                    ' Open the connection and execute the command
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Staff member added successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    ' If an error occurs, show an error message
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    ' Close the connection
                    connection.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        ' Retrieve data from the checkout table
        Dim connectionString As String = "server=localhost; database=hms; uid=root; pwd=admin;"
        Dim query As String = "SELECT * FROM checkout"

        Dim checkoutData As New StringBuilder()

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                Try
                    connection.Open()
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            ' Format the data for display
                            checkoutData.AppendLine($"Customer ID: {reader("cusid")}, Total Price: {reader("total_price")}, Payment Method: {reader("payment_method")}")
                        End While
                    End Using
                Catch ex As Exception
                    MessageBox.Show($"Failed to retrieve checkout data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End Try
            End Using
        End Using

        ' Display the checkout table data in a message box
        MessageBox.Show(checkoutData.ToString(), "Checkout Table", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        Application.Exit()
    End Sub
End Class