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
Imports iTextSharp.text
Imports iTextSharp.text.pdf




Public Class Frmconstancia
    Public dias_pendi As Integer
    Public imagen As iTextSharp.text.Image
    Public imagen_firma As iTextSharp.text.Image

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
    Public IDENTIFICACION, PUESTO As String
    Public FECHA_INGRESO, BRUTO_LETRAS, NETO_LETRAS As String
    Public SALARIO_REFERENCIA, SALARIO_NETO As Decimal
    Public CAJA As Decimal
    Public RENTA, PENSION_ALIMENTICIA, EMBARGOS As Decimal




    'Public cmdconsulta, cmdconsulta_documentos As SqlClient.SqlCommand
    'Public transaccion, transaccion_documentos As SqlClient.SqlTransaction
    'Public cmdincluir, cmdincluir_documentos As SqlClient.SqlCommand
    'Public iregincluir, iregincluird, cod, t, y As Integer
    'Public contrarecibo, strconexionex, parametros, asunto, cuerpo, CLIENTE, FECHA, DOCUMENTO, TIPO, MONEDA, CREDITO, NOMBRE As String
    'Public MONTO, SALDO As Decimal
    'Public conexion As SqlClient.SqlConnection
    'Public conexionbi As SqlClient.SqlConnection
    'Public daconsulta, daconsulta_documentos As SqlClient.SqlDataAdapter
    'Public dsconsulta, dsconsulta_documentos As DataSet
    'Public contador As Integer = 130
    'Public NombreArchivo As String
    'Public imagen As iTextSharp.text.Image 'declaración de imagen
    'Public EMPLEADO, ESTADO, PUESTO, IDENTIFICACION, UBICACION, SITUACION As String



    Private Sub Frmconstancia_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try


            Formmenu.btnactualiza.Enabled = False
            '   BtnSolicitar.Enabled = True
            '------------------------------------------abro el dispositivo------------------------------------------------------------------
            System.Threading.Thread.Sleep(3000)
            Dim bandera_error As Boolean = False



            Dim iError As Int32

            ' Me.BackColor = Color.Gray


            m_LedOn = False
       

            comboBoxSecuLevel_R.SelectedIndex = 7
            comboBoxSecuLevel_V.SelectedIndex = 6


            m_FPM = New SGFingerPrintManager
            EnumerateBtn_Click(sender, e)

            OpenDeviceBtn_Click(sender, e)


            emplea2_encontrado = "NO"



            '  MsgBox("CONEXION")

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
                    Formmenu.btnactualiza.Enabled = False
                Else
                    DisplayError("CreateTemplate", iError)
                    bandera_error = True
                    m_FPM.CloseDevice()
                    Formmenu.btnactualiza.Enabled = False
                    Me.Close()
                End If
            Else
                DisplayError("GetImage", iError)
                bandera_error = True
                m_FPM.CloseDevice()
                Formmenu.btnactualiza.Enabled = False
                Me.Close()
            End If



            'Dim img_qlty As Int32
            'ReDim fp_image(m_ImageWidth * m_ImageHeight)

            'iError = m_FPM.GetImage(fp_image)

            'If iError = SGFPMError.ERROR_NONE Then
            '    DrawImage(fp_image, pictureBox1)
            '    m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, img_qlty)
            '    ' progressBar_R1.Value = img_qlty
            '    iError = m_FPM.CreateTemplate(fp_image, m_RegMin1)

            '    If (iError = SGFPMError.ERROR_NONE) Then
            '        StatusBar.Text = "First image is captured"
            '        m_FPM.CloseDevice()
            '    Else
            '        DisplayError("CreateTemplate", iError)
            '        bandera_error = True
            '        m_FPM.CloseDevice()
            '        MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
            '        m_FPM.CloseDevice()
            '        Formmenu.btnactualiza.Enabled = True
            '        '   Me.Close()


            '    End If
            'Else
            '    DisplayError("GetImage", iError)
            '    bandera_error = True
            '    MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
            '    Formmenu.btnactualiza.Enabled = True
            '    m_FPM.CloseDevice()
            '    Me.Close()
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


                For CONTADOR = 0 To dsconsulta.Tables(0).Rows.Count - 1

                    If encontrado = False Then


                        obj = dsconsulta.Tables(0).Rows(CONTADOR).Item("TEMPLATE_1")
                        obj2 = dsconsulta.Tables(0).Rows(CONTADOR).Item("TEMPLATE_2")
                        emplea2 = dsconsulta.Tables(0).Rows(CONTADOR).Item("EMPLEADO")

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
                        .CommandText = "SELECT EMPLEADO,NOMBRE,E_MAIL FROM EXACTUS.DIGEMA.EMPLEADO WHERE EMPLEADO='" & emplea2_encontrado & "'"



                        .CommandType = CommandType.Text
                    End With
                    daconsulta.SelectCommand = cmdconsulta
                    daconsulta.Fill(dsconsulta)


                    For z = 0 To dsconsulta.Tables(0).Rows.Count - 1


                        txtempleado.Text = dsconsulta.Tables(0).Rows(z).Item("EMPLEADO")
                        txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                        'dias_pendi = dsconsulta.Tables(0).Rows(INCRE).Item("VACS_PENDIENTES")
                        '   lblvacaciones_pendientes.Text = "Vacaciones pendientes: " & Format(dsconsulta.Tables(0).Rows(Z).Item("VACS_PENDIENTES"), "0.00")
                        txtcorreo.Text = dsconsulta.Tables(0).Rows(z).Item("E_MAIL")

                    Next

                End If

            End If

            If bandera_error = True Then


                MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")

                m_FPM.CloseDevice()
                Me.Close()
                Formmenu.btnactualiza.Enabled = True


            End If




        Catch ex As Exception
            MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
            m_FPM.CloseDevice()
            Me.Close()
            Formmenu.btnactualiza.Enabled = True
            ' Show the stack trace, which is a list of methods 
            ' that are currently executing.
            '  MessageBox.Show("Stack Trace: " & vbCrLf & ex.StackTrace)
        Finally
            m_FPM.CloseDevice()
            ' This line executes whether or not the exception occurs.
            '  MessageBox.Show("in Finally block")
        End Try

        btnenviar.Focus()




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


    Private Sub atras_Click(sender As Object, e As EventArgs) Handles atras.Click
        txtcorreo.Clear()
        txtcorreo.Focus()


    End Sub

    Private Sub btnQ_Click(sender As Object, e As EventArgs) Handles btnQ.Click
        txtcorreo.Text = txtcorreo.Text & "Q"
    End Sub

    Private Sub btnW_Click(sender As Object, e As EventArgs) Handles btnW.Click
        txtcorreo.Text = txtcorreo.Text & "W"

    End Sub

    Private Sub btnE_Click(sender As Object, e As EventArgs) Handles btnE.Click
        txtcorreo.Text = txtcorreo.Text & "E"
    End Sub

    Private Sub btnR_Click(sender As Object, e As EventArgs) Handles btnR.Click
        txtcorreo.Text = txtcorreo.Text & "R"
    End Sub

    Private Sub btnT_Click(sender As Object, e As EventArgs) Handles btnT.Click
        txtcorreo.Text = txtcorreo.Text & "T"
    End Sub

    Private Sub btnY_Click(sender As Object, e As EventArgs) Handles btnY.Click
        txtcorreo.Text = txtcorreo.Text & "Y"
    End Sub

    Private Sub btnU_Click(sender As Object, e As EventArgs) Handles btnU.Click
        txtcorreo.Text = txtcorreo.Text & "U"
    End Sub

    Private Sub btnI_Click(sender As Object, e As EventArgs) Handles btnI.Click
        txtcorreo.Text = txtcorreo.Text & "I"
    End Sub

    Private Sub btnO_Click(sender As Object, e As EventArgs) Handles btnO.Click
        txtcorreo.Text = txtcorreo.Text & "O"
    End Sub

    Private Sub btnP_Click(sender As Object, e As EventArgs) Handles btnP.Click
        txtcorreo.Text = txtcorreo.Text & "P"
    End Sub

    Private Sub arroba_Click(sender As Object, e As EventArgs) Handles arroba.Click
        txtcorreo.Text = txtcorreo.Text & "@"
    End Sub

    Private Sub btnA_Click(sender As Object, e As EventArgs) Handles btnA.Click
        txtcorreo.Text = txtcorreo.Text & "A"
    End Sub

    Private Sub btnS_Click(sender As Object, e As EventArgs) Handles btnS.Click
        txtcorreo.Text = txtcorreo.Text & "S"
    End Sub

    Private Sub btnD_Click(sender As Object, e As EventArgs) Handles btnD.Click
        txtcorreo.Text = txtcorreo.Text & "D"
    End Sub

    Private Sub btnF_Click(sender As Object, e As EventArgs) Handles btnF.Click
        txtcorreo.Text = txtcorreo.Text & "F"
    End Sub

    Private Sub btnG_Click(sender As Object, e As EventArgs) Handles btnG.Click
        txtcorreo.Text = txtcorreo.Text & "G"

    End Sub

    Private Sub btnH_Click(sender As Object, e As EventArgs) Handles btnH.Click
        txtcorreo.Text = txtcorreo.Text & "H"
    End Sub

    Private Sub btnJ_Click(sender As Object, e As EventArgs) Handles btnJ.Click
        txtcorreo.Text = txtcorreo.Text & "J"
    End Sub

    Private Sub btnK_Click(sender As Object, e As EventArgs) Handles btnK.Click
        txtcorreo.Text = txtcorreo.Text & "K"
    End Sub

    Private Sub btnL_Click(sender As Object, e As EventArgs) Handles btnL.Click
        txtcorreo.Text = txtcorreo.Text & "L"
    End Sub

    Private Sub rayaarriba_Click(sender As Object, e As EventArgs) Handles rayaarriba.Click
        txtcorreo.Text = txtcorreo.Text & "-"
    End Sub

    Private Sub btnZ_Click(sender As Object, e As EventArgs) Handles btnZ.Click
        txtcorreo.Text = txtcorreo.Text & "Z"
    End Sub

    Private Sub btnX_Click(sender As Object, e As EventArgs) Handles btnX.Click
        txtcorreo.Text = txtcorreo.Text & "X"
    End Sub

    Private Sub btnC_Click(sender As Object, e As EventArgs) Handles btnC.Click
        txtcorreo.Text = txtcorreo.Text & "C"
    End Sub

    Private Sub btnV_Click(sender As Object, e As EventArgs) Handles btnV.Click
        txtcorreo.Text = txtcorreo.Text & "V"
    End Sub

    Private Sub btnB_Click(sender As Object, e As EventArgs) Handles btnB.Click
        txtcorreo.Text = txtcorreo.Text & "B"
    End Sub

    Private Sub btnN_Click(sender As Object, e As EventArgs) Handles btnN.Click
        txtcorreo.Text = txtcorreo.Text & "N"
    End Sub

    Private Sub btnM_Click(sender As Object, e As EventArgs) Handles btnM.Click
        txtcorreo.Text = txtcorreo.Text & "M"
    End Sub

    Private Sub punto_Click(sender As Object, e As EventArgs) Handles punto.Click
        txtcorreo.Text = txtcorreo.Text & "."
    End Sub

    Private Sub rayaabajo_Click(sender As Object, e As EventArgs) Handles rayaabajo.Click
        txtcorreo.Text = txtcorreo.Text & "_"
    End Sub

    Private Sub siete_Click(sender As Object, e As EventArgs) Handles siete.Click
        txtcorreo.Text = txtcorreo.Text & "7"
    End Sub

    Private Sub ocho_Click(sender As Object, e As EventArgs) Handles ocho.Click
        txtcorreo.Text = txtcorreo.Text & "8"
    End Sub

    Private Sub nueve_Click(sender As Object, e As EventArgs) Handles nueve.Click
        txtcorreo.Text = txtcorreo.Text & "9"
    End Sub

    Private Sub cuatro_Click(sender As Object, e As EventArgs) Handles cuatro.Click
        txtcorreo.Text = txtcorreo.Text & "4"
    End Sub

    Private Sub cinco_Click(sender As Object, e As EventArgs) Handles cinco.Click
        txtcorreo.Text = txtcorreo.Text & "5"
    End Sub

    Private Sub seis_Click(sender As Object, e As EventArgs) Handles seis.Click
        txtcorreo.Text = txtcorreo.Text & "6"
    End Sub

    Private Sub uno_Click(sender As Object, e As EventArgs) Handles uno.Click
        txtcorreo.Text = txtcorreo.Text & "1"
    End Sub

    Private Sub dos_Click(sender As Object, e As EventArgs) Handles dos.Click
        txtcorreo.Text = txtcorreo.Text & "2"
    End Sub

    Private Sub tres_Click(sender As Object, e As EventArgs) Handles tres.Click
        txtcorreo.Text = txtcorreo.Text & "3"
    End Sub

    Private Sub btncerrar_Click(sender As Object, e As EventArgs) Handles btncerrar.Click
        Me.Close()
        Formmenu.btnactualiza.Enabled = True

    End Sub

    Private Sub btnenviar_Click(sender As Object, e As EventArgs) Handles btnenviar.Click


        Dim fecha_de_hoy As String = DateTime.Now.ToLongDateString()



        Dim oDoc As New iTextSharp.text.Document(PageSize.A4, 0, 0, 0, 0)
        Dim pdfw As iTextSharp.text.pdf.PdfWriter
        Dim cb As PdfContentByte
        Dim fuente As iTextSharp.text.pdf.BaseFont
        NombreArchivo = "C:\AMONESTACIONES\constancia_salarial.pdf"
        '        Dim NombreArchivo As String = "C:\Users\lsegura\Desktop\Amador\Envio_PDF\ejemplo.pdf"
        pdfw = PdfWriter.GetInstance(oDoc, New FileStream(NombreArchivo,
               FileMode.Create, FileAccess.Write, FileShare.None))
        oDoc.Open()
        cb = pdfw.DirectContent



        Try

            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT C2.EMPLEADO,C2.BRUTO_LETRAS,C2.NOMBRE,C2.IDENTIFICACION,C2.FECHA_INGRESO,C2.PUESTO,C2.SALARIO_REFERENCIA,C2.CAJA,C2.RENTA,C2.PENSION_ALIMENTICIA,C2.EMBARGOS,(C2.SALARIO_REFERENCIA)-(C2.CAJA + C2.RENTA + C2.PENSION_ALIMENTICIA + C2.EMBARGOS)AS TOTAL_NETO,BI.DBO.CantidadConLetras((C2.SALARIO_REFERENCIA)-(C2.CAJA + C2.RENTA + C2.PENSION_ALIMENTICIA + C2.EMBARGOS))AS NETO_LETRAS " +
                                "FROM (SELECT C1.EMPLEADO,C1.BRUTO_LETRAS,C1.NOMBRE,C1.IDENTIFICACION,C1.FECHA_INGRESO,C1.PUESTO,C1.SALARIO_REFERENCIA,C1.CAJA " +
                                ",CASE WHEN C1.RENTA > 0 THEN C1.RENTA - C1.CONYUGUE - C1.HIJOS ELSE 0 END AS RENTA " +
                                ",ISNULL((SELECT MONTO * 2 from DIGEMA.EMPLEADO_CONCEPTO where CONCEPTO = 'DQ14' and ACTIVO = 'S' and MONTO > 0 AND EMPLEADO=C1.EMPLEADO),0) AS PENSION_ALIMENTICIA " +
                                ",ISNULL((SELECT (SELECT TOP 1 (TOTAL) FROM EXACTUS.DIGEMA.EMPLEADO_CONC_NOMI N WHERE N.EMPLEADO=EC.EMPLEADO  and N.TOTAL > 0 and N.CONCEPTO='DQ13' ORDER BY N.NUMERO_NOMINA DESC) FROM EXACTUS.DIGEMA.EMPLEADO_CONCEPTO EC  WHERE EC.CONCEPTO = 'DQ13' and EC.ACTIVO = 'S' AND EC.EMPLEADO=C1.EMPLEADO),0) AS EMBARGOS " +
                                "FROM (SELECT E.EMPLEADO,BI.DBO.CantidadConLetras(SALARIO_REFERENCIA)AS BRUTO_LETRAS,E.NOMBRE,E.IDENTIFICACION,FORMAT(E.FECHA_INGRESO, N'D', 'es-la')as FECHA_INGRESO,P.DESCRIPCION AS PUESTO,E.SALARIO_REFERENCIA " +
                                ",10.5/ 100 * E.SALARIO_REFERENCIA AS CAJA " +
                                ",CASE WHEN E.SALARIO_REFERENCIA <= 842000 THEN 0 " +
                                      "WHEN E.SALARIO_REFERENCIA > 842000 AND E.SALARIO_REFERENCIA <= 1236000 THEN " +
                                           "(e.SALARIO_REFERENCIA - 842000) * 0.1 " +
                                      "WHEN E.SALARIO_REFERENCIA >= 1236000 AND E.SALARIO_REFERENCIA <= 2169000 THEN " +
                                           "(e.SALARIO_REFERENCIA - 1236000) * 0.15 + 39400 " +
                                      "WHEN E.SALARIO_REFERENCIA >= 2169000 AND E.SALARIO_REFERENCIA <= 4337000 THEN " +
                                           "(e.SALARIO_REFERENCIA - 2169000) * 0.2 + 139950 + 39400 " +
                                      "WHEN  E.SALARIO_REFERENCIA > 4337000 THEN " +
                                           "(e.SALARIO_REFERENCIA - 4337000) * 0.25 + 433600 + 139950 + 39400 " +
                                "ELSE 1 END AS RENTA " +
                                ",CASE WHEN E.ESTADO_CIVIL='C' THEN 2370 ELSE 0 END AS CONYUGUE " +
                                ",CASE WHEN E.DEPENDIENTES > 0 THEN E.DEPENDIENTES * 1570 ELSE 0 END AS HIJOS " +
                                "FROM EXACTUS.DIGEMA.EMPLEADO E WITH(NOLOCK) " +
                                "INNER JOIN EXACTUS.DIGEMA.PUESTO P WITH(NOLOCK) ON P.PUESTO=E.PUESTO)C1)C2 " +
                                "WHERE C2.EMPLEADO='" & txtempleado.Text & "'"






                '"SELECT C2.EMPLEADO,C2.BRUTO_LETRAS,C2.NOMBRE,C2.IDENTIFICACION,C2.FECHA_INGRESO,C2.PUESTO,C2.SALARIO_REFERENCIA,C2.CAJA,C2.RENTA,C2.PENSION_ALIMENTICIA,C2.EMBARGOS,(C2.SALARIO_REFERENCIA)-(C2.CAJA + C2.RENTA + C2.PENSION_ALIMENTICIA + C2.EMBARGOS)AS TOTAL_NETO,BI.DBO.CantidadConLetras((C2.SALARIO_REFERENCIA)-(C2.CAJA + C2.RENTA + C2.PENSION_ALIMENTICIA + C2.EMBARGOS))AS NETO_LETRAS " +
                '                                "FROM (SELECT C1.EMPLEADO,C1.BRUTO_LETRAS,C1.NOMBRE,C1.IDENTIFICACION,C1.FECHA_INGRESO,C1.PUESTO,C1.SALARIO_REFERENCIA,C1.CAJA " +
                '                                ",CASE WHEN C1.RENTA > 0 THEN C1.RENTA - C1.CONYUGUE - C1.HIJOS ELSE 0 END AS RENTA " +
                '                                ",ISNULL((SELECT MONTO * 2 from DIGEMA.EMPLEADO_CONCEPTO where CONCEPTO = 'DQ14' and ACTIVO = 'S' and MONTO > 0 AND EMPLEADO=C1.EMPLEADO),0) AS PENSION_ALIMENTICIA " +
                '                                ",ISNULL((SELECT (SELECT TOP 1 (TOTAL) FROM EXACTUS.DIGEMA.EMPLEADO_CONC_NOMI N WHERE N.EMPLEADO=EC.EMPLEADO  and N.TOTAL > 0 and N.CONCEPTO='DQ13' ORDER BY N.NUMERO_NOMINA DESC) FROM EXACTUS.DIGEMA.EMPLEADO_CONCEPTO EC  WHERE EC.CONCEPTO = 'DQ13' and EC.ACTIVO = 'S' AND EC.EMPLEADO=C1.EMPLEADO),0) AS EMBARGOS " +
                '                                "FROM (SELECT E.EMPLEADO,BI.DBO.CantidadConLetras(SALARIO_REFERENCIA)AS BRUTO_LETRAS,E.NOMBRE,E.IDENTIFICACION,FORMAT(E.FECHA_INGRESO, N'D', 'es-la')as FECHA_INGRESO,P.DESCRIPCION AS PUESTO,E.SALARIO_REFERENCIA " +
                '                                ",10.5/ 100 * E.SALARIO_REFERENCIA AS CAJA " +
                '                                ",CASE WHEN E.SALARIO_REFERENCIA <= 840000 THEN 0 " +
                '                                 "     WHEN E.SALARIO_REFERENCIA > 840000 AND E.SALARIO_REFERENCIA <= 1233000 THEN " +
                '                                 "          (e.SALARIO_REFERENCIA - 840000) * 0.1 " +
                '                                 "	  WHEN E.SALARIO_REFERENCIA >= 1233000 AND E.SALARIO_REFERENCIA <= 2163000 THEN " +
                '                                 "          (e.SALARIO_REFERENCIA - 1233000) * 0.15 + 39300  " +
                '                                 "	  WHEN E.SALARIO_REFERENCIA >= 2163000 AND E.SALARIO_REFERENCIA <= 4325000 THEN " +
                '                                 "          (e.SALARIO_REFERENCIA - 2163000) * 0.2 + 139500 + 39300  " +
                '                                 "	  WHEN  E.SALARIO_REFERENCIA > 4325000 THEN " +
                '                                 "          (e.SALARIO_REFERENCIA - 4325000) * 0.25 + 432400 + 139500 + 39300 " +
                '                                "ELSE 1 END AS RENTA " +
                '                                ",CASE WHEN E.ESTADO_CIVIL='C' THEN 2360 ELSE 0 END AS CONYUGUE " +
                '                                ",CASE WHEN E.DEPENDIENTES > 0 THEN E.DEPENDIENTES * 1570 ELSE 0 END AS HIJOS " +
                '                                "FROM EXACTUS.DIGEMA.EMPLEADO E WITH(NOLOCK) " +
                '                                "INNER JOIN EXACTUS.DIGEMA.PUESTO P WITH(NOLOCK) ON P.PUESTO=E.PUESTO)C1)C2 " +
                '                                "WHERE C2.EMPLEADO= '" & txtempleado.Text & "'"




                '    "WHERE C1.EMPLEADO= '" & txtempleado.Text & "'"



                '"SELECT SALARIO_REFERENCIA FROM EXACTUS.DIGEMA.EMPLEADO WHERE EMPLEADO= '" & txtempleado.Text & "'"


                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)




            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
                empleado = dsconsulta.Tables(0).Rows(z).Item("EMPLEADO")
                NOMBRE = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                IDENTIFICACION = dsconsulta.Tables(0).Rows(z).Item("IDENTIFICACION")
                PUESTO = dsconsulta.Tables(0).Rows(z).Item("PUESTO")
                SALARIO_REFERENCIA = dsconsulta.Tables(0).Rows(z).Item("SALARIO_REFERENCIA")
                CAJA = dsconsulta.Tables(0).Rows(z).Item("CAJA")
                RENTA = dsconsulta.Tables(0).Rows(z).Item("RENTA")
                PENSION_ALIMENTICIA = dsconsulta.Tables(0).Rows(z).Item("PENSION_ALIMENTICIA")
                FECHA_INGRESO = dsconsulta.Tables(0).Rows(z).Item("FECHA_INGRESO")
                BRUTO_LETRAS = dsconsulta.Tables(0).Rows(z).Item("BRUTO_LETRAS")
                NETO_LETRAS = dsconsulta.Tables(0).Rows(z).Item("NETO_LETRAS")

                EMBARGOS = dsconsulta.Tables(0).Rows(z).Item("EMBARGOS")





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
                '  cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, UBICACION, 80, PageSize.A4.Height - 400, 0)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Construplaza S.A.", 80, PageSize.A4.Height - 30, 0)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Cédula Jurídica 3-101-289562", 80, PageSize.A4.Height - 40, 0)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Tel: +506 2588-8888", 80, PageSize.A4.Height - 50, 0)


                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, fecha_de_hoy, 450, PageSize.A4.Height - 20, 0)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "A quien interese", 300, PageSize.A4.Height - 200, 0)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "     La empresa Construplaza S.A,  cédula jurídica número 3-101-289562, hace constar que " & NOMBRE & ",con documento de identidad número  " & IDENTIFICACION & vbCrLf & vbCrLf, 30, PageSize.A4.Height - 300, 0)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "labora para la empresa desde " & FECHA_INGRESO & ", desempeñando el puesto de " & PUESTO & " percibiendo un ingreso mensual bruto de: ", 30, PageSize.A4.Height - 310, 0)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "" & "" & (Format(CDec(Val(dsconsulta.Tables(0).Rows(z).Item("SALARIO_REFERENCIA"))), "##,##0.00")) & " colones (" & BRUTO_LETRAS & "),  y neto de " & (Format(CDec(Val(dsconsulta.Tables(0).Rows(z).Item("TOTAL_NETO"))), "##,##0.00")) & " colones," & "(" & NETO_LETRAS & ")", 30, PageSize.A4.Height - 320, 0)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "desglosado de la siguiente manera:  ", 30, PageSize.A4.Height - 330, 0)
                'cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "" & "(" & NETO_LETRAS & ")," & " desglosado de la siguiente manera:  ", 30, PageSize.A4.Height - 330, 0)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "", 30, PageSize.A4.Height - 340, 0)



                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, " Salario Bruto:  " & (Format(CDec(Val(dsconsulta.Tables(0).Rows(z).Item("SALARIO_REFERENCIA"))), "##,##0.00")) & " colones", 80, PageSize.A4.Height - 400, 0)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, " CCSS:  " & (Format(CDec(Val(CAJA)), "##,##0.00")) & " colones", 80, PageSize.A4.Height - 410, 0)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, " Impuesto de renta:  " & (Format(CDec(Val(RENTA)), "##,##0.00")) & " colones", 80, PageSize.A4.Height - 420, 0)

                If PENSION_ALIMENTICIA > 0 Then

                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, " Pensión Alimenticia:  " & (Format(CDec(Val(dsconsulta.Tables(0).Rows(z).Item("PENSION_ALIMENTICIA"))), "##,##0.00")) & " colones", 80, PageSize.A4.Height - 430, 0)

                End If


                If EMBARGOS > 0 Then

                    cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, " Embargos:  " & (Format(CDec(Val(dsconsulta.Tables(0).Rows(z).Item("EMBARGOS"))), "##,##0.00")) & " colones", 80, PageSize.A4.Height - 440, 0)

                End If

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "________________________________", 80, PageSize.A4.Height - 450, 0)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, " Salario Neto:  " & (Format(CDec(Val(dsconsulta.Tables(0).Rows(z).Item("TOTAL_NETO"))), "##,##0.00")) & " colones", 80, PageSize.A4.Height - 460, 0)


                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, " Se extiende la presente, a solicitud del interesado.  ", 30, PageSize.A4.Height - 480, 0)
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, " Sin otro particular me despido,  ", 30, PageSize.A4.Height - 500, 0)

                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, " Silvia Flores Trejos,  ", 300, PageSize.A4.Height - 610, 0)


                'jpeg


                imagen_firma = iTextSharp.text.Image.GetInstance("C:\Imagenes\firma.png") 'nombre y ruta de la imagen a insertar
                imagen_firma.ScalePercent(46.7) 'escala al tamaño de la imagen
                imagen_firma.SetAbsolutePosition(300, 240) 'posición en la que se inserta. 40 (de izquierda a derecha). 500 (de abajo hacia arriba)
                oDoc.Add(imagen_firma) 'se agrega la imagen al documento


                imagen_firma = iTextSharp.text.Image.GetInstance("C:\Imagenes\sello.png") 'nombre y ruta de la imagen a insertar
                imagen_firma.ScalePercent(46.7) 'escala al tamaño de la imagen
                imagen_firma.SetAbsolutePosition(30, 140) 'posición en la que se inserta. 40 (de izquierda a derecha). 500 (de abajo hacia arriba)
                oDoc.Add(imagen_firma) 'se agrega la imagen al documento


                '  , (Format(CDec(Val( dsconsulta.Tables(0).Rows(z).Item("SALARIO_REFERENCIA"))), "##,##0.00"))


                cb.EndText()
                'Forzamos vaciamiento del buffer.
                pdfw.Flush()


            Next


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


        '-------------------------envio correo--------------------------------------------------------

        asunto = "Envío de Constancia salarial" '& dsconsultasoli.Tables(0).Rows(i).Item("VENDEDOR")

        cuerpo = cuerpo & "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>"

        'Cuerpo = Cuerpo & "<table style='border-collapse:collapse;' width='1024px'>"
        'Cuerpo = Cuerpo & "<tr valign='bottom'>"
        'Cuerpo = Cuerpo & "<td style='width: 1024px; padding: 0px 0px 0px 0px; border:1px; height: 100px; font-family: Calibri; font-size: 11pt; font-weight: 500;' colspan='2'>"
        'Cuerpo = Cuerpo & "<img alt='' src='cid:heder1'  align='top' border='1px' />"
        'Cuerpo = Cuerpo & "</td>"
        'Cuerpo = Cuerpo & "</tr>"
        'Cuerpo = Cuerpo & "<tr valign='top'>"
        'Cuerpo = Cuerpo & "<td style='width: 222px; padding: 0px 0px 0px 0px; border:1px; height: 75px;'><img alt='' src='cid:heder2' align='top' border='1px' />"
        'Cuerpo = Cuerpo & "</td><td style='width: 802px; height: 75px; padding: 0px 0px 0px 0px; border:1px; font-family: Calibri; font-size: 14pt; font-weight: 500;'>"
        'Cuerpo = Cuerpo & "<div style='width: 700px; height: 40px; position: absolute; top: 160px; left: 230px;padding-left: 25px; color: #00539B;'>"

        ' cuerpo = cuerpo & "<br/>Envío de nota de crédito</div>"
        'Cuerpo = Cuerpo & "</td>"
        'Cuerpo = Cuerpo & "</tr>"
        'Cuerpo = Cuerpo & "<tr>"
        'Cuerpo = Cuerpo & "<td style='padding: 10px 20px 10px 20px; width: 1024px;' colspan='2'>"
        'Cuerpo = Cuerpo & "</tr>"
        'Cuerpo = Cuerpo & "<br/>"
        'Cuerpo = Cuerpo & "</table>"

        ' Cuerpo = Cuerpo & "<div style='max-height:200px; overflow:scroll;'>"
        cuerpo = cuerpo & "<table style='width: 100%; border:1px solid white; font-family: Calibri; font-size: 11pt; font-weight: 200; text-transform: capitalize;' cellpadding='0px' cellspacing='0px'>"



        '----------------------------------------------------------------------------------------------


        enviarCorreo(asunto, cuerpo)

        Formmenu.btnactualiza.Enabled = True

        Me.Close()


    End Sub


    Public Sub enviarCorreo(ByVal subject As String, ByVal body As String)
        Try
            Dim StmpSetting As DataTable = StpmDinamico.GetStmp()
            Dim smtpServidor As String = StmpSetting.Rows(0)("smtpServidor").ToString()
            Dim smtpUsuario As String = StmpSetting.Rows(0)("smtpUsuario").ToString()
            Dim smtpPassword As String = StmpSetting.Rows(0)("smtpPassword").ToString()
            Dim adjuntos As System.Net.Mail.Attachment


            '  Dim cuentaDestino As String = CORREO_PRINCIPAL

            Dim cuentaOrigen As String = "notificador@construplaza.com"
            Dim prueba As String

            'Credenciales de autenticación
            Dim credenciales As NetworkCredential = New NetworkCredential(smtpUsuario, smtpPassword)

            'Permite a las aplicaciones enviar mensajes de correo electrónico mediante el protocolo SMTP 
            Dim cliente As SmtpClient = New SmtpClient(smtpServidor)

            'Se descartan las credenciales por defecto
            cliente.UseDefaultCredentials = False

            'Credenciales de autenticación
            cliente.Credentials = credenciales

            'Los mensajes de correo electrónico se envían a un servidor SMTP a través de la red
            cliente.DeliveryMethod = SmtpDeliveryMethod.Network


            'Dirección de destino 
            '  Dim direccionDestino As MailAddress = New MailAddress(cuentaDestino)




            'Dirección de origen
            Dim direccionOrigen As MailAddress = New MailAddress(cuentaOrigen, "Notificador Automatico.")
            Dim direccion_correo As String = txtcorreo.Text

            'Mensaje
            Dim mensaje As MailMessage = New MailMessage()
            mensaje.From = direccionOrigen
            ' mensaje.To.Add(New MailAddress("lsegura@construplaza.com"))
            mensaje.To.Add(New MailAddress(direccion_correo))

            mensaje.Subject = DateTime.Now.ToString() & " " & subject
            prueba = NombreArchivo
            adjuntos = New System.Net.Mail.Attachment(prueba)


            mensaje.Attachments.Add(adjuntos)

            '------------------------------------
            'Creamos la vista para clientes que
            'sólo pueden acceder a texto plano...

            Dim text As String = "Construplaza, Correo automatico." &
                                 "Para poder recibir la alerta automatica solicite al administrador de TI un cliente de correo con capacidad de lectura html."
            Dim plainView As AlternateView = AlternateView.CreateAlternateViewFromString(text, System.Text.Encoding.UTF8, MediaTypeNames.Text.Plain)

            'Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString(body, System.Text.Encoding.UTF8, MediaTypeNames.Text.Html)


            Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString("Envío de constancias salariales", System.Text.Encoding.UTF8, MediaTypeNames.Text.Html)



            mensaje.Subject = "Envío de Constancia Salarial"
            mensaje.AlternateViews.Add(plainView)
            mensaje.AlternateViews.Add(htmlView)

            '------------------------------------

            'Envío del mensaje
            cliente.Send(mensaje)

            'Limpieza
            mensaje = Nothing
            direccionOrigen = Nothing

            ' direccionDestino = Nothing

            credenciales = Nothing
            cliente = Nothing
        Catch ex As Exception

        End Try

    End Sub


    Public Class StpmDinamico

        Shared Function GetStmp() As DataTable
            Dim con As New SqlClient.SqlConnection
            con.ConnectionString = "Data Source=192.168.101.215\BDAPP01;Initial Catalog=BI;Persist Security Info=True;User ID=sa;Password=Igoloncet!2"
            Dim cmd As New SqlClient.SqlCommand("SELECT smtpServidor, smtpUsuario, smtpPassword FROM BI.dbo.SMPT_ALERTAS", con)
            Dim settings As DataTable = New DataTable
            Dim reader As SqlClient.SqlDataAdapter = New SqlClient.SqlDataAdapter(cmd)
            reader.Fill(settings)
            Return settings
        End Function


    End Class


    Private Sub cero_Click(sender As Object, e As EventArgs) Handles cero.Click
        txtcorreo.Text = txtcorreo.Text & "0"
    End Sub
End Class
