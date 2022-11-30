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


Public Class Frmvacaciones
    Public paso As Boolean = False
    Public dias_pendi As Integer
    Public cmdconsulta, cmdconsulta_local, cmdconsulta_fin, cmdconsulta_repetida, cmdconsulta_chofer, cmdconsulta_documentos As SqlClient.SqlCommand
    Public transaccion, transaccion_local, transaccion_fin, transaccion_repetida, transaccion_documentos As SqlClient.SqlTransaction
    Public cmdincluir, cmdincluir_repetida, cmdincluir_documentos, cmdincluir_local As SqlClient.SqlCommand
    Public iregincluir, iregincluir_local, iregincluird, cod, t, y As Integer
    Public contrarecibo, strconexionex, strconexionex_local, parametros, asunto, cuerpo, CLIENTE, FECHA, DOCUMENTO, TIPO, MONEDA, CREDITO, NOMBRE, nombres As String
    Public MONTO, SALDO As Decimal
    Public conexion, conexion_local As SqlClient.SqlConnection
    Public conexionbi As SqlClient.SqlConnection
    Public daconsulta, daconsulta_local, daconsulta_fin, daconsulta_repetida, daconsulta_chofer, daconsulta_documentos As SqlClient.SqlDataAdapter
    Public dsconsulta, dsconsulta_local, dsconsulta_fin, dsconsulta_repetida, dsconsulta_chofer, dsconsulta_documentos As DataSet
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

    Public empleados_registrados As Decimal = 0
    Public total_empleados As Decimal = 0
    Public calculo_empleados As Decimal = 0




    Private Sub Frmvacaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try


            Formmenu.btnvacaciones.Enabled = False
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
                    Formmenu.btnvacaciones.Enabled = True
                    '   Me.Close()


                End If
            Else
                DisplayError("GetImage", iError)
                bandera_error = True
                MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
                Formmenu.btnvacaciones.Enabled = True
                m_FPM.CloseDevice()
                Me.Close()
            End If


            'If bandera_error = True Then
            '    MsgBox("Error leyendo huella", MsgBoxStyle.Critical)
            '    Formmenu.btnvacaciones.Enabled = True
            '    m_FPM.CloseDevice()
            '    Close()

            'End If



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



                strconexionex_local = "Data Source=192.168.101.215\BDAPP01;Initial Catalog=exactus;User Id=sa;Password=Igoloncet!2"
                Try
                    conexion_local = New SqlClient.SqlConnection
                    conexion_local.ConnectionString = strconexionex_local
                    conexion_local.Open()


                Catch exc As SqlClient.SqlException
                    Console.WriteLine("Error de conexion")
                    End
                Finally
                    If Not (conexion_local.State = ConnectionState.Open) Then
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
                        'emplea2 = "2496"

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


                    cmdconsulta_local = New SqlClient.SqlCommand
                    daconsulta_local = New SqlClient.SqlDataAdapter
                    dsconsulta_local = New DataSet
                    With cmdconsulta_local
                        .Connection = conexion_local
                        .CommandText = "SELECT VACS_PENDIENTES,EMPLEADO,NOMBRE FROM EXACTUS.DIGEMA.EMPLEADO WHERE EMPLEADO='" & emplea2_encontrado & "'"



                        .CommandType = CommandType.Text
                    End With
                    daconsulta_local.SelectCommand = cmdconsulta_local
                    daconsulta_local.Fill(dsconsulta_local)


                    For z = 0 To dsconsulta_local.Tables(0).Rows.Count - 1


                        txtempleado.Text = dsconsulta_local.Tables(0).Rows(z).Item("EMPLEADO")
                        txtnombre.Text = dsconsulta_local.Tables(0).Rows(z).Item("NOMBRE")
                        dias_pendi = dsconsulta_local.Tables(0).Rows(z).Item("VACS_PENDIENTES")
                        lblvacaciones_pendientes.Text = "Vacaciones pendientes: " & Format(dsconsulta_local.Tables(0).Rows(z).Item("VACS_PENDIENTES"), "0.00")
                        total_vacaciones = dsconsulta_local.Tables(0).Rows(z).Item("VACS_PENDIENTES")

                    Next

                End If

            End If


            If emplea2_encontrado = "NO" And bandera_error = False Then

                MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
                Formmenu.btnvacaciones.Enabled = True
                m_FPM.CloseDevice()
                Me.Close()

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

    Private Sub Btncerrar_Click(sender As Object, e As EventArgs) Handles Btncerrar.Click
        conexion.Close()
        Formmenu.btnvacaciones.Enabled = True
        m_FPM.CloseDevice()
        Close()


    End Sub

    Private Sub BtnSolicitar_Click(sender As Object, e As EventArgs) Handles BtnSolicitar.Click

        Try

            Dim fecha_inicial As Date
            Dim fecha_final As Date
            Dim fecha_actual As Date = Today
            Dim nueva_Fecha As Date
            Dim iniciales As String
            Dim finales As String
            Dim hoy_cero As String = "DateAdd(dd, DateDiff(dd, 0, GETDATE()), 0)"



            Dim bandera_de_error As Boolean = False
            Dim consecutivo As Integer = 0
            Dim texto As String
            Dim dias_diferencia As Integer = 0
            Dim hoy As String = "GETDATE()"
            Dim noteexistflag As Integer = 0


            BtnSolicitar.Enabled = False


            cmdconsulta_local = New SqlClient.SqlCommand
            daconsulta_local = New SqlClient.SqlDataAdapter
            dsconsulta_local = New DataSet
            With cmdconsulta_local
                .Connection = conexion_local
                .CommandText = "SELECT ULTIMA_ACCION AS MAXIMA FROM EXACTUS.DIGEMA.GLOBALES_RH"
                .CommandType = CommandType.Text
            End With
            daconsulta_local.SelectCommand = cmdconsulta_local
            daconsulta_local.Fill(dsconsulta_local)


            For z = 0 To dsconsulta_local.Tables(0).Rows.Count - 1
                consecutivo = dsconsulta_local.Tables(0).Rows(z).Item("MAXIMA")

            Next





            fecha_inicial = DtFecha_inicial.Value.ToShortDateString


            fecha_final = DTFecha_final.Value.ToShortDateString





            nueva_Fecha = fecha_actual.AddDays(1)



            If fecha_inicial < nueva_Fecha Then

                MsgBox("Error no puedes pedir vacaciones desde días anteriores,solo un día posterior hoy", MsgBoxStyle.Critical, "Construplaza")
                bandera_de_error = True
                BtnSolicitar.Enabled = True

            End If


            If fecha_final < fecha_inicial Then

                MsgBox("Error, la fecha final debe de ser posterior a la inicial", MsgBoxStyle.Critical, "Construplaza")
                bandera_de_error = True
                BtnSolicitar.Enabled = True

            End If


            '-------------------ERROR POR OTRAS MARCAS------------------------------------------------------------------

            '--------------------------------------------------------busca ubicacion y puesto del empleado---------------

            Dim puesto_empleado As String = ""
            Dim ubicacion_empleado As String = ""




            cmdconsulta_local = New SqlClient.SqlCommand
            daconsulta_local = New SqlClient.SqlDataAdapter
            dsconsulta_local = New DataSet
            With cmdconsulta_local
                .Connection = conexion_local
                .CommandText = "SELECT UBICACION,PUESTO FROM EXACTUS.DIGEMA.EMPLEADO WHERE EMPLEADO='" & emplea2_encontrado & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta_local.SelectCommand = cmdconsulta_local
            daconsulta_local.Fill(dsconsulta_local)


            For z = 0 To dsconsulta_local.Tables(0).Rows.Count - 1

                ubicacion_empleado = dsconsulta_local.Tables(0).Rows(z).Item("UBICACION")
                puesto_empleado = dsconsulta_local.Tables(0).Rows(z).Item("PUESTO")


            Next



            '--------------------------------------------------------busca ubicacion del compañeros ya con solicitudes---------------

            Dim iniciales_vacaciones As String = Format(DtFecha_inicial.Value, "yyyy-MM-dd" & " 00:00:00.000")
            Dim finales_vacaciones As String = Format(DTFecha_final.Value, "yyyy-MM-dd" & " 00:00:00.000")
            Dim nombre As String = ""


            'cmdconsulta = New SqlClient.SqlCommand
            'daconsulta = New SqlClient.SqlDataAdapter
            'dsconsulta = New DataSet
            'With cmdconsulta
            '    .Connection = conexion
            '    .CommandText = "SELECT S.EMPLEADO,E.NOMBRE,S.FECHA_RIGE,S.FECHA_VENCE " +
            '                    "FROM EXACTUS.DIGEMA.SOLICITUD_RH S WITH(NOLOCK) " +
            '                    "INNER JOIN EXACTUS.DIGEMA.EMPLEADO E WITH(NOLOCK) ON S.EMPLEADO=E.EMPLEADO " +
            '                    "WHERE S.TIPO_SOLICITUD_RH='VACA' AND S.ESTADO IN ('P','H','A') " +
            '                    "AND E.UBICACION='" & ubicacion_empleado & "' AND E.PUESTO='" & puesto_empleado & "' AND S.FECHA_RIGE >='" & iniciales_vacaciones & "' AND FECHA_VENCE <='" & finales_vacaciones & "'"


            '    .CommandType = CommandType.Text
            'End With
            'daconsulta.SelectCommand = cmdconsulta
            'daconsulta.Fill(dsconsulta)


            'For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

            '    MsgBox("Error, ya el empleado de su departamento:  " & dsconsulta.Tables(0).Rows(z).Item("NOMBRE") & " solicitó vacaciones para esa fecha indicada", MsgBoxStyle.Critical, "Construplaza")
            '    bandera_de_error = True

            '    BtnSolicitar.Enabled = True
            '    Me.Close()



            'Next




            '--------------------------------------------------------busca ubicacion del compañeros ya con solicitudes---------------

            'Dim iniciales_vacaciones As String = Format(DtFecha_inicial.Value, "yyyy-MM-dd" & " 00:00:00.000")
            'Dim finales_vacaciones As String = Format(DTFecha_final.Value, "yyyy-MM-dd" & " 00:00:00.000")
            'Dim nombre As String = ""
 




            cmdconsulta_local = New SqlClient.SqlCommand
            daconsulta_local = New SqlClient.SqlDataAdapter
            dsconsulta_local = New DataSet
            With cmdconsulta_local
                .Connection = conexion_local
                .CommandText = "SELECT count(A.EMPLEADO)AS EMPLEADOS_REGISTRADOS " +
                                ",ISNULL((SELECT COUNT(EMPLEADO) FROM EXACTUS.DIGEMA.EMPLEADO WHERE ACTIVO='S' AND PUESTO=E.PUESTO AND UBICACION=E.UBICACION),1)AS TOTAL_EMPLEADOS " +
                                "FROM EXACTUS.DIGEMA.EMPLEADO E " +
                                "INNER JOIN EXACTUS.DIGEMA.EMPLEADO_ACC_PER A WITH(NOLOCK) ON A.EMPLEADO=E.EMPLEADO " +
                                "WHERE A.TIPO_ACCION='VAGO' AND ESTADO_ACCION='S' " +
                                "AND E.UBICACION='" & ubicacion_empleado & "' AND E.PUESTO='" & puesto_empleado & "' AND A.FECHA_RIGE >='" & iniciales_vacaciones & "' AND A.FECHA_VENCE <='" & finales_vacaciones & "' " +
                                "GROUP BY E.PUESTO,E.UBICACION"




                .CommandType = CommandType.Text
            End With
            daconsulta_local.SelectCommand = cmdconsulta_local
            daconsulta_local.Fill(dsconsulta_local)


            For z = 0 To dsconsulta_local.Tables(0).Rows.Count - 1

                empleados_registrados = dsconsulta_local.Tables(0).Rows(z).Item("EMPLEADOS_REGISTRADOS")
                total_empleados = dsconsulta_local.Tables(0).Rows(z).Item("TOTAL_EMPLEADOS")
                calculo_empleados = (empleados_registrados / total_empleados) * 100



                If calculo_empleados > 10 Then

                    MsgBox("Error no puedes disfrutar el dia de vacaciones, debido a que el " & calculo_empleados & " %  de tu departamento ya lo solicitó", MsgBoxStyle.Critical, "Construplaza")

                    bandera_de_error = True
                    BtnSolicitar.Enabled = True
                    m_FPM.CloseDevice()
                    Formmenu.btnvacaciones.Enabled = True
                    Me.Close()

                End If



            Next








            'luis
            '-----------------------------------------------verifica dias ya ingresado----------------------------------------------------------------------

            Dim iniciales_vacaciones_incluidas As String = Format(DtFecha_inicial.Value, "yyyy-MM-dd" & " 00:00:00.000")
            Dim finales_vacaciones_incluidas As String = Format(DTFecha_final.Value, "yyyy-MM-dd" & " 00:00:00.000")
            'Dim nombre As String = ""


            cmdconsulta_repetida = New SqlClient.SqlCommand
            daconsulta_repetida = New SqlClient.SqlDataAdapter
            dsconsulta_repetida = New DataSet
            With cmdconsulta_repetida
                .Connection = conexion_local
                .CommandText = "SELECT TOP 1 * " +
                                "FROM EXACTUS.DIGEMA.EMPLEADO_ACC_PER " +
                                "WHERE EMPLEADO='" & emplea2_encontrado & "'" +
                                "AND FECHA_RIGE >=GETDATE() " +
                                "AND TIPO_ACCION='VAGO' " +
                                "AND FECHA_APLICACION IS NOT NULL AND FECHA_ANULACION IS NULL " +
                                "AND ((FECHA_RIGE <='" & iniciales_vacaciones_incluidas & "' AND FECHA_VENCE >='" & iniciales_vacaciones_incluidas & "') OR (FECHA_RIGE <='" & finales_vacaciones_incluidas & "' AND FECHA_VENCE >='" & finales_vacaciones_incluidas & "')) " +
                                "ORDER BY CREATEDATE"

                .CommandType = CommandType.Text
            End With
            daconsulta_repetida.SelectCommand = cmdconsulta_repetida
            daconsulta_repetida.Fill(dsconsulta_repetida)


            For z = 0 To dsconsulta_repetida.Tables(0).Rows.Count - 1

                MsgBox("Error, ya usted escogió días de vacaciones en el rango de las fechas indicadas, favor revisar", MsgBoxStyle.Critical, "Construplaza")
                bandera_de_error = True
                BtnSolicitar.Enabled = True


            Next









            '--------------------------------------CHOFER-------------------------------------------------------------------------------------------

            'cmdconsulta_chofer = New SqlClient.SqlCommand
            'daconsulta_chofer = New SqlClient.SqlDataAdapter
            'dsconsulta_chofer = New DataSet
            'With cmdconsulta_chofer
            '    .Connection = conexion
            '    .CommandText = "SELECT DISTINCT(P.DESCRIPCION) AS PUESTO FROM EXACTUS.DIGEMA.EMPLEADO E WITH(NOLOCK) INNER JOIN EXACTUS.DIGEMA.PUESTO P WITH(NOLOCK) ON P.PUESTO=E.PUESTO WHERE E.ACTIVO='S' AND (P.DESCRIPCION LIKE '%chofer%') and E.EMPLEADO='" & emplea2_encontrado & "'AND '2021-01-01 00:00:00.000' > '" & iniciales_vacaciones & "'"
            '    .CommandType = CommandType.Text
            'End With
            'daconsulta_chofer.SelectCommand = cmdconsulta_chofer
            'daconsulta_chofer.Fill(dsconsulta_chofer)


            'For i = 0 To dsconsulta_chofer.Tables(0).Rows.Count - 1

            '    MsgBox("Usted pertenece al departamento de transportes, el cual ya no hay vacaciones aplicables hasta el mes de enero, favor alguna duda comunicarse al departamento de RRHH o a su línea directa de Whatsapp  6167-7827.", MsgBoxStyle.Critical, "Construplaza")
            '    bandera_de_error = True
            '    BtnSolicitar.Enabled = True
            '    m_FPM.CloseDevice()
            '    Formmenu.btnvacaciones.Enabled = True
            '    Me.Close()

            'Next





            '-------------------------------------------------------feriados-----------------------------------


            Dim FERIADO As String = ""
            '   Dim fecha_iniciales As Date



            cmdconsulta_local = New SqlClient.SqlCommand
            daconsulta_local = New SqlClient.SqlDataAdapter
            dsconsulta_local = New DataSet
            With cmdconsulta_local
                .Connection = conexion_local
                .CommandText = "SELECT FERIADO FROM ASISTENCIA.DBO.CALENDARIO_EVENTO WHERE convert(datetime, FECHA, 103) >='" & iniciales_vacaciones & "' AND convert(datetime, FECHA, 103) <='" & finales_vacaciones & "' AND FERIADO='SI'"

                .CommandType = CommandType.Text
            End With
            daconsulta_local.SelectCommand = cmdconsulta_local
            daconsulta_local.Fill(dsconsulta_local)


            For z = 0 To dsconsulta_local.Tables(0).Rows.Count - 1

                FERIADO = dsconsulta_local.Tables(0).Rows(z).Item("FERIADO")


                If FERIADO = "SI" Then


                    MsgBox("Error, esta escogiendo un dia antes o posterior a un día feriado como vacaciones, favor revise  ", MsgBoxStyle.Critical, "Construplaza")
                    bandera_de_error = True
                    BtnSolicitar.Enabled = True
                    m_FPM.CloseDevice()
                    Formmenu.btnvacaciones.Enabled = True
                    Me.Close()

                End If


            Next



            '---------------------------------------------------DIA DE LA SEMANA-----------------------------





            Dim FINSEMANA As String = ""


            cmdconsulta_local = New SqlClient.SqlCommand
            daconsulta_local = New SqlClient.SqlDataAdapter
            dsconsulta_local = New DataSet
            With cmdconsulta_local
                .Connection = conexion_local
                .CommandText = "SELECT CASE WHEN (DATENAME (DW,convert(datetime, FECHA, 103)))='Sunday' THEN 'SI' WHEN (DATENAME (DW,convert(datetime, FECHA, 103)))='Saturday' THEN 'SI' ELSE 'NO'END AS FINSEMANAS FROM ASISTENCIA.DBO.CALENDARIO_EVENTO WHERE convert(datetime, FECHA, 103) >='" & iniciales_vacaciones & "' AND convert(datetime, FECHA, 103) <='" & finales_vacaciones & "'AND (CASE WHEN (DATENAME (DW,convert(datetime, FECHA, 103)))='Saturday' OR (DATENAME (DW,convert(datetime, FECHA, 103)))='Sunday' THEN 'SI' ELSE 'NO' END)='SI'"

                .CommandType = CommandType.Text
            End With
            daconsulta_local.SelectCommand = cmdconsulta_local
            daconsulta_local.Fill(dsconsulta_local)



            For z = 0 To dsconsulta_local.Tables(0).Rows.Count - 1

                FINSEMANA = dsconsulta_local.Tables(0).Rows(z).Item("FINSEMANAS")


                If FINSEMANA = "SI" And ubicacion_empleado <> "019" And ubicacion_empleado <> "003" Then



                    cmdconsulta_fin = New SqlClient.SqlCommand
                    daconsulta_fin = New SqlClient.SqlDataAdapter
                    dsconsulta_fin = New DataSet
                    With cmdconsulta_fin
                        .Connection = conexion_local
                        .CommandText = "SELECT count(A.EMPLEADO)AS EMPLEADOS_REGISTRADOS " +
                                        ",ISNULL((SELECT COUNT(EMPLEADO) FROM EXACTUS.DIGEMA.EMPLEADO WHERE ACTIVO='S' AND PUESTO=E.PUESTO AND UBICACION=E.UBICACION),1)AS TOTAL_EMPLEADOS " +
                                        "FROM EXACTUS.DIGEMA.EMPLEADO E " +
                                        "INNER JOIN EXACTUS.DIGEMA.EMPLEADO_ACC_PER A WITH(NOLOCK) ON A.EMPLEADO=E.EMPLEADO " +
                                        "WHERE A.TIPO_ACCION='VAGO' AND ESTADO_ACCION='S' " +
                                        "AND E.UBICACION='" & ubicacion_empleado & "' AND E.PUESTO='" & puesto_empleado & "' AND A.FECHA_RIGE >='" & iniciales_vacaciones & "' AND A.FECHA_VENCE <='" & finales_vacaciones & "' " +
                                        "GROUP BY E.PUESTO,E.UBICACION"




                        .CommandType = CommandType.Text
                    End With
                    daconsulta_fin.SelectCommand = cmdconsulta_fin
                    daconsulta_fin.Fill(dsconsulta_fin)


                    For y = 0 To dsconsulta_fin.Tables(0).Rows.Count - 1

                        empleados_registrados = dsconsulta_fin.Tables(0).Rows(y).Item("EMPLEADOS_REGISTRADOS")
                        total_empleados = dsconsulta_fin.Tables(0).Rows(y).Item("TOTAL_EMPLEADOS")
                        calculo_empleados = (empleados_registrados / total_empleados) * 100



                        If calculo_empleados > 5 Then

                            MsgBox("Error no puedes disfrutar el dia de vacaciones, debido a que el " & calculo_empleados & " %  de tu departamento ya lo solicitó", MsgBoxStyle.Critical, "Construplaza")

                            bandera_de_error = True
                            BtnSolicitar.Enabled = True
                            m_FPM.CloseDevice()
                            Formmenu.btnvacaciones.Enabled = True
                            Me.Close()

                        End If



                    Next



                    'MsgBox("Error, esta escogiendo un dia (Sábado o Domingo) fin de semana como vacaciones, favor revise  ", MsgBoxStyle.Critical, "Construplaza")
                    'bandera_de_error = True
                    'BtnSolicitar.Enabled = True
                    'm_FPM.CloseDevice()
                    'Formmenu.btnvacaciones.Enabled = True
                    'Me.Close()

                End If


            Next




            '---------------------------------------------------------------------------------------------------------------------------------------------------



            If fecha_final >= fecha_inicial And fecha_inicial >= nueva_Fecha And fecha_final >= nueva_Fecha And bandera_de_error = False Then



                iniciales = Format(DtFecha_inicial.Value, "yyyy-MM-dd" & " 00:00:00.000")
                finales = Format(DTFecha_final.Value, "yyyy-MM-dd" & " 00:00:00.000")


                texto = "DATEDIFF(DD,'" & iniciales & "','" & finales & "')"



                Dim Intervalo As TimeSpan

                Intervalo = DTFecha_final.Value.Subtract(DtFecha_inicial.Value)



                dias_diferencia = Intervalo.Days.ToString




                dias_diferencia = dias_diferencia + 1


                If dias_diferencia > dias_pendi Then
                    MsgBox("Esta eligiendo mas dias de los disponibles favor revisar", MsgBoxStyle.Critical, "Construplaza")
                    BtnSolicitar.Enabled = True
                Else



                    If conexion.State = ConnectionState.Closed Then
                        conexion.Open()
                    End If

                    transaccion = conexion.BeginTransaction
                    cmdincluir = New SqlClient.SqlCommand



                    If conexion_local.State = ConnectionState.Closed Then
                        conexion_local.Open()
                    End If

                    transaccion_local = conexion_local.BeginTransaction
                    cmdincluir_local = New SqlClient.SqlCommand


                    'parametros = "" & consecutivo & ",'AJUS','" & txtempleado.Text & "','P','" & iniciales & "','" & finales & "'," & dias_diferencia & ",'" & txtempleado.Text & "'," & hoy & "," & hoy & ",'Hecho desde kiosco'," & noteexistflag & ",'XPP','XPP'" '"

                    parametros = "" & consecutivo & ",'VAGO'," & hoy_cero & ",'" & txtempleado.Text & "','JUS','" & iniciales & "','" & finales & "','Ausencia justificada por Feriado','LSEGURA'," & hoy & ",'LSEGURA'," & hoy & ",'LSEGURA'," & hoy & ",'LSEGURA'," & hoy & ",'S'," & dias_diferencia & "," & dias_diferencia & "," & noteexistflag & ""





                    'If conexion.State = ConnectionState.Closed Then
                    '    conexion.Open()
                    'End If

                    'transaccion = conexion.BeginTransaction
                    'cmdincluir = New SqlClient.SqlCommand


                    'parametros = "" & consecutivo & ",'VACA','" & txtempleado.Text & "','P','" & iniciales & "','" & finales & "'," & dias_diferencia & ",'" & txtempleado.Text & "'," & hoy & "," & hoy & ",'Hecho desde kiosco'," & noteexistflag & ",'XPP','XPP'" '"



                    With cmdincluir_local
                        .Connection = conexion_local
                        .CommandText = "INSERT INTO EXACTUS.DIGEMA.EMPLEADO_ACC_PER(NUMERO_ACCION,TIPO_ACCION,FECHA,EMPLEADO,TIPO_AUSENCIA,FECHA_RIGE,FECHA_VENCE,NOTAS,USUARIO,FECHA_HORA,APROBADA_POR,FECHA_APROBACION,USUARIO_VIGENTE,FECHA_VIGENTE,USUARIO_APLICACION,FECHA_APLICACION,ESTADO_ACCION,DIAS_ACCION,SALDO,NoteExistsFlag) values (" & parametros & ")"
                        .CommandType = CommandType.Text
                        .Transaction = transaccion_local
                    End With
                    iregincluir_local = cmdincluir_local.ExecuteNonQuery
                    transaccion_local.Commit()
                    '  MsgBox("Datos incluidos satisfactoriamente, su número de acción de personal es la: " & consecutivo & "", vbInformation, "Construplaza")
                    BtnSolicitar.Enabled = True
                    Formmenu.btnvacaciones.Enabled = True


                    '------------------------------------------------------------------------------------------------------------------------------------


                    If conexion_local.State = ConnectionState.Closed Then
                        conexion_local.Open()
                    End If

                    transaccion_local = conexion_local.BeginTransaction
                    cmdincluir_local = New SqlClient.SqlCommand


                    With cmdincluir_local
                        .Connection = conexion_local
                        .CommandText = "UPDATE EXACTUS.DIGEMA.EMPLEADO SET VACS_PENDIENTES=VACS_PENDIENTES-" & dias_diferencia & " WHERE EMPLEADO='" & txtempleado.Text & "'"
                        .CommandType = CommandType.Text
                        .Transaction = transaccion_local
                    End With
                    iregincluir_local = cmdincluir_local.ExecuteNonQuery
                    transaccion_local.Commit()
                    '    MsgBox("Datos incluidos satisfactoriamente, su número de acción de personal es la: " & consecutivo & " ya quedó en firme y aprobado su goce de vacaciones", vbInformation, "Construplaza")
                    BtnSolicitar.Enabled = True
                    Formmenu.btnvacaciones.Enabled = True



                    '------------------------------------------------------------------------------------------------------------------------------------

                    If conexion_local.State = ConnectionState.Closed Then
                        conexion_local.Open()
                    End If

                    transaccion_local = conexion_local.BeginTransaction
                    cmdincluir_local = New SqlClient.SqlCommand



                    With cmdincluir_local
                        .Connection = conexion_local
                        .CommandText = "UPDATE EXACTUS.DIGEMA.GLOBALES_RH SET ULTIMA_ACCION= " & consecutivo + 1 & " "
                        .CommandType = CommandType.Text
                        .Transaction = transaccion_local
                    End With
                    iregincluir_local = cmdincluir_local.ExecuteNonQuery
                    transaccion_local.Commit()
                    MsgBox("Datos incluidos satisfactoriamente, su número de acción de personal es la: " & consecutivo & " ya quedó en firme y aprobado su goce de vacaciones", vbInformation, "Construplaza")
                    BtnSolicitar.Enabled = True
                    Formmenu.btnvacaciones.Enabled = True








                    'With cmdincluir
                    '    .Connection = conexion
                    '    .CommandText = "INSERT INTO EXACTUS.DIGEMA.SOLICITUD_RH (SOLICITUD_RH,TIPO_SOLICITUD_RH,EMPLEADO,ESTADO,FECHA_RIGE,FECHA_VENCE,DIAS,GENERADA_POR,FECHA_HORA,FECHA_ESTADO,OBSERVACIONES,NOTEEXISTSFLAG,CREATEDBY,UPDATEDBY) values (" & parametros & ")"
                    '    .CommandType = CommandType.Text
                    '    .Transaction = transaccion
                    'End With
                    'iregincluir = cmdincluir.ExecuteNonQuery
                    'transaccion.Commit()
                    'MsgBox("Datos incluidos satisfactoriamente, su número de solicitud es la: " & consecutivo & " favor coordinar con RRHH", vbInformation, "Construplaza")
                    'BtnSolicitar.Enabled = True

                End If



            End If

        Catch ex As Exception
            MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
            m_FPM.CloseDevice()
            Formmenu.btnvacaciones.Enabled = True
            Me.Close()

            ' Show the stack trace, which is a list of methods 
            ' that are currently executing.
            '  MessageBox.Show("Stack Trace: " & vbCrLf & ex.StackTrace)
        Finally
            m_FPM.CloseDevice()
            Formmenu.btnvacaciones.Enabled = True
            Me.Close()
            ' This line executes whether or not the exception occurs.
            '  MessageBox.Show("in Finally block")
        End Try



    End Sub


    Private Sub StatusBar_Click(sender As Object, e As EventArgs) Handles StatusBar.Click

    End Sub
End Class