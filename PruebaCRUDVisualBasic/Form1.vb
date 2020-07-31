
Public Class Form1

    Public Id As Integer

    Public Sub PropiedadesGrip()
        Me.dgvPersona.Columns(0).HeaderText = "Id"
        Me.dgvPersona.Columns(2).HeaderText = "Nombre completo"
        Me.dgvPersona.Columns(3).HeaderText = "Fecha nacimiento"
    End Sub

    Public Sub LlenarGrid()
        Dim conexion As New AccesoADatos.Conexion
        Dim query As String = "exec SelectPersona"
        conexion.LlenarGrid(query, dgvPersona)
        PropiedadesGrip()
    End Sub

    'Function ValidarCampos() As Boolean
    '    Dim mensaje As String = "Llene los campos" + vbCrLf
    '    Dim resultado As Boolean = True

    '    If TextBox1.Text.Trim = "" Then
    '        mensaje += " Campo 1" + vbCrLf
    '        resultado = False

    '    End If

    '    If TextBox2.Text.Trim = "" Then
    '        mensaje += " Campo 2"
    '        resultado = False

    '    End If

    '    If resultado = False Then
    '        MessageBox.Show(mensaje)

    '    End If

    '    Return resultado
    'End Function

    'Public Sub llenadoPersona()

    '    For index = 1 To 3
    '        persona.id = 1
    '        persona.nombre = "Carlos"
    '        persona.edad = 22

    '        listaP.Add(persona)

    '    Next


    'End Sub


    'Function Mostrar() As String

    '    Dim resul As String = " Persona" + vbCrLf


    '    For Each item In listaP
    '        'aqui va lo que recorre el for each

    '        resul += "Nombre: " + item.nombre + ", Edad: " + item.edad +
    '            ", ID: " + Convert.ToString(item.id) + vbCrLf

    '    Next

    '    'For index = 0 To listaP.Count

    '    '    resul += "Nombre: " + listaP(index).nombre + ", Edad: " + listaP(index).edad +
    '    '        ", ID: " + Convert.ToString(listaP(index).id) + vbCrLf

    '    'Next



    '    Return resul

    'End Function





    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        LlenarGrid()


    End Sub

    Private Sub btnCrear_Click(sender As Object, e As EventArgs) Handles btnCrear.Click
        Dim frmNuevo As New frmNuevaPersona()
        frmNuevo.ShowDialog()
        LlenarGrid()

    End Sub

    Private Sub SalirToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalirToolStripMenuItem.Click
        Application.Exit()
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        If (dgvPersona.Rows.Count = 0) Then

            MessageBox.Show("Selecione un registro", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Else

            Id = Me.dgvPersona.CurrentRow.Cells(0).Value.ToString()
            Dim frmEditar As New frmEditarPersona(Id)
            frmEditar.ShowDialog()
            LlenarGrid()

        End If

    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If (dgvPersona.Rows.Count = 0) Then

            MessageBox.Show("Selecione un registro", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Else
            If (MessageBox.Show("Desea Eliminar el Registro?", "Informacion Requerida",
      MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then

                Try
                    Dim conexion As New AccesoADatos.Conexion
                    Id = Me.dgvPersona.CurrentRow.Cells(0).Value.ToString

                    Dim Query As String
                    Query = "exec DeletePersona " + Id.ToString

                    conexion.TransaccionBD(Query)
                    LlenarGrid()

                    MessageBox.Show("Transaccion realizada con exito", "Informacion del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Catch ex As Exception
                    MessageBox.Show("Problemas al realizar la transaccion", "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try

            Else
                MessageBox.Show("No se elimino el registro", "Error del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        End If




    End Sub
End Class
