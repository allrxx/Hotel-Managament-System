Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient

Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initially hide these GroupBoxes
        Guna2GroupBox2.Visible = False
        Guna2GroupBox3.Visible = False

        ' Load data into ComboBoxes
        LoadCustomerIDs()
        LoadRoomIDs()
        LoadCustomerIDss()
        ' Load payment methods into ComboBox
        LoadPaymentMethods()
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        ' Toggle visibility of GroupBoxes
        Guna2GroupBox2.Visible = True
        Guna2GroupBox3.Visible = False
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        ' Toggle visibility of GroupBoxes
        Guna2GroupBox3.Visible = True
        Guna2GroupBox2.Visible = False
    End Sub

    Private Sub LoadCustomerIDs()
        ' Your connection string goes here
        Dim connectionString As String = "server=localhost; database=hms; uid=root; pwd=admin;"
        Dim query As String = "SELECT cusid FROM customer"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                Try
                    connection.Open()
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            ' Assuming Guna2ComboBox1 is the customer ID selector
                            Guna2ComboBox1.Items.Add(reader("cusid").ToString())
                        End While
                    End Using
                Catch ex As Exception
                    MessageBox.Show($"Failed to load customer IDs: {ex.Message}")
                End Try
            End Using
        End Using
    End Sub
    Private Sub LoadCustomerIDss()
        ' Your connection string goes here
        Dim connectionString As String = "server=localhost; database=hms; uid=root; pwd=admin;"
        Dim query As String = "SELECT cusid FROM customer"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                Try
                    connection.Open()
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            ' Assuming Guna2ComboBox1 is the customer ID selector
                            Guna2ComboBox4.Items.Add(reader("cusid").ToString())
                        End While
                    End Using
                Catch ex As Exception
                    MessageBox.Show($"Failed to load customer IDs: {ex.Message}")
                End Try
            End Using
        End Using
    End Sub

    Private Sub LoadRoomIDs()
        ' Your connection string goes here
        Dim connectionString As String = "server=localhost; database=hms; uid=root; pwd=admin;"
        Dim query As String = "SELECT roomid FROM rooms"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                Try
                    connection.Open()
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            ' Assuming Guna2ComboBox2 is the room ID selector
                            Guna2ComboBox2.Items.Add(reader("roomid").ToString())
                        End While
                    End Using
                Catch ex As Exception
                    MessageBox.Show($"Failed to load room IDs: {ex.Message}")
                End Try
            End Using
        End Using
    End Sub

    Private Sub LoadPaymentMethods()
        ' Add payment methods to ComboBox
        Guna2ComboBox3.Items.AddRange({"Card", "Net Banking", "UPI"})
    End Sub

    Private Sub Guna2Button11_Click(sender As Object, e As EventArgs) Handles Guna2Button11.Click
        Dim connectionString As String = "server=localhost; database=hms; uid=root; pwd=admin;"
        ' Assuming your booking table structure matches the fields below
        Dim query As String = "INSERT INTO booking (cusid, bkdays, roomid) VALUES (@cusid, @bkdays, @roomid)"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                ' Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@cusid", Guna2ComboBox1.SelectedItem.ToString())
                command.Parameters.AddWithValue("@bkdays", Guna2TextBox3.Text)
                command.Parameters.AddWithValue("@roomid", Guna2ComboBox2.SelectedItem.ToString())

                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Booking successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show($"Failed to book: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    connection.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        ' Validate bkdays input
        Dim bkdays As Integer
        If Not Integer.TryParse(Guna2TextBox1.Text, bkdays) Then
            MessageBox.Show("Please enter a valid number of days.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If

        ' Calculate total price
        Dim totalPrice As Integer = bkdays * 500 ' Assuming the rate is 500 per day
        Guna2TextBox2.Text = totalPrice.ToString()

        ' Insert data into checkout table
        Dim connectionString As String = "server=localhost; database=hms; uid=root; pwd=admin;"
        Dim query As String = "INSERT INTO checkout (cusid, total_price, payment_method) VALUES (@cusid, @total_price, @payment_method)"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                ' Add parameters to prevent SQL injection
                command.Parameters.AddWithValue("@cusid", Guna2ComboBox4.SelectedItem.ToString())
                command.Parameters.AddWithValue("@total_price", Guna2TextBox2.Text)
                command.Parameters.AddWithValue("@payment_method", Guna2ComboBox3.SelectedItem.ToString())
                ' You need to map roomid from the booking table as in booking table, but it's not clear from your description. You should add it here accordingly.

                Try
                    connection.Open()
                    command.ExecuteNonQuery()
                    MessageBox.Show("Checkout successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show($"Failed to checkout: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Finally
                    connection.Close()
                End Try
            End Using
        End Using
    End Sub

    Private Sub Guna2ComboBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox4.SelectedIndexChanged
        ' Load bkdays into Guna2TextBox1 based on the selected cusid from Guna2ComboBox4
        Dim selectedCusID As String = Guna2ComboBox4.SelectedItem.ToString()
        Dim connectionString As String = "server=localhost; database=hms; uid=root; pwd=admin;"
        Dim query As String = "SELECT bkdays FROM booking WHERE cusid = @cusid"

        Using connection As New MySqlConnection(connectionString)
            Using command As New MySqlCommand(query, connection)
                command.Parameters.AddWithValue("@cusid", selectedCusID)
                Try
                    connection.Open()
                    Dim bkdaysObj As Object = command.ExecuteScalar()
                    If bkdaysObj IsNot Nothing Then
                        Dim bkdays As Integer = Convert.ToInt32(bkdaysObj)
                        Guna2TextBox1.Text = bkdays.ToString()

                        ' Calculate total price
                        Dim totalPrice As Integer = bkdays * 500 ' Assuming the rate is 500 per day
                        Guna2TextBox2.Text = totalPrice.ToString()
                    Else
                        MessageBox.Show("No booking found for the selected customer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                Catch ex As Exception
                    MessageBox.Show($"Failed to load booking details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End Using
        End Using
    End Sub

    Private Sub Guna2Button6_Click(sender As Object, e As EventArgs) Handles Guna2Button6.Click
        Me.Hide()
        Form2.Show()
    End Sub
End Class
