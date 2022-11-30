Imports System.Text
Imports SecuGen.FDxSDKPro.Windows
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices
Imports System.Net
Imports System.Net.Mail
Imports System.Net.Mime

Public Class frmjustificadas
    Public dias_pendi As Integer
    Public total_empleados As Decimal = 0
    Public empleados_registrados As Decimal = 0
    Public calculo_empleados As Decimal = 0



    Public puesto_empleado As String = ""
    Public ubicacion_empleado As String = ""

    Public consecutivo As Integer = 0
    Public consecutivos As Integer = 0
    Public cmdconsulta, cmdconsulta_consecutivo, cmdconsulta_documentos As SqlClient.SqlCommand
    Public transaccion, transaccion_documentos As SqlClient.SqlTransaction
    Public cmdincluir, cmdincluir_documentos As SqlClient.SqlCommand
    Public iregincluir, iregincluird, cod, t, y As Integer
    Public contrarecibo, strconexionex, parametros, asunto, cuerpo, CLIENTE, FECHA, DOCUMENTO, TIPO, MONEDA, CREDITO, NOMBRE, nombres As String
    Public MONTO, SALDO As Decimal
    Public conexion As SqlClient.SqlConnection
    Public conexionbi As SqlClient.SqlConnection
    Public daconsulta, daconsulta_consecutivo, daconsulta_documentos As SqlClient.SqlDataAdapter
    Public dsconsulta, dsconsulta_consecutivo, dsconsulta_documentos As DataSet
    Public contador As Integer = 130
    Public NombreArchivo, empleado As String
    Public contadores, total_vacaciones As Integer
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
    Public co As Integer = 0





    Private Sub Btncerrar_Click(sender As Object, e As EventArgs) Handles Btncerrar.Click
        conexion.Close()
        Me.Close()
        Formmenu.Btnferiados.Enabled = True
    End Sub

    Private Sub frmjustificadas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try


            Formmenu.Btnferiados.Enabled = False
            BtnSolicitar.Enabled = True
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
                    MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
                    m_FPM.CloseDevice()
                    Formmenu.Btnferiados.Enabled = True
                    Me.Close()


                End If
            Else
                DisplayError("GetImage", iError)
                bandera_error = True
                MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
                Formmenu.Btnferiados.Enabled = True
                m_FPM.CloseDevice()
                Me.Close()
            End If



            If bandera_error = False Then


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
                                    'Me.Close()
                                    'Formmenu.btnvacaciones.Enabled = True

                                End If
                            Else
                                DisplayError("MatchTemplate", iErrorver)
                                m_FPM.CloseDevice()
                            End If


                        End If

                    End If


                Next




                If emplea2_encontrado <> "NO" Then


                    cmdconsulta = New SqlClient.SqlCommand
                    daconsulta = New SqlClient.SqlDataAdapter
                    dsconsulta = New DataSet
                    With cmdconsulta
                        .Connection = conexion
                        .CommandText = "SELECT VACS_PENDIENTES,EMPLEADO,NOMBRE FROM EXACTUS.DIGEMA.EMPLEADO WHERE EMPLEADO='" & emplea2_encontrado & "'"



                        .CommandType = CommandType.Text
                    End With
                    daconsulta.SelectCommand = cmdconsulta
                    daconsulta.Fill(dsconsulta)


                    For z = 0 To dsconsulta.Tables(0).Rows.Count - 1


                        txtempleado.Text = dsconsulta.Tables(0).Rows(z).Item("EMPLEADO")
                        txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                        dias_pendi = dsconsulta.Tables(0).Rows(z).Item("VACS_PENDIENTES")
                        lblvacaciones_pendientes.Text = "Vacaciones pendientes: " & Format(dsconsulta.Tables(0).Rows(z).Item("VACS_PENDIENTES"), "0.00")
                        total_vacaciones = dsconsulta.Tables(0).Rows(z).Item("VACS_PENDIENTES")

                    Next

                End If

            End If




        Catch ex As Exception
            MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
            m_FPM.CloseDevice()
            Me.Close()
            Formmenu.btnvacaciones.Enabled = True
            ' Show the stack trace, which is a list of methods 
            ' that are currently executing.
            '  MessageBox.Show("Stack Trace: " & vbCrLf & ex.StackTrace)
        Finally
            m_FPM.CloseDevice()
            ' This line executes whether or not the exception occurs.
            '  MessageBox.Show("in Finally block")
        End Try



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


    Private Sub BtnSolicitar_Click(sender As Object, e As EventArgs) Handles BtnSolicitar.Click
        Try

            Dim fecha_inicial As Date
            Dim fecha_final As Date
            Dim fecha_actual As Date = Today
            Dim nueva_Fecha As Date
            Dim iniciales As String
            Dim finales As String


            Dim bandera_de_error As Boolean = False

            Dim texto As String
            Dim dias_diferencia As Integer = 0
            Dim hoy As String = "GETDATE()"
            Dim hoy_cero As String = "DateAdd(dd, DateDiff(dd, 0, GETDATE()), 0)"
            Dim noteexistflag As Integer = 0
            Dim fechaferiado As Date
            Dim FERIADO As String
            



            BtnSolicitar.Enabled = False

            cmdconsulta_consecutivo = New SqlClient.SqlCommand
            daconsulta_consecutivo = New SqlClient.SqlDataAdapter
            dsconsulta_consecutivo = New DataSet
            With cmdconsulta_consecutivo
                .Connection = conexion
                .CommandText = "SELECT CONVERT(INT,ULTIMA_ACCION) as MAXIMA FROM EXACTUS.DIGEMA.GLOBALES_RH WITH(NOLOCK)"
                .CommandType = CommandType.Text
            End With
            daconsulta_consecutivo.SelectCommand = cmdconsulta_consecutivo
            daconsulta_consecutivo.Fill(dsconsulta_consecutivo)


            For co = 0 To dsconsulta_consecutivo.Tables(0).Rows.Count - 1

                consecutivo = dsconsulta_consecutivo.Tables(0).Rows(co).Item("MAXIMA")

            Next




            fecha_inicial = DtFecha_inicial.Value.ToShortDateString

            fecha_final = DtFecha_inicial.Value.ToShortDateString



        
            '--------------------------------------------verifica feriado------------------------------------------------

            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT ISNULL(FERIADO,'NO')AS FERIADO FROM ASISTENCIA.DBO.CALENDARIO_EVENTO WHERE FECHA='" & fecha_inicial & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                FERIADO = dsconsulta.Tables(0).Rows(z).Item("FERIADO")



            Next

            '-------------------------------------------------------------------------------------------------------------------

            If FERIADO = "NO" Then
                m_FPM.CloseDevice()
                MsgBox("Esta fecha que usted escogió  " & fecha_inicial & "  NO es un día FERIADO, favor revisar con RRHH ", MsgBoxStyle.Critical, "Construplaza")
                bandera_de_error = True
                BtnSolicitar.Enabled = True
                Formmenu.Btnferiados.Enabled = True
                Me.Close()

            End If

            

            nueva_Fecha = fecha_actual.AddDays(0)

            If fecha_inicial < nueva_Fecha Then

                MsgBox("Error no puedes pedir disfrutar el feriado desde días anteriores,solo tres dias posteriores a hoy", MsgBoxStyle.Critical, "Construplaza")
                bandera_de_error = True
                BtnSolicitar.Enabled = True
                Formmenu.Btnferiados.Enabled = True
                Me.Close()


            End If






            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT UBICACION,PUESTO FROM EXACTUS.DIGEMA.EMPLEADO WHERE EMPLEADO='" & emplea2_encontrado & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                ubicacion_empleado = dsconsulta.Tables(0).Rows(z).Item("UBICACION")
                puesto_empleado = dsconsulta.Tables(0).Rows(z).Item("PUESTO")


            Next




            '--------------------------------------------------------busca ubicacion del compañeros ya con solicitudes---------------

            Dim iniciales_vacaciones As String = Format(DtFecha_inicial.Value, "yyyy-MM-dd" & " 00:00:00.000")
            Dim finales_vacaciones As String = Format(DTFecha_final.Value, "yyyy-MM-dd" & " 00:00:00.000")
            Dim nombre As String = ""




            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT count(A.EMPLEADO)AS EMPLEADOS_REGISTRADOS " +
                                ",ISNULL((SELECT COUNT(EMPLEADO) FROM EXACTUS.DIGEMA.EMPLEADO WHERE ACTIVO='S' AND PUESTO=E.PUESTO AND UBICACION=E.UBICACION),1)AS TOTAL_EMPLEADOS " +
                                "FROM EXACTUS.DIGEMA.EMPLEADO E " +
                                "INNER JOIN EXACTUS.DIGEMA.EMPLEADO_ACC_PER A WITH(NOLOCK) ON A.EMPLEADO=E.EMPLEADO " +
                                "WHERE A.TIPO_ACCION='AJUS' AND ESTADO_ACCION='S' " +
                                "AND E.UBICACION='" & ubicacion_empleado & "' AND E.PUESTO='" & puesto_empleado & "' AND A.FECHA_RIGE >='" & iniciales_vacaciones & "' AND A.FECHA_VENCE <='" & iniciales_vacaciones & "' " +
                                "GROUP BY E.PUESTO,E.UBICACION"




                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                empleados_registrados = dsconsulta.Tables(0).Rows(z).Item("EMPLEADOS_REGISTRADOS")
                total_empleados = dsconsulta.Tables(0).Rows(z).Item("TOTAL_EMPLEADOS")
                calculo_empleados = (empleados_registrados / total_empleados) * 100

         

                If calculo_empleados > 20 Then

                    MsgBox("Error no puedes disfrutar el feriado, debido a que el " & calculo_empleados & " %  de tu departamento ya lo solicitó", MsgBoxStyle.Critical, "Construplaza")

                    bandera_de_error = True
                    BtnSolicitar.Enabled = True
                    m_FPM.CloseDevice()
                    Formmenu.Btnferiados.Enabled = True
                    Me.Close()

                End If



            Next




            '-----------------------------------------------fin de semana-------------------------------------------------------------------------------------




            Dim FINSEMANA As String = ""


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT CASE WHEN (DATENAME (DW,convert(datetime, FECHA, 103)))='Sunday' THEN 'SI' ELSE 'NO' END AS FINSEMANAS FROM ASISTENCIA.DBO.CALENDARIO_EVENTO WHERE convert(datetime, FECHA, 103) >='" & iniciales_vacaciones & "' AND convert(datetime, FECHA, 103) <='" & finales_vacaciones & "'AND (CASE WHEN (DATENAME (DW,convert(datetime, FECHA, 103)))='Saturday' OR (DATENAME (DW,convert(datetime, FECHA, 103)))='Sunday' THEN 'SI' ELSE 'NO' END)='SI'"

                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)



            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                FINSEMANA = dsconsulta.Tables(0).Rows(z).Item("FINSEMANAS")


                If FINSEMANA = "SI" Then
                    MsgBox("Error, esta escogiendo un dia (Domingo) fin de semana para disfrutar feriado, favor revise  ", MsgBoxStyle.Critical, "Construplaza")
                    bandera_de_error = True
                    BtnSolicitar.Enabled = True
                    m_FPM.CloseDevice()
                    Formmenu.Btnferiados.Enabled = True
                    Me.Close()

                End If


            Next














            '-----------------------aqui inserta-----------------------------------------------------------------------------






            If bandera_de_error = False Then
                ' And FERIADO = "SI" 


                '-------------------------------------------------------feriados-----------------------------------


                'Dim FERIADO As String = ""
                ''   Dim fecha_iniciales As Date



                'cmdconsulta = New SqlClient.SqlCommand
                'daconsulta = New SqlClient.SqlDataAdapter
                'dsconsulta = New DataSet
                'With cmdconsulta
                '    .Connection = conexion
                '    .CommandText = "SELECT FERIADO FROM ASISTENCIA.DBO.CALENDARIO_EVENTO WHERE convert(datetime, FECHA, 103)-1 >='" & iniciales_vacaciones & "' AND convert(datetime, FECHA, 103) + 1 <='" & finales_vacaciones & "' AND FERIADO='SI'"

                '    .CommandType = CommandType.Text
                'End With
                'daconsulta.SelectCommand = cmdconsulta
                'daconsulta.Fill(dsconsulta)


                'For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                '    FERIADO = dsconsulta.Tables(0).Rows(z).Item("FERIADO")
                '    If FERIADO = "SI" Then
                '        MsgBox("Error, esta escogiendo un dia antes o posterior a un día feriado como vacaciones, favor revise  ", MsgBoxStyle.Critical, "Construplaza")
                '        bandera_de_error = True
                '        BtnSolicitar.Enabled = True


                '    End If


                'Next



                '---------------------------------------------------DIA DE LA SEMANA-----------------------------





                '---------------------------------------------------------------------------------------------------------------------------------------------------



                '   If fecha_final >= fecha_inicial And fecha_inicial >= nueva_Fecha And fecha_final >= nueva_Fecha And bandera_de_error = False Then



                iniciales = Format(DtFecha_inicial.Value, "yyyy-MM-dd" & " 00:00:00.000")
                finales = Format(DTFecha_final.Value, "yyyy-MM-dd" & " 00:00:00.000")


                texto = "DATEDIFF(DD,'" & iniciales & "','" & finales & "')"



                Dim Intervalo As TimeSpan

                Intervalo = DTFecha_final.Value.Subtract(DtFecha_inicial.Value)



                dias_diferencia = Intervalo.Days.ToString




                dias_diferencia = dias_diferencia + 1





                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If

                transaccion = conexion.BeginTransaction
                cmdincluir = New SqlClient.SqlCommand


                'parametros = "" & consecutivo & ",'AJUS','" & txtempleado.Text & "','P','" & iniciales & "','" & finales & "'," & dias_diferencia & ",'" & txtempleado.Text & "'," & hoy & "," & hoy & ",'Hecho desde kiosco'," & noteexistflag & ",'XPP','XPP'" '"

                parametros = "" & consecutivo & ",'AJUS'," & hoy_cero & ",'" & txtempleado.Text & "','JUS','" & iniciales & "','" & iniciales & "','Ausencia justificada por Feriado','LSEGURA'," & hoy & ",'LSEGURA'," & hoy & ",'LSEGURA'," & hoy & ",'LSEGURA'," & hoy & ",'S'," & 1 & "," & 1 & "," & noteexistflag & ""





                With cmdincluir
                    .Connection = conexion
                    .CommandText = "INSERT INTO EXACTUS.DIGEMA.EMPLEADO_ACC_PER(NUMERO_ACCION,TIPO_ACCION,FECHA,EMPLEADO,TIPO_AUSENCIA,FECHA_RIGE,FECHA_VENCE,NOTAS,USUARIO,FECHA_HORA,APROBADA_POR,FECHA_APROBACION,USUARIO_VIGENTE,FECHA_VIGENTE,USUARIO_APLICACION,FECHA_APLICACION,ESTADO_ACCION,DIAS_ACCION,SALDO,NoteExistsFlag) values (" & parametros & ")"
                    .CommandType = CommandType.Text
                    .Transaction = transaccion
                End With
                iregincluir = cmdincluir.ExecuteNonQuery
                transaccion.Commit()
                '  MsgBox("Datos incluidos satisfactoriamente, su número de acción de personal es la: " & consecutivo & "", vbInformation, "Construplaza")
                BtnSolicitar.Enabled = True
                Formmenu.Btnferiados.Enabled = True



                '--------------------------actualiza consecutivo--------------------------------------------------------------





                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If

                transaccion = conexion.BeginTransaction
                cmdincluir = New SqlClient.SqlCommand



                With cmdincluir
                    .Connection = conexion
                    .CommandText = "UPDATE EXACTUS.DIGEMA.GLOBALES_RH SET ULTIMA_ACCION= " & consecutivo + 1 & " "
                    .CommandType = CommandType.Text
                    .Transaction = transaccion
                End With
                iregincluir = cmdincluir.ExecuteNonQuery
                transaccion.Commit()
                MsgBox("Datos incluidos satisfactoriamente, su número de acción de personal es la: " & consecutivo & "", vbInformation, "Construplaza")
                BtnSolicitar.Enabled = True
                Formmenu.Btnferiados.Enabled = True



            End If








            ' End If

        Catch ex As Exception
            MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
            m_FPM.CloseDevice()
            Formmenu.Btnferiados.Enabled = True
            Me.Close()

            ' Show the stack trace, which is a list of methods 
            ' that are currently executing.
            '  MessageBox.Show("Stack Trace: " & vbCrLf & ex.StackTrace)
        Finally
            m_FPM.CloseDevice()
            Formmenu.Btnferiados.Enabled = True
            Me.Close()
            ' This line executes whether or not the exception occurs.
            '  MessageBox.Show("in Finally block")
        End Try


    End Sub
End Class