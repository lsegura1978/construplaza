Public Class frmencuesta

    Public contrarecibo, strconexionex, parametros, asunto, cuerpo, CLIENTE, FECHA, DOCUMENTO, TIPO, MONEDA, CREDITO, NOMBRE, nombres As String

    Private Sub Btnno_Click(sender As Object, e As EventArgs) Handles Btnno.Click

        If conexion.State = ConnectionState.Closed Then
            conexion.Open()
        End If



        transaccion = conexion.BeginTransaction
        cmdincluir = New SqlClient.SqlCommand

        'tipo
        Dim NOS As String = "NO"

        parametros = "'" & Label6.Text & "','" & NOS & "'"


        With cmdincluir
            .Connection = conexion
            .CommandText = "INSERT INTO ASISTENCIA.DBO.ENCUESTA(EMPLEADO,BANDERA) values (" & parametros & ")"
            .CommandType = CommandType.Text
            .Transaction = transaccion
        End With
        iregincluir = cmdincluir.ExecuteNonQuery
        transaccion.Commit()

        Me.Close()


    End Sub

    Public cmdconsulta, cmdconsulta_documentos As SqlClient.SqlCommand
    Public transaccion, transaccion_documentos As SqlClient.SqlTransaction
    Public cmdincluir, cmdincluir_documentos As SqlClient.SqlCommand
    Public iregincluir, iregincluird, cod, t, y As Integer

    Private Sub btnsi_Click(sender As Object, e As EventArgs) Handles btnsi.Click

        If conexion.State = ConnectionState.Closed Then
            conexion.Open()
        End If



        transaccion = conexion.BeginTransaction
        cmdincluir = New SqlClient.SqlCommand

        'tipo
        Dim si As String = "SI"
        parametros = "'" & Label6.Text & "','" & si & "'"


        With cmdincluir
            .Connection = conexion
            .CommandText = "INSERT INTO ASISTENCIA.DBO.ENCUESTA(EMPLEADO,BANDERA) values (" & parametros & ")"
            .CommandType = CommandType.Text
            .Transaction = transaccion
        End With
        iregincluir = cmdincluir.ExecuteNonQuery
        transaccion.Commit()

        Me.Close()


    End Sub

    Public MONTO, SALDO As Decimal
    Public conexion As SqlClient.SqlConnection
    Public conexionbi As SqlClient.SqlConnection
    Public daconsulta, daconsulta_documentos As SqlClient.SqlDataAdapter
    Public dsconsulta, dsconsulta_documentos As DataSet


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub frmencuesta_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        '---------------------------------------------abro conexion bd----------------------------------------------------------------------

        strconexionex = "Data Source=DIGEMA0020080\BDAPP01;Initial Catalog=exactus;User Id=sa;Password=Igoloncet!2"
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




    End Sub
End Class