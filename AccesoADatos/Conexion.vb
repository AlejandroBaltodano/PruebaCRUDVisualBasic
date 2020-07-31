Imports System
Imports System.Data.SqlClient
Imports System.Data
Imports System.Windows.Forms


Public Class Conexion

    Dim connection As SqlConnection


    Public Sub New()
        'this is a Conexion constructor
        connection = New SqlConnection("Data Source = REYES;Initial Catalog=CRUDPersonaVB;User ID=sa;Password=sa")

    End Sub



    Public Function Conectar() As SqlConnection
        Try
            Desconectar()
        Catch ex As Exception
            Throw

        End Try
        connection.Open()
        Return connection
    End Function


    Public Sub Desconectar()
        connection.Close()
    End Sub


    Public Function ConsultarBD(ByVal consulta As String) As SqlDataReader
        Conectar()

        Dim comando As New SqlCommand(consulta, connection)

        Return comando.ExecuteReader()

    End Function


    Public Sub TransaccionBD(ByVal consulta As String)
        Conectar()

        Dim comando As New SqlCommand(consulta, connection)

        comando.ExecuteNonQuery()
        Desconectar()

    End Sub

    Public Function LlenarGrid(ByVal query As String, ByVal grid As DataGridView)
        Conectar()
        Dim comando = New SqlCommand(query, connection)
        comando.CommandTimeout = 0

        Dim adapter = New SqlDataAdapter(comando)
        Dim ds = New DataSet()

        adapter.Fill(ds)
        grid.DataSource = ds.Tables(0)


        Desconectar()

        Return ds

    End Function

    Public Function LlenarCombo(ByVal query As String, ByVal id As String, ByVal descripcion As String, ByVal nombretabla As String, ByVal ComboBox As ComboBox) As DataSet
        Conectar()
        Dim comando As New SqlCommand(query, connection)

        comando.CommandTimeout = 0


        Dim adapter As New SqlDataAdapter(comando)
        Dim ds As New DataSet()
        adapter.Fill(ds, nombretabla)

        ComboBox.DataSource = ds.Tables(0).DefaultView
        ComboBox.ValueMember = id
        ComboBox.DisplayMember = descripcion


        Desconectar()
        Return ds


    End Function


End Class
