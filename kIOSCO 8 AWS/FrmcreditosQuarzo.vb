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

Public Class FrmcreditosQuarzo

    Public dias_pendi As Integer
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
    Public contadores, total_vacaciones As Integer
    Public conta_marcas As Integer = 0
    Public total_disponible As Decimal = 0

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
    Public totales As Decimal = 0
    Public salario_quincenal As Decimal = 0
    Private Sub FrmcreditosQuarzo_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        txtmontocuota.Text = 0
        txtcuotas.Text = 0




        Try


            Formmenu.btncreditos.Enabled = False

            txtsolicitado.Text = 0
            txtcuotas.Text = 0

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
                    Formmenu.btncreditos.Enabled = True
                    '   Me.Close()


                End If
            Else
                DisplayError("GetImage", iError)
                bandera_error = True
                MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
                Formmenu.btncreditos.Enabled = True
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
                        'dias_pendi = dsconsulta.Tables(0).Rows(z).Item("VACS_PENDIENTES")
                        'lblvacaciones_pendientes.Text = "Vacaciones pendientes: " & Format(dsconsulta.Tables(0).Rows(z).Item("VACS_PENDIENTES"), "0.00")
                        'total_vacaciones = dsconsulta.Tables(0).Rows(z).Item("VACS_PENDIENTES")

                    Next

                End If

            End If

            '--------------------------verifica el limite----------------------------------------------------------------------


            Dim credenciales As New com.quarzo.gestiones6.BEWSCredenciales
            credenciales.IdUsuariWs = "034900"
            credenciales.PasswordWs = "TES2871212"
            '"ACP2871212"
            credenciales.TokenWs = "0349APJ8LH2P5"

            '-------ahorros-------------------------------------------------------------------------------------------------------
            Dim ws As com.quarzo.gestiones6.ServiceNubeAseconstruPlaza = New com.quarzo.gestiones6.ServiceNubeAseconstruPlaza()

            txtdisponible.Text = CDbl(CStr(ws.Met_DisponiblePorAsociado(credenciales, emplea2_encontrado).NDisponible)).ToString("N2")

            '--------------------------------------------------------------------------------------------------------------------



            'cmdconsulta = New SqlClient.SqlCommand
            'daconsulta = New SqlClient.SqlDataAdapter
            'dsconsulta = New DataSet
            'With cmdconsulta
            '    .Connection = conexion
            '    .CommandText = "SELECT nDisponible as DISPONIBLE " +
            '    "FROM [DIGEMA0020050\BDAPP02].[coviaseconstruplaza].[dbo].[Vista_DisponiblesCredito] " +
            '    "WHERE cidasociad='" & emplea2_encontrado & "'"

            '    ''" & emplea2_encontrado & "' " +

            '    .CommandType = CommandType.Text
            'End With
            'daconsulta.SelectCommand = cmdconsulta
            'daconsulta.Fill(dsconsulta)

            'For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

            '    txtdisponible.Text = CDbl(dsconsulta.Tables(0).Rows(z).Item("DISPONIBLE")).ToString("N2")
            '    total_disponible = dsconsulta.Tables(0).Rows(z).Item("DISPONIBLE")

            'Next


            BTN24.Visible = False
            BTN36.Visible = False

            If txtdisponible.Text >= 1000000 Then
                BTN24.Visible = True
                BTN36.Visible = True

            End If


        Catch ex As Exception
            MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
            m_FPM.CloseDevice()
            Me.Close()
            Formmenu.btncreditos.Enabled = True
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



    Private Sub btn5000_Click(sender As Object, e As EventArgs) Handles btn5000.Click
        Dim capturas As Decimal = 0

        capturas = txtsolicitado.Text
        capturas = capturas + 5000
        txtsolicitado.Text = CDbl(capturas.ToString("N2"))
        txtmontocuota.Text = 0
        txtcuotas.Text = 0
        totales = totales + txtsolicitado.Text



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim capturas As Decimal = 0

        capturas = txtsolicitado.Text
        capturas = capturas + 10000
        txtsolicitado.Text = CDbl(capturas.ToString("N2"))
        txtmontocuota.Text = 0
        txtcuotas.Text = 0
        totales = totales + txtsolicitado.Text

    End Sub

    Private Sub btn20000_Click(sender As Object, e As EventArgs) Handles btn20000.Click
        Dim capturas As Decimal = 0

        capturas = txtsolicitado.Text
        capturas = capturas + 20000
        txtsolicitado.Text = CDbl(capturas.ToString("N2"))
        txtmontocuota.Text = 0
        txtcuotas.Text = 0
        totales = totales + txtsolicitado.Text
    End Sub

    Private Sub btn30000_Click(sender As Object, e As EventArgs) Handles btn30000.Click
        Dim capturas As Decimal = 0

        capturas = txtsolicitado.Text
        capturas = capturas + 30000
        txtsolicitado.Text = CDbl(capturas.ToString("N2"))
        txtmontocuota.Text = 0
        txtcuotas.Text = 0
        totales = totales + txtsolicitado.Text

    End Sub

    Private Sub btn50000_Click(sender As Object, e As EventArgs) Handles btn50000.Click
        Dim capturas As Decimal = 0

        capturas = txtsolicitado.Text
        capturas = capturas + 50000
        txtsolicitado.Text = CDbl(capturas.ToString("N2"))
        txtmontocuota.Text = 0
        txtcuotas.Text = 0
        totales = totales + txtsolicitado.Text
    End Sub

    Private Sub Btn100000_Click(sender As Object, e As EventArgs) Handles Btn100000.Click
        Dim capturas As Decimal = 0
        capturas = txtsolicitado.Text
        capturas = capturas + 100000
        txtsolicitado.Text = CDbl(capturas.ToString("N2"))
        txtmontocuota.Text = 0
        txtcuotas.Text = 0
        totales = totales + txtsolicitado.Text

    End Sub

    Private Sub btncerrar_Click(sender As Object, e As EventArgs) Handles btncerrar.Click
        Formmenu.btncreditos.Enabled = True
        conexion.Close()
        Me.Close()

    End Sub

    Private Sub btnlimpiar_Click(sender As Object, e As EventArgs) Handles btnlimpiar.Click
        txtcuotas.Text = 0
        txtsolicitado.Text = 0
        txtmontocuota.Text = 0
    End Sub

    Private Sub btn4_Click(sender As Object, e As EventArgs) Handles btn4.Click

        If txtsolicitado.Text <= 0 Then
            MsgBox("Favor solicite un monto mas alto", MsgBoxStyle.Critical, "Construplaza")
        Else
            txtcuotas.Text = 4
            Dim interes As Decimal = 0
            Dim total As Decimal = 0
            Dim total_general As Decimal = 0
            interes = txtsolicitado.Text * 0.2
            total = txtsolicitado.Text + interes
            total_general = total / txtcuotas.Text
            txtmontocuota.Text = total_general
        End If



    End Sub

    Private Sub btn8_Click(sender As Object, e As EventArgs) Handles btn8.Click
        'txtcuotas.Text = 8
        'Dim interes As Decimal = 0
        'Dim total As Decimal = 0
        'Dim total_general As Decimal = 0
        'interes = txtsolicitado.Text * 0.2
        'total = txtsolicitado.Text + interes
        'total_general = total / txtcuotas.Text
        'txtmontocuota.Text = total_general


        If txtsolicitado.Text <= 0 Then
            MsgBox("Favor solicite un monto mas alto", MsgBoxStyle.Critical, "Construplaza")
        Else
            txtcuotas.Text = 8
            Dim interes As Decimal = 0
            Dim total As Decimal = 0
            Dim total_general As Decimal = 0
            interes = txtsolicitado.Text * 0.2
            total = txtsolicitado.Text + interes
            total_general = total / txtcuotas.Text
            txtmontocuota.Text = total_general
        End If


    End Sub

    Private Sub btn12_Click(sender As Object, e As EventArgs) Handles btn12.Click

        'txtcuotas.Text = 12
        'Dim interes As Decimal = 0
        'Dim total As Decimal = 0
        'Dim total_general As Decimal = 0
        'interes = txtsolicitado.Text * 0.2
        'total = txtsolicitado.Text + interes
        'total_general = total / txtcuotas.Text
        'txtmontocuota.Text = total_general


        If txtsolicitado.Text <= 0 Then
            MsgBox("Favor solicite un monto mas alto", MsgBoxStyle.Critical, "Construplaza")
        Else
            txtcuotas.Text = 12
            Dim interes As Decimal = 0
            Dim total As Decimal = 0
            Dim total_general As Decimal = 0
            interes = txtsolicitado.Text * 0.2
            total = txtsolicitado.Text + interes
            total_general = total / txtcuotas.Text
            txtmontocuota.Text = total_general
        End If


    End Sub

    Private Sub BTN18_Click(sender As Object, e As EventArgs) Handles BTN18.Click
        'txtcuotas.Text = 18
        'Dim interes As Decimal = 0
        'Dim total As Decimal = 0
        'Dim total_general As Decimal = 0
        'interes = txtsolicitado.Text * 0.2
        'total = txtsolicitado.Text + interes
        'total_general = total / txtcuotas.Text
        'txtmontocuota.Text = total_general



        If txtsolicitado.Text <= 0 Then
            MsgBox("Favor solicite un monto mas alto", MsgBoxStyle.Critical, "Construplaza")
        Else
            txtcuotas.Text = 18
            Dim interes As Decimal = 0
            Dim total As Decimal = 0
            Dim total_general As Decimal = 0
            interes = txtsolicitado.Text * 0.2
            total = txtsolicitado.Text + interes
            total_general = total / txtcuotas.Text
            txtmontocuota.Text = total_general
        End If
    End Sub

    Private Sub BTN24_Click(sender As Object, e As EventArgs) Handles BTN24.Click
        'txtcuotas.Text = 24
        'Dim interes As Decimal = 0
        'Dim total As Decimal = 0
        'Dim total_general As Decimal = 0
        'interes = txtsolicitado.Text * 0.2
        'total = txtsolicitado.Text + interes
        'total_general = total / txtcuotas.Text
        'txtmontocuota.Text = total_general

        If txtsolicitado.Text <= 0 Then
            MsgBox("Favor solicite un monto mas alto", MsgBoxStyle.Critical, "Construplaza")
        Else
            txtcuotas.Text = 24
            Dim interes As Decimal = 0
            Dim total As Decimal = 0
            Dim total_general As Decimal = 0
            interes = txtsolicitado.Text * 0.2
            total = txtsolicitado.Text + interes
            total_general = total / txtcuotas.Text
            txtmontocuota.Text = total_general
        End If

    End Sub

    Private Sub BTN36_Click(sender As Object, e As EventArgs) Handles BTN36.Click


        If txtsolicitado.Text <= 0 Then
            MsgBox("Favor solicite un monto mas alto", MsgBoxStyle.Critical, "Construplaza")
        Else
            txtcuotas.Text = 36
            Dim interes As Decimal = 0
            Dim total As Decimal = 0
            Dim total_general As Decimal = 0
            interes = txtsolicitado.Text * 0.2
            total = txtsolicitado.Text + interes
            total_general = total / txtcuotas.Text
            txtmontocuota.Text = total_general
        End If

        'txtcuotas.Text = 36
        'Dim interes As Decimal = 0
        'Dim total As Decimal = 0
        'Dim total_general As Decimal = 0
        'interes = txtsolicitado.Text * 0.2
        'total = txtsolicitado.Text + interes
        'total_general = total / txtcuotas.Text
        'txtmontocuota.Text = total_general
    End Sub

    Private Sub btn2_Click(sender As Object, e As EventArgs) Handles btn2.Click

        If txtsolicitado.Text <= 0 Then
            MsgBox("Favor solicite un monto mas alto", MsgBoxStyle.Critical, "Construplaza")
        Else
            txtcuotas.Text = 2
            Dim interes As Decimal = 0
            Dim total As Decimal = 0
            Dim total_general As Decimal = 0
            interes = txtsolicitado.Text * 0.2
            total = txtsolicitado.Text + interes
            total_general = total / txtcuotas.Text
            txtmontocuota.Text = total_general
        End If

    End Sub

    Private Sub btn6_Click(sender As Object, e As EventArgs) Handles btn6.Click

        If txtsolicitado.Text <= 0 Then
            MsgBox("Favor solicite un monto mas alto", MsgBoxStyle.Critical, "Construplaza")
        Else
            txtcuotas.Text = 6
            Dim interes As Decimal = 0
            Dim total As Decimal = 0
            Dim total_general As Decimal = 0
            interes = txtsolicitado.Text * 0.2
            total = txtsolicitado.Text + interes
            total_general = total / txtcuotas.Text
            txtmontocuota.Text = total_general
        End If

    End Sub


    Private Sub btningresar_Click(sender As Object, e As EventArgs) Handles btningresar.Click
        Try


            'If (CDec(txtsolicitado.Text) < 30000 And CInt(txtcuotas.Text) > 2) Then


            '    MsgBox("Montos menores o iguales a 30 mil solo en dos o mas cuotas no es permitido, favor revisar", MsgBoxStyle.Critical, "Construplaza")

            'Else

            '    If CDec(txtsolicitado.Text) <= CDec(total_disponible) And CInt(txtcuotas.Text) > 0 And CDec(txtsolicitado.Text) >= 1 Then

            If (CDec(txtsolicitado.Text) < 30000) Then
                MsgBox("El monto mínimo para crédito es de 30 mil colones, favor revisar", MsgBoxStyle.Critical, "Construplaza")
            Else


                If CDec(txtsolicitado.Text) > CDec(txtdisponible.Text) Or CInt(txtcuotas.Text) <= 0 Or CDec(txtsolicitado.Text) <= 0 Then

                    MsgBox("Monto solicitado mayor a su dispoible o numero de cuotas insuficiente, favor revisar", MsgBoxStyle.Critical)


                Else



                    Dim credenciales As New com.quarzo.gestiones6.BEWSCredenciales
                    credenciales.IdUsuariWs = "034900"
                    credenciales.PasswordWs = "TES2871212"
                    '"ACP2871212"
                    credenciales.TokenWs = "0349APJ8LH2P5"


                    '--------------------------------------------------------------------------------------------------------------------

                    Dim Ws As com.quarzo.gestiones6.ServiceNubeAseconstruPlaza = New com.quarzo.gestiones6.ServiceNubeAseconstruPlaza()
                    Ws.Met_Solicitud_Credito(credenciales, txtempleado.Text, CDec(txtsolicitado.Text), CInt(txtcuotas.Text), "0000000001", "Desde Kiosco a la Nube")


                    MsgBox("Gracias su solicitud fue ingresada", MsgBoxStyle.Information)


                    'Else
                    '    MsgBox("Monto solicitado mayor a su dispoible o numero de cuotas insuficiente, favor revisar", MsgBoxStyle.Critical)


                End If

            End If






        Catch ex As Exception
            MsgBox("No se logró insertar la solicitud de credito, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
            m_FPM.CloseDevice()
            Me.Close()
            Formmenu.btncreditos.Enabled = True
            ' Show the stack trace, which is a list of methods 
            ' that are currently executing.
            '  MessageBox.Show("Stack Trace: " & vbCrLf & ex.StackTrace)
        Finally
            m_FPM.CloseDevice()
            ' This line executes whether or not the exception occurs.
            '  MessageBox.Show("in Finally block")
        End Try





    End Sub
End Class