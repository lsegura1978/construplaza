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


Public Class Frmactualizaempleado
    Public dias_pendi As Integer
    Public textfocus As String = "nada"
    Public imagen As iTextSharp.text.Image
    Public imagen_firma As iTextSharp.text.Image
    Public bandera As Boolean = False


    Public cmdconsulta, cmdconsulta_nacionalidad, cmdconsulta_pais, cmdconsulta_provincia, cmdconsulta_canton, cmdconsulta_distrito, cmdconsulta_documentos As SqlClient.SqlCommand
    Public transaccion, transaccion_documentos As SqlClient.SqlTransaction
    Public cmdincluir, cmdincluir_documentos As SqlClient.SqlCommand
    Public iregincluir, iregincluird, cod, t, y As Integer
    Public contrarecibo, strconexionex, parametros, asunto, cuerpo, CLIENTE, FECHA, DOCUMENTO, TIPO, MONEDA, CREDITO, NOMBRE, nombres As String
    Public MONTO, SALDO As Decimal
    Public conexion As SqlClient.SqlConnection
    Public conexionbi As SqlClient.SqlConnection
    Public daconsulta, daconsulta_pais, daconsulta_nacionalidad, daconsulta_provincia, daconsulta_canton, daconsulta_distrito, daconsulta_documentos As SqlClient.SqlDataAdapter
    Public dsconsulta, dsconsulta_pais, dsconsulta_nacionalidad, dsconsulta_provincia, dsconsulta_canton, dsconsulta_distrito, dsconsulta_documentos As DataSet
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


    Private Sub siete_Click(sender As Object, e As EventArgs) Handles siete.Click
        If textfocus = "dependientes" Then
            txtdependientes.Text = txtdependientes.Text & "7"
        End If
        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "7"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "7"
        End If

        If textfocus = "telefono1" Then
            txttelefono1.Text = txttelefono1.Text & "7"
        End If

        If textfocus = "telefono2" Then
            txttelefono2.Text = txttelefono2.Text & "7"
        End If
        If textfocus = "telefono3" Then
            Txttelefono3.Text = Txttelefono3.Text & "7"
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "7"
        End If
    End Sub

    Private Sub ocho_Click(sender As Object, e As EventArgs) Handles ocho.Click
        If textfocus = "dependientes" Then
            txtdependientes.Text = txtdependientes.Text & "8"
        End If
        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "8"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "8"
        End If

        If textfocus = "telefono1" Then
            txttelefono1.Text = txttelefono1.Text & "8"
        End If

        If textfocus = "telefono2" Then
            txttelefono2.Text = txttelefono2.Text & "8"
        End If
        If textfocus = "telefono3" Then
            Txttelefono3.Text = Txttelefono3.Text & "8"
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "8"
        End If
    End Sub

    Private Sub nueve_Click(sender As Object, e As EventArgs) Handles nueve.Click
        If textfocus = "dependientes" Then
            txtdependientes.Text = txtdependientes.Text & "9"
        End If
        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "9"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "9"
        End If

        If textfocus = "telefono1" Then
            txttelefono1.Text = txttelefono1.Text & "9"
        End If

        If textfocus = "telefono2" Then
            txttelefono2.Text = txttelefono2.Text & "9"
        End If
        If textfocus = "telefono3" Then
            Txttelefono3.Text = Txttelefono3.Text & "9"
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "9"
        End If
    End Sub

    Private Sub cuatro_Click(sender As Object, e As EventArgs) Handles cuatro.Click
        If textfocus = "dependientes" Then
            txtdependientes.Text = txtdependientes.Text & "4"
        End If
        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "4"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "4"
        End If

        If textfocus = "telefono1" Then
            txttelefono1.Text = txttelefono1.Text & "4"
        End If

        If textfocus = "telefono2" Then
            txttelefono2.Text = txttelefono2.Text & "4"
        End If
        If textfocus = "telefono3" Then
            Txttelefono3.Text = Txttelefono3.Text & "4"
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "4"
        End If
    End Sub

    Private Sub cinco_Click(sender As Object, e As EventArgs) Handles cinco.Click
        If textfocus = "dependientes" Then
            txtdependientes.Text = txtdependientes.Text & "5"
        End If
        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "5"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "5"
        End If

        If textfocus = "telefono1" Then
            txttelefono1.Text = txttelefono1.Text & "5"
        End If

        If textfocus = "telefono2" Then
            txttelefono2.Text = txttelefono2.Text & "5"
        End If
        If textfocus = "telefono3" Then
            Txttelefono3.Text = Txttelefono3.Text & "5"
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "5"
        End If
    End Sub

    Private Sub seis_Click(sender As Object, e As EventArgs) Handles seis.Click
        If textfocus = "dependientes" Then
            txtdependientes.Text = txtdependientes.Text & "6"
        End If
        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "6"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "6"
        End If

        If textfocus = "telefono1" Then
            txttelefono1.Text = txttelefono1.Text & "6"
        End If

        If textfocus = "telefono2" Then
            txttelefono2.Text = txttelefono2.Text & "6"
        End If
        If textfocus = "telefono3" Then
            Txttelefono3.Text = Txttelefono3.Text & "6"
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "6"
        End If
    End Sub

    Private Sub uno_Click(sender As Object, e As EventArgs) Handles uno.Click

        If textfocus = "dependientes" Then
            txtdependientes.Text = txtdependientes.Text & "1"
        End If
        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "1"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "1"
        End If

        If textfocus = "telefono1" Then
            txttelefono1.Text = txttelefono1.Text & "1"
        End If

        If textfocus = "telefono2" Then
            txttelefono2.Text = txttelefono2.Text & "1"
        End If
        If textfocus = "telefono3" Then
            Txttelefono3.Text = Txttelefono3.Text & "1"
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "1"
        End If
        'txtcorreo.Text = txtcorreo.Text & "1"
    End Sub

    Private Sub dos_Click(sender As Object, e As EventArgs) Handles dos.Click
        If textfocus = "dependientes" Then
            txtdependientes.Text = txtdependientes.Text & "2"
        End If
        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "2"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "2"
        End If

        If textfocus = "telefono1" Then
            txttelefono1.Text = txttelefono1.Text & "2"
        End If

        If textfocus = "telefono2" Then
            txttelefono2.Text = txttelefono2.Text & "2"
        End If
        If textfocus = "telefono3" Then
            Txttelefono3.Text = Txttelefono3.Text & "2"
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "2"
        End If
    End Sub

    Private Sub tres_Click(sender As Object, e As EventArgs) Handles tres.Click
        If textfocus = "dependientes" Then
            txtdependientes.Text = txtdependientes.Text & "3"
        End If
        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "3"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "3"
        End If

        If textfocus = "telefono1" Then
            txttelefono1.Text = txttelefono1.Text & "3"
        End If

        If textfocus = "telefono2" Then
            txttelefono2.Text = txttelefono2.Text & "3"
        End If
        If textfocus = "telefono3" Then
            Txttelefono3.Text = Txttelefono3.Text & "3"
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "3"
        End If
    End Sub




    Private Sub Frmactualizaempleado_Load(sender As Object, e As EventArgs) Handles MyBase.Load








        Me.BackColor = Color.Gray


        Try


            Formmenu.btnactualizacion.Enabled = False
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
                    Formmenu.btnactualizacion.Enabled = False
                Else
                    DisplayError("CreateTemplate", iError)
                    bandera_error = True
                    m_FPM.CloseDevice()
                    Formmenu.btnactualizacion.Enabled = False
                    Me.Close()
                End If
            Else
                DisplayError("GetImage", iError)
                bandera_error = True
                m_FPM.CloseDevice()
                Formmenu.btnactualizacion.Enabled = False
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


                For contador = 0 To dsconsulta.Tables(0).Rows.Count - 1

                    If encontrado = False Then


                        obj = dsconsulta.Tables(0).Rows(contador).Item("TEMPLATE_1")
                        obj2 = dsconsulta.Tables(0).Rows(contador).Item("TEMPLATE_2")
                        emplea2 = dsconsulta.Tables(0).Rows(contador).Item("EMPLEADO")

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


                    cmdconsulta_nacionalidad = New SqlClient.SqlCommand
                    daconsulta_nacionalidad = New SqlClient.SqlDataAdapter
                    dsconsulta_nacionalidad = New DataSet
                    With cmdconsulta_nacionalidad
                        .Connection = conexion
                        .CommandText = "SELECT NOMBRE FROM EXACTUS.DIGEMA.PAIS WITH(NOLOCK)"




                        .CommandType = CommandType.Text
                    End With
                    daconsulta_nacionalidad.SelectCommand = cmdconsulta_nacionalidad
                    daconsulta_nacionalidad.Fill(dsconsulta_nacionalidad)

                    cmbnacionalidad.Items.Clear()


                    For z = 0 To dsconsulta_nacionalidad.Tables(0).Rows.Count - 1
                   
                        cmbnacionalidad.Items.Add(dsconsulta_nacionalidad.Tables(0).Rows(z).Item("NOMBRE"))


                    Next







                    '--------------------------------------------------------------------------------------------------------------------------------

                    'emplea2_encontrado = "0677"

                    cmdconsulta = New SqlClient.SqlCommand
                    daconsulta = New SqlClient.SqlDataAdapter
                    dsconsulta = New DataSet
                    With cmdconsulta
                        .Connection = conexion
                        .CommandText = "SELECT PR.NOMBRE AS PROVINCIA,CA.NOMBRE AS CANTON,E.DIRECCION_HAB,E.TELEFONO1,E.TELEFONO2,E.TELEFONO3,CONVERT(CHAR(4), E.FECHA_NACIMIENTO, 112) AS ANO_NACIMIENTO,CONVERT(CHAR(2) " +
                                        ", E.FECHA_NACIMIENTO, 113) DIA_NACIMIENTO,CONVERT(CHAR(2), E.FECHA_NACIMIENTO, 110) MES_NACIMIENTO,E.EMPLEADO,E.NOMBRE " +
                                        ",E.E_MAIL,E.U_E_MAIL_PERSONAL,E.DEPENDIENTES,E.TIPO_SANGRE,P.NOMBRE AS NACIONALIDAD,E.PERMISO_CONDUCIR, " +
                                        "CASE WHEN E.ESTADO_CIVIL='C' THEN 'Casado' " +
                                            "WHEN E.ESTADO_CIVIL='S' THEN 'Soltero' " +
                                            "WHEN E.ESTADO_CIVIL='D' THEN 'Divociado' " +
                                           "WHEN E.ESTADO_CIVIL='U' THEN 'Unión Libre' " +
                                           "WHEN E.ESTADO_CIVIL='V' THEN 'Viudo' " +
                                           "ELSE 'Otro' END AS ESTADO_CIVIL " +
                                           ",E.U_DISTRITO AS DISTRITO " +
                                        "FROM EXACTUS.DIGEMA.EMPLEADO E WITH(NOLOCK) " +
                                        "INNER JOIN EXACTUS.DIGEMA.PAIS P WITH(NOLOCK) ON P.PAIS=E.PAIS " +
                                        "INNER JOIN EXACTUS.DIGEMA.DIVISION_GEOGRAFICA1 PR WITH(NOLOCK) ON  PR.DIVISION_GEOGRAFICA1=E.DIVISION_GEOGRAFICA1 " +
                                        "INNER JOIN EXACTUS.DIGEMA.DIVISION_GEOGRAFICA2 CA WITH(NOLOCK) ON  CA.DIVISION_GEOGRAFICA1=PR.DIVISION_GEOGRAFICA1 AND CA.DIVISION_GEOGRAFICA2=E.DIVISION_GEOGRAFICA2 " +
                                        "WHERE E.EMPLEADO='" & emplea2_encontrado & "'"



                        '"SELECT E.DIRECCION_HAB,E.TELEFONO1,E.TELEFONO2,E.TELEFONO3,CONVERT(CHAR(4), E.FECHA_NACIMIENTO, 112) AS ANO_NACIMIENTO,CONVERT(CHAR(2), E.FECHA_NACIMIENTO, 113) DIA_NACIMIENTO,CONVERT(CHAR(2), E.FECHA_NACIMIENTO, 110) MES_NACIMIENTO,E.EMPLEADO,E.NOMBRE,E.E_MAIL,E.U_E_MAIL_PERSONAL,E.DEPENDIENTES,E.TIPO_SANGRE,P.NOMBRE AS NACIONALIDAD,E.PERMISO_CONDUCIR FROM EXACTUS.DIGEMA.EMPLEADO E WITH(NOLOCK) INNER JOIN EXACTUS.DIGEMA.PAIS P WITH(NOLOCK) ON P.PAIS=E.PAIS WHERE E.EMPLEADO='" & emplea2_encontrado & "'"



                        .CommandType = CommandType.Text
                    End With
                    daconsulta.SelectCommand = cmdconsulta
                    daconsulta.Fill(dsconsulta)


                    For z = 0 To dsconsulta.Tables(0).Rows.Count - 1


                        txtempleado.Text = dsconsulta.Tables(0).Rows(z).Item("EMPLEADO")
                        txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
                        txtcorreo.Text = dsconsulta.Tables(0).Rows(z).Item("E_MAIL")
                        txtcorreo_personal.Text = dsconsulta.Tables(0).Rows(z).Item("U_E_MAIL_PERSONAL")
                        txtdependientes.Text = dsconsulta.Tables(0).Rows(z).Item("DEPENDIENTES")
                        cmbtipo_sangre.Text = dsconsulta.Tables(0).Rows(z).Item("TIPO_SANGRE")
                        cmbnacionalidad.Text = dsconsulta.Tables(0).Rows(z).Item("NACIONALIDAD")
                        cmbpermiso_conducir.Text = dsconsulta.Tables(0).Rows(z).Item("PERMISO_CONDUCIR")
                        cmbdia_nacimiento.Text = dsconsulta.Tables(0).Rows(z).Item("DIA_NACIMIENTO")
                        cmb_mes_nacimiento.Text = dsconsulta.Tables(0).Rows(z).Item("MES_NACIMIENTO")
                        cmbano_nacimiento.Text = dsconsulta.Tables(0).Rows(z).Item("ANO_NACIMIENTO")
                        cmbano_nacimiento.Text = dsconsulta.Tables(0).Rows(z).Item("ANO_NACIMIENTO")
                        txttelefono1.Text = dsconsulta.Tables(0).Rows(z).Item("TELEFONO1")
                        txttelefono2.Text = dsconsulta.Tables(0).Rows(z).Item("TELEFONO2")
                        Txttelefono3.Text = dsconsulta.Tables(0).Rows(z).Item("TELEFONO3")
                        txtdireccion.Text = dsconsulta.Tables(0).Rows(z).Item("DIRECCION_HAB")
                        Cmbprovincia.Text = dsconsulta.Tables(0).Rows(z).Item("PROVINCIA")
                        cmbcanton.Text = dsconsulta.Tables(0).Rows(z).Item("CANTON")
                        cmbdistrito.Text = dsconsulta.Tables(0).Rows(z).Item("DISTRITO")
                        cmbestado_civil.Text = dsconsulta.Tables(0).Rows(z).Item("ESTADO_CIVIL")




                    Next

                End If

            End If

            If bandera_error = True Then


                MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")

                m_FPM.CloseDevice()
                Me.Close()
                Formmenu.btnactualizacion.Enabled = True


            End If




        Catch ex As Exception
            MsgBox("No se logró leer su huella, intente nuevamente por favor &", MsgBoxStyle.Critical, "Construplaza")
            m_FPM.CloseDevice()
            Me.Close()
            Formmenu.btnactualizacion.Enabled = True
            ' Show the stack trace, which is a list of methods 
            ' that are currently executing.
            '  MessageBox.Show("Stack Trace: " & vbCrLf & ex.StackTrace)
        Finally
            m_FPM.CloseDevice()
            ' This line executes whether or not the exception occurs.
            '  MessageBox.Show("in Finally block")
        End Try

        bandera = True


        cmbcanton.Enabled = False

        cmbdistrito.Enabled = False

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


    Private Sub btncerrar_Click(sender As Object, e As EventArgs) Handles btncerrar.Click
        bandera = True
        conexion.Close()
        Me.Close()
        Formmenu.btnactualizacion.Enabled = True
    End Sub

    Private Sub btnenviar_Click(sender As Object, e As EventArgs)

        Dim bandera_vacia As Boolean = False

        If txtcorreo.Text = "" Then
            txtcorreo.Text = "_"
        End If

        If txtcorreo_personal.Text = "" Then
            txtcorreo_personal.Text = "_"
        End If


        If txtdependientes.Text = "" Then
            txtdependientes.Text = 0
        End If

        If cmbtipo_sangre.Text = "" Then
            cmbtipo_sangre.Text = "Nulo"
        End If

        If cmbpermiso_conducir.Text = "" Then
            cmbtipo_sangre.Text = "No tiene"
        End If

        If cmbdia_nacimiento.Text = "" Then
            MsgBox("Debe de escoger el día de nacimiento", MsgBoxStyle.Critical, "Construplaza")
            cmbdia_nacimiento.Focus()
            bandera_vacia = True
        End If

        If cmb_mes_nacimiento.Text = "" Then
            MsgBox("Debe de escoger el mes de nacimiento", MsgBoxStyle.Critical, "Construplaza")
            cmb_mes_nacimiento.Focus()
            bandera_vacia = True
        End If


        If cmbano_nacimiento.Text = "" Then
            MsgBox("Debe de escoger el año de nacimiento", MsgBoxStyle.Critical, "Construplaza")
            cmbano_nacimiento.Focus()
            bandera_vacia = True
        End If


        If cmbestado_civil.Text = "" Then
            MsgBox("Debe de escoger su estado civil", MsgBoxStyle.Critical, "Construplaza")
            cmbestado_civil.Focus()
            bandera_vacia = True
        End If


        If txttelefono1.Text = "" Then
            txttelefono1.Text = "25888888"
        End If


        If txttelefono2.Text = "" Then
            txttelefono2.Text = "00000000"
        End If


        If Txttelefono3.Text = "" Then
            Txttelefono3.Text = "00000000"
        End If


        If Cmbprovincia.Text = "" Then
            MsgBox("Debe de escoger la provincia donde vive", MsgBoxStyle.Critical, "Construplaza")
            Cmbprovincia.Focus()
            bandera_vacia = True
        End If



        If cmbcanton.Text = "" Then
            MsgBox("Debe de escoger el cantón donde vive", MsgBoxStyle.Critical, "Construplaza")
            cmbcanton.Focus()
            bandera_vacia = True
        End If



        If cmbdistrito.Text = "" Then
            MsgBox("Debe de escoger el distrito donde vive", MsgBoxStyle.Critical, "Construplaza")
            cmbdistrito.Focus()
            bandera_vacia = True
        End If

        If txtdireccion.Text = "" Then
            MsgBox("Debe de incluir una dirección de su casa", MsgBoxStyle.Critical, "Construplaza")
            txtdireccion.Focus()
            bandera_vacia = True
        End If

        If bandera_vacia = False Then
            Dim fecha_nacimiento_insertar As String
            Dim ESTADO_CIVIL As String
            Dim NACIONALIDAD As String
            Dim PROVINCIA_EMPLEADO As String
            Dim CANTON_EMPLEADO As String
            Dim DISTRITO_EMPLEADO As String

            '--aqui devuelve datos de nombre a codigos


            fecha_nacimiento_insertar = cmbano_nacimiento.Text & "-" & cmb_mes_nacimiento.Text & "-" & cmbdia_nacimiento.Text & " 00:00:00.000"



            If cmbestado_civil.Text = "Casado" Then
                ESTADO_CIVIL = "C"
            End If

            If cmbestado_civil.Text = "Soltero" Then
                ESTADO_CIVIL = "S"
            End If

            If cmbestado_civil.Text = "Divorciado" Then
                ESTADO_CIVIL = "D"
            End If

            If cmbestado_civil.Text = "Unión Libre" Then
                ESTADO_CIVIL = "U"
            End If

            If cmbestado_civil.Text = "Viudo" Then
                ESTADO_CIVIL = "V"
            End If
            If cmbestado_civil.Text = "Otro" Then
                ESTADO_CIVIL = "O"
            End If




            '-- BUSCA PAIS PARA NACIONALIDAD



            cmdconsulta_pais = New SqlClient.SqlCommand
            daconsulta_pais = New SqlClient.SqlDataAdapter
            dsconsulta_pais = New DataSet
            With cmdconsulta_pais
                .Connection = conexion
                .CommandText = "SELECT PAIS FROM EXACTUS.DIGEMA.PAIS WHERE NOMBRE='" & cmbnacionalidad.Text & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta_pais.SelectCommand = cmdconsulta_pais
            daconsulta_pais.Fill(dsconsulta_pais)




            For z = 0 To dsconsulta_pais.Tables(0).Rows.Count - 1

                NACIONALIDAD = dsconsulta_pais.Tables(0).Rows(z).Item("PAIS")


            Next

            '-------------------BUSCA PROVINCIA A ACTUALIZAR-------------------------------------------------------------------------------


            '-- BUSCA PAIS PARA NACIONALIDAD



            cmdconsulta_provincia = New SqlClient.SqlCommand
            daconsulta_provincia = New SqlClient.SqlDataAdapter
            dsconsulta_provincia = New DataSet
            With cmdconsulta_provincia
                .Connection = conexion
                .CommandText = "SELECT DIVISION_GEOGRAFICA1 FROM EXACTUS.DIGEMA.DIVISION_GEOGRAFICA1 WHERE NOMBRE='" & Cmbprovincia.Text & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta_provincia.SelectCommand = cmdconsulta_provincia
            daconsulta_provincia.Fill(dsconsulta_provincia)




            For z = 0 To dsconsulta_provincia.Tables(0).Rows.Count - 1

                PROVINCIA_EMPLEADO = dsconsulta_provincia.Tables(0).Rows(z).Item("DIVISION_GEOGRAFICA1")


            Next


            cmdconsulta_canton = New SqlClient.SqlCommand
            daconsulta_canton = New SqlClient.SqlDataAdapter
            dsconsulta_canton = New DataSet
            With cmdconsulta_canton
                .Connection = conexion
                .CommandText = "SELECT DIVISION_GEOGRAFICA2 FROM EXACTUS.DIGEMA.DIVISION_GEOGRAFICA2 WHERE NOMBRE='" & cmbcanton.Text & "' AND DIVISION_GEOGRAFICA1='" & PROVINCIA_EMPLEADO & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta_canton.SelectCommand = cmdconsulta_canton
            daconsulta_canton.Fill(dsconsulta_canton)




            For z = 0 To dsconsulta_canton.Tables(0).Rows.Count - 1

                CANTON_EMPLEADO = dsconsulta_canton.Tables(0).Rows(z).Item("DIVISION_GEOGRAFICA2")


            Next


            cmdconsulta_distrito = New SqlClient.SqlCommand
            daconsulta_distrito = New SqlClient.SqlDataAdapter
            dsconsulta_distrito = New DataSet
            With cmdconsulta_distrito
                .Connection = conexion
                .CommandText = "SELECT DIVISION_GEOGRAFICA3 FROM EXACTUS.DIGEMA.DIVISION_GEOGRAFICA3 WHERE NOMBRE='" & cmbdistrito.Text & "' AND DIVISION_GEOGRAFICA2='" & CANTON_EMPLEADO & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta_distrito.SelectCommand = cmdconsulta_distrito
            daconsulta_distrito.Fill(dsconsulta_distrito)




            For z = 0 To dsconsulta_distrito.Tables(0).Rows.Count - 1

                DISTRITO_EMPLEADO = dsconsulta_distrito.Tables(0).Rows(z).Item("DIVISION_GEOGRAFICA3")

            Next




            '-------------actualiza el empleado ------------------------------------------------------

            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
            End If

            transaccion = conexion.BeginTransaction
            cmdincluir = New SqlClient.SqlCommand



            With cmdincluir
                .Connection = conexion
                .CommandText = "UPDATE EXACTUS.DIGEMA.EMPLEADO SET E_MAIL= '" & txtcorreo.Text & "',U_E_MAIL_PERSONAL='" & txtcorreo_personal.Text & "',DEPENDIENTES=" & txtdependientes.Text & ",TIPO_SANGRE='" & cmbtipo_sangre.Text & "',PAIS='" & NACIONALIDAD & "',PERMISO_CONDUCIR='" & cmbpermiso_conducir.Text & "',FECHA_NACIMIENTO='" & fecha_nacimiento_insertar & "',ESTADO_CIVIL='" & ESTADO_CIVIL & "',TELEFONO1='" & txttelefono1.Text & "',TELEFONO2='" & txttelefono2.Text & "',TELEFONO3='" & Txttelefono3.Text & "',DIVISION_GEOGRAFICA1=" & PROVINCIA_EMPLEADO & ",DIVISION_GEOGRAFICA2='" & CANTON_EMPLEADO & "',U_DISTRITO='" & DISTRITO_EMPLEADO & "',DIRECCION_HAB='" & txtdireccion.Text & "' WHERE EMPLEADO='" & txtempleado.Text & "'"
                .CommandType = CommandType.Text
                .Transaction = transaccion
            End With
            iregincluir = cmdincluir.ExecuteNonQuery
            transaccion.Commit()
            MsgBox("Se actualizó el empleado " & txtnombre.Text & " satisfactoriamente", vbInformation, "Construplaza")


            bandera = True
            Me.Close()
            Formmenu.btnactualizacion.Enabled = True




























            '--END IF DE BANDERA_VACIA
        End If









        bandera = True
        Formmenu.btnactualizacion.Enabled = True

    End Sub

    'Private Sub txtdependientes_Click(sender As Object, e As EventArgs) Handles txtdependientes.Click
    '    MsgBox("aqui`")


    'End Sub

    Private Sub txtdependientes_GotFocus(sender As Object, e As EventArgs) Handles txtdependientes.GotFocus

        textfocus = "dependientes"


    End Sub

    Private Sub txtcorreo_GotFocus(sender As Object, e As EventArgs) Handles txtcorreo.GotFocus
        textfocus = "correo"
    End Sub


   

    Private Sub GroupBox5_Enter(sender As Object, e As EventArgs) Handles GroupBox5.Enter

    End Sub

    Private Sub cmbpermiso_conducir_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbpermiso_conducir.SelectedIndexChanged

    End Sub

   
 
    Private Sub Cmbprovincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Cmbprovincia.SelectedIndexChanged

        If bandera = True Then

       
        Dim provincias As Integer
        Dim cantones As String




        cmdconsulta_provincia = New SqlClient.SqlCommand
        daconsulta_provincia = New SqlClient.SqlDataAdapter
        dsconsulta_provincia = New DataSet
        With cmdconsulta_provincia
            .Connection = conexion
            .CommandText = "SELECT DIVISION_GEOGRAFICA1 FROM EXACTUS.DIGEMA.DIVISION_GEOGRAFICA1 WITH(NOLOCK) WHERE NOMBRE='" & Cmbprovincia.Text & "'"

            .CommandType = CommandType.Text
        End With
        daconsulta_provincia.SelectCommand = cmdconsulta_provincia
        daconsulta_provincia.Fill(dsconsulta_provincia)




        For z = 0 To dsconsulta_provincia.Tables(0).Rows.Count - 1

            provincias = dsconsulta_provincia.Tables(0).Rows(z).Item("DIVISION_GEOGRAFICA1")
            ' cmbnacionalidad.Items.Add(dsconsulta_nacionalidad.Tables(0).Rows(z).Item("NOMBRE"))


        Next





        cmbcanton.Items.Clear()


        cmdconsulta_canton = New SqlClient.SqlCommand
        daconsulta_canton = New SqlClient.SqlDataAdapter
        dsconsulta_canton = New DataSet
        With cmdconsulta_canton
            .Connection = conexion
            .CommandText = "SELECT NOMBRE FROM EXACTUS.DIGEMA.DIVISION_GEOGRAFICA2 WITH(NOLOCK) WHERE DIVISION_GEOGRAFICA1=" & provincias & ""

            .CommandType = CommandType.Text
        End With
        daconsulta_canton.SelectCommand = cmdconsulta_canton
        daconsulta_canton.Fill(dsconsulta_canton)




        For z = 0 To dsconsulta_canton.Tables(0).Rows.Count - 1

            ' provincias = dsconsulta_canton.Tables(0).Rows(z).Item("DIVISION_GEOGRAFICA1")
            cmbcanton.Items.Add(dsconsulta_canton.Tables(0).Rows(z).Item("NOMBRE"))
            cantones = dsconsulta_canton.Tables(0).Rows(z).Item("NOMBRE")



        Next

        cmbcanton.Text = cantones

        End If


        cmbcanton.Enabled = True

        cmbdistrito.Enabled = True

    End Sub

    'Private Sub Cmbprovincia_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles Cmbprovincia.SelectionChangeCommitted
    '    Dim provincias As Integer
    '    Dim cantones As String


    '    cmdconsulta_provincia = New SqlClient.SqlCommand
    '    daconsulta_provincia = New SqlClient.SqlDataAdapter
    '    dsconsulta_provincia = New DataSet
    '    With cmdconsulta_provincia
    '        .Connection = conexion
    '        .CommandText = "SELECT DIVISION_GEOGRAFICA1 FROM EXACTUS.DIGEMA.DIVISION_GEOGRAFICA1 WITH(NOLOCK) WHERE NOMBRE='" & Cmbprovincia.Text & "'"

    '        .CommandType = CommandType.Text
    '    End With
    '    daconsulta_provincia.SelectCommand = cmdconsulta_provincia
    '    daconsulta_provincia.Fill(dsconsulta_provincia)




    '    For z = 0 To dsconsulta_provincia.Tables(0).Rows.Count - 1

    '        provincias = dsconsulta_provincia.Tables(0).Rows(z).Item("DIVISION_GEOGRAFICA1")
    '        ' cmbnacionalidad.Items.Add(dsconsulta_nacionalidad.Tables(0).Rows(z).Item("NOMBRE"))


    '    Next





    '    cmbcanton.Items.Clear()


    '    cmdconsulta_canton = New SqlClient.SqlCommand
    '    daconsulta_canton = New SqlClient.SqlDataAdapter
    '    dsconsulta_canton = New DataSet
    '    With cmdconsulta_canton
    '        .Connection = conexion
    '        .CommandText = "SELECT NOMBRE FROM EXACTUS.DIGEMA.DIVISION_GEOGRAFICA2 WITH(NOLOCK) WHERE DIVISION_GEOGRAFICA1=" & provincias & ""

    '        .CommandType = CommandType.Text
    '    End With
    '    daconsulta_canton.SelectCommand = cmdconsulta_canton
    '    daconsulta_canton.Fill(dsconsulta_canton)




    '    For z = 0 To dsconsulta_canton.Tables(0).Rows.Count - 1

    '        ' provincias = dsconsulta_canton.Tables(0).Rows(z).Item("DIVISION_GEOGRAFICA1")
    '        cmbcanton.Items.Add(dsconsulta_canton.Tables(0).Rows(z).Item("NOMBRE"))
    '        cantones = dsconsulta_canton.Tables(0).Rows(z).Item("NOMBRE")



    '    Next

    '    cmbcanton.Text = cantones

    'End Sub

    Private Sub txtcorreo_TextChanged(sender As Object, e As EventArgs) Handles txtcorreo.TextChanged

    End Sub

    Private Sub txtcorreo_personal_GotFocus(sender As Object, e As EventArgs) Handles txtcorreo_personal.GotFocus
        textfocus = "correo_personal"
    End Sub

    Private Sub txtcorreo_personal_TextChanged(sender As Object, e As EventArgs) Handles txtcorreo_personal.TextChanged

    End Sub

    Private Sub txtdependientes_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtdependientes.KeyPress

    End Sub

    Private Sub txtdependientes_TextChanged(sender As Object, e As EventArgs) Handles txtdependientes.TextChanged

    End Sub

    Private Sub txttelefono1_GotFocus(sender As Object, e As EventArgs) Handles txttelefono1.GotFocus
        textfocus = "telefono1"
    End Sub

    Private Sub txttelefono1_TextChanged(sender As Object, e As EventArgs) Handles txttelefono1.TextChanged

    End Sub

    Private Sub txttelefono2_GotFocus(sender As Object, e As EventArgs) Handles txttelefono2.GotFocus
        textfocus = "telefono2"

    End Sub

    Private Sub txttelefono2_TextChanged(sender As Object, e As EventArgs) Handles txttelefono2.TextChanged

    End Sub

    Private Sub Txttelefono3_GotFocus(sender As Object, e As EventArgs) Handles Txttelefono3.GotFocus
        textfocus = "telefono3"
    End Sub

    Private Sub Txttelefono3_TextChanged(sender As Object, e As EventArgs) Handles Txttelefono3.TextChanged

    End Sub

    Private Sub txtdireccion_GotFocus(sender As Object, e As EventArgs) Handles txtdireccion.GotFocus
        textfocus = "direccion"

    End Sub

    Private Sub txtdireccion_TextChanged(sender As Object, e As EventArgs) Handles txtdireccion.TextChanged

    End Sub

    Private Sub cero_Click(sender As Object, e As EventArgs) Handles cero.Click
        If textfocus = "dependientes" Then
            txtdependientes.Text = txtdependientes.Text & "0"
        End If
        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "0"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "0"
        End If

        If textfocus = "telefono1" Then
            txttelefono1.Text = txttelefono1.Text & "0"
        End If

        If textfocus = "telefono2" Then
            txttelefono2.Text = txttelefono2.Text & "0"
        End If
        If textfocus = "telefono3" Then
            Txttelefono3.Text = Txttelefono3.Text & "0"
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "0"
        End If
    End Sub

    Private Sub btnQ_Click(sender As Object, e As EventArgs) Handles btnQ.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If

        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "q"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "q"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()
            
        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "q"
        End If
    End Sub

    Private Sub btnW_Click(sender As Object, e As EventArgs) Handles btnW.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "w"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "w"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "w"
        End If
    End Sub

    Private Sub btnE_Click(sender As Object, e As EventArgs) Handles btnE.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If

        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "e"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "e"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "e"
        End If
    End Sub

    Private Sub btnR_Click(sender As Object, e As EventArgs) Handles btnR.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If

        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "r"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "r"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "r"
        End If
    End Sub

    Private Sub btnT_Click(sender As Object, e As EventArgs) Handles btnT.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If

        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "t"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "t"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "t"
        End If
    End Sub

    Private Sub btnY_Click(sender As Object, e As EventArgs) Handles btnY.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If

        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "y"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "y"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If


        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "y"
        End If
    End Sub

    Private Sub btnU_Click(sender As Object, e As EventArgs) Handles btnU.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If

        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "u"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "u"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If


        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "u"
        End If
    End Sub

    Private Sub btnI_Click(sender As Object, e As EventArgs) Handles btnI.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "i"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "i"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "i"
        End If
    End Sub

    Private Sub btnO_Click(sender As Object, e As EventArgs) Handles btnO.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "o"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "o"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "o"
        End If
    End Sub

    Private Sub btnP_Click(sender As Object, e As EventArgs) Handles btnP.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "p"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "p"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "p"
        End If
    End Sub

    Private Sub arroba_Click(sender As Object, e As EventArgs) Handles arroba.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "@"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "@"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "@"
        End If
    End Sub

    Private Sub btnA_Click(sender As Object, e As EventArgs) Handles btnA.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "a"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "a"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "a"
        End If
    End Sub

    Private Sub btnS_Click(sender As Object, e As EventArgs) Handles btnS.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "s"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "s"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "s"
        End If
    End Sub

    Private Sub btnD_Click(sender As Object, e As EventArgs) Handles btnD.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "d"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "d"
        End If
        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "d"
        End If





    End Sub

    Private Sub btnF_Click(sender As Object, e As EventArgs) Handles btnF.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "f"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "f"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "f"
        End If





    End Sub

    Private Sub btnG_Click(sender As Object, e As EventArgs) Handles btnG.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "g"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "g"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "g"
        End If





    End Sub

    Private Sub btnH_Click(sender As Object, e As EventArgs) Handles btnH.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "h"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "h"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "h"
        End If





    End Sub

    Private Sub btnJ_Click(sender As Object, e As EventArgs) Handles btnJ.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "j"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "j"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "j"
        End If





    End Sub

    Private Sub btnK_Click(sender As Object, e As EventArgs) Handles btnK.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "k"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "k"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "k"
        End If





    End Sub

    Private Sub btnL_Click(sender As Object, e As EventArgs) Handles btnL.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "l"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "l"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "l"
        End If





    End Sub

    Private Sub rayaarriba_Click(sender As Object, e As EventArgs) Handles rayaarriba.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "-"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "-"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "-"
        End If





    End Sub

    Private Sub btnZ_Click(sender As Object, e As EventArgs) Handles btnZ.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "z"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "z"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "z"
        End If





    End Sub

    Private Sub btnX_Click(sender As Object, e As EventArgs) Handles btnX.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "x"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "x"
        End If
        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "x"
        End If





    End Sub

    Private Sub btnC_Click(sender As Object, e As EventArgs) Handles btnC.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "c"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "c"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "c"
        End If





    End Sub

    Private Sub btnV_Click(sender As Object, e As EventArgs) Handles btnV.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "v"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "v"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "v"
        End If





    End Sub

    Private Sub btnB_Click(sender As Object, e As EventArgs) Handles btnB.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "b"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "b"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "b"
        End If





    End Sub

    Private Sub btnN_Click(sender As Object, e As EventArgs) Handles btnN.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "n"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "n"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "n"
        End If





    End Sub

    Private Sub btnM_Click(sender As Object, e As EventArgs) Handles btnM.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "m"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "m"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "m"
        End If





    End Sub

    Private Sub punto_Click(sender As Object, e As EventArgs) Handles punto.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "."
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "."
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "."
        End If





    End Sub

    Private Sub rayaabajo_Click(sender As Object, e As EventArgs) Handles rayaabajo.Click

        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "_"
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "_"
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "_"
        End If





    End Sub

    Private Sub atras_Click(sender As Object, e As EventArgs) Handles atras.Click

        If textfocus = "dependientes" Then
            txtdependientes.Text = ""
            txtdependientes.Focus()

        End If
        If textfocus = "correo" Then
            'txtcorreo.Text = ""
            txtcorreo.Text = Mid(txtcorreo.Text, 1, Len(txtcorreo.Text) - 1)
            'txtcorreo.Focus()
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = Mid(txtcorreo_personal.Text, 1, Len(txtcorreo_personal.Text) - 1)
            'txtcorreo_personal.Focus()
        End If

        If textfocus = "telefono1" Then
            'txttelefono1.Text = ""
            txttelefono1.Text = Mid(txttelefono1.Text, 1, Len(txttelefono1.Text) - 1)

            'txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            txttelefono2.Text = Mid(txttelefono2.Text, 1, Len(txttelefono2.Text) - 1)
            'txttelefono2.Text = ""
            'txttelefono2.Focus()


        End If
        If textfocus = "telefono3" Then
            Txttelefono3.Text = Mid(Txttelefono3.Text, 1, Len(Txttelefono3.Text) - 1)
            'Txttelefono3.Text = ""
            'Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = Mid(txtdireccion.Text, 1, Len(txtdireccion.Text) - 1)
            'txtdireccion.Text = ""
            'txtdireccion.Focus()
        End If





    End Sub

    Private Sub cmbcanton_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbcanton.SelectedIndexChanged

        If bandera = True Then


            Dim provincias As Integer
            Dim cantoness As String
            Dim distrito As String





            cmdconsulta_provincia = New SqlClient.SqlCommand
            daconsulta_provincia = New SqlClient.SqlDataAdapter
            dsconsulta_provincia = New DataSet
            With cmdconsulta_provincia
                .Connection = conexion
                .CommandText = "SELECT DIVISION_GEOGRAFICA1 FROM EXACTUS.DIGEMA.DIVISION_GEOGRAFICA1 WITH(NOLOCK) WHERE NOMBRE='" & Cmbprovincia.Text & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta_provincia.SelectCommand = cmdconsulta_provincia
            daconsulta_provincia.Fill(dsconsulta_provincia)




            For z = 0 To dsconsulta_provincia.Tables(0).Rows.Count - 1

                provincias = dsconsulta_provincia.Tables(0).Rows(z).Item("DIVISION_GEOGRAFICA1")
                ' cmbnacionalidad.Items.Add(dsconsulta_nacionalidad.Tables(0).Rows(z).Item("NOMBRE"))


            Next





            cmdconsulta_canton = New SqlClient.SqlCommand
            daconsulta_canton = New SqlClient.SqlDataAdapter
            dsconsulta_canton = New DataSet
            With cmdconsulta_canton
                .Connection = conexion
                .CommandText = "SELECT DIVISION_GEOGRAFICA2 FROM EXACTUS.DIGEMA.DIVISION_GEOGRAFICA2 WITH(NOLOCK) WHERE NOMBRE='" & cmbcanton.Text & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta_canton.SelectCommand = cmdconsulta_canton
            daconsulta_canton.Fill(dsconsulta_canton)




            For z = 0 To dsconsulta_canton.Tables(0).Rows.Count - 1

                cantoness = dsconsulta_canton.Tables(0).Rows(z).Item("DIVISION_GEOGRAFICA2")
                ' cmbnacionalidad.Items.Add(dsconsulta_nacionalidad.Tables(0).Rows(z).Item("NOMBRE"))


            Next




            cmbdistrito.Items.Clear()


            cmdconsulta_distrito = New SqlClient.SqlCommand
            daconsulta_distrito = New SqlClient.SqlDataAdapter
            dsconsulta_distrito = New DataSet
            With cmdconsulta_distrito
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.DIGEMA.DIVISION_GEOGRAFICA3 WITH(NOLOCK) WHERE DIVISION_GEOGRAFICA1=" & provincias & " and  DIVISION_GEOGRAFICA2='" & cantoness & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta_distrito.SelectCommand = cmdconsulta_distrito
            daconsulta_distrito.Fill(dsconsulta_distrito)




            For z = 0 To dsconsulta_distrito.Tables(0).Rows.Count - 1

                ' provincias = dsconsulta_canton.Tables(0).Rows(z).Item("DIVISION_GEOGRAFICA1")
                cmbdistrito.Items.Add(dsconsulta_distrito.Tables(0).Rows(z).Item("NOMBRE"))
                distrito = dsconsulta_distrito.Tables(0).Rows(z).Item("NOMBRE")



            Next

            cmbdistrito.Text = distrito


        End If
    End Sub

    Private Sub cmbdistrito_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbdistrito.SelectedIndexChanged

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub


    Private Sub Btnespacio_Click(sender As Object, e As EventArgs) Handles Btnespacio.Click
        If textfocus = "dependientes" Then
            MsgBox("solo números para el campo de dependientes", MsgBoxStyle.Critical, "Construplaza")
            txtdependientes.Focus()
            'txtdependientes.Text = txtdependientes.Text & "q"
        End If


        If textfocus = "correo" Then
            txtcorreo.Text = txtcorreo.Text & "  "
        End If

        If textfocus = "correo_personal" Then
            txtcorreo_personal.Text = txtcorreo_personal.Text & "  "
        End If

        If textfocus = "telefono1" Then
            MsgBox("solo números para el campo de teléfono de empresa", MsgBoxStyle.Critical, "Construplaza")
            txttelefono1.Focus()

        End If

        If textfocus = "telefono2" Then
            MsgBox("solo números para el campo de teléfono celular", MsgBoxStyle.Critical, "Construplaza")
            txttelefono2.Focus()
        End If
        If textfocus = "telefono3" Then
            MsgBox("solo números para el campo de télefono de emergencia", MsgBoxStyle.Critical, "Construplaza")
            Txttelefono3.Focus()
        End If

        If textfocus = "direccion" Then
            txtdireccion.Text = txtdireccion.Text & "  "
        End If

    End Sub

    Private Sub btnenviar_Click_1(sender As Object, e As EventArgs) Handles btnenviar.Click

        Dim bandera_vacia As Boolean = False

        If txtcorreo.Text = "" Then
            txtcorreo.Text = "_"
        End If

        If txtcorreo_personal.Text = "" Then
            txtcorreo_personal.Text = "_"
        End If


        If txtdependientes.Text = "" Then
            txtdependientes.Text = 0
        End If

        If cmbtipo_sangre.Text = "" Then
            cmbtipo_sangre.Text = "Nulo"
        End If

        If cmbpermiso_conducir.Text = "" Then
            cmbtipo_sangre.Text = "No tiene"
        End If

        If cmbdia_nacimiento.Text = "" Then
            MsgBox("Debe de escoger el día de nacimiento", MsgBoxStyle.Critical, "Construplaza")
            cmbdia_nacimiento.Focus()
            bandera_vacia = True
        End If

        If cmb_mes_nacimiento.Text = "" Then
            MsgBox("Debe de escoger el mes de nacimiento", MsgBoxStyle.Critical, "Construplaza")
            cmb_mes_nacimiento.Focus()
            bandera_vacia = True
        End If


        If cmbano_nacimiento.Text = "" Then
            MsgBox("Debe de escoger el año de nacimiento", MsgBoxStyle.Critical, "Construplaza")
            cmbano_nacimiento.Focus()
            bandera_vacia = True
        End If


        If cmbestado_civil.Text = "" Then
            MsgBox("Debe de escoger su estado civil", MsgBoxStyle.Critical, "Construplaza")
            cmbestado_civil.Focus()
            bandera_vacia = True
        End If


        If txttelefono1.Text = "" Then
            txttelefono1.Text = "25888888"
        End If


        If txttelefono2.Text = "" Then
            txttelefono2.Text = "00000000"
        End If


        If Txttelefono3.Text = "" Then
            Txttelefono3.Text = "00000000"
        End If


        If Cmbprovincia.Text = "" Then
            MsgBox("Debe de escoger la provincia donde vive", MsgBoxStyle.Critical, "Construplaza")
            Cmbprovincia.Focus()
            bandera_vacia = True
        End If



        If cmbcanton.Text = "" Then
            MsgBox("Debe de escoger el cantón donde vive", MsgBoxStyle.Critical, "Construplaza")
            cmbcanton.Focus()
            bandera_vacia = True
        End If



        If cmbdistrito.Text = "" Then
            MsgBox("Debe de escoger el distrito donde vive", MsgBoxStyle.Critical, "Construplaza")
            cmbdistrito.Focus()
            bandera_vacia = True
        End If

        If txtdireccion.Text = "" Then
            MsgBox("Debe de incluir una dirección de su casa", MsgBoxStyle.Critical, "Construplaza")
            txtdireccion.Focus()
            bandera_vacia = True
        End If

        If bandera_vacia = False Then
            Dim fecha_nacimiento_insertar As String
            Dim ESTADO_CIVIL As String
            Dim NACIONALIDAD As String
            Dim PROVINCIA_EMPLEADO As String
            Dim CANTON_EMPLEADO As String
            Dim DISTRITO_EMPLEADO As String

            '--aqui devuelve datos de nombre a codigos


            fecha_nacimiento_insertar = cmbano_nacimiento.Text & "-" & cmb_mes_nacimiento.Text & "-" & cmbdia_nacimiento.Text & " 00:00:00.000"



            If cmbestado_civil.Text = "Casado" Then
                ESTADO_CIVIL = "C"
            End If

            If cmbestado_civil.Text = "Soltero" Then
                ESTADO_CIVIL = "S"
            End If

            If cmbestado_civil.Text = "Divorciado" Then
                ESTADO_CIVIL = "D"
            End If

            If cmbestado_civil.Text = "Unión Libre" Then
                ESTADO_CIVIL = "U"
            End If

            If cmbestado_civil.Text = "Viudo" Then
                ESTADO_CIVIL = "V"
            End If
            If cmbestado_civil.Text = "Otro" Then
                ESTADO_CIVIL = "O"
            End If




            '-- BUSCA PAIS PARA NACIONALIDAD



            cmdconsulta_pais = New SqlClient.SqlCommand
            daconsulta_pais = New SqlClient.SqlDataAdapter
            dsconsulta_pais = New DataSet
            With cmdconsulta_pais
                .Connection = conexion
                .CommandText = "SELECT PAIS FROM EXACTUS.DIGEMA.PAIS WHERE NOMBRE='" & cmbnacionalidad.Text & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta_pais.SelectCommand = cmdconsulta_pais
            daconsulta_pais.Fill(dsconsulta_pais)




            For z = 0 To dsconsulta_pais.Tables(0).Rows.Count - 1

                NACIONALIDAD = dsconsulta_pais.Tables(0).Rows(z).Item("PAIS")


            Next

            '-------------------BUSCA PROVINCIA A ACTUALIZAR-------------------------------------------------------------------------------


            '-- BUSCA PAIS PARA NACIONALIDAD



            cmdconsulta_provincia = New SqlClient.SqlCommand
            daconsulta_provincia = New SqlClient.SqlDataAdapter
            dsconsulta_provincia = New DataSet
            With cmdconsulta_provincia
                .Connection = conexion
                .CommandText = "SELECT DIVISION_GEOGRAFICA1 FROM EXACTUS.DIGEMA.DIVISION_GEOGRAFICA1 WHERE NOMBRE='" & Cmbprovincia.Text & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta_provincia.SelectCommand = cmdconsulta_provincia
            daconsulta_provincia.Fill(dsconsulta_provincia)




            For z = 0 To dsconsulta_provincia.Tables(0).Rows.Count - 1

                PROVINCIA_EMPLEADO = dsconsulta_provincia.Tables(0).Rows(z).Item("DIVISION_GEOGRAFICA1")


            Next


            cmdconsulta_canton = New SqlClient.SqlCommand
            daconsulta_canton = New SqlClient.SqlDataAdapter
            dsconsulta_canton = New DataSet
            With cmdconsulta_canton
                .Connection = conexion
                .CommandText = "SELECT DIVISION_GEOGRAFICA2 FROM EXACTUS.DIGEMA.DIVISION_GEOGRAFICA2 WHERE NOMBRE='" & cmbcanton.Text & "' AND DIVISION_GEOGRAFICA1='" & PROVINCIA_EMPLEADO & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta_canton.SelectCommand = cmdconsulta_canton
            daconsulta_canton.Fill(dsconsulta_canton)




            For z = 0 To dsconsulta_canton.Tables(0).Rows.Count - 1

                CANTON_EMPLEADO = dsconsulta_canton.Tables(0).Rows(z).Item("DIVISION_GEOGRAFICA2")


            Next


            cmdconsulta_distrito = New SqlClient.SqlCommand
            daconsulta_distrito = New SqlClient.SqlDataAdapter
            dsconsulta_distrito = New DataSet
            With cmdconsulta_distrito
                .Connection = conexion
                .CommandText = "SELECT DIVISION_GEOGRAFICA3 FROM EXACTUS.DIGEMA.DIVISION_GEOGRAFICA3 WHERE NOMBRE='" & cmbdistrito.Text & "' AND DIVISION_GEOGRAFICA2='" & CANTON_EMPLEADO & "'"

                .CommandType = CommandType.Text
            End With
            daconsulta_distrito.SelectCommand = cmdconsulta_distrito
            daconsulta_distrito.Fill(dsconsulta_distrito)




            For z = 0 To dsconsulta_distrito.Tables(0).Rows.Count - 1

                DISTRITO_EMPLEADO = cmbdistrito.Text
                'dsconsulta_distrito.Tables(0).Rows(z).Item("DIVISION_GEOGRAFICA3")

            Next




            '-------------actualiza el empleado ------------------------------------------------------

            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
            End If

            transaccion = conexion.BeginTransaction
            cmdincluir = New SqlClient.SqlCommand



            With cmdincluir
                .Connection = conexion
                .CommandText = "UPDATE EXACTUS.DIGEMA.EMPLEADO SET E_MAIL= '" & txtcorreo.Text & "',U_E_MAIL_PERSONAL='" & txtcorreo_personal.Text & "',DEPENDIENTES=" & txtdependientes.Text & ",TIPO_SANGRE='" & cmbtipo_sangre.Text & "',PAIS='" & NACIONALIDAD & "',PERMISO_CONDUCIR='" & cmbpermiso_conducir.Text & "',FECHA_NACIMIENTO='" & fecha_nacimiento_insertar & "',ESTADO_CIVIL='" & ESTADO_CIVIL & "',TELEFONO1='" & txttelefono1.Text & "',TELEFONO2='" & txttelefono2.Text & "',TELEFONO3='" & Txttelefono3.Text & "',DIVISION_GEOGRAFICA1=" & PROVINCIA_EMPLEADO & ",DIVISION_GEOGRAFICA2='" & CANTON_EMPLEADO & "',U_DISTRITO='" & DISTRITO_EMPLEADO & "',DIRECCION_HAB='" & txtdireccion.Text & "' WHERE EMPLEADO='" & txtempleado.Text & "'"
                .CommandType = CommandType.Text
                .Transaction = transaccion
            End With
            iregincluir = cmdincluir.ExecuteNonQuery
            transaccion.Commit()
            MsgBox("Se actualizó el empleado " & txtnombre.Text & " satisfactoriamente", vbInformation, "Construplaza")


            bandera = True
            Me.Close()
            Formmenu.btnactualizacion.Enabled = True

            '--END IF DE BANDERA_VACIA
        End If

        bandera = True
        Formmenu.btnactualizacion.Enabled = True

    End Sub
End Class