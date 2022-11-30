Imports System.Text
Imports SecuGen.FDxSDKPro.Windows
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
'Imports System.Text
'Imports SecuGen.FDxSDKPro.Windows
'Imports System.Data.SqlClient
'Imports System.IO
'Imports System.Data
'Imports System.Drawing.Imaging
'Imports System.Runtime.InteropServices


Imports iTextSharp.text
Imports iTextSharp.text.pdf



Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime


Public Class frmcomprobantepago
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

    Public z, z2, z3, z4, z5, i As Integer
    Public fp_imagen40() As Byte
    Dim m_FPM As SGFingerPrintManager
    Dim m_LedOn As Boolean
    Dim m_ImageWidth As Int32
    Dim m_ImageHeight As Int32
    Dim m_RegMin1(400) As Byte
    Dim m_RegMin2(400) As Byte
    Dim m_VrfMin(400) As Byte
    Dim m_DevList() As SGFPMDeviceList
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



    Public imagen As iTextSharp.text.Image 'declaración de imagen
    





    Private Sub frmcomprobantepago_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try


            salario = 0

            Formmenu.btncomprobante.Enabled = False



            '------------------------------------------abro el dispositivo------------------------------------------------------------------
            System.Threading.Thread.Sleep(3000)
            Dim bandera_error As Boolean = False



            Dim iError As Int32

            ' Me.BackColor = Color.Gray


            m_LedOn = False
            'comboBoxSecuLevel_R.SelectedIndex = 4
            'comboBoxSecuLevel_V.SelectedIndex = 3

            comboBoxSecuLevel_R.SelectedIndex = 7
            comboBoxSecuLevel_V.SelectedIndex = 6

            '  EnableButtons(False)

            m_FPM = New SGFingerPrintManager
            EnumerateBtn_Click(sender, e)

            OpenDeviceBtn_Click(sender, e)


            emplea2_encontrado = "NO"


            '----------------------------------------------------------capturo imagen ---------------------------------------------------------------

            bandera_error = False

            Dim img_qlty As Int32
            ReDim fp_image(m_ImageWidth * m_ImageHeight)

            iError = m_FPM.GetImage(fp_image)

            If iError = SGFPMError.ERROR_NONE Then
                DrawImage(fp_image, pictureBox1)
                m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, img_qlty)
                ' progressBar_R1.Value = img_qlty
                iError = m_FPM.CreateTemplate(fp_image, m_RegMin1)

                If (iError = SGFPMError.ERROR_NONE) Then
                    StatusBar.Text = "First image is captured"
                    m_FPM.CloseDevice()
                Else
                    DisplayError("CreateTemplate", iError)
                    bandera_error = True
                    m_FPM.CloseDevice()
                    Me.Close()
                End If
            Else
                DisplayError("GetImage", iError)
                bandera_error = True
                m_FPM.CloseDevice()
                Me.Close()
            End If




            If bandera_error = False Then

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


                encontrado = False


                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT EMPLEADO,TEMPLATE_1,TEMPLATE_2 FROM ASISTENCIA.dbo.EMPLEADO_HUELLA WITH(NOLOCK)"
                    .CommandType = CommandType.Text
                End With
                daconsulta.SelectCommand = cmdconsulta
                daconsulta.Fill(dsconsulta)


                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                    If encontrado = False Then


                        obj = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                        obj2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")
                        emplea2 = dsconsulta.Tables(0).Rows(z).Item("EMPLEADO")

                        If Not IsDBNull(obj) Then
                            B = DirectCast(obj, Byte())

                            fp_plantilla = DirectCast(obj, Byte())
                            fp_plantilla2 = DirectCast(obj2, Byte())


                            'Using ms As New IO.MemoryStream(B)
                            '    '   pictureBox2.Image = Image.FromStream(ms)

                            'End Using


                            ''----------------------------------------verificacion-------------------------------------------------------------------

                            Dim iErrorver As Int32
                            Dim matched1 As Boolean
                            Dim matched2 As Boolean
                            Dim secu_levelver As SGFPMSecurityLevel
                            secu_levelver = comboBoxSecuLevel_V.SelectedIndex
                            iErrorver = m_FPM.MatchTemplate(fp_plantilla, m_RegMin1, secu_levelver, matched1)
                            iErrorver = m_FPM.MatchTemplate(fp_plantilla2, m_RegMin1, secu_levelver, matched2)
                            If (iErrorver = SGFPMError.ERROR_NONE) Then
                                If (matched1 And matched2) Then


                                    emplea2_encontrado = emplea2
                                    encontrado = True

                                    StatusBar.Text = "Empleado # " & emplea2_encontrado
                                    m_FPM.CloseDevice()

                                    Exit For

                                Else
                                    StatusBar.Text = "Usted No es esta registrado"
                                    ' btningresar.Enabled = False
                                    m_FPM.CloseDevice()
                                    Me.Close()

                                End If
                            Else
                                DisplayError("MatchTemplate", iErrorver)
                                m_FPM.CloseDevice()
                            End If


                        End If

                    End If


                Next



                If emplea2_encontrado <> "NO" Then


                    '-------------- aqui va el reporte





                    cmdconsulta = New SqlClient.SqlCommand
                    daconsulta = New SqlClient.SqlDataAdapter
                    dsconsulta = New DataSet
                    With cmdconsulta
                        .Connection = conexion
                        .CommandText = "SELECT E.NOMBRE " +
                                                ",E.EMPLEADO " +
                                                ",E.IDENTIFICACION " +
                                                ",U.DESCRIPCION AS UBICACION " +
                                                ",P.DESCRIPCION AS PUESTO " +
                                                ",D.DESCRIPCION AS DEPARTAMENTO " +
                                                ",N.DESCRIPCION AS NOMINA " +
                                                ",E.ENTIDAD_FINANCIERA AS BANCO " +
                                                ",E.RUBRO7 + '-' + E.CUENTA_ENTIDAD AS NUMERO_CUENTA " +
                                                ",E_MAIL " +
                                                "FROM EXACTUS.DIGEMA.EMPLEADO E " +
                                                "INNER JOIN EXACTUS.DIGEMA.UBICACION U WITH(NOLOCK) ON E.UBICACION=U.UBICACION " +
                                                "INNER JOIN EXACTUS.DIGEMA.PUESTO P WITH(NOLOCK) ON E.PUESTO=P.PUESTO " +
                                                "INNER JOIN EXACTUS.DIGEMA.DEPARTAMENTO D WITH(NOLOCK) ON E.DEPARTAMENTO=D.DEPARTAMENTO " +
                                                "INNER JOIN EXACTUS.DIGEMA.NOMINA N WITH(NOLOCK) ON N.NOMINA=E.NOMINA AND N.ESTADO='A' " +
                                                "WHERE E.EMPLEADO='" & emplea2_encontrado & "'"



                        .CommandType = CommandType.Text
                    End With
                    daconsulta.SelectCommand = cmdconsulta
                    daconsulta.Fill(dsconsulta)


                    For z = 0 To dsconsulta.Tables(0).Rows.Count - 1


                        empleados = dsconsulta.Tables(0).Rows(z).Item("EMPLEADO")
                        nombres_empleados = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")

                        identificaciones = dsconsulta.Tables(0).Rows(z).Item("IDENTIFICACION")
                        ubicaciones = dsconsulta.Tables(0).Rows(z).Item("UBICACION")

                        puestos = dsconsulta.Tables(0).Rows(z).Item("PUESTO")
                        departamentos = dsconsulta.Tables(0).Rows(z).Item("DEPARTAMENTO")


                        nominas = dsconsulta.Tables(0).Rows(z).Item("NOMINA")
                        bancos = dsconsulta.Tables(0).Rows(z).Item("BANCO")

                        cuentas = dsconsulta.Tables(0).Rows(z).Item("NUMERO_CUENTA")

                        correo = dsconsulta.Tables(0).Rows(z).Item("E_MAIL")



                        Dim oDoc As New iTextSharp.text.Document(PageSize.A4, 0, 0, 0, 0)
                        Dim pdfw As iTextSharp.text.pdf.PdfWriter
                        Dim cb As PdfContentByte
                        Dim fuente As iTextSharp.text.pdf.BaseFont
                        '   Dim NombreArchivo As String = "\\192.168.101.216\BDAPP02\c$\Users\Administrator\Dropbox\CONSTRUPLAZA\CP-" & total_facturas & ".pdf"
                        NombreArchivo = "C:\CONTRARECIBOS\" & emplea2_encontrado & ".pdf"
                        '        Dim NombreArchivo As String = "C:\Users\lsegura\Desktop\Amador\Envio_PDF\ejemplo.pdf"

                        Try
                            pdfw = PdfWriter.GetInstance(oDoc, New FileStream(NombreArchivo,
                                FileMode.Create, FileAccess.Write, FileShare.None))
                            oDoc.Open()
                            cb = pdfw.DirectContent
                            oDoc.NewPage()
                            'Iniciamos el flujo de bytes.
                            cb.BeginText()
                            fuente = FontFactory.GetFont(FontFactory.HELVETICA, iTextSharp.text.Font.DEFAULTSIZE, iTextSharp.text.Font.NORMAL).BaseFont

                            cb.SetFontAndSize(fuente, 7)
                            'Seteamos el color del texto a escribir.
                            cb.SetColorFill(iTextSharp.text.BaseColor.BLACK)


                            imagen = iTextSharp.text.Image.GetInstance("C:\Imagenes\logos.jpg") 'nombre y ruta de la imagen a insertar
                            imagen.ScalePercent(46.7) 'escala al tamaño de la imagen
                            imagen.SetAbsolutePosition(10, 780) 'posición en la que se inserta. 40 (de izquierda a derecha). 500 (de abajo hacia arriba)
                            oDoc.Add(imagen) 'se agrega la imagen al documento

                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "www.construplaza.com", 80, PageSize.A4.Height - 20, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Construplaza S.A.", 80, PageSize.A4.Height - 30, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Cédula Jurídica 3-101-289562", 80, PageSize.A4.Height - 40, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Tel: +506 4101-8888", 80, PageSize.A4.Height - 50, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Comprobante de pago", 80, PageSize.A4.Height - 60, 0)

                            '---------------------------------------------------------------------------------------------------------------------



                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Empleado:", 30, PageSize.A4.Height - 90, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, empleados, 80, PageSize.A4.Height - 90, 0)

                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Nombre:", 30, PageSize.A4.Height - 100, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, nombres_empleados, 80, PageSize.A4.Height - 100, 0)

                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Identificacíon:", 30, PageSize.A4.Height - 110, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, identificaciones, 80, PageSize.A4.Height - 110, 0)

                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Ubicación:", 30, PageSize.A4.Height - 120, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, ubicaciones, 80, PageSize.A4.Height - 120, 0)

                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Puesto:", 30, PageSize.A4.Height - 130, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, puestos, 80, PageSize.A4.Height - 130, 0)

                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Departamento:", 30, PageSize.A4.Height - 140, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, departamentos, 80, PageSize.A4.Height - 140, 0)

                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Nómina:", 30, PageSize.A4.Height - 150, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, nominas, 80, PageSize.A4.Height - 150, 0)

                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Banco:", 30, PageSize.A4.Height - 160, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, bancos, 80, PageSize.A4.Height - 160, 0)

                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Cuenta banco:", 30, PageSize.A4.Height - 170, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, cuentas, 80, PageSize.A4.Height - 170, 0)

                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Correo:", 30, PageSize.A4.Height - 190, 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, correo, 80, PageSize.A4.Height - 190, 0)



                            salario = 0


                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "*************************Beneficios*********************************", 20, PageSize.A4.Height - 200, 0)


                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Concepto:                                                                 Total", 10, PageSize.A4.Height - 220, 0)


                            Dim linea As Integer = 230






                            cmdconsulta = New SqlClient.SqlCommand
                            daconsulta = New SqlClient.SqlDataAdapter
                            dsconsulta = New DataSet
                            With cmdconsulta
                                .Connection = conexion
                                .CommandText = "SELECT C1.EMPLEADO,C1.NOMBRE,C1.UBICACION,C1.CONCEPTO,C1.DESCRIPCION,C1.HORAS,C1.TOTAL,CONVERT(varchar(11),C1.FECHA_INICIO,105)AS FECHA_INICIO,CONVERT(varchar(11),C1.FECHA_FIN,105)AS FECHA_FIN " +
                                                "FROM (SELECT E.EMPLEADO,E.NOMBRE,U.DESCRIPCION AS UBICACION,C.CONCEPTO,C.DESCRIPCION,ECN.CANTIDAD AS HORAS,ECN.TOTAL,NH.FECHA_INICIO,NH.FECHA_FIN " +
                                                "FROM  EXACTUS.digema.EMPLEADO AS E WITH(NOLOCK) " +
                                                "INNER JOIN EXACTUS.digema.EMPLEADO_CONC_NOMI AS ECN WITH(NOLOCK) ON ECN.EMPLEADO = E.EMPLEADO " +
                                                "INNER JOIN EXACTUS.digema.UBICACION U  ON E.UBICACION= U.UBICACION " +
                                                "INNER JOIN EXACTUS.digema.NOMINA_HISTORICO AS NH WITH(NOLOCK) ON ECN.NUMERO_NOMINA = NH.NUMERO_NOMINA AND ECN.NOMINA = NH.NOMINA " +
                                                "INNER JOIN EXACTUS.digema.CONCEPTO AS C WITH(NOLOCK) ON ECN.CONCEPTO = C.CONCEPTO " +
                                                "WHERE (NH.NUMERO_NOMINA =(SELECT TOP (1) NUMERO_NOMINA " +
                                                "FROM EXACTUS.digema.NOMINA_HISTORICO NH WITH(NOLOCK) " +
                                                "INNER JOIN EXACTUS.DIGEMA.NOMINA N WITH(NOLOCK) ON N.NOMINA=NH.NOMINA AND N.ESTADO='A' " +
                                                "ORDER BY FECHA_FIN DESC)) AND (C.CONCEPTO LIKE '%BQ%' OR " +
                                                "C.CONCEPTO LIKE '%DQ%'))C1 WHERE C1.EMPLEADO='" & emplea2_encontrado & "' AND (C1.HORAS > 0 OR C1.TOTAL > 0) AND C1.CONCEPTO LIKE 'B%'"


                                '"SELECT C1.EMPLEADO,C1.NOMBRE,C1.UBICACION,C1.CONCEPTO,C1.DESCRIPCION,C1.HORAS,C1.TOTAL,CONVERT(varchar(11),C1.FECHA_INICIO,105)AS FECHA_INICIO,CONVERT(varchar(11),C1.FECHA_FIN,105)AS FECHA_FIN " +
                                '                                                    "FROM (SELECT E.EMPLEADO,E.NOMBRE,U.DESCRIPCION AS UBICACION,C.CONCEPTO,C.DESCRIPCION,ECN.CANTIDAD AS HORAS,ECN.TOTAL,NH.FECHA_INICIO,NH.FECHA_FIN " +
                                '                                                    "FROM  EXACTUS.digema.EMPLEADO AS E WITH(NOLOCK) " +
                                '                                                    "INNER JOIN EXACTUS.digema.EMPLEADO_CONC_NOMI AS ECN WITH(NOLOCK) ON ECN.EMPLEADO = E.EMPLEADO " +
                                '                                                    "INNER JOIN EXACTUS.digema.UBICACION U  ON E.UBICACION= U.UBICACION " +
                                '                                                    "INNER JOIN EXACTUS.digema.NOMINA_HISTORICO AS NH WITH(NOLOCK) ON ECN.NUMERO_NOMINA = NH.NUMERO_NOMINA AND ECN.NOMINA = NH.NOMINA " +
                                '                                                    "INNER JOIN EXACTUS.digema.CONCEPTO AS C WITH(NOLOCK) ON ECN.CONCEPTO = C.CONCEPTO " +
                                '                                                    "WHERE (NH.NUMERO_NOMINA =(SELECT TOP (1) NUMERO_NOMINA " +
                                '                                                    "FROM EXACTUS.digema.NOMINA_HISTORICO WITH(NOLOCK) " +
                                '                                                    "ORDER BY FECHA_FIN DESC)) AND (C.CONCEPTO LIKE '%BQ%' OR " +
                                '                                                    "C.CONCEPTO LIKE '%DQ%'))C1 WHERE C1.EMPLEADO='" & emplea2_encontrado & "' AND (C1.HORAS > 0 OR C1.TOTAL > 0) " +
                                '                                                    "AND C1.CONCEPTO LIKE 'B%'"

                                .CommandType = CommandType.Text
                            End With
                            daconsulta.SelectCommand = cmdconsulta
                            daconsulta.Fill(dsconsulta)


                            For z2 = 0 To dsconsulta.Tables(0).Rows.Count - 1


                                Dim DESCRI As String = dsconsulta.Tables(0).Rows(z2).Item("DESCRIPCION")
                                Dim HORA As Decimal = dsconsulta.Tables(0).Rows(z2).Item("HORAS")
                                Dim TOTS As Decimal = dsconsulta.Tables(0).Rows(z2).Item("TOTAL")

                                Dim FINI As String = dsconsulta.Tables(0).Rows(z2).Item("FECHA_INICIO")
                                Dim FFIN As String = dsconsulta.Tables(0).Rows(z2).Item("FECHA_FIN")


                                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Planilla del: ", 30, PageSize.A4.Height - 180, 0)
                                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, FINI & " al " & FFIN, 80, PageSize.A4.Height - 180, 0)


                                salario = salario + TOTS


                                '         cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Concepto: ", 30, PageSize.A4.Height - linea, 0)
                                '    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, DESCRI & "                                            " & Format(CType(TOTS, Decimal), "#,##0.00"), 10, PageSize.A4.Height - (linea + 20), 0)


                                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, DESCRI, 10, PageSize.A4.Height - (linea + 20), 0)
                                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Format(CType(TOTS, Decimal), "#,##0.00"), 165, PageSize.A4.Height - (linea + 20), 0)


                                linea = linea + 10


                            Next


                            '---------------------------------------------------------------------------------------------------------------------

                            linea = linea + 10


                            deduce = 0



                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "************************Deducciones*****************************", 20, PageSize.A4.Height - (linea + 20), 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Concepto:                                                                 Total", 10, PageSize.A4.Height - (linea + 40), 0)


                            linea = linea + 50


                            cmdconsulta = New SqlClient.SqlCommand
                            daconsulta = New SqlClient.SqlDataAdapter
                            dsconsulta = New DataSet
                            With cmdconsulta
                                .Connection = conexion
                                .CommandText = "SELECT C1.EMPLEADO,C1.NOMBRE,C1.UBICACION,C1.CONCEPTO,C1.DESCRIPCION,C1.HORAS,C1.TOTAL,C1.FECHA_INICIO,C1.FECHA_FIN " +
                                                "FROM (SELECT E.EMPLEADO,E.NOMBRE,U.DESCRIPCION AS UBICACION,C.CONCEPTO,C.DESCRIPCION,ECN.CANTIDAD AS HORAS,ECN.TOTAL,NH.FECHA_INICIO,NH.FECHA_FIN " +
                                                "FROM  EXACTUS.digema.EMPLEADO AS E WITH(NOLOCK) " +
                                                "INNER JOIN EXACTUS.digema.EMPLEADO_CONC_NOMI AS ECN WITH(NOLOCK) ON ECN.EMPLEADO = E.EMPLEADO " +
                                                "INNER JOIN EXACTUS.digema.UBICACION U  ON E.UBICACION= U.UBICACION " +
                                                "INNER JOIN EXACTUS.digema.NOMINA_HISTORICO AS NH WITH(NOLOCK) ON ECN.NUMERO_NOMINA = NH.NUMERO_NOMINA AND ECN.NOMINA = NH.NOMINA " +
                                                "INNER JOIN EXACTUS.digema.CONCEPTO AS C WITH(NOLOCK) ON ECN.CONCEPTO = C.CONCEPTO " +
                                                "WHERE (NH.NUMERO_NOMINA =(SELECT TOP (1) NUMERO_NOMINA " +
                                                "FROM EXACTUS.digema.NOMINA_HISTORICO NH WITH(NOLOCK) " +
                                                "INNER JOIN EXACTUS.DIGEMA.NOMINA N WITH(NOLOCK) ON N.NOMINA=NH.NOMINA AND N.ESTADO='A' " +
                                                "ORDER BY FECHA_FIN DESC)) AND (C.CONCEPTO LIKE '%BQ%' OR " +
                                                "C.CONCEPTO LIKE '%DQ%'))C1 WHERE C1.EMPLEADO='" & emplea2_encontrado & "' AND (C1.HORAS > 0 OR C1.TOTAL > 0) " +
                                                "AND C1.CONCEPTO LIKE 'D%'"





                                '"SELECT C1.EMPLEADO,C1.NOMBRE,C1.UBICACION,C1.CONCEPTO,C1.DESCRIPCION,C1.HORAS,C1.TOTAL,C1.FECHA_INICIO,C1.FECHA_FIN " +
                                '                                                    "FROM (SELECT E.EMPLEADO,E.NOMBRE,U.DESCRIPCION AS UBICACION,C.CONCEPTO,C.DESCRIPCION,ECN.CANTIDAD AS HORAS,ECN.TOTAL,NH.FECHA_INICIO,NH.FECHA_FIN " +
                                '                                                    "FROM  EXACTUS.digema.EMPLEADO AS E WITH(NOLOCK) " +
                                '                                                    "INNER JOIN EXACTUS.digema.EMPLEADO_CONC_NOMI AS ECN WITH(NOLOCK) ON ECN.EMPLEADO = E.EMPLEADO " +
                                '                                                    "INNER JOIN EXACTUS.digema.UBICACION U  ON E.UBICACION= U.UBICACION " +
                                '                                                    "INNER JOIN EXACTUS.digema.NOMINA_HISTORICO AS NH WITH(NOLOCK) ON ECN.NUMERO_NOMINA = NH.NUMERO_NOMINA AND ECN.NOMINA = NH.NOMINA " +
                                '                                                    "INNER JOIN EXACTUS.digema.CONCEPTO AS C WITH(NOLOCK) ON ECN.CONCEPTO = C.CONCEPTO " +
                                '                                                    "WHERE (NH.NUMERO_NOMINA =(SELECT TOP (1) NUMERO_NOMINA " +
                                '                                                    "FROM EXACTUS.digema.NOMINA_HISTORICO WITH(NOLOCK) " +
                                '                                                    "ORDER BY FECHA_FIN DESC)) AND (C.CONCEPTO LIKE '%BQ%' OR " +
                                '                                                    "C.CONCEPTO LIKE '%DQ%'))C1 WHERE C1.EMPLEADO='" & emplea2_encontrado & "' AND (C1.HORAS > 0 OR C1.TOTAL > 0) " +
                                '                                                    "AND C1.CONCEPTO LIKE 'D%'"

                                .CommandType = CommandType.Text
                            End With
                            daconsulta.SelectCommand = cmdconsulta
                            daconsulta.Fill(dsconsulta)


                            For z3 = 0 To dsconsulta.Tables(0).Rows.Count - 1


                                Dim DESCRI As String = dsconsulta.Tables(0).Rows(z3).Item("DESCRIPCION")
                                Dim HORA As Decimal = dsconsulta.Tables(0).Rows(z3).Item("HORAS")
                                Dim TOTD As Decimal = dsconsulta.Tables(0).Rows(z3).Item("TOTAL")




                                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, DESCRI, 10, PageSize.A4.Height - (linea + 20), 0)
                                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Format(CType(TOTD, Decimal), "#,##0.00"), 165, PageSize.A4.Height - (linea + 20), 0)

                                deduce = deduce + TOTD


                                linea = linea + 10


                            Next




                            '---------------------------------------------------------------------------------------------------------------------




                            totalizado = salario - deduce

                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "************************************************************", 10, PageSize.A4.Height - (linea + 20), 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "SALARIO NETO", 10, PageSize.A4.Height - (linea + 40), 0)
                            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Format(CType(totalizado, Decimal), "#,##0.00"), 165, PageSize.A4.Height - (linea + 40), 0)


                            '" & Format(CType(totalizado, Decimal), "#,##0.00")




                            'Fin del flujo de byt
                            cb.EndText()
                            'Forzamos vaciamiento del buffer.
                            pdfw.Flush()
                            'Cerramos el documento.
                            oDoc.Close()
                        Catch ex As Exception
                            'Si hubo una excepcion y el archivo existe ...
                            If File.Exists(NombreArchivo) Then
                                'Cerramos el documento si esta abierto.
                                'Y asi desbloqueamos el archivo para su eliminacion.
                                If oDoc.IsOpen Then oDoc.Close()
                                '... lo eliminamos de disco.
                                File.Delete(NombreArchivo + ".pdf")
                            End If
                            '  Throw New Exception("Error al generar archivo PDF (" & amp; ex.Message &amp; ")")
                        Finally
                            cb = Nothing
                            pdfw = Nothing
                            oDoc = Nothing
                        End Try




                        Dim psi As New ProcessStartInfo

                        psi.UseShellExecute = True

                        psi.Verb = "print"

                        psi.WindowStyle = ProcessWindowStyle.Hidden

                        'psi.Arguments = PrintDialog1.PrinterSettings.PrinterName.ToString()

                        psi.FileName = "C:\CONTRARECIBOS\" & emplea2_encontrado & ".pdf" ' Here specify a document to be printed

                        Process.Start(psi)





                    Next




                End If



            End If


            If emplea2_encontrado = "NO" Or bandera_error = True Then

                MsgBox("No se logró leer su huella,favor intente nuevamente", MsgBoxStyle.Critical, "Construplaza")
                m_FPM.CloseDevice()
                Formmenu.btncomprobante.Enabled = True
                Me.Close()

            End If

            '----------------------catch


            'If bandera_error = True Then
            '    MsgBox("No se logró leer su huella,favor intente nuevamente", MsgBoxStyle.Critical, "Construplaza")
            '    m_FPM.CloseDevice()
            '    Formmenu.btncomprobante.Enabled = True
            '    Me.Close()

            'End If


            m_FPM.CloseDevice()



        Catch ex As Exception
            MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
            m_FPM.CloseDevice()
            Formmenu.btncomprobante.Enabled = True
            Me.Close()
            m_FPM.CloseDevice()
            Me.Close()
            Formmenu.Button1.Enabled = True
            ' Show the stack trace, which is a list of methods 
            ' that are currently executing.
            '  MessageBox.Show("Stack Trace: " & vbCrLf & ex.StackTrace)
        Finally
            m_FPM.CloseDevice()
            ' This line executes whether or not the exception occurs.
            '  MessageBox.Show("in Finally block")
        End Try


        If emplea2_encontrado <> "NO" Then



            System.Threading.Thread.Sleep(5000)

            Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName("AcroRd32")

            For Each p As Process In pProcess
                p.Kill()
            Next

        End If


        Formmenu.btncomprobante.Enabled = True

        Me.Close()




    End Sub



    Private Sub DrawImage(ByVal imgData() As Byte, ByVal picBox As PictureBox)
        Dim colorval As Int32
        Dim bmp As Bitmap
        Dim i, j As Int32

        bmp = New Bitmap(m_ImageWidth, m_ImageHeight)
        picBox.Image = bmp

        For i = 0 To bmp.Width - 1
            For j = 0 To bmp.Height - 1
                colorval = imgData((j * m_ImageWidth) + i)
                bmp.SetPixel(i, j, Color.FromArgb(colorval, colorval, colorval))
            Next j
        Next i

        picBox.Refresh()

    End Sub
    Private Sub EnumerateBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EnumerateBtn.Click
        Dim iError As Int32
        Dim enum_device As String
        Dim i As Int32

        comboBoxDeviceName.Items.Clear()

        ' Enumerate Device
        iError = m_FPM.EnumerateDevice()

        'Get enumeration info into SGFPMDeviceList
        ReDim m_DevList(m_FPM.NumberOfDevice)


        For i = 0 To m_FPM.NumberOfDevice - 1

            m_DevList(i) = New SGFPMDeviceList
            m_FPM.GetEnumDeviceInfo(i, m_DevList(i))

            enum_device = m_DevList(i).DevName.ToString() + " : " + Convert.ToString(m_DevList(i).DevID)
            comboBoxDeviceName.Items.Add(enum_device)

            If (comboBoxDeviceName.Items.Count > 0) Then
                comboBoxDeviceName.SelectedIndex = 0
            End If

        Next

    End Sub


    'Private Sub EnableButtons(ByVal enable As Boolean)
    '    'ConfigBtn.Enabled = enable
    '    'GetImageBtn.Enabled = enable
    '    'GetLiveImageBtn.Enabled = enable
    '    'BtnCapture1.Enabled = enable
    '    'BtnCapture2.Enabled = enable
    '    'BtnCapture3.Enabled = enable
    '    'BtnRegister.Enabled = enable
    '    'BtnVerify.Enabled = enable
    '    'GetBtn.Enabled = enable
    '    'SetBrightnessBtn.Enabled = enable
    'End Sub

    Private Sub DisplayError(ByVal funcName As String, ByVal iError As Int32)
        Dim text As String

        text = ""
        Select Case iError
            Case 0                             'SGFDX_ERROR_NONE				= 0,
                text = "Error none"

            Case 1 'SGFDX_ERROR_CREATION_FAILED	= 1,
                text = "Can not create object"

            Case 2 '   SGFDX_ERROR_FUNCTION_FAILED	= 2,
                text = "Function Failed"

            Case 3 '   SGFDX_ERROR_INVALID_PARAM	= 3,
                text = "Invalid Parameter"

            Case 4 '   SGFDX_ERROR_NOT_USED			= 4,
                text = "Not used function"

            Case 5 'SGFDX_ERROR_DLLLOAD_FAILED	= 5,
                text = "Can not create object"

            Case 6 'SGFDX_ERROR_DLLLOAD_FAILED_DRV	= 6,
                text = "Can not load device driver"

            Case 7 'SGFDX_ERROR_DLLLOAD_FAILED_ALGO = 7,
                text = "Can not load sgfpamx.dll"

            Case 51 'SGFDX_ERROR_SYSLOAD_FAILED	   = 51,	// system file load fail
                text = "Can not load driver kernel file"

            Case 52 'SGFDX_ERROR_INITIALIZE_FAILED  = 52,   // chip initialize fail
                text = "Failed to initialize the device"

            Case 53 'SGFDX_ERROR_LINE_DROPPED		   = 53,   // image data drop
                text = "Data transmission is not good"

            Case 54 'SGFDX_ERROR_TIME_OUT			   = 54,   // getliveimage timeout error
                text = "Time out"

            Case 55 'SGFDX_ERROR_DEVICE_NOT_FOUND	= 55,   // device not found
                text = "Device not found"

            Case 56 'SGFDX_ERROR_DRVLOAD_FAILED	   = 56,   // dll file load fail
                text = "Can not load driver file"

            Case 57 'SGFDX_ERROR_WRONG_IMAGE		   = 57,   // wrong image
                text = "Wrong Image"

            Case 58 'SGFDX_ERROR_LACK_OF_BANDWIDTH  = 58,   // USB Bandwith Lack Error
                text = "Lack of USB Bandwith"

            Case 59 'SGFDX_ERROR_DEV_ALREADY_OPEN	= 59,   // Device Exclusive access Error
                text = "Device is already opened"

            Case 60 'SGFDX_ERROR_GETSN_FAILED		   = 60,   // Fail to get Device Serial Number
                text = "Device serial number error"

            Case 61 'SGFDX_ERROR_UNSUPPORTED_DEV		   = 61,   // Unsupported device
                text = "Unsupported device"

                ' Extract & Verification error
            Case 101 'SGFDX_ERROR_FEAT_NUMBER		= 101, // utoo small number of minutiae
                text = "The number of minutiae is too small"

            Case 102 'SGFDX_ERROR_INVALID_TEMPLATE_TYPE		= 102, // wrong template type
                text = "Template is invalid"


            Case 103 'SGFDX_ERROR_INVALID_TEMPLATE1		= 103, // wrong template type
                text = "1st template is invalid"

            Case 104 'SGFDX_ERROR_INVALID_TEMPLATE2		= 104, // vwrong template type
                text = "2nd template is invalid"

            Case 105 'SGFDX_ERROR_EXTRACT_FAIL		= 105, // extraction fail
                text = "Minutiae extraction failed"

            Case 106 'SGFDX_ERROR_MATCH_FAIL		= 106, // matching  fail
                text = "Matching failed"


        End Select

        text = funcName + " Error # " + Convert.ToString(iError) + " :" + text
        StatusBar.Text = text

    End Sub


    Private Sub OpenDeviceBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenDeviceBtn.Click
        Dim iError As Int32
        Dim device_name As SGFPMDeviceName
        Dim device_id As Int32

        If m_FPM.NumberOfDevice = 0 Then
            Return
        End If

        device_name = m_DevList(comboBoxDeviceName.SelectedIndex).DevName
        device_id = m_DevList(comboBoxDeviceName.SelectedIndex).DevID

        iError = m_FPM.Init(device_name)
        iError = m_FPM.OpenDevice(device_id)

        If (iError = SGFPMError.ERROR_NONE) Then
            GetBtn_Click(sender, e)
            StatusBar.Text = "Initialization Success"
            '    EnableButtons(True)

            ' FDU03 or FDU04 only
            If ((device_name = SGFPMDeviceName.DEV_FDU03) Or (device_name = SGFPMDeviceName.DEV_FDU04)) Then
                CheckBoxAutoOn.Enabled = True
            Else
                CheckBoxAutoOn.Enabled = False
            End If

        Else
            DisplayError("OpenDevice", iError)
        End If


    End Sub

    Private Sub GetBtn_Click(sender As Object, e As EventArgs) Handles GetBtn.Click
        Dim pInfo As SGFPMDeviceInfoParam
        Dim iError As Int32

        pInfo = New SGFPMDeviceInfoParam
        iError = m_FPM.GetDeviceInfo(pInfo)

        If (iError = SGFPMError.ERROR_NONE) Then
            m_ImageWidth = pInfo.ImageWidth
            m_ImageHeight = pInfo.ImageHeight

            'textDeviceID.Text = Convert.ToString(pInfo.DeviceID)
            'textFWVersion.Text = Convert.ToString(pInfo.FWVersion, 16)

            'Device Serial Number
            Dim encoding As New ASCIIEncoding
            'textSerialNum.Text = encoding.GetString(pInfo.DeviceSN)

            'textImageDPI.Text = Convert.ToString(pInfo.ImageDPI)
            'textImageHeight.Text = Convert.ToString(pInfo.ImageHeight)
            'textImageWidth.Text = Convert.ToString(pInfo.ImageWidth)
            'textBrightness.Text = Convert.ToString(pInfo.Brightness)
            'textContrast.Text = Convert.ToString(pInfo.Contrast)
            'textGain.Text = Convert.ToString(pInfo.Gain)

            'BrightnessUpDown.Value = pInfo.Brightness
        End If

    End Sub


    Private Sub StatusBar_Click(sender As Object, e As EventArgs) Handles StatusBar.Click

    End Sub
End Class