Public Class Frmtecladovb
    Public cmdconsulta, cmdconsulta_documentos As SqlClient.SqlCommand
    Public transaccion, transaccion_documentos As SqlClient.SqlTransaction
    Public cmdincluir, cmdincluir_documentos As SqlClient.SqlCommand
    Public iregincluir, iregincluird, cod, t, y As Integer
    Public contrarecibo, strconexionex, parametros, asunto, cuerpo, CLIENTE, FECHA, DOCUMENTO, TIPO, MONEDA, CREDITO, NOMBRE, nombres As String
    Public MONTO, SALDO As Decimal
    Public conexion As SqlClient.SqlConnection
    Public conexionbi As SqlClient.SqlConnection
    Public daconsulta, daconsulta_documentos As SqlClient.SqlDataAdapter
    Public dsconsulta, dsconsulta_documentos As DataSet
    Public contador As Integer = 130
    Public NombreArchivo, empleado As String
    Public contadores As Integer
    Public conta_marcas As Integer = 0
    Public str(16) As String
    Public str2(16) As String
    Public num As Integer
    Public bene, dedu, tota As Decimal
    Public JORNADA As String = "DIURNA"





    Public z, z2, z3, z4, z5, i As Integer
    Public fp_imagen40() As Byte
    Dim m_LedOn As Boolean
    Dim m_ImageWidth As Int32
    Dim m_ImageHeight As Int32
    Dim m_RegMin1(400) As Byte
    Dim m_RegMin2(400) As Byte
    Dim m_VrfMin(400) As Byte
    Dim fp_image() As Byte
    Dim fp_image2() As Byte
    Dim conta As Integer = 0
    Dim obj As Object, obj2 As Object, B(), plantilla1, plantilla2 As Byte
    Dim fp_plantilla() As Byte
    Dim fp_plantilla2() As Byte
    Public encontrado As Boolean = False
    Public emplea2, emplea2_encontrado, emplea2_encontrado_nombre As String
    Dim obj_carga As Object, B_carga() As Byte
    Public empleados, nombres_empleados, identificaciones, ubicaciones, puestos, departamentos, nominas, bancos, cuentas As String
    Public salario As Decimal = 0
    Public deduce As Decimal = 0
    Public totalizado As Decimal = 0
    Public correo As String


    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        txtempleado.Text = ""
        txtempleado.Focus()

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Me.Close()

    End Sub

    Private Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click

        'If txtempleado.Text = "2154" Or txtempleado.Text = "0538" Then
        '    frmmarca_clave.emplea2_encontrado = txtempleado.Text
        '    frmmarca_clave.ShowDialog()
        '    Me.Close()

        'End If
        'If txtempleado.Text <> "2154" Or txtempleado.Text <> "0538" Then
        '    MsgBox("Usted no es un empleado aprob")
        '    Me.Close()

        '  End If

        encontrado = False


        strconexionex = "Data Source=192.168.101.215\BDAPP01;Initial Catalog=exactus;User Id=sa;Password=Igoloncet!2"
        Try
            conexion = New SqlClient.SqlConnection
            conexion.ConnectionString = strconexionex
            conexion.Open()


        Catch exc As SqlClient.SqlException
            Console.WriteLine("Error de conexion")
            End
        Finally
            If Not (conexion.State = ConnectionState.Open) Then
                Console.WriteLine("Error de conexion")
                End
            End If
        End Try



        cmdconsulta = New SqlClient.SqlCommand
        daconsulta = New SqlClient.SqlDataAdapter
        dsconsulta = New DataSet
        With cmdconsulta
            .Connection = conexion
            .CommandText = "SELECT EMPLEADO FROM ASISTENCIA.dbo.SIN_DEDO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleado.Text & "'"
            .CommandType = CommandType.Text
        End With
        daconsulta.SelectCommand = cmdconsulta
        daconsulta.Fill(dsconsulta)


        For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

            encontrado = True
            frmmarca_clave.emplea2_encontrado = txtempleado.Text
            frmmarca_clave.ShowDialog()
            conexion.Close()
            Me.Close()

            Formmenu.btnclave.Enabled = True
        
        Next


        If encontrado = False Then
            MsgBox("Usted no esta asignado para marcar con esta opción", MsgBoxStyle.Critical, AcceptButton)
            Me.Close()

            Formmenu.btnclave.Enabled = True

        End If








    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        txtempleado.Text = txtempleado.Text & "1"

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        txtempleado.Text = txtempleado.Text & "2"
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        txtempleado.Text = txtempleado.Text & "3"
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        txtempleado.Text = txtempleado.Text & "4"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        txtempleado.Text = txtempleado.Text & "5"
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        txtempleado.Text = txtempleado.Text & "6"
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        txtempleado.Text = txtempleado.Text & "7"
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        txtempleado.Text = txtempleado.Text & "8"
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        txtempleado.Text = txtempleado.Text & "9"
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        txtempleado.Text = txtempleado.Text & "0"
    End Sub

    Private Sub Frmtecladovb_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtempleado.Text = ""

    End Sub
End Class