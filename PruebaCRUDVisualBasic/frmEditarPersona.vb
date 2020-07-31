
Imports System.Data.SqlClient
Public Class frmEditarPersona

    Public idPersona As Integer

    Public Sub New(ByVal id As Integer)

        idPersona = id

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Function ValidarCampos() As Boolean
        Dim mensaje As String = "Llene los campos" + vbCrLf
        Dim resultado As Boolean = True

        If txtCedula.Text.Trim = "" Then
            mensaje += " Campo cedula" + vbCrLf
            resultado = False

        End If

        If txtNombre.Text.Trim = "" Then
            mensaje += " Campo nombre"
            resultado = False

        End If

        If resultado = False Then
            MessageBox.Show(mensaje)

        End If

        Return resultado
    End Function


    Public Sub obtenerUsuarioporId(ByVal id As Integer)
        lblId.Text = idPersona.ToString
        Dim conexion As New AccesoADatos.Conexion
        Dim query As String
        query = "exec SelectPersonaPorId " + id.ToString

        Dim reader As SqlDataReader = conexion.ConsultarBD(query)
        If (reader.HasRows) Then
            While (reader.Read)
                txtCedula.Text = reader.GetString(1)
                txtNombre.Text = reader.GetString(2)
                dtpFechaNacimiento.Text = reader.GetDateTime(3)

                ''asi con las demas
            End While


        End If





    End Sub


    Private Sub frmEditarPersona_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        obtenerUsuarioporId(idPersona)
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        If ValidarCampos() = True Then

            If (MessageBox.Show("Desea editar el Registro?", "Informacion Requerida",
      MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then

                Try
                    Dim conexion As New AccesoADatos.Conexion
                    Dim Query As String
                    Query = "exec EditPersona " + idPersona.ToString() + ", '" + txtCedula.Text + "','" + txtNombre.Text + "','" + dtpFechaNacimiento.Value.ToString("yyyy-MM-dd") + "'"

                    conexion.TransaccionBD(Query)
                    Me.Close()

                    MessageBox.Show("Transaccion realizada con exito", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    MessageBox.Show("Problemas al realizar la transaccion", "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            Else
                MessageBox.Show("No se edito el registro", "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

    End Sub
End Class