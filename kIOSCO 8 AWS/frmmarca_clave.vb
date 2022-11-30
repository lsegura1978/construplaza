Imports System.Text
Imports SecuGen.FDxSDKPro.Windows
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices



Public Class frmmarca_clave

    Public cmdconsulta, cmdconsulta_total, cmdconsulta_total_tardias, cmdconsulta_ubicacion, cmdconsulta_chofer, cmdconsulta_documentos As SqlClient.SqlCommand
    Public transaccion, transaccion_total, transaccion_total_tardias, transaccion_ubicacion, transaccion_chofer, transaccion_documentos As SqlClient.SqlTransaction
    Public cmdincluir, cmdincluir_total, cmdincluir_total_tardias, cmdincluir_ubicacion, cmdincluir_chofer, cmdincluir_documentos As SqlClient.SqlCommand
    Public iregincluir, iregincluird, cod, t, y As Integer
    Public contrarecibo, strconexionex, parametros, asunto, cuerpo, CLIENTE, FECHA, DOCUMENTO, TIPO, MONEDA, CREDITO, NOMBRE, nombres As String
    Public MONTO, SALDO As Decimal
    Public conexion As SqlClient.SqlConnection
    Public conexionbi As SqlClient.SqlConnection
    Public daconsulta, daconsulta_total, daconsulta_total_tardias, daconsulta_ubicacion, daconsulta_chofer, daconsulta_documentos As SqlClient.SqlDataAdapter
    Public dsconsulta, dsconsulta_total, dsconsulta_total_tardias, dsconsulta_ubicacion, dsconsulta_chofer, dsconsulta_documentos As DataSet
    Public contador As Integer = 130
    Public cuenta_tardias As Integer = 0
    Public NombreArchivo, empleado As String
    Public contadores As Integer
    Public conta_marcas As Integer = 0
    Public conta_braza As Integer = 0
    Public bandera_ingreso As Integer = 0
    Public DIA As String
    Public JORNADA As String
    Public HORACHEQUEO As String
    Public objStreamReader As StreamReader
    Public strLine As String




    Public z, i, k As Integer
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
    Public bandera_error As Boolean = False
    Public bandera_nocturna As Boolean = False
    Public NOMBREN As String
    Public HORAN As String
    Public ENTRADAN As String
    Public SALIDAN As String
    Public ESTADON As String
    Public bandera_total As Boolean = False
    Public bandera_ubicacion As Boolean = False
    Public bandera_chofer As Boolean = False
    Public empleado_total, nombre_total, salida_total, tipo_total As String
    Public diferencia_total As Decimal
    Public ubicacion, EMPLEADO_UBICACION As String



    Private Sub frmmarca_clave_Load(sender As Object, e As EventArgs) Handles MyBase.Load
                Try

            cuenta_tardias = 0



            txttexto.Text = ""

            lblestados.Text = ""



            Formmenu.btnclave.Enabled = False



            '------------------------------------------abro el dispositivo------------------------------------------------------------------
            'System.Threading.Thread.Sleep(3000)


            Dim iError As Int32

            Me.BackColor = Color.Gray


            m_LedOn = False
         
            comboBoxSecuLevel_R.SelectedIndex = 7
            comboBoxSecuLevel_V.SelectedIndex = 6


            'EnableButtons(False)

            m_FPM = New SGFingerPrintManager
            EnumerateBtn_Click(sender, e)

            OpenDeviceBtn_Click(sender, e)


            'emplea2_encontrado = "NO"


            '---------------------------------------------abro conexion bd----------------------------------------------------------------------

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

            '----------------------------------------------------------capturo imagen ---------------------------------------------------------------


            bandera_error = False

            'Dim img_qlty As Int32
            'ReDim fp_image(m_ImageWidth * m_ImageHeight)

            'iError = m_FPM.GetImage(fp_image)

            'If iError = SGFPMError.ERROR_NONE Then
            '     DrawImage(fp_image, pictureBox1)
            '    m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, img_qlty)
            '    ' progressBar_R1.Value = img_qlty
            '    iError = m_FPM.CreateTemplate(fp_image, m_RegMin1)

            '    If (iError = SGFPMError.ERROR_NONE) Then
            '        StatusBar.Text = "First image is captured"
            '        m_FPM.CloseDevice()
            '    Else
            '        DisplayError("CreateTemplate", iError)
            '        Me.Close()
            '        Formmenu.BackColor = Color.Gray
            '        bandera_error = True
            '        m_FPM.CloseDevice()
            '    End If
            'Else
            '    DisplayError("GetImage", iError)
            '    Me.Close()
            '    Formmenu.BackColor = Color.Gray
            '    bandera_error = True
            '    m_FPM.CloseDevice()
            'End If

            encontrado = False



            bandera_error = False

            If bandera_error = False Then

                'emplea2_encontrado = "0538"

                '-------------------------------------------------------recorro tabla---------------------------------------------------------------------

                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT EMPLEADO,TEMPLATE_1,TEMPLATE_2 FROM ASISTENCIA.dbo.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & emplea2_encontrado & "'"
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


                            Using ms As New IO.MemoryStream(B)
                                '   pictureBox2.Image = Image.FromStream(ms)

                            End Using


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
                                    StatusBar.Text = "Verification Success"

                                    '  emplea2_encontrado = emplea2
                                    'emplea2_encontrado = "0538"
                                    m_FPM.CloseDevice()
                                    encontrado = True

                                Else
                                    StatusBar.Text = "Verification Failed"
                                    encontrado = False
                                    m_FPM.CloseDevice()

                                End If
                            Else
                                DisplayError("MatchTemplate", iErrorver)
                                m_FPM.CloseDevice()

                            End If


                        End If

                    End If


                Next

                '   MsgBox(emplea2_encontrado)

                ' MsgBox(encontrado)

                encontrado = True


                If emplea2_encontrado = "NO" And encontrado = False Then
                    StatusBar.Text = "No se encontró empleado"

                    Me.BackColor = Color.Red
                    Me.PictureBox2.Image = Nothing
                    Formmenu.BackColor = Color.Red
                    Formmenu.btnIngresar.BackColor = Color.Red
                    Formmenu.btnmarcar.BackColor = Color.Red
                    Formmenu.Button1.BackColor = Color.Red
                    Formmenu.btnvacaciones.BackColor = Color.Red
                    Formmenu.btncomprobante.BackColor = Color.Red
                    Formmenu.btnmarcasq.BackColor = Color.Red
                    Formmenu.btnmarcasg.BackColor = Color.Red
                    Formmenu.btnaseconstruplaza.BackColor = Color.Red
                    Formmenu.btncreditos.BackColor = Color.Red
                    Formmenu.btnvacaciones.BackColor = Color.Red
                    Formmenu.btncomprobante.BackColor = Color.Red
                    Formmenu.btnmarcasq.BackColor = Color.Red
                    Formmenu.btnmarcasg.BackColor = Color.Red
                    Formmenu.btncalendario.BackColor = Color.Red
                    Formmenu.btncircular.BackColor = Color.Red
                    Formmenu.Btnferiados.BackColor = Color.Red

                    Formmenu.Button1.Text = "Marcar"
                    m_FPM.CloseDevice()
                    Me.Close()


                End If


                bandera_total = False
                bandera_ubicacion = False




                '---------------------------------busco tienda en archivo txt---------------------------------------------

                'Pass the file path and the file name to the StreamReader constructor.
                objStreamReader = New StreamReader("C:\kIOSCO 8 AWS\TIENDA.txt")

                'Read the first line of text.
                strLine = objStreamReader.ReadLine

                'Do While Not strLine Is Nothing

                '    strLine = objStreamReader.ReadLine
                'Loop

                'Close the file.
                objStreamReader.Close()









                '-----------------bandera ubicacion aca--------------------------------------------------




                cmdconsulta_ubicacion = New SqlClient.SqlCommand
                daconsulta_ubicacion = New SqlClient.SqlDataAdapter
                dsconsulta_ubicacion = New DataSet
                With cmdconsulta_ubicacion
                    .Connection = conexion
                    .CommandText = "SELECT DISTINCT(U.DESCRIPCION),E.NOMBRE FROM EXACTUS.DIGEMA.EMPLEADO E WITH(NOLOCK) INNER JOIN EXACTUS.DIGEMA.UBICACION U WITH(NOLOCK) ON E.UBICACION=U.UBICACION WHERE e.EMPLEADO='" & emplea2_encontrado & "'"



                    .CommandType = CommandType.Text
                End With
                daconsulta_ubicacion.SelectCommand = cmdconsulta_ubicacion
                daconsulta_ubicacion.Fill(dsconsulta_ubicacion)


                For i = 0 To dsconsulta_ubicacion.Tables(0).Rows.Count - 1




                    ubicacion = dsconsulta_ubicacion.Tables(0).Rows(i).Item("DESCRIPCION")
                    EMPLEADO_UBICACION = dsconsulta_ubicacion.Tables(0).Rows(i).Item("NOMBRE")




                    If ubicacion <> strLine Then
                        If ubicacion <> "ADMINISTRATIVO" Then

                            bandera_ubicacion = True

                        End If

                    End If

                Next





                If bandera_ubicacion = True Then


                    Formmenu.Button1.Text = "Marcar"
                    m_FPM.CloseDevice()
                    MsgBox(EMPLEADO_UBICACION & "  Su ubicación de trabajo es  " & ubicacion & " debe marcar en ese quiosco respectivo", MsgBoxStyle.Critical, "Construplaza")
                    Me.Close()

                End If

                '-----------------bandera ubicacion aca--------------------------------------------------


                '-----------------bandera choferes aca--------------------------------------------------

                bandera_chofer = False



                cmdconsulta_chofer = New SqlClient.SqlCommand
                daconsulta_chofer = New SqlClient.SqlDataAdapter
                dsconsulta_chofer = New DataSet
                With cmdconsulta_chofer
                    .Connection = conexion
                    .CommandText = "SELECT DISTINCT(P.DESCRIPCION) AS PUESTO FROM EXACTUS.DIGEMA.EMPLEADO E WITH(NOLOCK) INNER JOIN EXACTUS.DIGEMA.PUESTO P WITH(NOLOCK) ON P.PUESTO=E.PUESTO WHERE E.ACTIVO='S' AND (P.DESCRIPCION LIKE '%chofer%') and E.EMPLEADO='" & emplea2_encontrado & "'"


                    .CommandType = CommandType.Text
                End With
                daconsulta_chofer.SelectCommand = cmdconsulta_chofer
                daconsulta_chofer.Fill(dsconsulta_chofer)


                For i = 0 To dsconsulta_chofer.Tables(0).Rows.Count - 1

                    bandera_chofer = True

                Next




                'bandera total aca------------------------------------------------


                If bandera_ubicacion = False And bandera_chofer = False Then


                    'cmdconsulta_total = New SqlClient.SqlCommand
                    'daconsulta_total = New SqlClient.SqlDataAdapter
                    'dsconsulta_total = New DataSet
                    'With cmdconsulta_total
                    '    .Connection = conexion
                    '    .CommandText = "SELECT " +
                    '    "C1.EMPLEADO " +
                    '    ",C1.NOMBRE " +
                    '    ",C1.SALIDA_OFICIAL " +
                    '    ",C1.TIPO " +
                    '    ",C1.DIFERENCIA_TIEMPO " +
                    '    "FROM " +
                    '    "(SELECT M.EMPLEADO " +
                    '    ",E.NOMBRE " +
                    '    ",CASE WHEN C.JORNADA='DIURNA' THEN M.HORA_SALIDA_OFICIAL ELSE M.HORA_ENTRADA_OFICIAL END AS SALIDA_OFICIAL " +
                    '    ",ISNULL(M.TIPO,0) AS TIPO " +
                    '    ",CASE WHEN C.JORNADA='DIURNA'  THEN DATEDIFF(MINUTE, DATEADD(MINUTE,-0,CONVERT(TIME, M.HORA_SALIDA_OFICIAL, 108)), DATEADD(MINUTE,0,CONVERT(TIME, GETDATE(), 108))) " +
                    '    "ELSE  DATEDIFF(MINUTE, DATEADD(MINUTE,-0,CONVERT(TIME, M.HORA_ENTRADA_OFICIAL, 108)), DATEADD(MINUTE,0,CONVERT(TIME, GETDATE(), 108))) END " +
                    '    "AS DIFERENCIA_TIEMPO " +
                    '    "FROM ASISTENCIA.DBO.REVISION_MARCA M " +
                    '    "INNER JOIN ASISTENCIA.DBO.CALENDARIO C WITH(NOLOCK) ON C.EMPLEADO=M.EMPLEADO " +
                    '    "INNER JOIN EXACTUS.DIGEMA.EMPLEADO E WITH(NOLOCK) ON E.EMPLEADO=M.EMPLEADO)C1 " +
                    '    "WHERE C1.DIFERENCIA_TIEMPO < 0 AND C1.TIPO > 0 AND C1.EMPLEADO='" & emplea2_encontrado & "'"



                    '    .CommandType = CommandType.Text
                    'End With
                    'daconsulta_total.SelectCommand = cmdconsulta_total
                    'daconsulta_total.Fill(dsconsulta_total)


                    'For i = 0 To dsconsulta_total.Tables(0).Rows.Count - 1

                    '    'bandera_total = True

                    '    bandera_total = False




                    '    empleado_total = dsconsulta_total.Tables(0).Rows(i).Item("EMPLEADO")
                    '    nombre_total = dsconsulta_total.Tables(0).Rows(i).Item("NOMBRE")
                    '    salida_total = dsconsulta_total.Tables(0).Rows(i).Item("SALIDA_OFICIAL")
                    '    diferencia_total = dsconsulta_total.Tables(0).Rows(i).Item("DIFERENCIA_TIEMPO")


                    'Next


                    'If bandera_total = True Then


                    '    Formmenu.Button1.Text = "Marcar"
                    '    m_FPM.CloseDevice()
                    '    MsgBox(nombre_total & "  su hora de salida es a las " & salida_total & "  antes de su hora de salida el reloj marcador no aceptara su marca", MsgBoxStyle.Critical, "Construplaza")
                    '    Me.Close()

                    'End If

                    bandera_total = False

                End If

                cuenta_tardias = 0




                '--------------CUENTA TARDIAS-------------------------------------------------------------------------------------------






                cmdconsulta_total_tardias = New SqlClient.SqlCommand
                daconsulta_total_tardias = New SqlClient.SqlDataAdapter
                dsconsulta_total_tardias = New DataSet
                With cmdconsulta_total_tardias
                    .Connection = conexion
                    .CommandText = "SELECT COUNT(C4.EMPLEADO) AS TOTAL " +
                        "FROM " +
                        "(SELECT " +
                        "C3.UBICACION " +
                        ",C3.EMPLEADO " +
                        ",C3.NOMBRE " +
                        ",C3.DIA + ' ' +  CONVERT(VARCHAR(6), CONVERT(datetime, C3.FECHA, 103), 6)  AS FECHA " +
                        ",C3.HORA_ENTRADA " +
                        ",c3.MARCA_ENTRADA " +
                        ",CASE WHEN C3.PUESTO LIKE ('%Chofer%') THEN ' Trabajar de Confianza' ELSE C3.HORA_SALIDA END AS HORA_SALIDA " +
                        ",c3.MARCA_SALIDA " +
                        ",CASE " +
                        "WHEN C3.PUESTO LIKE ('%Chofer%') AND C3.HORA_ENTRADA NOT LIKE '%LIBRE%' " +
                        "AND C3.MARCA_ENTRADA='00:00' AND C3.MARCA_SALIDA='00:00' AND C3.ESTADO NOT LIKE '%FERIADO%' " +
                        "AND C3.ESTADO NOT LIKE '%VACACIONES%'AND C3.ESTADO NOT LIKE '%INCAPACI%' THEN 'AUSENTE' " +
                        "WHEN C3.PUESTO LIKE ('%Chofer%') AND C3.HORA_ENTRADA <> C3.MARCA_ENTRADA THEN 'OK' " +
                        "WHEN C3.TARDIA='' THEN C3.ESTADO " +
                        "ELSE C3.TARDIA END AS ESTADO " +
                        "FROM (SELECT " +
                        "C2.EMPLEADO " +
                        ",C2.NOMBRE " +
                        ",C2.UBICACION " +
                        ",C2.FERIADO " +
                        ",C2.PUESTO " +
                        ",CASE WHEN (DATENAME(dw, CONVERT(datetime, C2.FECHA, 103)))='Monday' THEN 'LUNES' " +
                        "WHEN (DATENAME(dw, CONVERT(datetime, C2.FECHA, 103)))='Tuesday' THEN 'MARTES' " +
                        "WHEN (DATENAME(dw, CONVERT(datetime, C2.FECHA, 103)))='Wednesday' THEN 'MIERCOLES' " +
                        "WHEN (DATENAME(dw, CONVERT(datetime, C2.FECHA, 103)))='Thursday' THEN 'JUEVES' " +
                        "WHEN (DATENAME(dw, CONVERT(datetime, C2.FECHA, 103)))='Friday' THEN 'VIERNES' " +
                        "WHEN (DATENAME(dw, CONVERT(datetime, C2.FECHA, 103)))='Saturday' THEN 'SABADO' " +
                        "WHEN (DATENAME(dw, CONVERT(datetime, C2.FECHA, 103)))='Sunday' THEN 'DOMINGO' " +
                        "END AS DIA " +
                        ",C2.FECHA " +
                        ",C2.ENTRADA AS MARCA_ENTRADA " +
                        ",ISNULL(C2.ENTRADA_OFICIAL,'LIBRE')AS HORA_ENTRADA " +
                        ",C2.SALIDA AS MARCA_SALIDA " +
                        ",ISNULL(C2.SALIDA_OFICIAL,'LIBRE')AS HORA_SALIDA " +
                        ",CASE WHEN C2.HORAS_LABORADAS=0 AND C2.ENTRADA_OFICIAL IS NULL AND C2.SALIDA_OFICIAL IS NULL AND C2.JORNADA='DIURNA' THEN 'OK' " +
                        "WHEN C2.PUESTO LIKE ('%Chofer%') AND C2.ENTRADA_OFICIAL <> C2.ENTRADA THEN 'OK' " +
                        "WHEN (SELECT COUNT(NUMERO_ACCION) FROM EXACTUS.DIGEMA.EMPLEADO_ACC_PER where EMPLEADO=C2.EMPLEADO " +
                        "AND FECHA_RIGE <= Convert(datetime, C2.FECHA, 103) AND FECHA_VENCE >=Convert(datetime, C2.FECHA, 103) AND TIPO_ACCION IN ('IINS','ICCS')) > 0 THEN 'AUSENTE_INCAPACIDAD*' " +
                        "WHEN (SELECT COUNT(NUMERO_ACCION) FROM EXACTUS.DIGEMA.EMPLEADO_ACC_PER where EMPLEADO=C2.EMPLEADO " +
                        "AND FECHA_RIGE <= Convert(datetime, C2.FECHA, 103) AND FECHA_VENCE >=Convert(datetime, C2.FECHA, 103) AND TIPO_ACCION IN ('VAGO')) > 0 THEN 'VACACIONES*' " +
                        "WHEN C2.PUESTO LIKE ('%Chofer%') AND C2.ENTRADA_OFICIAL IS NOT NULL AND C2.SALIDA_OFICIAL IS NOT NULL AND C2.FERIADO IS NOT NULL  AND C2.JORNADA='DIURNA' AND C2.ENTRADA <> '00:00' AND C2.SALIDA <> '00:00'  THEN 'FERIADO + 8*' " +
                        "WHEN C2.HORAS_LABORADAS > 0 AND C2.ENTRADA_OFICIAL IS NOT NULL AND C2.SALIDA_OFICIAL IS NOT NULL AND C2.FERIADO IS NOT NULL  AND C2.JORNADA='DIURNA' THEN 'FERIADO + 8*' " +
                        "WHEN C2.HORAS_LABORADAS = 0 AND C2.ENTRADA_OFICIAL IS NOT NULL AND C2.SALIDA_OFICIAL IS NOT NULL AND C2.FERIADO IS NOT NULL  AND C2.JORNADA='DIURNA' THEN 'FERIADO*' " +
                        "WHEN C2.PUESTO LIKE ('%Chofer%') AND C2.ENTRADA = C2.SALIDA AND C2.ENTRADA <> C2.ENTRADA_OFICIAL THEN 'OK' " +
                        "WHEN C2.PUESTO LIKE ('%Chofer%') AND C2.ENTRADA = '00:00' AND C2.SALIDA='00:00' THEN 'AUSENTE' " +
                        "WHEN C2.PUESTO LIKE ('%Chofer%') AND C2.ENTRADA <> '00:00' AND C2.SALIDA <> '00:00' THEN 'OK' " +
                        "WHEN DATEADD(MINUTE,0,CONVERT(TIME, C2.ENTRADA, 108)) > DATEADD(MINUTE,15,CONVERT(TIME, C2.ENTRADA_OFICIAL, 108))  AND C2.JORNADA='DIURNA' THEN 'AUSENTE' " +
                        "WHEN DATEADD(MINUTE,0,CONVERT(TIME, C2.SALIDA, 108)) < DATEADD(MINUTE,-15,CONVERT(TIME, C2.SALIDA_OFICIAL, 108))  AND C2.JORNADA='DIURNA' THEN 'AUSENTE' " +
                        "WHEN C2.HORAS_LABORADAS > 0 AND C2.ENTRADA_OFICIAL IS NOT NULL AND C2.SALIDA_OFICIAL IS NOT NULL  AND C2.JORNADA='DIURNA' THEN 'OK' " +
                        "WHEN C2.HORAS_LABORADAS > 0 AND C2.ENTRADA_OFICIAL IS NULL AND C2.SALIDA_OFICIAL IS NULL  AND C2.JORNADA='DIURNA' THEN 'OK' " +
                        "ELSE 'AUSENTE' " +
                        "END AS ESTADO " +
                        ",CASE WHEN DATEADD(MINUTE,0,CONVERT(TIME, C2.ENTRADA, 108)) > DATEADD(MINUTE,0,CONVERT(TIME, C2.ENTRADA_OFICIAL, 108)) AND DATEADD(MINUTE,0,CONVERT(TIME, C2.ENTRADA, 108)) <=DATEADD(MINUTE,15,CONVERT(TIME, C2.ENTRADA_OFICIAL, 108)) THEN 'MARCA TARDE' " +
                        "WHEN DATEADD(MINUTE,0,CONVERT(TIME, C2.SALIDA, 108)) >= DATEADD(MINUTE,-15,CONVERT(TIME, C2.SALIDA_OFICIAL, 108)) AND DATEADD(MINUTE,0,CONVERT(TIME, C2.SALIDA, 108)) < DATEADD(MINUTE,0,CONVERT(TIME, C2.SALIDA_OFICIAL, 108)) THEN 'AUSENTE' " +
                        "ELSE ' ' " +
                        "END AS TARDIA " +
                        "FROM " +
                        "(SELECT DISTINCT(FECHA) " +
                        ",C.JORNADA " +
                        ",E.EMPLEADO " +
                        ",E.NOMBRE_PILA + ' ' + E.PRIMER_APELLIDO AS NOMBRE " +
                        ",DATEDIFF(hh, ISNULL((SELECT TOP 1 HORA FROM ASISTENCIA.DBO.MARCA S WITH(NOLOCK) WHERE S.EMPLEADO=E.EMPLEADO AND S.FECHA=CE.FECHA ORDER BY TIPO),'00:00'),ISNULL((SELECT TOP 1 HORA FROM ASISTENCIA.DBO.MARCA S WITH(NOLOCK) WHERE S.EMPLEADO=E.EMPLEADO AND S.FECHA=CE.FECHA ORDER BY TIPO DESC),'00:00'))AS HORAS_LABORADAS " +
                        ",ISNULL((SELECT TOP 1 HORA FROM ASISTENCIA.DBO.MARCA S WITH(NOLOCK) WHERE S.EMPLEADO=E.EMPLEADO AND S.FECHA=CE.FECHA ORDER BY TIPO),'00:00')AS ENTRADA " +
                        ",ISNULL((SELECT TOP 1 HORA FROM ASISTENCIA.DBO.MARCA S WITH(NOLOCK) WHERE S.EMPLEADO=E.EMPLEADO AND S.FECHA=CE.FECHA ORDER BY TIPO DESC),'00:00')AS SALIDA " +
                        ",CE.EVENTO " +
                        ",CE.INDICADOR " +
                        ",CE.FERIADO " +
                        ",CASE WHEN CE.INDICADOR='1-L' THEN C.[1-L-AM] " +
                        "WHEN CE.INDICADOR='2-L' THEN C.[2-L-AM] " +
                        "WHEN CE.INDICADOR='1-K' THEN C.[1-K-AM] " +
                        "WHEN CE.INDICADOR='2-K' THEN C.[2-K-AM] " +
                        "WHEN CE.INDICADOR='1-M' THEN C.[1-M-AM] " +
                        "WHEN CE.INDICADOR='2-M' THEN C.[2-M-AM] " +
                        "WHEN CE.INDICADOR='1-J' THEN C.[1-J-AM] " +
                        "WHEN CE.INDICADOR='2-J' THEN C.[2-J-AM] " +
                        "WHEN CE.INDICADOR='1-V' THEN C.[1-V-AM] " +
                        "WHEN CE.INDICADOR='2-V' THEN C.[2-V-AM] " +
                        "WHEN CE.INDICADOR='1-S' THEN C.[1-S-AM] " +
                        "WHEN CE.INDICADOR='2-S' THEN C.[2-S-AM] " +
                        "WHEN CE.INDICADOR='1-D' THEN C.[1-D-AM] " +
                        "WHEN CE.INDICADOR='2-D' THEN C.[2-D-AM] " +
                        "END AS ENTRADA_OFICIAL " +
                        ",CASE WHEN CE.INDICADOR='1-L' THEN C.[1-L-PM] " +
                        "WHEN CE.INDICADOR='2-L' THEN C.[2-L-PM] " +
                        "WHEN CE.INDICADOR='1-K' THEN C.[1-K-PM] " +
                        "WHEN CE.INDICADOR='2-K' THEN C.[2-K-PM] " +
                        "WHEN CE.INDICADOR='1-M' THEN C.[1-M-PM] " +
                        "WHEN CE.INDICADOR='2-M' THEN C.[2-M-PM] " +
                        "WHEN CE.INDICADOR='1-J' THEN C.[1-J-PM] " +
                        "WHEN CE.INDICADOR='2-J' THEN C.[2-J-PM] " +
                        "WHEN CE.INDICADOR='1-V' THEN C.[1-V-PM] " +
                        "WHEN CE.INDICADOR='2-V' THEN C.[2-V-PM] " +
                        "WHEN CE.INDICADOR='1-S' THEN C.[1-S-PM] " +
                        "WHEN CE.INDICADOR='2-S' THEN C.[2-S-PM] " +
                        "WHEN CE.INDICADOR='1-D' THEN C.[1-D-PM] " +
                        "WHEN CE.INDICADOR='2-D' THEN C.[2-D-PM] " +
                        "END AS SALIDA_OFICIAL " +
                        ",P.DESCRIPCION AS PUESTO " +
                        ",U.DESCRIPCION AS UBICACION " +
                        "FROM ASISTENCIA.DBO.CALENDARIO_EVENTO CE WITH(NOLOCK) " +
                        ",EXACTUS.digema.EMPLEADO  E WITH(NOLOCK) " +
                        ",EXACTUS.digema.PUESTO  P WITH(NOLOCK) " +
                        ",EXACTUS.digema.UBICACION  U WITH(NOLOCK) " +
                        ",ASISTENCIA.DBO.CALENDARIO C WITH(NOLOCK) " +
                        "WHERE CONVERT(datetime, CE.FECHA, 103) < DATEADD(ms,-3,DATEADD(dd,DATEDIFF(dd,0,GETDATE()-1),1)) " +
                        "AND CONVERT(datetime, CE.FECHA, 103) >= DATEADD(mm,DATEDIFF(mm,0,GETDATE()),0) " +
                        "AND E.ACTIVO='S' " +
                        "AND E.EMPLEADO=C.EMPLEADO " +
                        "AND C.JORNADA='DIURNA' " +
                        "AND E.EMPLEADO=C.EMPLEADO " +
                        "AND U.UBICACION NOT IN ('020') " +
                        "AND E.UBICACION=U.UBICACION " +
                        "AND E.PUESTO=P.PUESTO)C2)C3 WHERE C3.EMPLEADO NOT IN ('0655','0670','0980','0677','0678'))C4 " +
                        "WHERE C4.ESTADO='MARCA TARDE' AND C4.EMPLEADO='" & emplea2_encontrado & "'"



                    .CommandType = CommandType.Text
                End With
                daconsulta_total_tardias.SelectCommand = cmdconsulta_total_tardias
                daconsulta_total_tardias.Fill(dsconsulta_total_tardias)




                For k = 0 To dsconsulta_total_tardias.Tables(0).Rows.Count - 1



                    cuenta_tardias = CInt(dsconsulta_total_tardias.Tables(0).Rows(k).Item("TOTAL"))


                Next


                If cuenta_tardias > 0 And cuenta_tardias < 5 Then
                    MsgBox("Estimado compañero, usted lleva " & cuenta_tardias & " tardias en el mes, se le recuerda que después de la quinta tardía, se tomará su marca como ausente ", MsgBoxStyle.Critical, "Construplaza")

                End If

                'If cuenta_tardias >= 5 Then
                '    MsgBox("Estimado compañero, usted lleva " & cuenta_tardias & " tardias en el mes, se ha marcado un día como ausente y cada vez que marque tardía en el mismo mes", MsgBoxStyle.Critical, "Construplaza")

                'End If

                '-----------------------------------------------------------------------------------------------------------------------


                '-----------------bandera total aca--------------------------------------------------


                If emplea2_encontrado <> "NO" And encontrado = True And bandera_total = False And bandera_ubicacion = False Then


                    '----------------------------------------marco y muestro datos de empleado-------------------------------------------------------------

                    cmdconsulta_documentos = New SqlClient.SqlCommand
                    daconsulta_documentos = New SqlClient.SqlDataAdapter
                    dsconsulta_documentos = New DataSet
                    With cmdconsulta_documentos
                        .Connection = conexion
                        .CommandText = "SELECT NOMBRE,FOTOGRAFIA FROM EXACTUS.DIGEMA.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & emplea2_encontrado & "' "
                        .CommandType = CommandType.Text
                    End With
                    daconsulta_documentos.SelectCommand = cmdconsulta_documentos
                    daconsulta_documentos.Fill(dsconsulta_documentos)


                    For i = 0 To dsconsulta_documentos.Tables(0).Rows.Count - 1

                        emplea2_encontrado_nombre = dsconsulta_documentos.Tables(0).Rows(i).Item("NOMBRE")


                        obj_carga = dsconsulta_documentos.Tables(0).Rows(i).Item("FOTOGRAFIA")

                        '  MsgBox(emplea2_encontrado_nombre)

                        If Not IsDBNull(obj_carga) Then
                            B_carga = DirectCast(obj_carga, Byte())

                            Using ms As New IO.MemoryStream(B_carga)

                                PictureBox2.Image = Image.FromStream(ms)
                                PictureBox2.SizeMode = PictureBoxSizeMode.StretchImage


                            End Using



                        End If


                    Next


                    ' StatusBar.Text = emplea2_encontrado & " " & emplea2_encontrado_nombre


                    Dim feha_marca As String = DateTime.Now.ToString("dd/MM/yyyy")
                    Dim hora_marca As String = Now.ToString("HH:mm")
                    'Date.Now.ToShortTimeString
                    Dim tipo As Integer = 0


                    '-----------------------------------------------------------------------------------CONTADOR DE MARCAS

                    cmdconsulta_documentos = New SqlClient.SqlCommand
                    daconsulta_documentos = New SqlClient.SqlDataAdapter
                    dsconsulta_documentos = New DataSet
                    With cmdconsulta_documentos
                        .Connection = conexion
                        .CommandText = "SELECT COUNT(TIPO)AS CONTADOR FROM ASISTENCIA.DBO.MARCA WITH(NOLOCK) WHERE EMPLEADO='" & emplea2_encontrado & "' AND FECHA='" & feha_marca & "'"
                        .CommandType = CommandType.Text
                    End With
                    daconsulta_documentos.SelectCommand = cmdconsulta_documentos
                    daconsulta_documentos.Fill(dsconsulta_documentos)


                    For i = 0 To dsconsulta_documentos.Tables(0).Rows.Count - 1

                        conta_marcas = dsconsulta_documentos.Tables(0).Rows(i).Item("CONTADOR") + 1

                    Next

                    bandera_ingreso = 0



                    '-----------------------------------------------INSERTA MARCA


                    If conexion.State = ConnectionState.Closed Then
                        conexion.Open()
                    End If



                    transaccion = conexion.BeginTransaction
                    cmdincluir = New SqlClient.SqlCommand

                    'tipo

                    parametros = "'" & emplea2_encontrado & "','" & feha_marca & "','" & hora_marca & "'," & conta_marcas & ""

                    '   MsgBox("Empleado no ha sido ingresado ," & parametros, MsgBoxStyle.Critical)



                    With cmdincluir
                        .Connection = conexion
                        .CommandText = "INSERT INTO ASISTENCIA.DBO.MARCA(EMPLEADO,FECHA,HORA,TIPO) values (" & parametros & ")"
                        .CommandType = CommandType.Text
                        .Transaction = transaccion
                    End With
                    iregincluir = cmdincluir.ExecuteNonQuery
                    transaccion.Commit()

                    '  MsgBox("Estimado " & nombres & "  ustes esta marcando a las " & hora_marca & " del día " & feha_marca, vbInformation)

                    StatusBar.Text = "Estimado " & emplea2_encontrado_nombre & "  usted esta marcando a las " & hora_marca & " del día " & feha_marca

                    lblnombre.Text = emplea2_encontrado_nombre


                    bandera_ingreso = 1

                    lblhora.Text = hora_marca
                    lblfecha.Text = feha_marca
                    lblhora.Visible = True
                    lblfecha.Visible = True


                    Me.BackColor = Color.Green
                    Formmenu.BackColor = Color.Green
                    Formmenu.btnIngresar.BackColor = Color.Green
                    Formmenu.btnmarcar.BackColor = Color.Green
                    Formmenu.Button1.BackColor = Color.Green
                    Formmenu.btnaseconstruplaza.BackColor = Color.Green
                    Formmenu.btncreditos.BackColor = Color.Green
                    Formmenu.btnvacaciones.BackColor = Color.Green
                    Formmenu.btncomprobante.BackColor = Color.Green
                    Formmenu.btnmarcasq.BackColor = Color.Green
                    Formmenu.btnmarcasg.BackColor = Color.Green
                    Formmenu.btncalendario.BackColor = Color.Green
                    Formmenu.btncircular.BackColor = Color.Green
                    Formmenu.Btnferiados.BackColor = Color.Green
                    Formmenu.Button1.Text = "Marcar"
                    m_FPM.CloseDevice()




                    '---AQUI VERIFICA SI ES NOCTURNO O DIURNO------------------------------------------------------------------------



                    cmdconsulta_documentos = New SqlClient.SqlCommand
                    daconsulta_documentos = New SqlClient.SqlDataAdapter
                    dsconsulta_documentos = New DataSet
                    With cmdconsulta_documentos
                        .Connection = conexion
                        .CommandText = "SELECT JORNADA FROM ASISTENCIA.DBO.CALENDARIO WHERE EMPLEADO='" & emplea2_encontrado & "'"
                        .CommandType = CommandType.Text
                    End With
                    daconsulta_documentos.SelectCommand = cmdconsulta_documentos
                    daconsulta_documentos.Fill(dsconsulta_documentos)


                    For i = 0 To dsconsulta_documentos.Tables(0).Rows.Count - 1

                        JORNADA = dsconsulta_documentos.Tables(0).Rows(i).Item("JORNADA")

                    Next


                    '-----------------------------------EMPIEZA LA JORNADA NOCTURNA---


                    If JORNADA = "NOCTURNA" Then


                        '  HORACHEQUEO = DateTime.Now.ToString("hh:mm:ss")
                        'Now.ToShortTimeString.ToString()

                        Dim TIME As Date
                        TIME = DateTime.Now
                        Dim HORACHEQUEO As Integer = TIME.Hour




                        cmdconsulta = New SqlClient.SqlCommand
                        daconsulta = New SqlClient.SqlDataAdapter
                        dsconsulta = New DataSet
                        With cmdconsulta
                            .Connection = conexion
                            .CommandText = "SELECT [ASISTENCIA].[dbo].[VALORA_LIBRE_NOCTURNO] (EMPLEADO)AS DIA FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & emplea2_encontrado & "'"
                            .CommandType = CommandType.Text
                        End With
                        daconsulta.SelectCommand = cmdconsulta
                        daconsulta.Fill(dsconsulta)


                        For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
                            DIA = dsconsulta.Tables(0).Rows(z).Item("DIA")
                        Next



                        If DIA = "LIBRE" Then

                            txttexto.Text = "Estimado usted marcó la ENTRADA antes de su hora de ingreso, HOY ES SU DIA LIBRE"

                            Me.BackColor = Color.Green
                            Formmenu.BackColor = Color.Green
                            Formmenu.btnIngresar.BackColor = Color.Green
                            Formmenu.btnmarcar.BackColor = Color.Green
                            Formmenu.Button1.BackColor = Color.Green
                            Formmenu.btnaseconstruplaza.BackColor = Color.Green
                            Formmenu.btnaseconstruplaza.BackColor = Color.Green
                            Formmenu.btncreditos.BackColor = Color.Green
                            Formmenu.btnvacaciones.BackColor = Color.Green
                            Formmenu.btncomprobante.BackColor = Color.Green
                            Formmenu.btnmarcasq.BackColor = Color.Green
                            Formmenu.btnmarcasg.BackColor = Color.Green
                            Formmenu.btncalendario.BackColor = Color.Green
                            Formmenu.btncircular.BackColor = Color.Green
                            Formmenu.Btnferiados.BackColor = Color.Green
                            Formmenu.Button1.Text = "Marcar"
                            m_FPM.CloseDevice()
                            lblestados.ForeColor = Color.White
                            lblestados.Visible = True

                        End If




                        '---------------------------------MARCA ENTRADA--------------------------------------------------------------------------
                        If DIA <> "LIBRE" Then






                            If HORACHEQUEO > 12 Then

                                'MsgBox(HORACHEQUEO)




                                'MsgBox("ENTRADA")

                                cmdconsulta = New SqlClient.SqlCommand
                                daconsulta = New SqlClient.SqlDataAdapter
                                dsconsulta = New DataSet
                                With cmdconsulta
                                    .Connection = conexion
                                    .CommandText = "SELECT TOP 1 E.NOMBRE,HORA,ASISTENCIA.dbo.VALORA_MARCA_ENTRADA_NOCTURNO(E.EMPLEADO)AS ESTADO " +
                                                    ",(SELECT CASE WHEN CE.INDICADOR='1-L' THEN CN.[1-L-PM] " +
                                                    "WHEN CE.INDICADOR='2-L' THEN CN.[2-L-PM] " +
                                                    "WHEN CE.INDICADOR='1-K' THEN CN.[1-K-PM] " +
                                                    "WHEN CE.INDICADOR='2-K' THEN CN.[2-K-PM] " +
                                                    "WHEN CE.INDICADOR='1-M' THEN CN.[1-M-PM] " +
                                                    "WHEN CE.INDICADOR='2-M' THEN CN.[2-M-PM] " +
                                                    "WHEN CE.INDICADOR='1-J' THEN CN.[1-J-PM] " +
                                                    "WHEN CE.INDICADOR='2-J' THEN CN.[2-J-PM] " +
                                                    "WHEN CE.INDICADOR='1-V' THEN CN.[1-V-PM] " +
                                                    "WHEN CE.INDICADOR='2-V' THEN CN.[2-V-PM] " +
                                                    "WHEN CE.INDICADOR='1-S' THEN CN.[1-S-PM] " +
                                                    "WHEN CE.INDICADOR='2-S' THEN CN.[2-S-PM] " +
                                                    "WHEN CE.INDICADOR='1-D' THEN CN.[1-D-PM] " +
                                                    "WHEN CE.INDICADOR='2-D' THEN CN.[2-D-PM] " +
                                                    "END FROM ASISTENCIA.DBO.CALENDARIO CN WITH(NOLOCK) WHERE CN.EMPLEADO=E.EMPLEADO)AS ENTRADA " +
                                                    "FROM ASISTENCIA.DBO.MARCA M WITH(NOLOCK),EXACTUS.DIGEMA.EMPLEADO E WITH(NOLOCK),ASISTENCIA.DBO.CALENDARIO_EVENTO CE WITH(NOLOCK) " +
                                                    "WHERE M.EMPLEADO='" & emplea2_encontrado & "' AND E.EMPLEADO=M.EMPLEADO AND M.FECHA=CONVERT(varchar(11),GETDATE(),103)AND CE.FECHA=M.FECHA " +
                                                    "ORDER BY M.TIPO DESC"
                                    .CommandType = CommandType.Text
                                End With
                                daconsulta.SelectCommand = cmdconsulta
                                daconsulta.Fill(dsconsulta)


                                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1



                                    NOMBREN = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                                    HORAN = dsconsulta.Tables(0).Rows(z).Item("HORA")
                                    ENTRADAN = dsconsulta.Tables(0).Rows(z).Item("ENTRADA")
                                    ESTADON = dsconsulta.Tables(0).Rows(z).Item("ESTADO")



                                Next




                                If ESTADON = "OK" And emplea2_encontrado <> "NO" And encontrado = True Then
                                    txttexto.Text = "Estimado '" & NOMBREN & "' usted marcó la ENTRADA antes de su hora de ingreso la cual es a las : " & ENTRADAN
                                    Me.BackColor = Color.Green
                                    Formmenu.BackColor = Color.Green
                                    Formmenu.btnIngresar.BackColor = Color.Green
                                    Formmenu.btnmarcar.BackColor = Color.Green
                                    Formmenu.Button1.BackColor = Color.Green
                                    Formmenu.btnaseconstruplaza.BackColor = Color.Green
                                    Formmenu.btnaseconstruplaza.BackColor = Color.Green
                                    Formmenu.btncreditos.BackColor = Color.Green
                                    Formmenu.btnvacaciones.BackColor = Color.Green
                                    Formmenu.btncomprobante.BackColor = Color.Green
                                    Formmenu.btnmarcasq.BackColor = Color.Green
                                    Formmenu.btnmarcasg.BackColor = Color.Green
                                    Formmenu.btncalendario.BackColor = Color.Green
                                    Formmenu.btncircular.BackColor = Color.Green
                                    Formmenu.Btnferiados.BackColor = Color.Green
                                    Formmenu.Button1.Text = "Marcar"
                                    m_FPM.CloseDevice()
                                    lblestados.Text = "Estado:  " & ESTADON
                                    lblestados.ForeColor = Color.White
                                    lblestados.Visible = True

                                End If



                                If ESTADON = "TARDIA" And emplea2_encontrado <> "NO" And encontrado = True Then

                                    txttexto.Text = "Estimado '" & NOMBREN & "' usted marcó la ENTRADA en un estado ya de TARDIA, su hora oficial de ENTRADA es a las : " & ENTRADAN

                                    Me.BackColor = Color.Yellow
                                    Formmenu.BackColor = Color.Yellow
                                    Formmenu.btnIngresar.BackColor = Color.Yellow
                                    Formmenu.btnmarcar.BackColor = Color.Yellow
                                    Formmenu.Button1.BackColor = Color.Yellow
                                    Formmenu.btnaseconstruplaza.BackColor = Color.Yellow
                                    Formmenu.btnaseconstruplaza.BackColor = Color.Yellow
                                    Formmenu.btncreditos.BackColor = Color.Yellow
                                    Formmenu.btnvacaciones.BackColor = Color.Yellow
                                    Formmenu.btncomprobante.BackColor = Color.Yellow
                                    Formmenu.btnmarcasq.BackColor = Color.Yellow
                                    Formmenu.btnmarcasg.BackColor = Color.Yellow
                                    Formmenu.btncalendario.BackColor = Color.Yellow
                                    Formmenu.btncircular.BackColor = Color.Yellow
                                    Formmenu.Btnferiados.BackColor = Color.Yellow
                                    Formmenu.Button1.Text = "Marcar"
                                    m_FPM.CloseDevice()

                                    lblestados.Text = "Estado:  " & ESTADON
                                    lblestados.ForeColor = Color.White
                                    lblestados.Visible = True

                                End If


                                If ESTADON = "AUSENTE" And emplea2_encontrado <> "NO" And encontrado = True Then

                                    txttexto.Text = "Estimado '" & NOMBREN & "' usted marcó la ENTRADA en un estado ya de AUSENTE, su hora oficial de ENTRADA es a las: " & ENTRADAN

                                    Me.BackColor = Color.Black
                                    Formmenu.BackColor = Color.Black
                                    Formmenu.btnIngresar.BackColor = Color.Black
                                    Formmenu.btnmarcar.BackColor = Color.Black
                                    Formmenu.Button1.BackColor = Color.Black
                                    Formmenu.btnaseconstruplaza.BackColor = Color.Black
                                    Formmenu.btnaseconstruplaza.BackColor = Color.Black
                                    Formmenu.btncreditos.BackColor = Color.Black
                                    Formmenu.btnvacaciones.BackColor = Color.Black
                                    Formmenu.btncomprobante.BackColor = Color.Black
                                    Formmenu.btnmarcasq.BackColor = Color.Black
                                    Formmenu.btnmarcasg.BackColor = Color.Black
                                    Formmenu.btncalendario.BackColor = Color.Black
                                    Formmenu.btncircular.BackColor = Color.Black
                                    Formmenu.Btnferiados.BackColor = Color.Black
                                    Formmenu.Button1.Text = "Marcar"
                                    m_FPM.CloseDevice()

                                    lblestados.Text = "Estado:  " & ESTADON
                                    lblestados.ForeColor = Color.White
                                    lblestados.Visible = True

                                End If





                            End If


                            '------------------------------------SALIDA----------------------------------------------------------




                            If HORACHEQUEO < 12 Then


                                cmdconsulta = New SqlClient.SqlCommand
                                daconsulta = New SqlClient.SqlDataAdapter
                                dsconsulta = New DataSet
                                With cmdconsulta
                                    .Connection = conexion
                                    .CommandText = "SELECT TOP 1 E.NOMBRE,HORA,ASISTENCIA.dbo.VALORA_MARCA_SALIDA_NOCTURNO(E.EMPLEADO)AS ESTADO " +
                                                    ",(SELECT CASE WHEN CE.INDICADOR='1-L' THEN CN.[1-L-AM] " +
                                                    "WHEN CE.INDICADOR='2-L' THEN CN.[2-L-AM] " +
                                                    "WHEN CE.INDICADOR='1-K' THEN CN.[1-K-AM] " +
                                                    "WHEN CE.INDICADOR='2-K' THEN CN.[2-K-AM] " +
                                                    "WHEN CE.INDICADOR='1-M' THEN CN.[1-M-AM] " +
                                                    "WHEN CE.INDICADOR='2-M' THEN CN.[2-M-AM] " +
                                                    "WHEN CE.INDICADOR='1-J' THEN CN.[1-J-AM] " +
                                                    "WHEN CE.INDICADOR='2-J' THEN CN.[2-J-AM] " +
                                                    "WHEN CE.INDICADOR='1-V' THEN CN.[1-V-AM] " +
                                                    "WHEN CE.INDICADOR='2-V' THEN CN.[2-V-AM] " +
                                                    "WHEN CE.INDICADOR='1-S' THEN CN.[1-S-AM] " +
                                                    "WHEN CE.INDICADOR='2-S' THEN CN.[2-S-AM] " +
                                                    "WHEN CE.INDICADOR='1-D' THEN CN.[1-D-AM] " +
                                                    "WHEN CE.INDICADOR='2-D' THEN CN.[2-D-AM] " +
                                                    "END FROM ASISTENCIA.DBO.CALENDARIO CN WITH(NOLOCK) WHERE CN.EMPLEADO=E.EMPLEADO)AS SALIDA " +
                                                    "FROM ASISTENCIA.DBO.MARCA M WITH(NOLOCK),EXACTUS.DIGEMA.EMPLEADO E WITH(NOLOCK),ASISTENCIA.DBO.CALENDARIO_EVENTO CE WITH(NOLOCK) " +
                                                    "WHERE M.EMPLEADO='" & emplea2_encontrado & "' AND E.EMPLEADO=M.EMPLEADO AND M.FECHA=CONVERT(varchar(11),GETDATE(),103) AND CE.FECHA=M.FECHA " +
                                                    "ORDER BY M.TIPO DESC"
                                    .CommandType = CommandType.Text
                                End With
                                daconsulta.SelectCommand = cmdconsulta
                                daconsulta.Fill(dsconsulta)


                                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1



                                    NOMBREN = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                                    HORAN = dsconsulta.Tables(0).Rows(z).Item("HORA")
                                    SALIDAN = dsconsulta.Tables(0).Rows(z).Item("SALIDA")
                                    ESTADON = dsconsulta.Tables(0).Rows(z).Item("ESTADO")



                                Next




                                If ESTADON = "OK" And emplea2_encontrado <> "NO" And encontrado = True Then
                                    txttexto.Text = "Estimado '" & NOMBREN & "' usted marcó la SALIDA antes de su hora de ingreso la cual es a las : " & SALIDAN
                                    Me.BackColor = Color.Green
                                    Formmenu.BackColor = Color.Green
                                    Formmenu.btnIngresar.BackColor = Color.Green
                                    Formmenu.btnmarcar.BackColor = Color.Green
                                    Formmenu.Button1.BackColor = Color.Green
                                    Formmenu.btnaseconstruplaza.BackColor = Color.Green
                                    Formmenu.btnaseconstruplaza.BackColor = Color.Green
                                    Formmenu.btncreditos.BackColor = Color.Green
                                    Formmenu.btnvacaciones.BackColor = Color.Green
                                    Formmenu.btncomprobante.BackColor = Color.Green
                                    Formmenu.btnmarcasq.BackColor = Color.Green
                                    Formmenu.btnmarcasg.BackColor = Color.Green
                                    Formmenu.btncalendario.BackColor = Color.Green
                                    Formmenu.btncircular.BackColor = Color.Green
                                    Formmenu.Btnferiados.BackColor = Color.Green
                                    Formmenu.Button1.Text = "Marcar"
                                    m_FPM.CloseDevice()
                                    lblestados.Text = "Estado:  " & ESTADON
                                    lblestados.ForeColor = Color.White
                                    lblestados.Visible = True

                                End If



                                If ESTADON = "TARDIA" And emplea2_encontrado <> "NO" And encontrado = True Then

                                    txttexto.Text = "Estimado '" & NOMBREN & "' usted marcó la SALIDA en un estado ya de TARDIA, su hora oficial de ENTRADA es a las : " & SALIDAN

                                    Me.BackColor = Color.Yellow
                                    Formmenu.BackColor = Color.Yellow
                                    Formmenu.btnIngresar.BackColor = Color.Yellow
                                    Formmenu.btnmarcar.BackColor = Color.Yellow
                                    Formmenu.Button1.BackColor = Color.Yellow
                                    Formmenu.btnaseconstruplaza.BackColor = Color.Yellow
                                    Formmenu.btnaseconstruplaza.BackColor = Color.Yellow
                                    Formmenu.btncreditos.BackColor = Color.Yellow
                                    Formmenu.btnvacaciones.BackColor = Color.Yellow
                                    Formmenu.btncomprobante.BackColor = Color.Yellow
                                    Formmenu.btnmarcasq.BackColor = Color.Yellow
                                    Formmenu.btnmarcasg.BackColor = Color.Yellow
                                    Formmenu.btncalendario.BackColor = Color.Yellow
                                    Formmenu.btncircular.BackColor = Color.Yellow
                                    Formmenu.Btnferiados.BackColor = Color.Yellow
                                    Formmenu.Button1.Text = "Marcar"
                                    m_FPM.CloseDevice()

                                    lblestados.Text = "Estado:  " & ESTADON
                                    lblestados.ForeColor = Color.White
                                    lblestados.Visible = True

                                End If


                                If ESTADON = "AUSENTE" And emplea2_encontrado <> "NO" And encontrado = True Then

                                    txttexto.Text = "Estimado '" & NOMBREN & "' usted marcó la SALIDA en un estado ya de AUSENTE, su hora oficial de ENTRADA es a las: " & SALIDAN

                                    Me.BackColor = Color.Black
                                    Formmenu.BackColor = Color.Black
                                    Formmenu.btnIngresar.BackColor = Color.Black
                                    Formmenu.btnmarcar.BackColor = Color.Black
                                    Formmenu.Button1.BackColor = Color.Black
                                    Formmenu.btnaseconstruplaza.BackColor = Color.Black
                                    Formmenu.btnaseconstruplaza.BackColor = Color.Black
                                    Formmenu.btncreditos.BackColor = Color.Black
                                    Formmenu.btnvacaciones.BackColor = Color.Black
                                    Formmenu.btncomprobante.BackColor = Color.Black
                                    Formmenu.btnmarcasq.BackColor = Color.Black
                                    Formmenu.btnmarcasg.BackColor = Color.Black
                                    Formmenu.btncalendario.BackColor = Color.Black
                                    Formmenu.btncircular.BackColor = Color.Black
                                    Formmenu.Btnferiados.BackColor = Color.Black
                                    Formmenu.Button1.Text = "Marcar"
                                    m_FPM.CloseDevice()

                                    lblestados.Text = "Estado:  " & ESTADON
                                    lblestados.ForeColor = Color.White
                                    lblestados.Visible = True

                                End If








                            End If



                        End If








                    End If





                    '----------------------------------CIERRA HORARIO NOCTURNO----------------------------------------------------------------------------------------------







                    '---------------------------JORNADA DIURNA------------------------------------------------------------------------------

                    If JORNADA = "DIURNA" Then


                        '--VER LIBRES

                        DIA = "LIBRE"





                        cmdconsulta = New SqlClient.SqlCommand
                        daconsulta = New SqlClient.SqlDataAdapter
                        dsconsulta = New DataSet
                        With cmdconsulta
                            .Connection = conexion
                            .CommandText = "SELECT [ASISTENCIA].[dbo].[VALORA_LIBRE] (EMPLEADO)AS DIA FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & emplea2_encontrado & "'"
                            .CommandType = CommandType.Text
                        End With
                        daconsulta.SelectCommand = cmdconsulta
                        daconsulta.Fill(dsconsulta)


                        For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                            DIA = dsconsulta.Tables(0).Rows(z).Item("DIA")


                        Next


                        If DIA = "LIBRE" Then

                            If bandera_chofer = True Then
                                txttexto.Text = "Usted es empleado de confianza"
                            Else
                                txttexto.Text = "Estimado usted marcó la ENTRADA antes de su hora de ingreso, HOY ES SU DIA LIBRE"
                            End If
                            Me.BackColor = Color.Green
                            Formmenu.BackColor = Color.Green
                            Formmenu.btnIngresar.BackColor = Color.Green
                            Formmenu.btnmarcar.BackColor = Color.Green
                            Formmenu.Button1.BackColor = Color.Green
                            Formmenu.btnaseconstruplaza.BackColor = Color.Green
                            Formmenu.btnaseconstruplaza.BackColor = Color.Green
                            Formmenu.btncreditos.BackColor = Color.Green
                            Formmenu.btnvacaciones.BackColor = Color.Green
                            Formmenu.btncomprobante.BackColor = Color.Green
                            Formmenu.btnmarcasq.BackColor = Color.Green
                            Formmenu.btnmarcasg.BackColor = Color.Green
                            Formmenu.btncalendario.BackColor = Color.Green
                            Formmenu.btncircular.BackColor = Color.Green
                            Formmenu.Btnferiados.BackColor = Color.Green
                            Formmenu.Button1.Text = "Marcar"
                            m_FPM.CloseDevice()
                            lblestados.ForeColor = Color.White
                            lblestados.Visible = True

                        End If





                        '-----------------------------------banderas de tardias------------------------------------------------------------------------

                        If DIA <> "LIBRE" Then



                            Dim TNOMBRE As String
                            Dim TMARCA_ENTRADA As String
                            Dim THORA_ENTRADA_OFICIAL As String
                            Dim TMARCA_SALIDA As String
                            Dim THORA_SALIDA_OFICIAL As String
                            Dim TESTADO_ENTRADA As String
                            Dim TESTADO_SALIDA As String
                            Dim TTIPO As Integer = 0


                            cmdconsulta = New SqlClient.SqlCommand
                            daconsulta = New SqlClient.SqlDataAdapter
                            dsconsulta = New DataSet
                            With cmdconsulta
                                .Connection = conexion
                                .CommandText = "SELECT EMPLEADO,NOMBRE,MARCA_ENTRADA,HORA_ENTRADA_OFICIAL,MARCA_SALIDA,HORA_SALIDA_OFICIAL,ESTADO_ENTRADA,ESTADO_SALIDA,TIPO FROM ASISTENCIA.DBO.REVISION_MARCA WHERE EMPLEADO='" & emplea2_encontrado & "'"
                                .CommandType = CommandType.Text
                            End With
                            daconsulta.SelectCommand = cmdconsulta
                            daconsulta.Fill(dsconsulta)


                            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                                TNOMBRE = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                                TMARCA_ENTRADA = dsconsulta.Tables(0).Rows(z).Item("MARCA_ENTRADA")
                                THORA_ENTRADA_OFICIAL = dsconsulta.Tables(0).Rows(z).Item("HORA_ENTRADA_OFICIAL")
                                TMARCA_SALIDA = dsconsulta.Tables(0).Rows(z).Item("MARCA_SALIDA")
                                THORA_SALIDA_OFICIAL = dsconsulta.Tables(0).Rows(z).Item("HORA_SALIDA_OFICIAL")
                                TESTADO_ENTRADA = dsconsulta.Tables(0).Rows(z).Item("ESTADO_ENTRADA")
                                TESTADO_SALIDA = dsconsulta.Tables(0).Rows(z).Item("ESTADO_SALIDA")
                                TTIPO = dsconsulta.Tables(0).Rows(z).Item("TIPO")


                            Next


                            '------entradas----------------------------------------------------------------------------------

                            If TTIPO = 1 And TESTADO_ENTRADA = "OK" And emplea2_encontrado <> "NO" And encontrado = True And cuenta_tardias < 5 Then

                                If bandera_chofer = True Then
                                    txttexto.Text = "" & TNOMBRE & "' usted es empleado de confianza"
                                Else
                                    txttexto.Text = "Estimado '" & TNOMBRE & "' usted marcó la ENTRADA antes de su hora de ingreso la cual es a las : " & THORA_ENTRADA_OFICIAL
                                End If
                                Me.BackColor = Color.Green
                                Formmenu.BackColor = Color.Green
                                Formmenu.btnIngresar.BackColor = Color.Green
                                Formmenu.btnmarcar.BackColor = Color.Green
                                Formmenu.Button1.BackColor = Color.Green
                                Formmenu.btnaseconstruplaza.BackColor = Color.Green
                                Formmenu.btnaseconstruplaza.BackColor = Color.Green
                                Formmenu.btncreditos.BackColor = Color.Green
                                Formmenu.btnvacaciones.BackColor = Color.Green
                                Formmenu.btncomprobante.BackColor = Color.Green
                                Formmenu.btnmarcasq.BackColor = Color.Green
                                Formmenu.btnmarcasg.BackColor = Color.Green
                                Formmenu.btncalendario.BackColor = Color.Green
                                Formmenu.btncircular.BackColor = Color.Green
                                Formmenu.Btnferiados.BackColor = Color.Green

                                Formmenu.Button1.Text = "Marcar"
                                m_FPM.CloseDevice()

                                If bandera_chofer = True Then
                                    lblestados.Text = "EMPLEADO DE CONFIANZA"

                                Else
                                    lblestados.Text = "Estado:  " & TESTADO_ENTRADA
                                End If

                                lblestados.ForeColor = Color.White
                                lblestados.Visible = True


                            End If


                            'luis

                            If TTIPO = 1 And TESTADO_ENTRADA = "TARDIA" And emplea2_encontrado <> "NO" And encontrado = True And cuenta_tardias < 5 Then


                                If bandera_chofer = True Then
                                    txttexto.Text = "" & TNOMBRE & "' usted es empleado de confianza"
                                Else
                                    txttexto.Text = "Estimado '" & TNOMBRE & "' usted marcó la ENTRADA en un estado ya de TARDIA, su hora oficial de ENTRADA es a las : " & THORA_ENTRADA_OFICIAL
                                End If

                                txttexto.Text = "Estimado '" & TNOMBRE & "' usted marcó la ENTRADA en un estado ya de TARDIA, su hora oficial de ENTRADA es a las : " & THORA_ENTRADA_OFICIAL


                                Me.BackColor = Color.Yellow
                                Formmenu.BackColor = Color.Yellow
                                Formmenu.btnIngresar.BackColor = Color.Yellow
                                Formmenu.btnmarcar.BackColor = Color.Yellow
                                Formmenu.Button1.BackColor = Color.Yellow
                                Formmenu.btnaseconstruplaza.BackColor = Color.Yellow
                                Formmenu.btnaseconstruplaza.BackColor = Color.Yellow
                                Formmenu.btncreditos.BackColor = Color.Yellow
                                Formmenu.btnvacaciones.BackColor = Color.Yellow
                                Formmenu.btncomprobante.BackColor = Color.Yellow
                                Formmenu.btnmarcasq.BackColor = Color.Yellow
                                Formmenu.btnmarcasg.BackColor = Color.Yellow
                                Formmenu.btncalendario.BackColor = Color.Yellow
                                Formmenu.btncircular.BackColor = Color.Yellow
                                Formmenu.Btnferiados.BackColor = Color.Yellow
                                Formmenu.Button1.Text = "Marcar"
                                m_FPM.CloseDevice()
                                If bandera_chofer = True Then
                                    lblestados.Text = "EMPLEADO DE CONFIANZA"
                                Else
                                    lblestados.Text = "Estado:  " & TESTADO_ENTRADA
                                End If
                                lblestados.ForeColor = Color.White
                                lblestados.Visible = True

                            End If




                            If TTIPO = 1 And TESTADO_ENTRADA = "TARDIA" And emplea2_encontrado <> "NO" And encontrado = True And cuenta_tardias >= 5 Then


                                If bandera_chofer = True Then
                                    txttexto.Text = "" & TNOMBRE & "' usted es empleado de confianza"
                                Else
                                    txttexto.Text = "Estimado '" & TNOMBRE & "' usted marcó la ENTRADA en un estado ya de AUSENTE por acumulación de mas de 5 TARDIAS en el mes, su hora oficial de ENTRADA es a las : " & THORA_ENTRADA_OFICIAL
                                End If

                                txttexto.Text = "Estimado '" & TNOMBRE & "' usted marcó la ENTRADA en un estado ya de AUSENTE por acumulación de mas de 5 TARDIAS en el mes, su hora oficial de ENTRADA es a las : " & THORA_ENTRADA_OFICIAL


                                Me.BackColor = Color.Black
                                Formmenu.BackColor = Color.Black
                                Formmenu.btnIngresar.BackColor = Color.Black
                                Formmenu.btnmarcar.BackColor = Color.Black
                                Formmenu.Button1.BackColor = Color.Black
                                Formmenu.btnaseconstruplaza.BackColor = Color.Black
                                Formmenu.btnaseconstruplaza.BackColor = Color.Black
                                Formmenu.btncreditos.BackColor = Color.Black
                                Formmenu.btnvacaciones.BackColor = Color.Black
                                Formmenu.btncomprobante.BackColor = Color.Black
                                Formmenu.btnmarcasq.BackColor = Color.Black
                                Formmenu.btnmarcasg.BackColor = Color.Black
                                Formmenu.btncalendario.BackColor = Color.Black
                                Formmenu.btncircular.BackColor = Color.Black
                                Formmenu.Btnferiados.BackColor = Color.Black
                                Formmenu.Button1.Text = "Marcar"
                                m_FPM.CloseDevice()
                                If bandera_chofer = True Then
                                    lblestados.Text = "EMPLEADO DE CONFIANZA"
                                Else
                                    lblestados.Text = "Estado:  " & TESTADO_ENTRADA
                                End If
                                lblestados.ForeColor = Color.Black
                                lblestados.Visible = True

                            End If



                            If TTIPO = 1 And TESTADO_ENTRADA = "AUSENTE" And emplea2_encontrado <> "NO" And encontrado = True Then

                                If bandera_chofer = True Then
                                    txttexto.Text = "" & TNOMBRE & "' usted es empleado de confianza"
                                Else
                                    txttexto.Text = "Estimado '" & TNOMBRE & "' usted marcó la ENTRADA en un estado ya de AUSENTE, su hora oficial de ENTRADA es a las: " & THORA_ENTRADA_OFICIAL
                                End If
                                Me.BackColor = Color.Black
                                Formmenu.BackColor = Color.Black
                                Formmenu.btnIngresar.BackColor = Color.Black
                                Formmenu.btnmarcar.BackColor = Color.Black
                                Formmenu.Button1.BackColor = Color.Black
                                Formmenu.btnaseconstruplaza.BackColor = Color.Black
                                Formmenu.btnaseconstruplaza.BackColor = Color.Black
                                Formmenu.btncreditos.BackColor = Color.Black
                                Formmenu.btnvacaciones.BackColor = Color.Black
                                Formmenu.btncomprobante.BackColor = Color.Black
                                Formmenu.btnmarcasq.BackColor = Color.Black
                                Formmenu.btnmarcasg.BackColor = Color.Black
                                Formmenu.btncalendario.BackColor = Color.Black
                                Formmenu.btncircular.BackColor = Color.Black
                                Formmenu.Btnferiados.BackColor = Color.Black
                                Formmenu.Button1.Text = "Marcar"
                                m_FPM.CloseDevice()
                                If bandera_chofer = True Then
                                    lblestados.Text = "EMPLEADO DE CONFIANZA"
                                Else
                                    lblestados.Text = "Estado:  " & TESTADO_ENTRADA
                                End If
                                lblestados.ForeColor = Color.White
                                lblestados.Visible = True

                            End If



                            '-------salidas--------------------------------------------------------------------------------------------------------------


                            If TTIPO > 1 And TESTADO_SALIDA = "OK" And emplea2_encontrado <> "NO" And encontrado = True Then


                                If bandera_chofer = True Then
                                    txttexto.Text = "" & TNOMBRE & "' usted es empleado de confianza"
                                Else

                                    txttexto.Text = "Estimado '" & TNOMBRE & "' usted marcó su SALIDA correctamente, su hora oficial de SALIDA es a las : " & THORA_SALIDA_OFICIAL
                                End If
                                Me.BackColor = Color.Green
                                Formmenu.BackColor = Color.Green
                                Formmenu.btnIngresar.BackColor = Color.Green
                                Formmenu.btnmarcar.BackColor = Color.Green
                                Formmenu.Button1.BackColor = Color.Green
                                Formmenu.btnaseconstruplaza.BackColor = Color.Green
                                Formmenu.btnaseconstruplaza.BackColor = Color.Green
                                Formmenu.btncreditos.BackColor = Color.Green
                                Formmenu.btnvacaciones.BackColor = Color.Green
                                Formmenu.btncomprobante.BackColor = Color.Green
                                Formmenu.btnmarcasq.BackColor = Color.Green
                                Formmenu.btnmarcasg.BackColor = Color.Green
                                Formmenu.btncalendario.BackColor = Color.Green
                                Formmenu.btncircular.BackColor = Color.Green
                                Formmenu.Btnferiados.BackColor = Color.Green
                                Formmenu.Button1.Text = "Marcar"
                                m_FPM.CloseDevice()
                                If bandera_chofer = True Then
                                    lblestados.Text = "EMPLEADO DE CONFIANZA"
                                Else
                                    lblestados.Text = "Estado:  " & TESTADO_SALIDA
                                End If
                                lblestados.ForeColor = Color.White
                                lblestados.Visible = True


                            End If


                            If TTIPO > 1 And TESTADO_SALIDA = "TARDIA" And emplea2_encontrado <> "NO" And encontrado = True Then

                                If bandera_chofer = True Then
                                    txttexto.Text = "" & TNOMBRE & "' usted es empleado de confianza"
                                Else

                                    txttexto.Text = "Estimado '" & TNOMBRE & "' usted marcó la SALIDA en un estado de TARDIA, su hora oficial de SALIDA es a las : " & THORA_SALIDA_OFICIAL
                                End If
                                Me.BackColor = Color.Yellow
                                Formmenu.BackColor = Color.Yellow
                                Formmenu.btnIngresar.BackColor = Color.Yellow
                                Formmenu.btnmarcar.BackColor = Color.Yellow
                                Formmenu.Button1.BackColor = Color.Yellow
                                Formmenu.btnaseconstruplaza.BackColor = Color.Yellow
                                Formmenu.btnaseconstruplaza.BackColor = Color.Yellow
                                Formmenu.btncreditos.BackColor = Color.Yellow
                                Formmenu.btnvacaciones.BackColor = Color.Yellow
                                Formmenu.btncomprobante.BackColor = Color.Yellow
                                Formmenu.btnmarcasq.BackColor = Color.Yellow
                                Formmenu.btnmarcasg.BackColor = Color.Yellow
                                Formmenu.btncalendario.BackColor = Color.Yellow
                                Formmenu.btncircular.BackColor = Color.Yellow
                                Formmenu.Btnferiados.BackColor = Color.Yellow
                                Formmenu.Button1.Text = "Marcar"
                                m_FPM.CloseDevice()

                                If bandera_chofer = True Then
                                    lblestados.Text = "EMPLEADO DE CONFIANZA"
                                Else
                                    lblestados.Text = "Estado:  " & TESTADO_SALIDA
                                End If
                                lblestados.ForeColor = Color.White
                                lblestados.Visible = True

                            End If


                            If TTIPO > 1 And TESTADO_SALIDA = "AUSENTE" And emplea2_encontrado <> "NO" And encontrado = True Then

                                If bandera_chofer = True Then
                                    txttexto.Text = "" & TNOMBRE & "' usted es empleado de confianza"
                                Else

                                    txttexto.Text = "Estimado '" & TNOMBRE & "' usted marcó la SALIDA en un estado  AUSENTE, su hora oficial de SALIDA es a las: " & THORA_SALIDA_OFICIAL
                                End If
                                Me.BackColor = Color.Black
                                Formmenu.BackColor = Color.Black
                                Formmenu.btnIngresar.BackColor = Color.Black
                                Formmenu.btnmarcar.BackColor = Color.Black
                                Formmenu.Button1.BackColor = Color.Black
                                Formmenu.btnaseconstruplaza.BackColor = Color.Black
                                Formmenu.btnaseconstruplaza.BackColor = Color.Black
                                Formmenu.btncreditos.BackColor = Color.Black
                                Formmenu.btnvacaciones.BackColor = Color.Black
                                Formmenu.btncomprobante.BackColor = Color.Black
                                Formmenu.btnmarcasq.BackColor = Color.Black
                                Formmenu.btnmarcasg.BackColor = Color.Black
                                Formmenu.btncalendario.BackColor = Color.Black
                                Formmenu.btncircular.BackColor = Color.Black
                                Formmenu.Btnferiados.BackColor = Color.Black
                                Formmenu.Button1.Text = "Marcar"
                                m_FPM.CloseDevice()
                                If bandera_chofer = True Then
                                    lblestados.Text = "EMPLEADO DE CONFIANZA"
                                Else
                                    lblestados.Text = "Estado:  " & TESTADO_SALIDA
                                End If
                                lblestados.ForeColor = Color.White
                                lblestados.Visible = True

                            End If


                        End If

                        '----------------------------------------------------------------------------------------------------------------------------


                    End If


                End If   '--FIN JORNADA

                '  MessageBox.Show("Estimados compañeros, esta quincena se estará aplicando los rebajos correspondientes a los días marcados en AUSENTE,si su horario no está correcto en sistema comunicarlo de inmediato a Sebastian Fait.", "Construplaza", MessageBoxButtons.OK)


            End If

            If bandera_error = True Or encontrado = False Then

                Me.BackColor = Color.Red
                Me.PictureBox2.Image = Nothing
                Formmenu.BackColor = Color.Red
                Formmenu.btnIngresar.BackColor = Color.Red
                Formmenu.btnmarcar.BackColor = Color.Red
                Formmenu.Button1.BackColor = Color.Red
                Formmenu.btnvacaciones.BackColor = Color.Red
                Formmenu.btncomprobante.BackColor = Color.Red
                Formmenu.btnmarcasq.BackColor = Color.Red
                Formmenu.btnmarcasg.BackColor = Color.Red
                Formmenu.btnaseconstruplaza.BackColor = Color.Red
                Formmenu.btnaseconstruplaza.BackColor = Color.Red
                Formmenu.btncreditos.BackColor = Color.Red
                Formmenu.btnvacaciones.BackColor = Color.Red
                Formmenu.btncomprobante.BackColor = Color.Red
                Formmenu.btnmarcasq.BackColor = Color.Red
                Formmenu.btnmarcasg.BackColor = Color.Red
                Formmenu.btncalendario.BackColor = Color.Red
                Formmenu.btncircular.BackColor = Color.Red
                Formmenu.Btnferiados.BackColor = Color.Red
                Formmenu.Button1.Text = "Marcar"
                m_FPM.CloseDevice()


                MsgBox("No se logró leer su huella, intente nuevamente por favor", MsgBoxStyle.Critical, "Construplaza")


                '--ojo---
                Me.BackColor = Color.Gray
                Me.PictureBox2.Image = Nothing
                Formmenu.BackColor = Color.Gray
                Formmenu.btnIngresar.BackColor = Color.Gray
                Formmenu.btnmarcar.BackColor = Color.Gray
                Formmenu.Button1.BackColor = Color.Gray
                Formmenu.Button1.BackColor = Color.Gray
                Formmenu.btnvacaciones.BackColor = Color.Gray
                Formmenu.btncomprobante.BackColor = Color.Gray
                Formmenu.btnmarcasq.BackColor = Color.Gray
                Formmenu.btnmarcasg.BackColor = Color.Gray
                Formmenu.btnaseconstruplaza.BackColor = Color.Gray
                Formmenu.btnaseconstruplaza.BackColor = Color.Gray
                Formmenu.btncreditos.BackColor = Color.Gray
                Formmenu.btnvacaciones.BackColor = Color.Gray
                Formmenu.btncomprobante.BackColor = Color.Gray
                Formmenu.btnmarcasq.BackColor = Color.Gray
                Formmenu.btnmarcasg.BackColor = Color.Gray
                Formmenu.btncalendario.BackColor = Color.Gray
                Formmenu.btncircular.BackColor = Color.Gray
                Formmenu.Btnferiados.BackColor = Color.Gray
                Formmenu.Button1.Text = "Marcar"
                m_FPM.CloseDevice()



            End If


            Formmenu.Button1.Enabled = True


        Catch ex As Exception

            Me.BackColor = Color.Red
            Me.PictureBox2.Image = Nothing
            Formmenu.BackColor = Color.Red
            Formmenu.btnIngresar.BackColor = Color.Red
            Formmenu.btnmarcar.BackColor = Color.Red
            Formmenu.Button1.BackColor = Color.Red
            Formmenu.btnvacaciones.BackColor = Color.Red
            Formmenu.btncomprobante.BackColor = Color.Red
            Formmenu.btnmarcasq.BackColor = Color.Red
            Formmenu.btnmarcasg.BackColor = Color.Red
            Formmenu.btnaseconstruplaza.BackColor = Color.Red
            Formmenu.btnaseconstruplaza.BackColor = Color.Red
            Formmenu.btncreditos.BackColor = Color.Red
            Formmenu.btnvacaciones.BackColor = Color.Red
            Formmenu.btncomprobante.BackColor = Color.Red
            Formmenu.btnmarcasq.BackColor = Color.Red
            Formmenu.btnmarcasg.BackColor = Color.Red
            Formmenu.btncalendario.BackColor = Color.Red
            Formmenu.btncircular.BackColor = Color.Red
            Formmenu.Btnferiados.BackColor = Color.Red
            Formmenu.Button1.Text = "Marcar"
            m_FPM.CloseDevice()





            MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")

            '---ojo---
            Me.BackColor = Color.Gray
            Me.PictureBox2.Image = Nothing
            Formmenu.BackColor = Color.Gray
            Formmenu.btnIngresar.BackColor = Color.Gray
            Formmenu.btnmarcar.BackColor = Color.Gray
            Formmenu.Button1.BackColor = Color.Gray
            Formmenu.Button1.BackColor = Color.Gray
            Formmenu.btnvacaciones.BackColor = Color.Gray
            Formmenu.btncomprobante.BackColor = Color.Gray
            Formmenu.btnmarcasq.BackColor = Color.Gray
            Formmenu.btnmarcasg.BackColor = Color.Gray
            Formmenu.btnaseconstruplaza.BackColor = Color.Gray
            Formmenu.btnaseconstruplaza.BackColor = Color.Gray
            Formmenu.btncreditos.BackColor = Color.Gray

            Formmenu.btnvacaciones.BackColor = Color.Gray
            Formmenu.btncomprobante.BackColor = Color.Gray
            Formmenu.btnmarcasq.BackColor = Color.Gray
            Formmenu.btnmarcasg.BackColor = Color.Gray
            Formmenu.btncalendario.BackColor = Color.Gray
            Formmenu.btncircular.BackColor = Color.Gray
            Formmenu.Btnferiados.BackColor = Color.Gray

            Formmenu.Button1.Text = "Marcar"
            m_FPM.CloseDevice()


            m_FPM.CloseDevice()
            ' MessageBox.Show(ex.Message)
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


    Private Sub btncerrar_Click(sender As Object, e As EventArgs) Handles btncerrar.Click
        Formmenu.BackColor = Color.Gray
        Formmenu.btnIngresar.BackColor = Color.Gray
        Formmenu.btnmarcar.BackColor = Color.Gray
        Formmenu.Button1.BackColor = Color.Gray
        Formmenu.btnaseconstruplaza.BackColor = Color.Gray
        Formmenu.btnvacaciones.BackColor = Color.Gray
        Formmenu.btncomprobante.BackColor = Color.Gray
        Formmenu.btnmarcasq.BackColor = Color.Gray
        Formmenu.btnmarcasg.BackColor = Color.Gray
        Formmenu.btncreditos.BackColor = Color.Gray
        Formmenu.btncalendario.BackColor = Color.Gray
        Formmenu.btncircular.BackColor = Color.Gray
        Formmenu.Btnferiados.BackColor = Color.Gray

        conexion.Close()


        Me.Close()

    End Sub

    Private Sub StatusBar_Click(sender As Object, e As EventArgs) Handles StatusBar.Click

    End Sub


End Class


