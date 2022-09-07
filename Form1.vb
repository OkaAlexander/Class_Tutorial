Imports System.Data.SqlClient

Public Class Form1
    'connecting your app to the database'
    Dim con As New SqlConnection("Data Source=OKAH;Initial Catalog=mrsellerDB;Integrated Security=True")
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click

        'declare variables that will take values from the various form control'
        Dim pId As Integer = txtpid.Text
        Dim pNmae As String = txtpname.Text
        Dim buyPrice As Double = txtbuyprice.Text
        Dim sellPrice As Double = txtproductsellprice.Text
        Dim pexpireDate As DateTime = txtdatetimepicker.Text
        Dim warriantyType As String = cmdwarriantytype.Text
        Dim productType As String = ""
        If radioforeignproduct.Checked = True Then
            productType = "Foreign product"
        Else
            productType = "Local product"

        End If

        'you must open the db first before u insert, close it after insert'
        'sql command to add data to your product taype'
        Try

            con.Open()
            Dim command As New SqlCommand("insert into ProductT values('" & pId & "','" & pNmae & "','" & buyPrice & "','" & sellPrice & "','" & pexpireDate & "','" & warriantyType & "','" & productType & "')", con)
            'now let execute our command'
            command.ExecuteNonQuery()
            con.Close()
            LoadDataInGrid()
            MessageBox.Show("Product add successfully..")
            txtpid.Clear()
            txtpname.Clear()
            txtbuyprice.Clear()
        Catch ex As Exception
            Throw New Exception(ex.Message & " Not again!")

        End Try
    End Sub
    'let display our data in a grid by creating a sub class'
    Private Sub LoadDataInGrid()
        Dim command As New SqlCommand("select * from ProductT", con)
        'Dim command As New SqlCommand("select * from ProductT")
        Dim sda As New SqlDataAdapter(Command)
        Dim dt As New DataTable
        sda.Fill(dt)
        DataGridView1.DataSource = dt
        'now call your class which is LoadDataInGrid() under the form load and also under the add botton event'
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadDataInGrid()
    End Sub

    Private Sub btnupdate_Click(sender As Object, e As EventArgs) Handles btnupdate.Click
        Dim pId As Integer = txtpid.Text
        Dim pNmae As String = txtpname.Text
        Dim buyPrice As Double = txtbuyprice.Text
        Dim sellPrice As Double = txtproductsellprice.Text
        Dim pexpireDate As DateTime = txtdatetimepicker.Text
        Dim warriantyType As String = cmdwarriantytype.Text
        Dim productType As String = ""
        If radioforeignproduct.Checked = True Then
            productType = "Foreign product"
        Else
            productType = "Local product"

        End If

        Try

            con.Open()
            Dim command As New SqlCommand("update ProductT  set productID='" & pId & "',productName='" & pNmae & "', buyPrice='" & buyPrice & "', sellPrice='" & sellPrice & "', productExpiredate='" & pexpireDate & "',warratyType='" & warriantyType & "', productType='" & productType & "' where productID='" & pId & "'", con)
            'now let execute our command'
            command.ExecuteNonQuery()
            con.Close()
            LoadDataInGrid()
            MessageBox.Show("Product updated..")
        Catch ex As Exception
            Throw New Exception(ex.Message & " Not again!")

        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If MessageBox.Show("are u sure u want to delete the selected record", "warning", MessageBoxButtons.YesNo) = DialogResult.Yes Then

            Dim pId As Integer = txtpid.Text
            con.Open()
            Dim command As New SqlCommand("Delete ProductT  where productID='" & pId & "'", con)
            'now let execute our command'
            command.ExecuteNonQuery()
            MessageBox.Show("record deleted succefully")
            con.Close()
            LoadDataInGrid()
        End If


    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim pId As Integer = txtsearch.Text
        Dim command As New SqlCommand("select * from ProductT where productID='" & pId & "'", con)
        Dim sda As New SqlDataAdapter(command)
        Dim dt As New DataTable
        sda.Fill(dt)
        DataGridView1.DataSource = dt
    End Sub


End Class
