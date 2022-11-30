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


Public Class Frmestadocuentaaseconstruplaza


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
    Public num As Integer



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


    Private Sub Frmestadocuentaaseconstruplaza_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        Try

            Formmenu.btnaseconstruplaza.Enabled = False

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
                    Formmenu.btnaseconstruplaza.Enabled = False
                Else
                    DisplayError("CreateTemplate", iError)
                    bandera_error = True
                    m_FPM.CloseDevice()
                    Formmenu.btnaseconstruplaza.Enabled = False
                    Me.Close()
                End If
            Else
                DisplayError("GetImage", iError)
                bandera_error = True
                m_FPM.CloseDevice()
                Formmenu.btnaseconstruplaza.Enabled = False
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
                                    m_FPM.CloseDevice()
                                End If
                            Else
                                DisplayError("MatchTemplate", iErrorver)
                                m_FPM.CloseDevice()
                            End If


                        End If

                    End If


                Next

            End If




            If emplea2_encontrado <> "NO" Then


                '---listview de ahorros-----------------------------------------------------------------------------------------------------------

                ListView1.Columns.Add("AHORRO", 350, HorizontalAlignment.Left)

                '      ListView1.Columns.Add((Format("TOTAL", "##.#0")), 200, HorizontalAlignment.Left)
                ListView1.Columns.Add("TOTAL", 200, HorizontalAlignment.Left)
                ListView1.View = View.Details
                '   MsgBox(emplea2_encontrado)


                Dim item As New ListViewItem

                ListView1.Items.Clear()


                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT " +
                                    "ISNULL(cDesDeducc,0) as AHORRO " +
                                    ",ISNULL(nMtoPrinci,0) AS TOTAL " +
                                    "FROM [DIGEMA0020050\BDAPP02].[coviaseconstruplaza].[dbo].[Vista_AhorrosPorAsociado] WITH(NOLOCK) " +
                                    "WHERE cidasociad='" & emplea2_encontrado & "' " +
                                    "UNION ALL " +
                                    "SELECT " +
                                    "'TOTAL' as AHORRO " +
                                    ",ISNULL(SUM(nMtoPrinci),0) AS TOTAL " +
                                    "FROM [DIGEMA0020050\BDAPP02].[coviaseconstruplaza].[dbo].[Vista_AhorrosPorAsociado] WITH(NOLOCK) " +
                                    "WHERE cidasociad='" & emplea2_encontrado & "'"



                    .CommandType = CommandType.Text
                End With
                daconsulta.SelectCommand = cmdconsulta
                daconsulta.Fill(dsconsulta)

                Dim itm As ListViewItem


                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1





                    str(0) = dsconsulta.Tables(0).Rows(z).Item("AHORRO")
                    str(1) = CDbl(dsconsulta.Tables(0).Rows(z).Item("TOTAL")).ToString("N2")
                    'dsconsulta.Tables(0).Rows(z).Item("TOTAL")

                    itm = New ListViewItem(str)
                    ListView1.Items.Add(itm)




                    Dim iView As Integer = ListView1.Items.Count - 1
                    For i = 1 To iView Step 2
                        ListView1.Items(i).UseItemStyleForSubItems = True
                        ListView1.Items(i).BackColor = Drawing.Color.WhiteSmoke
                    Next i



                Next









                '---listview de creditos-----------------------------------------------------------------------------------------------------------

                ListView2.Columns.Add("CREDITO", 210, HorizontalAlignment.Left)
                ListView2.Columns.Add("PRINCIPAL", 135, HorizontalAlignment.Left)
                ListView2.Columns.Add("SALDO", 130, HorizontalAlignment.Left)
                ListView2.Columns.Add("CUOTA", 100, HorizontalAlignment.Left)
                ListView2.Columns.Add("TASA", 100, HorizontalAlignment.Left)
                ListView2.Columns.Add("FORMALIZACIÓN", 150, HorizontalAlignment.Left)
                ListView2.View = View.Details
                '   MsgBox(emplea2_encontrado)


                Dim item2 As New ListViewItem

                ListView2.Items.Clear()


                cmdconsulta = New SqlClient.SqlCommand
                daconsulta = New SqlClient.SqlDataAdapter
                dsconsulta = New DataSet
                With cmdconsulta
                    .Connection = conexion
                    .CommandText = "SELECT ISNULL([CDetalleLi],'') as CREDITO " +
                                    ",ISNULL([NMontoApro],0) AS PRINCIPAL " +
                                    ",ISNULL([NSaldoCred],0) AS SALDO " +
                                    ",ISNULL([NCuotaReal],0) AS CUOTA " +
                                    ",ISNULL([NTasaInter],0) AS TASA " +
                                    ",ISNULL([DFechaForm],'') AS FECHA_FORMALIZACION " +
                                    "FROM [DIGEMA0020050\BDAPP02].[coviaseconstruplaza].[dbo].[Vista_CreditosPorAsociado] WITH(NOLOCK) " +
                                    "WHERE cidasociad='" & emplea2_encontrado & "' " +
                                    "UNION ALL " +
                                    "SELECT 'TOTAL' AS CREDITO " +
                                    ",ISNULL(SUM([NMontoApro]),0) AS PRINCIPAL " +
                                    ",ISNULL(SUM([NSaldoCred]),0) AS SALDO " +
                                    ",ISNULL(SUM([NCuotaReal]),0) AS CUOTA " +
                                    ",0 AS TASA " +
                                    ",''AS FECHA_FORMALIZACION " +
                                    "FROM [DIGEMA0020050\BDAPP02].[coviaseconstruplaza].[dbo].[Vista_CreditosPorAsociado] WITH(NOLOCK) " +
                                    "WHERE cidasociad='" & emplea2_encontrado & "'"


                    '"SELECT [CDetalleLi]as CREDITO " +
                    '                                    ",[NMontoApro] AS PRINCIPAL " +
                    '                                    ",[NSaldoCred] AS SALDO " +
                    '                                    ",[NCuotaReal]AS CUOTA " +
                    '                                    ",[NTasaInter] AS TASA " +
                    '                                    ",[DFechaForm] AS FECHA_FORMALIZACION " +
                    '                                    "FROM [DIGEMA0020050\BDAPP02].[coviaseconstruplaza].[dbo].[Vista_CreditosPorAsociado] WITH(NOLOCK) " +
                    '                                    "WHERE cidasociad='" & emplea2_encontrado & "'"

                    .CommandType = CommandType.Text
                End With
                daconsulta.SelectCommand = cmdconsulta
                daconsulta.Fill(dsconsulta)

                Dim itm2 As ListViewItem


                For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
                    str(0) = dsconsulta.Tables(0).Rows(z).Item("CREDITO")
                    str(1) = CDbl(dsconsulta.Tables(0).Rows(z).Item("PRINCIPAL")).ToString("N2")
                    str(2) = CDbl(dsconsulta.Tables(0).Rows(z).Item("SALDO")).ToString("N2")
                    str(3) = CDbl(dsconsulta.Tables(0).Rows(z).Item("CUOTA")).ToString("N2")
                    str(4) = CDbl(dsconsulta.Tables(0).Rows(z).Item("TASA")).ToString("N2")
                    str(5) = dsconsulta.Tables(0).Rows(z).Item("FECHA_FORMALIZACION")
                    itm2 = New ListViewItem(str)
                    ListView2.Items.Add(itm2)




                    Dim iView As Integer = ListView2.Items.Count - 1
                    For i = 1 To iView Step 2
                        ListView2.Items(i).UseItemStyleForSubItems = True
                        ListView2.Items(i).BackColor = Drawing.Color.WhiteSmoke
                    Next i



                Next





            End If










            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT nDisponible as DISPONIBLE " +
                "FROM [DIGEMA0020050\BDAPP02].[coviaseconstruplaza].[dbo].[Vista_DisponiblesCredito] " +
                "WHERE cidasociad='" & emplea2_encontrado & "'"

                ''" & emplea2_encontrado & "' " +

                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)

            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1

                txtdisponible.Text = CDbl(dsconsulta.Tables(0).Rows(z).Item("DISPONIBLE")).ToString("N2")



            Next







            '  ------------


            If bandera_error = True Then


                MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")

                m_FPM.CloseDevice()
                Me.Close()
                Formmenu.btnaseconstruplaza.Enabled = True


            End If


        Catch ex As Exception
            MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
            m_FPM.CloseDevice()
            Formmenu.btnaseconstruplaza.Enabled = True
            Me.Close()
            m_FPM.CloseDevice()
            Me.Close()
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
        Formmenu.btnaseconstruplaza.Enabled = True


        Me.Close()


    End Sub
End Class