Public Class frmNuevaPersona
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


    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub btnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click

        If ValidarCampos() = True Then
            If (MessageBox.Show("Desea insertar el Registro?", "Informacion Requerida",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then

                Try
                    Dim conexion As New AccesoADatos.Conexion

                    Dim Query As String
                    Query = "exec InsertPersona " + "'" + txtCedula.Text + "','" + txtNombre.Text + "','" + dtpFechaNacimiento.Value.ToString("yyyy-MM-dd") + "'"

                    conexion.TransaccionBD(Query)
                    Me.Close()

                    MessageBox.Show("Transaccion realizada con exito", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    MessageBox.Show("Problemas al realizar la transaccion", "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            Else
                MessageBox.Show("No se inserto el registro", "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If


        End If
    End Sub
End Class