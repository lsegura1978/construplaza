Imports System.Text
Imports SecuGen.FDxSDKPro.Windows
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Public Class Frmmarca

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
    Public z, i As Integer
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

    'Private Sub Frmmarca_unLoad(sender As Object, e As EventArgs) Handles MyBase.Load

    '    Formmenu.BackColor = Color.Gray
    '    Formmenu.btnIngresar.BackColor = Color.Gray
    '    Formmenu.btnmarcar.BackColor = Color.Gray

    'End Sub


    Private Sub Frmmarca_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.BackColor = Color.Gray


        m_LedOn = False
        comboBoxSecuLevel_R.SelectedIndex = 4
        comboBoxSecuLevel_V.SelectedIndex = 3
        EnableButtons(False)

        m_FPM = New SGFingerPrintManager
        EnumerateBtn_Click(sender, e)

        OpenDeviceBtn_Click(sender, e)




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
            EnableButtons(True)

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


    Private Sub EnableButtons(ByVal enable As Boolean)
        'ConfigBtn.Enabled = enable
        'GetImageBtn.Enabled = enable
        'GetLiveImageBtn.Enabled = enable
        'BtnCapture1.Enabled = enable
        'BtnCapture2.Enabled = enable
        'BtnCapture3.Enabled = enable
        'BtnRegister.Enabled = enable
        'BtnVerify.Enabled = enable
        'GetBtn.Enabled = enable
        'SetBrightnessBtn.Enabled = enable
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

    Private Sub Btnmarcar_Click(sender As Object, e As EventArgs) Handles Btnmarcar.Click

        '----------------------------carga-------------------------------------------------------------------------

        System.Threading.Thread.Sleep(1000)
        Dim iError As Int32




        ''-------------------------------------1 captura------------------------------------------------------------------


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
            Else
                DisplayError("CreateTemplate", iError)
            End If
        Else
            DisplayError("GetImage", iError)
        End If



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

                '----------------------------------------------------------MARCAR-------------------------


                If conexion.State = ConnectionState.Closed Then
                    conexion.Open()
                End If


                Dim feha_marca As DateTime = DateTime.Now.ToString("dd/MM/yyyy")
                Dim hora_marca As String = Now.ToString("HH:mm")
                'Date.Now.ToShortTimeString
                Dim tipo As Integer = 0


                transaccion = conexion.BeginTransaction
                cmdincluir = New SqlClient.SqlCommand


                parametros = "'" & empleado & "','" & feha_marca & "','" & hora_marca & "'," & tipo & ""

                '   MsgBox("Empleado no ha sido ingresado ," & parametros, MsgBoxStyle.Critical)



                With cmdincluir
                    .Connection = conexion
                    .CommandText = "INSERT INTO ASISTENCIA.DBO.MARCAS(EMPLEADO,FECHA,HORA,TIPO) values (" & parametros & ")"
                    .CommandType = CommandType.Text
                    .Transaction = transaccion
                End With
                iregincluir = cmdincluir.ExecuteNonQuery
                transaccion.Commit()

                '  MsgBox("Estimado " & nombres & "  ustes esta marcando a las " & hora_marca & " del día " & feha_marca, vbInformation)

                StatusBar.Text = "Estimado " & nombres & "  ustes esta marcando a las " & hora_marca & " del día " & feha_marca



                Me.BackColor = Color.Green
                Formmenu.BackColor = Color.Green
                Formmenu.btnIngresar.BackColor = Color.Green
                Formmenu.btnmarcar.BackColor = Color.Green






            Else
                StatusBar.Text = "Verification Failed"
            End If

        Else
            DisplayError("MatchTemplate", iErrorver)
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


    'Private Sub Button12_Click(sender As Object, e As EventArgs)
    '    Dim obj As Object, B() As Byte


    '    cmdconsulta = New SqlClient.SqlCommand
    '    daconsulta = New SqlClient.SqlDataAdapter
    '    dsconsulta = New DataSet
    '    With cmdconsulta
    '        .Connection = conexion
    '        .CommandText = "SELECT HUELLA FROM ASISTENCIA.dbo.EMPLEADO_HUELLA WHERE EMPLEADO='0538'"
    '        .CommandType = CommandType.Text
    '    End With
    '    daconsulta.SelectCommand = cmdconsulta
    '    daconsulta.Fill(dsconsulta)


    '    For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
    '        obj = dsconsulta.Tables(0).Rows(z).Item("HUELLA")


    '        If Not IsDBNull(obj) Then
    '            B = DirectCast(obj, Byte())

    '            Using ms As New IO.MemoryStream(B)

    '                PictureBox2.Image = Image.FromStream(ms)

    '            End Using

    '        End If

    '    Next
    ' End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        encontrado = False

        StatusBar.Text = ""

        conta = conta + 1
        Label1.Text = conta

        txtempleados.Text = txtempleados.Text & Button1.Text

        '  MsgBox("jaja" & conta)

        If conta >= 4 Then

            'Btnmarcar_Click(sender, e)


            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
            Button8.Enabled = False
            Button9.Enabled = False
            Button10.Enabled = False
            '   Button12.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                nombres = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next





            cmdconsulta_documentos = New SqlClient.SqlCommand
            daconsulta_documentos = New SqlClient.SqlDataAdapter
            dsconsulta_documentos = New DataSet
            With cmdconsulta_documentos
                .Connection = conexion
                .CommandText = "SELECT EMPLEADO FROM ASISTENCIA.DBO.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta_documentos.SelectCommand = cmdconsulta_documentos
            daconsulta_documentos.Fill(dsconsulta_documentos)


            For i = 0 To dsconsulta_documentos.Tables(0).Rows.Count - 1
                encontrado = True
                empleado = dsconsulta_documentos.Tables(0).Rows(i).Item("EMPLEADO")
            Next



            If encontrado = True Then


                '----------------------------------------------------------------------------------------------
                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT IMAGEN_1,TEMPLATE_1,TEMPLATE_2 FROM ASISTENCIA.dbo.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                    .CommandType = CommandType.Text
                End With
                daconsulta.SelectCommand = cmdconsulta
                daconsulta.Fill(dsconsulta)


                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                    obj = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    obj2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")
                    'plantilla1 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    'plantilla2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")


                    If Not IsDBNull(obj) Then
                        B = DirectCast(obj, Byte())

                        fp_plantilla = DirectCast(obj, Byte())
                        fp_plantilla2 = DirectCast(obj2, Byte())


                        Using ms As New IO.MemoryStream(B)

                            '   pictureBox2.Image = Image.FromStream(ms)



                        End Using

                    End If

                Next


                Btnmarcar_Click(sender, e)

            Else

                '  MsgBox("Empleado no ha sido ingresado, favor verificar", MsgBoxStyle.Critical)

                Me.BackColor = Color.Red
                Formmenu.BackColor = Color.Red
                Formmenu.btnIngresar.BackColor = Color.Red
                Formmenu.btnmarcar.BackColor = Color.Red
                StatusBar.Text = "Empleado no ha sido ingresado, favor verificar"




            End If


        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        encontrado = False

        StatusBar.Text = ""

        conta = conta + 1
        Label1.Text = conta

        txtempleados.Text = txtempleados.Text & Button2.Text

        '  MsgBox("jaja" & conta)

        If conta >= 4 Then

            'Btnmarcar_Click(sender, e)


            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
            Button8.Enabled = False
            Button9.Enabled = False
            Button10.Enabled = False
            '   Button12.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                nombres = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next





            cmdconsulta_documentos = New SqlClient.SqlCommand
            daconsulta_documentos = New SqlClient.SqlDataAdapter
            dsconsulta_documentos = New DataSet
            With cmdconsulta_documentos
                .Connection = conexion
                .CommandText = "SELECT EMPLEADO FROM ASISTENCIA.DBO.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta_documentos.SelectCommand = cmdconsulta_documentos
            daconsulta_documentos.Fill(dsconsulta_documentos)


            For i = 0 To dsconsulta_documentos.Tables(0).Rows.Count - 1
                encontrado = True
                empleado = dsconsulta_documentos.Tables(0).Rows(i).Item("EMPLEADO")
            Next



            If encontrado = True Then


                '----------------------------------------------------------------------------------------------
                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT IMAGEN_1,TEMPLATE_1,TEMPLATE_2 FROM ASISTENCIA.dbo.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                    .CommandType = CommandType.Text
                End With
                daconsulta.SelectCommand = cmdconsulta
                daconsulta.Fill(dsconsulta)


                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                    obj = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    obj2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")
                    'plantilla1 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    'plantilla2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")


                    If Not IsDBNull(obj) Then
                        B = DirectCast(obj, Byte())

                        fp_plantilla = DirectCast(obj, Byte())
                        fp_plantilla2 = DirectCast(obj2, Byte())


                        Using ms As New IO.MemoryStream(B)

                            '   pictureBox2.Image = Image.FromStream(ms)



                        End Using

                    End If

                Next


                Btnmarcar_Click(sender, e)

            Else

                '  MsgBox("Empleado no ha sido ingresado, favor verificar", MsgBoxStyle.Critical)

                Me.BackColor = Color.Red
                Formmenu.BackColor = Color.Red
                Formmenu.btnIngresar.BackColor = Color.Red
                Formmenu.btnmarcar.BackColor = Color.Red
                StatusBar.Text = "Empleado no ha sido ingresado, favor verificar"




            End If


        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        encontrado = False

        StatusBar.Text = ""

        conta = conta + 1
        Label1.Text = conta

        txtempleados.Text = txtempleados.Text & Button3.Text

        '  MsgBox("jaja" & conta)

        If conta >= 4 Then

            'Btnmarcar_Click(sender, e)


            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
            Button8.Enabled = False
            Button9.Enabled = False
            Button10.Enabled = False
            '   Button12.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                nombres = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next





            cmdconsulta_documentos = New SqlClient.SqlCommand
            daconsulta_documentos = New SqlClient.SqlDataAdapter
            dsconsulta_documentos = New DataSet
            With cmdconsulta_documentos
                .Connection = conexion
                .CommandText = "SELECT EMPLEADO FROM ASISTENCIA.DBO.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta_documentos.SelectCommand = cmdconsulta_documentos
            daconsulta_documentos.Fill(dsconsulta_documentos)


            For i = 0 To dsconsulta_documentos.Tables(0).Rows.Count - 1
                encontrado = True
                empleado = dsconsulta_documentos.Tables(0).Rows(i).Item("EMPLEADO")
            Next



            If encontrado = True Then


                '----------------------------------------------------------------------------------------------
                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT IMAGEN_1,TEMPLATE_1,TEMPLATE_2 FROM ASISTENCIA.dbo.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                    .CommandType = CommandType.Text
                End With
                daconsulta.SelectCommand = cmdconsulta
                daconsulta.Fill(dsconsulta)


                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                    obj = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    obj2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")
                    'plantilla1 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    'plantilla2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")


                    If Not IsDBNull(obj) Then
                        B = DirectCast(obj, Byte())

                        fp_plantilla = DirectCast(obj, Byte())
                        fp_plantilla2 = DirectCast(obj2, Byte())


                        Using ms As New IO.MemoryStream(B)

                            '   pictureBox2.Image = Image.FromStream(ms)



                        End Using

                    End If

                Next


                Btnmarcar_Click(sender, e)

            Else

                '  MsgBox("Empleado no ha sido ingresado, favor verificar", MsgBoxStyle.Critical)

                Me.BackColor = Color.Red
                Formmenu.BackColor = Color.Red
                Formmenu.btnIngresar.BackColor = Color.Red
                Formmenu.btnmarcar.BackColor = Color.Red
                StatusBar.Text = "Empleado no ha sido ingresado, favor verificar"




            End If


        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        encontrado = False

        StatusBar.Text = ""

        conta = conta + 1
        Label1.Text = conta

        txtempleados.Text = txtempleados.Text & Button4.Text

        '  MsgBox("jaja" & conta)

        If conta >= 4 Then

            'Btnmarcar_Click(sender, e)


            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
            Button8.Enabled = False
            Button9.Enabled = False
            Button10.Enabled = False
            '   Button12.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                nombres = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next





            cmdconsulta_documentos = New SqlClient.SqlCommand
            daconsulta_documentos = New SqlClient.SqlDataAdapter
            dsconsulta_documentos = New DataSet
            With cmdconsulta_documentos
                .Connection = conexion
                .CommandText = "SELECT EMPLEADO FROM ASISTENCIA.DBO.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta_documentos.SelectCommand = cmdconsulta_documentos
            daconsulta_documentos.Fill(dsconsulta_documentos)


            For i = 0 To dsconsulta_documentos.Tables(0).Rows.Count - 1
                encontrado = True
                empleado = dsconsulta_documentos.Tables(0).Rows(i).Item("EMPLEADO")
            Next



            If encontrado = True Then


                '----------------------------------------------------------------------------------------------
                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT IMAGEN_1,TEMPLATE_1,TEMPLATE_2 FROM ASISTENCIA.dbo.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                    .CommandType = CommandType.Text
                End With
                daconsulta.SelectCommand = cmdconsulta
                daconsulta.Fill(dsconsulta)


                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                    obj = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    obj2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")
                    'plantilla1 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    'plantilla2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")


                    If Not IsDBNull(obj) Then
                        B = DirectCast(obj, Byte())

                        fp_plantilla = DirectCast(obj, Byte())
                        fp_plantilla2 = DirectCast(obj2, Byte())


                        Using ms As New IO.MemoryStream(B)

                            '   pictureBox2.Image = Image.FromStream(ms)



                        End Using

                    End If

                Next


                Btnmarcar_Click(sender, e)

            Else

                '  MsgBox("Empleado no ha sido ingresado, favor verificar", MsgBoxStyle.Critical)

                Me.BackColor = Color.Red
                Formmenu.BackColor = Color.Red
                Formmenu.btnIngresar.BackColor = Color.Red
                Formmenu.btnmarcar.BackColor = Color.Red
                StatusBar.Text = "Empleado no ha sido ingresado, favor verificar"




            End If


        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click


        encontrado = False

        StatusBar.Text = ""

        conta = conta + 1
        Label1.Text = conta

        txtempleados.Text = txtempleados.Text & Button5.Text

        '  MsgBox("jaja" & conta)

        If conta >= 4 Then

            'Btnmarcar_Click(sender, e)


            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
            Button8.Enabled = False
            Button9.Enabled = False
            Button10.Enabled = False
            '   Button12.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                nombres = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next





            cmdconsulta_documentos = New SqlClient.SqlCommand
            daconsulta_documentos = New SqlClient.SqlDataAdapter
            dsconsulta_documentos = New DataSet
            With cmdconsulta_documentos
                .Connection = conexion
                .CommandText = "SELECT EMPLEADO FROM ASISTENCIA.DBO.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta_documentos.SelectCommand = cmdconsulta_documentos
            daconsulta_documentos.Fill(dsconsulta_documentos)


            For i = 0 To dsconsulta_documentos.Tables(0).Rows.Count - 1
                encontrado = True
                empleado = dsconsulta_documentos.Tables(0).Rows(i).Item("EMPLEADO")
            Next



            If encontrado = True Then


                '----------------------------------------------------------------------------------------------
                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT IMAGEN_1,TEMPLATE_1,TEMPLATE_2 FROM ASISTENCIA.dbo.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                    .CommandType = CommandType.Text
                End With
                daconsulta.SelectCommand = cmdconsulta
                daconsulta.Fill(dsconsulta)


                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                    obj = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    obj2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")
                    'plantilla1 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    'plantilla2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")


                    If Not IsDBNull(obj) Then
                        B = DirectCast(obj, Byte())

                        fp_plantilla = DirectCast(obj, Byte())
                        fp_plantilla2 = DirectCast(obj2, Byte())


                        Using ms As New IO.MemoryStream(B)

                            '   pictureBox2.Image = Image.FromStream(ms)



                        End Using

                    End If

                Next


                Btnmarcar_Click(sender, e)

            Else

                '  MsgBox("Empleado no ha sido ingresado, favor verificar", MsgBoxStyle.Critical)

                Me.BackColor = Color.Red
                Formmenu.BackColor = Color.Red
                Formmenu.btnIngresar.BackColor = Color.Red
                Formmenu.btnmarcar.BackColor = Color.Red
                StatusBar.Text = "Empleado no ha sido ingresado, favor verificar"




            End If


        End If

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        encontrado = False

        StatusBar.Text = ""

        conta = conta + 1
        Label1.Text = conta

        txtempleados.Text = txtempleados.Text & Button6.Text

        '  MsgBox("jaja" & conta)

        If conta >= 4 Then

            'Btnmarcar_Click(sender, e)


            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
            Button8.Enabled = False
            Button9.Enabled = False
            Button10.Enabled = False
            '   Button12.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                nombres = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next





            cmdconsulta_documentos = New SqlClient.SqlCommand
            daconsulta_documentos = New SqlClient.SqlDataAdapter
            dsconsulta_documentos = New DataSet
            With cmdconsulta_documentos
                .Connection = conexion
                .CommandText = "SELECT EMPLEADO FROM ASISTENCIA.DBO.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta_documentos.SelectCommand = cmdconsulta_documentos
            daconsulta_documentos.Fill(dsconsulta_documentos)


            For i = 0 To dsconsulta_documentos.Tables(0).Rows.Count - 1
                encontrado = True
                empleado = dsconsulta_documentos.Tables(0).Rows(i).Item("EMPLEADO")
            Next



            If encontrado = True Then


                '----------------------------------------------------------------------------------------------
                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT IMAGEN_1,TEMPLATE_1,TEMPLATE_2 FROM ASISTENCIA.dbo.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                    .CommandType = CommandType.Text
                End With
                daconsulta.SelectCommand = cmdconsulta
                daconsulta.Fill(dsconsulta)


                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                    obj = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    obj2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")
                    'plantilla1 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    'plantilla2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")


                    If Not IsDBNull(obj) Then
                        B = DirectCast(obj, Byte())

                        fp_plantilla = DirectCast(obj, Byte())
                        fp_plantilla2 = DirectCast(obj2, Byte())


                        Using ms As New IO.MemoryStream(B)

                            '   pictureBox2.Image = Image.FromStream(ms)



                        End Using

                    End If

                Next


                Btnmarcar_Click(sender, e)

            Else

                '  MsgBox("Empleado no ha sido ingresado, favor verificar", MsgBoxStyle.Critical)

                Me.BackColor = Color.Red
                Formmenu.BackColor = Color.Red
                Formmenu.btnIngresar.BackColor = Color.Red
                Formmenu.btnmarcar.BackColor = Color.Red
                StatusBar.Text = "Empleado no ha sido ingresado, favor verificar"




            End If


        End If

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        encontrado = False

        StatusBar.Text = ""

        conta = conta + 1
        Label1.Text = conta

        txtempleados.Text = txtempleados.Text & Button7.Text

        '  MsgBox("jaja" & conta)

        If conta >= 4 Then

            'Btnmarcar_Click(sender, e)


            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
            Button8.Enabled = False
            Button9.Enabled = False
            Button10.Enabled = False
            '   Button12.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                nombres = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next





            cmdconsulta_documentos = New SqlClient.SqlCommand
            daconsulta_documentos = New SqlClient.SqlDataAdapter
            dsconsulta_documentos = New DataSet
            With cmdconsulta_documentos
                .Connection = conexion
                .CommandText = "SELECT EMPLEADO FROM ASISTENCIA.DBO.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta_documentos.SelectCommand = cmdconsulta_documentos
            daconsulta_documentos.Fill(dsconsulta_documentos)


            For i = 0 To dsconsulta_documentos.Tables(0).Rows.Count - 1
                encontrado = True
                empleado = dsconsulta_documentos.Tables(0).Rows(i).Item("EMPLEADO")
            Next



            If encontrado = True Then


                '----------------------------------------------------------------------------------------------
                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT IMAGEN_1,TEMPLATE_1,TEMPLATE_2 FROM ASISTENCIA.dbo.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                    .CommandType = CommandType.Text
                End With
                daconsulta.SelectCommand = cmdconsulta
                daconsulta.Fill(dsconsulta)


                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                    obj = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    obj2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")
                    'plantilla1 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    'plantilla2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")


                    If Not IsDBNull(obj) Then
                        B = DirectCast(obj, Byte())

                        fp_plantilla = DirectCast(obj, Byte())
                        fp_plantilla2 = DirectCast(obj2, Byte())


                        Using ms As New IO.MemoryStream(B)

                            '   pictureBox2.Image = Image.FromStream(ms)



                        End Using

                    End If

                Next


                Btnmarcar_Click(sender, e)

            Else

                '  MsgBox("Empleado no ha sido ingresado, favor verificar", MsgBoxStyle.Critical)

                Me.BackColor = Color.Red
                Formmenu.BackColor = Color.Red
                Formmenu.btnIngresar.BackColor = Color.Red
                Formmenu.btnmarcar.BackColor = Color.Red
                StatusBar.Text = "Empleado no ha sido ingresado, favor verificar"




            End If


        End If

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click

        encontrado = False

        StatusBar.Text = ""

        conta = conta + 1
        Label1.Text = conta

        txtempleados.Text = txtempleados.Text & Button8.Text

        '  MsgBox("jaja" & conta)

        If conta >= 4 Then

            'Btnmarcar_Click(sender, e)


            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
            Button8.Enabled = False
            Button9.Enabled = False
            Button10.Enabled = False
            '   Button12.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                nombres = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next





            cmdconsulta_documentos = New SqlClient.SqlCommand
            daconsulta_documentos = New SqlClient.SqlDataAdapter
            dsconsulta_documentos = New DataSet
            With cmdconsulta_documentos
                .Connection = conexion
                .CommandText = "SELECT EMPLEADO FROM ASISTENCIA.DBO.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta_documentos.SelectCommand = cmdconsulta_documentos
            daconsulta_documentos.Fill(dsconsulta_documentos)


            For i = 0 To dsconsulta_documentos.Tables(0).Rows.Count - 1
                encontrado = True
                empleado = dsconsulta_documentos.Tables(0).Rows(i).Item("EMPLEADO")
            Next



            If encontrado = True Then


                '----------------------------------------------------------------------------------------------
                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT IMAGEN_1,TEMPLATE_1,TEMPLATE_2 FROM ASISTENCIA.dbo.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                    .CommandType = CommandType.Text
                End With
                daconsulta.SelectCommand = cmdconsulta
                daconsulta.Fill(dsconsulta)


                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                    obj = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    obj2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")
                    'plantilla1 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    'plantilla2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")


                    If Not IsDBNull(obj) Then
                        B = DirectCast(obj, Byte())

                        fp_plantilla = DirectCast(obj, Byte())
                        fp_plantilla2 = DirectCast(obj2, Byte())


                        Using ms As New IO.MemoryStream(B)

                            '   pictureBox2.Image = Image.FromStream(ms)



                        End Using

                    End If

                Next


                Btnmarcar_Click(sender, e)

            Else

                '  MsgBox("Empleado no ha sido ingresado, favor verificar", MsgBoxStyle.Critical)

                Me.BackColor = Color.Red
                Formmenu.BackColor = Color.Red
                Formmenu.btnIngresar.BackColor = Color.Red
                Formmenu.btnmarcar.BackColor = Color.Red
                StatusBar.Text = "Empleado no ha sido ingresado, favor verificar"




            End If


        End If

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click

        encontrado = False

        StatusBar.Text = ""

        conta = conta + 1
        Label1.Text = conta

        txtempleados.Text = txtempleados.Text & Button9.Text

        '  MsgBox("jaja" & conta)

        If conta >= 4 Then

            'Btnmarcar_Click(sender, e)


            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
            Button8.Enabled = False
            Button9.Enabled = False
            Button10.Enabled = False
            '   Button12.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                nombres = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next





            cmdconsulta_documentos = New SqlClient.SqlCommand
            daconsulta_documentos = New SqlClient.SqlDataAdapter
            dsconsulta_documentos = New DataSet
            With cmdconsulta_documentos
                .Connection = conexion
                .CommandText = "SELECT EMPLEADO FROM ASISTENCIA.DBO.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta_documentos.SelectCommand = cmdconsulta_documentos
            daconsulta_documentos.Fill(dsconsulta_documentos)


            For i = 0 To dsconsulta_documentos.Tables(0).Rows.Count - 1
                encontrado = True
                empleado = dsconsulta_documentos.Tables(0).Rows(i).Item("EMPLEADO")
            Next



            If encontrado = True Then


                '----------------------------------------------------------------------------------------------
                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT IMAGEN_1,TEMPLATE_1,TEMPLATE_2 FROM ASISTENCIA.dbo.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                    .CommandType = CommandType.Text
                End With
                daconsulta.SelectCommand = cmdconsulta
                daconsulta.Fill(dsconsulta)


                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                    obj = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    obj2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")
                    'plantilla1 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    'plantilla2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")


                    If Not IsDBNull(obj) Then
                        B = DirectCast(obj, Byte())

                        fp_plantilla = DirectCast(obj, Byte())
                        fp_plantilla2 = DirectCast(obj2, Byte())


                        Using ms As New IO.MemoryStream(B)

                            '   pictureBox2.Image = Image.FromStream(ms)



                        End Using

                    End If

                Next


                Btnmarcar_Click(sender, e)

            Else

                '  MsgBox("Empleado no ha sido ingresado, favor verificar", MsgBoxStyle.Critical)

                Me.BackColor = Color.Red
                Formmenu.BackColor = Color.Red
                Formmenu.btnIngresar.BackColor = Color.Red
                Formmenu.btnmarcar.BackColor = Color.Red
                StatusBar.Text = "Empleado no ha sido ingresado, favor verificar"




            End If


        End If

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click

        encontrado = False

        StatusBar.Text = ""

        conta = conta + 1
        Label1.Text = conta

        txtempleados.Text = txtempleados.Text & Button10.Text

        '  MsgBox("jaja" & conta)

        If conta >= 4 Then

            'Btnmarcar_Click(sender, e)


            Button1.Enabled = False
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button5.Enabled = False
            Button6.Enabled = False
            Button7.Enabled = False
            Button8.Enabled = False
            Button9.Enabled = False
            Button10.Enabled = False
            '   Button12.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                nombres = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next





            cmdconsulta_documentos = New SqlClient.SqlCommand
            daconsulta_documentos = New SqlClient.SqlDataAdapter
            dsconsulta_documentos = New DataSet
            With cmdconsulta_documentos
                .Connection = conexion
                .CommandText = "SELECT EMPLEADO FROM ASISTENCIA.DBO.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta_documentos.SelectCommand = cmdconsulta_documentos
            daconsulta_documentos.Fill(dsconsulta_documentos)


            For i = 0 To dsconsulta_documentos.Tables(0).Rows.Count - 1
                encontrado = True
                empleado = dsconsulta_documentos.Tables(0).Rows(i).Item("EMPLEADO")
            Next



            If encontrado = True Then


                '----------------------------------------------------------------------------------------------
                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT IMAGEN_1,TEMPLATE_1,TEMPLATE_2 FROM ASISTENCIA.dbo.EMPLEADO_HUELLA WITH(NOLOCK) WHERE EMPLEADO='" & txtempleados.Text & "' "
                    .CommandType = CommandType.Text
                End With
                daconsulta.SelectCommand = cmdconsulta
                daconsulta.Fill(dsconsulta)


                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                    obj = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    obj2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")
                    'plantilla1 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_1")
                    'plantilla2 = dsconsulta.Tables(0).Rows(z).Item("TEMPLATE_2")


                    If Not IsDBNull(obj) Then
                        B = DirectCast(obj, Byte())

                        fp_plantilla = DirectCast(obj, Byte())
                        fp_plantilla2 = DirectCast(obj2, Byte())


                        Using ms As New IO.MemoryStream(B)

                            '   pictureBox2.Image = Image.FromStream(ms)



                        End Using

                    End If

                Next


                Btnmarcar_Click(sender, e)

            Else

                '  MsgBox("Empleado no ha sido ingresado, favor verificar", MsgBoxStyle.Critical)

                Me.BackColor = Color.Red
                Formmenu.BackColor = Color.Red
                Formmenu.btnIngresar.BackColor = Color.Red
                Formmenu.btnmarcar.BackColor = Color.Red
                StatusBar.Text = "Empleado no ha sido ingresado, favor verificar"




            End If


        End If


    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        Me.BackColor = Color.Gray
        Formmenu.BackColor = Color.Gray
        Formmenu.btnIngresar.BackColor = Color.Gray
        Formmenu.btnmarcar.BackColor = Color.Gray



        txtempleado.Text = ""
        txtnombre.Text = ""
        Button1.Enabled = True
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True
        Button6.Enabled = True
        Button7.Enabled = True
        Button8.Enabled = True
        Button9.Enabled = True
        Button10.Enabled = True
        txtempleados.Text = ""


        pictureBox1.Image = Nothing
        '    PictureBox2.Image = Nothing
        conta = 0
        Label1.Text = conta



        m_LedOn = False
        comboBoxSecuLevel_R.SelectedIndex = 4
        comboBoxSecuLevel_V.SelectedIndex = 3
        EnableButtons(False)

        m_FPM = New SGFingerPrintManager
        EnumerateBtn_Click(sender, e)

        OpenDeviceBtn_Click(sender, e)

        StatusBar.Text = ""

        Me.BackColor = Color.Gray

    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click

        Me.Close()
        Formmenu.BackColor = Color.Gray
        Formmenu.btnIngresar.BackColor = Color.Gray
        Formmenu.btnmarcar.BackColor = Color.Gray


    End Sub
End Class