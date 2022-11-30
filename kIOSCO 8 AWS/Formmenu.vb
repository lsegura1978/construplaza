Imports System.Text
Imports SecuGen.FDxSDKPro.Windows
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Public Class Formmenu
    Public objStreamReader As StreamReader
    Public strLine As String
    Public objStreamReader_mensaje As StreamReader
    Public strLine_mensaje As String


    Private Sub btnmarcar_Click(sender As Object, e As EventArgs) Handles btnmarcar.Click
        Label1.Text = "Favor coloque el dedo en el lector y no lo retire"


        Frmmarca.ShowDialog()


    End Sub

    Private Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click

        'Form1.ShowDialog()
        frmvalidarsupervisor.ShowDialog()



    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        LBLHORA.Text = Now
    End Sub


    Private Sub Formmenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LBLHORA.ForeColor = Color.Red

        TextBox1.ForeColor = Color.Gray


        'Pass the file path and the file name to the StreamReader constructor.
        objStreamReader = New StreamReader("C:\kIOSCO 8 AWS\TIENDA.txt")

        'Read the first line of text.
        strLine = objStreamReader.ReadLine

        Me.Text = strLine & "  Copyright © 2021 Grupo Construplaza. Todos los derechos reservados. V 47.0 AWS"

        lblbeta.Text = strLine & " LOCAL V47 Construplaza"



        'Pass the file path and the file name to the StreamReader constructor.
        objStreamReader_mensaje = New StreamReader("C:\kIOSCO 8 AWS\mensaje.txt")

        'Read the first line of text.
        strLine_mensaje = objStreamReader_mensaje.ReadLine

        TextBox1.Text = strLine_mensaje


        objStreamReader_mensaje.Close()
        'Do While Not strLine Is Nothing

        '    strLine = objStreamReader.ReadLine
        'Loop

        'Close the file.
        objStreamReader.Close()


        With Timer1
            .Interval = 1000 ' -- 1 segundo  
            .Enabled = True
        End With

        'Label1.Text = "Favor después de presionar MARCAR coloque su dedo en el lector y no lo retire"

        '  TextBox1.Text = "Linea directa de whatsapp de Recursos Humanos 6167-7827"


        '"Compañeros no olviden que este viernes 3 de abril se van a hacer préstamos, ya está habilitada la opción en el reloj marcador.  Se aceptan solicitudes hasta el jueves 2 de abril a las 11:30 p.m. Adicionalmente, estamos ofreciendo depositar el ahorro navideño adelantado a quienes lo necesiten, coordinar en el celular 6167-7827."

        'TextBox1.Visible = False


        Me.BackColor = Color.Gray
        Me.btnIngresar.BackColor = Color.Gray
        Me.btnmarcar.BackColor = Color.Gray

      

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        '   Label1.Text = "Favor despúes de presionar MARCAR coloque su dedo en el lector y no lo retire"
        frmmarcas_huella.ShowDialog()
    End Sub

    Private Sub btncomprobante_Click(sender As Object, e As EventArgs) Handles btncomprobante.Click
        'frmcomprobantepago.ShowDialog()
        Frmvistacomprobante.ShowDialog()




    End Sub

    Private Sub btnvacaciones_Click(sender As Object, e As EventArgs) Handles btnvacaciones.Click
        Frmvacaciones.ShowDialog()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnmarcasq.Click
        frmmarcas.ShowDialog()

    End Sub

    Private Sub btnmarcasg_Click(sender As Object, e As EventArgs) Handles btnmarcasg.Click
        Frmmarcasg.ShowDialog()
    End Sub

    Private Sub btnaseconstruplaza_Click(sender As Object, e As EventArgs) Handles btnaseconstruplaza.Click

        Frmestadoquarzo.ShowDialog()


        'Frmestadocuentaaseconstruplaza.ShowDialog()
    End Sub

    Private Sub btncreditos_Click(sender As Object, e As EventArgs) Handles btncreditos.Click
        'btn10000.ShowDialog()
        FrmcreditosQuarzo.ShowDialog()

    End Sub

    Private Sub btncalendario_Click(sender As Object, e As EventArgs) Handles btncalendario.Click
        frmcalendario.ShowDialog()
    End Sub

    Private Sub btncircular_Click(sender As Object, e As EventArgs) Handles btncircular.Click
        frmcircular.ShowDialog()
        

    End Sub

    Private Sub Btnferiados_Click(sender As Object, e As EventArgs) Handles Btnferiados.Click
        frmjustificadas.ShowDialog()

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Frmestadoquarzo.ShowDialog()

    End Sub

    Private Sub btnaguinaldo_Click(sender As Object, e As EventArgs) Handles btnaguinaldo.Click
        Frmaguinaldo.ShowDialog()
    End Sub

    Private Sub btnclave_Click(sender As Object, e As EventArgs) Handles btnclave.Click

        Frmtecladovb.ShowDialog()


        'frmmarca_clave.emplea2_encontrado = "0902"
        'frmmarca_clave.ShowDialog()


    End Sub

    Private Sub btnactualiza_Click(sender As Object, e As EventArgs) Handles btnactualiza.Click
        Frmconstancia.ShowDialog()
    End Sub

    Private Sub btnactualizacion_Click(sender As Object, e As EventArgs) Handles btnactualizacion.Click
        Frmactualizaempleado.ShowDialog()

    End Sub

    Private Sub lblbeta_Click(sender As Object, e As EventArgs) Handles lblbeta.Click
        End
    End Sub
End Class