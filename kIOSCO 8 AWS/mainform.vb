Imports System.Text
Imports SecuGen.FDxSDKPro.Windows
Imports System.Data.SqlClient
Imports System.IO
Imports System.Data
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices





Public Class Form1


    Inherits System.Windows.Forms.Form

    Public cmdconsulta, cmdconsulta_documentos As SqlClient.SqlCommand
    Public transaccion, transaccion_documentos As SqlClient.SqlTransaction
    Public cmdincluir, cmdincluir_documentos As SqlClient.SqlCommand
    Public iregincluir, iregincluird, cod, t, y As Integer
    Public contrarecibo, strconexionex, parametros, asunto, cuerpo, CLIENTE, FECHA, DOCUMENTO, TIPO, MONEDA, CREDITO, NOMBRE As String
    Public MONTO, SALDO As Decimal
    Public conexion As SqlClient.SqlConnection
    Public conexionbi As SqlClient.SqlConnection
    Public daconsulta, daconsulta_documentos As SqlClient.SqlDataAdapter
    Public dsconsulta, dsconsulta_documentos As DataSet
    Public contador As Integer = 130
    Public NombreArchivo As String
    Public contadores As Integer
    Public z As Integer
    Public activado As Integer = 1


    Dim m_FPM As SGFingerPrintManager
    Dim m_LedOn As Boolean
    Dim m_ImageWidth As Int32
    Dim m_ImageHeight As Int32
    Public m_RegMin1(400) As Byte
    Public m_RegMin2(400) As Byte
    Public m_VrfMin(400) As Byte
    Friend WithEvents tabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents BtnVerify As System.Windows.Forms.Button
    Friend WithEvents BtnRegister As System.Windows.Forms.Button
    Friend WithEvents groupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnCapture2 As System.Windows.Forms.Button
    Friend WithEvents BtnCapture1 As System.Windows.Forms.Button
    Friend WithEvents pictureBoxR1 As System.Windows.Forms.PictureBox
    Friend WithEvents pictureBoxR2 As System.Windows.Forms.PictureBox
    Friend WithEvents progressBar_R1 As System.Windows.Forms.ProgressBar
    Friend WithEvents progressBar_R2 As System.Windows.Forms.ProgressBar
    Friend WithEvents comboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents BtnCapture3 As System.Windows.Forms.Button
    Friend WithEvents pictureBoxV1 As System.Windows.Forms.PictureBox
    Friend WithEvents progressBar_V1 As System.Windows.Forms.ProgressBar
    Friend WithEvents comboBoxSecuLevel_R As System.Windows.Forms.ComboBox
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents label14 As System.Windows.Forms.Label
    Friend WithEvents comboBoxSecuLevel_V As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents txtempleado As System.Windows.Forms.TextBox
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents txtnombre As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents chkadministrador As System.Windows.Forms.CheckBox
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Dim m_DevList() As SGFPMDeviceList


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    Property m_leon As Boolean

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents EnumerateBtn As System.Windows.Forms.Button
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents comboBoxDeviceName As System.Windows.Forms.ComboBox
    Friend WithEvents StatusBar As System.Windows.Forms.Label
    Friend WithEvents tabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents CheckBoxAutoOn As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox8 As System.Windows.Forms.GroupBox
    Friend WithEvents BrightnessUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents SetBrightnessBtn As System.Windows.Forms.Button
    Friend WithEvents ConfigBtn As System.Windows.Forms.Button
    Friend WithEvents groupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents textTimeout As System.Windows.Forms.TextBox
    Friend WithEvents label16 As System.Windows.Forms.Label
    Friend WithEvents label15 As System.Windows.Forms.Label
    Friend WithEvents textImgQuality As System.Windows.Forms.TextBox
    Friend WithEvents GetImageBtn As System.Windows.Forms.Button
    Friend WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents tabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents GetBtn As System.Windows.Forms.Button
    Friend WithEvents groupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents textImageDPI As System.Windows.Forms.TextBox
    Friend WithEvents textImageHeight As System.Windows.Forms.TextBox
    Friend WithEvents textImageWidth As System.Windows.Forms.TextBox
    Friend WithEvents textSerialNum As System.Windows.Forms.TextBox
    Friend WithEvents textFWVersion As System.Windows.Forms.TextBox
    Friend WithEvents textDeviceID As System.Windows.Forms.TextBox
    Friend WithEvents textBrightness As System.Windows.Forms.TextBox
    Friend WithEvents textGain As System.Windows.Forms.TextBox
    Friend WithEvents textContrast As System.Windows.Forms.TextBox
    Friend WithEvents label12 As System.Windows.Forms.Label
    Friend WithEvents label11 As System.Windows.Forms.Label
    Friend WithEvents label10 As System.Windows.Forms.Label
    Friend WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents label7 As System.Windows.Forms.Label
    Friend WithEvents label6 As System.Windows.Forms.Label
    Friend WithEvents label5 As System.Windows.Forms.Label
    Friend WithEvents label13 As System.Windows.Forms.Label
    Friend WithEvents OpenDeviceBtn As System.Windows.Forms.Button
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.EnumerateBtn = New System.Windows.Forms.Button()
        Me.label1 = New System.Windows.Forms.Label()
        Me.comboBoxDeviceName = New System.Windows.Forms.ComboBox()
        Me.StatusBar = New System.Windows.Forms.Label()
        Me.tabControl1 = New System.Windows.Forms.TabControl()
        Me.tabPage2 = New System.Windows.Forms.TabPage()
        Me.CheckBoxAutoOn = New System.Windows.Forms.CheckBox()
        Me.GroupBox8 = New System.Windows.Forms.GroupBox()
        Me.BrightnessUpDown = New System.Windows.Forms.NumericUpDown()
        Me.SetBrightnessBtn = New System.Windows.Forms.Button()
        Me.ConfigBtn = New System.Windows.Forms.Button()
        Me.groupBox4 = New System.Windows.Forms.GroupBox()
        Me.textTimeout = New System.Windows.Forms.TextBox()
        Me.label16 = New System.Windows.Forms.Label()
        Me.label15 = New System.Windows.Forms.Label()
        Me.textImgQuality = New System.Windows.Forms.TextBox()
        Me.GetImageBtn = New System.Windows.Forms.Button()
        Me.tabPage1 = New System.Windows.Forms.TabPage()
        Me.GetBtn = New System.Windows.Forms.Button()
        Me.groupBox3 = New System.Windows.Forms.GroupBox()
        Me.textImageDPI = New System.Windows.Forms.TextBox()
        Me.textImageHeight = New System.Windows.Forms.TextBox()
        Me.textImageWidth = New System.Windows.Forms.TextBox()
        Me.textSerialNum = New System.Windows.Forms.TextBox()
        Me.textFWVersion = New System.Windows.Forms.TextBox()
        Me.textDeviceID = New System.Windows.Forms.TextBox()
        Me.textBrightness = New System.Windows.Forms.TextBox()
        Me.textGain = New System.Windows.Forms.TextBox()
        Me.textContrast = New System.Windows.Forms.TextBox()
        Me.label12 = New System.Windows.Forms.Label()
        Me.label11 = New System.Windows.Forms.Label()
        Me.label10 = New System.Windows.Forms.Label()
        Me.label9 = New System.Windows.Forms.Label()
        Me.label8 = New System.Windows.Forms.Label()
        Me.label7 = New System.Windows.Forms.Label()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.label13 = New System.Windows.Forms.Label()
        Me.tabPage3 = New System.Windows.Forms.TabPage()
        Me.BtnVerify = New System.Windows.Forms.Button()
        Me.BtnRegister = New System.Windows.Forms.Button()
        Me.groupBox6 = New System.Windows.Forms.GroupBox()
        Me.groupBox2 = New System.Windows.Forms.GroupBox()
        Me.groupBox1 = New System.Windows.Forms.GroupBox()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.OpenDeviceBtn = New System.Windows.Forms.Button()
        Me.BtnCapture2 = New System.Windows.Forms.Button()
        Me.BtnCapture1 = New System.Windows.Forms.Button()
        Me.pictureBoxR1 = New System.Windows.Forms.PictureBox()
        Me.pictureBoxR2 = New System.Windows.Forms.PictureBox()
        Me.progressBar_R1 = New System.Windows.Forms.ProgressBar()
        Me.progressBar_R2 = New System.Windows.Forms.ProgressBar()
        Me.comboBox1 = New System.Windows.Forms.ComboBox()
        Me.BtnCapture3 = New System.Windows.Forms.Button()
        Me.pictureBoxV1 = New System.Windows.Forms.PictureBox()
        Me.progressBar_V1 = New System.Windows.Forms.ProgressBar()
        Me.comboBoxSecuLevel_R = New System.Windows.Forms.ComboBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.label14 = New System.Windows.Forms.Label()
        Me.comboBoxSecuLevel_V = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.txtempleado = New System.Windows.Forms.TextBox()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Button14 = New System.Windows.Forms.Button()
        Me.txtnombre = New System.Windows.Forms.TextBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.chkadministrador = New System.Windows.Forms.CheckBox()
        Me.Button13 = New System.Windows.Forms.Button()
        Me.tabControl1.SuspendLayout()
        Me.tabPage2.SuspendLayout()
        Me.GroupBox8.SuspendLayout()
        CType(Me.BrightnessUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.groupBox4.SuspendLayout()
        Me.tabPage1.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        Me.tabPage3.SuspendLayout()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBoxR1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBoxR2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBoxV1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox7.SuspendLayout()
        Me.SuspendLayout()
        '
        'EnumerateBtn
        '
        Me.EnumerateBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.EnumerateBtn.Location = New System.Drawing.Point(375, 729)
        Me.EnumerateBtn.Name = "EnumerateBtn"
        Me.EnumerateBtn.Size = New System.Drawing.Size(72, 24)
        Me.EnumerateBtn.TabIndex = 13
        Me.EnumerateBtn.Text = "Enumerate"
        Me.EnumerateBtn.UseVisualStyleBackColor = False
        Me.EnumerateBtn.Visible = False
        '
        'label1
        '
        Me.label1.Location = New System.Drawing.Point(49, 733)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(72, 24)
        Me.label1.TabIndex = 11
        Me.label1.Text = "Device Name"
        Me.label1.Visible = False
        '
        'comboBoxDeviceName
        '
        Me.comboBoxDeviceName.Location = New System.Drawing.Point(129, 730)
        Me.comboBoxDeviceName.Name = "comboBoxDeviceName"
        Me.comboBoxDeviceName.Size = New System.Drawing.Size(148, 21)
        Me.comboBoxDeviceName.TabIndex = 10
        Me.comboBoxDeviceName.Visible = False
        '
        'StatusBar
        '
        Me.StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StatusBar.ForeColor = System.Drawing.Color.White
        Me.StatusBar.Location = New System.Drawing.Point(0, 725)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(952, 34)
        Me.StatusBar.TabIndex = 12
        Me.StatusBar.Visible = False
        '
        'tabControl1
        '
        Me.tabControl1.Controls.Add(Me.tabPage2)
        Me.tabControl1.Controls.Add(Me.tabPage1)
        Me.tabControl1.Controls.Add(Me.tabPage3)
        Me.tabControl1.Location = New System.Drawing.Point(512, 690)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(486, 536)
        Me.tabControl1.TabIndex = 9
        Me.tabControl1.Visible = False
        '
        'tabPage2
        '
        Me.tabPage2.Controls.Add(Me.CheckBoxAutoOn)
        Me.tabPage2.Controls.Add(Me.GroupBox8)
        Me.tabPage2.Controls.Add(Me.ConfigBtn)
        Me.tabPage2.Controls.Add(Me.groupBox4)
        Me.tabPage2.Controls.Add(Me.GetImageBtn)
        Me.tabPage2.Location = New System.Drawing.Point(4, 22)
        Me.tabPage2.Name = "tabPage2"
        Me.tabPage2.Size = New System.Drawing.Size(478, 510)
        Me.tabPage2.TabIndex = 1
        Me.tabPage2.Text = "  Image  "
        '
        'CheckBoxAutoOn
        '
        Me.CheckBoxAutoOn.Enabled = False
        Me.CheckBoxAutoOn.Location = New System.Drawing.Point(3, 491)
        Me.CheckBoxAutoOn.Name = "CheckBoxAutoOn"
        Me.CheckBoxAutoOn.Size = New System.Drawing.Size(248, 16)
        Me.CheckBoxAutoOn.TabIndex = 19
        Me.CheckBoxAutoOn.Text = "Enable AutoOn Event (FDU03, FDU04)"
        '
        'GroupBox8
        '
        Me.GroupBox8.Controls.Add(Me.BrightnessUpDown)
        Me.GroupBox8.Controls.Add(Me.SetBrightnessBtn)
        Me.GroupBox8.Location = New System.Drawing.Point(310, 341)
        Me.GroupBox8.Name = "GroupBox8"
        Me.GroupBox8.Size = New System.Drawing.Size(161, 148)
        Me.GroupBox8.TabIndex = 18
        Me.GroupBox8.TabStop = False
        Me.GroupBox8.Text = "Brightness"
        '
        'BrightnessUpDown
        '
        Me.BrightnessUpDown.Increment = New Decimal(New Integer() {10, 0, 0, 0})
        Me.BrightnessUpDown.Location = New System.Drawing.Point(8, 24)
        Me.BrightnessUpDown.Name = "BrightnessUpDown"
        Me.BrightnessUpDown.Size = New System.Drawing.Size(44, 20)
        Me.BrightnessUpDown.TabIndex = 20
        Me.BrightnessUpDown.Value = New Decimal(New Integer() {70, 0, 0, 0})
        '
        'SetBrightnessBtn
        '
        Me.SetBrightnessBtn.Location = New System.Drawing.Point(56, 24)
        Me.SetBrightnessBtn.Name = "SetBrightnessBtn"
        Me.SetBrightnessBtn.Size = New System.Drawing.Size(56, 20)
        Me.SetBrightnessBtn.TabIndex = 19
        Me.SetBrightnessBtn.Text = "Apply"
        '
        'ConfigBtn
        '
        Me.ConfigBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ConfigBtn.Location = New System.Drawing.Point(395, 18)
        Me.ConfigBtn.Name = "ConfigBtn"
        Me.ConfigBtn.Size = New System.Drawing.Size(76, 24)
        Me.ConfigBtn.TabIndex = 12
        Me.ConfigBtn.Text = "Config..."
        Me.ConfigBtn.UseVisualStyleBackColor = False
        Me.ConfigBtn.Visible = False
        '
        'groupBox4
        '
        Me.groupBox4.Controls.Add(Me.textTimeout)
        Me.groupBox4.Controls.Add(Me.label16)
        Me.groupBox4.Controls.Add(Me.label15)
        Me.groupBox4.Controls.Add(Me.textImgQuality)
        Me.groupBox4.Location = New System.Drawing.Point(310, 48)
        Me.groupBox4.Name = "groupBox4"
        Me.groupBox4.Size = New System.Drawing.Size(161, 287)
        Me.groupBox4.TabIndex = 11
        Me.groupBox4.TabStop = False
        Me.groupBox4.Text = "LiveCapture"
        '
        'textTimeout
        '
        Me.textTimeout.Location = New System.Drawing.Point(8, 80)
        Me.textTimeout.Name = "textTimeout"
        Me.textTimeout.Size = New System.Drawing.Size(88, 20)
        Me.textTimeout.TabIndex = 18
        Me.textTimeout.Text = "10000"
        Me.textTimeout.Visible = False
        '
        'label16
        '
        Me.label16.Location = New System.Drawing.Point(8, 64)
        Me.label16.Name = "label16"
        Me.label16.Size = New System.Drawing.Size(96, 24)
        Me.label16.TabIndex = 17
        Me.label16.Text = "Capture Timeout"
        '
        'label15
        '
        Me.label15.Location = New System.Drawing.Point(8, 20)
        Me.label15.Name = "label15"
        Me.label15.Size = New System.Drawing.Size(96, 16)
        Me.label15.TabIndex = 16
        Me.label15.Text = "Image Quality:"
        '
        'textImgQuality
        '
        Me.textImgQuality.Location = New System.Drawing.Point(8, 36)
        Me.textImgQuality.MaxLength = 3
        Me.textImgQuality.Name = "textImgQuality"
        Me.textImgQuality.Size = New System.Drawing.Size(88, 20)
        Me.textImgQuality.TabIndex = 15
        Me.textImgQuality.Text = "50"
        Me.textImgQuality.Visible = False
        '
        'GetImageBtn
        '
        Me.GetImageBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GetImageBtn.Location = New System.Drawing.Point(303, 18)
        Me.GetImageBtn.Name = "GetImageBtn"
        Me.GetImageBtn.Size = New System.Drawing.Size(75, 24)
        Me.GetImageBtn.TabIndex = 7
        Me.GetImageBtn.Text = "Capture"
        Me.GetImageBtn.UseVisualStyleBackColor = False
        Me.GetImageBtn.Visible = False
        '
        'tabPage1
        '
        Me.tabPage1.Controls.Add(Me.GetBtn)
        Me.tabPage1.Controls.Add(Me.groupBox3)
        Me.tabPage1.Location = New System.Drawing.Point(4, 22)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Size = New System.Drawing.Size(478, 510)
        Me.tabPage1.TabIndex = 0
        Me.tabPage1.Text = "DeviceInfo"
        Me.tabPage1.Visible = False
        '
        'GetBtn
        '
        Me.GetBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GetBtn.Location = New System.Drawing.Point(288, 16)
        Me.GetBtn.Name = "GetBtn"
        Me.GetBtn.Size = New System.Drawing.Size(96, 24)
        Me.GetBtn.TabIndex = 43
        Me.GetBtn.Text = "Get"
        Me.GetBtn.UseVisualStyleBackColor = False
        '
        'groupBox3
        '
        Me.groupBox3.Controls.Add(Me.textImageDPI)
        Me.groupBox3.Controls.Add(Me.textImageHeight)
        Me.groupBox3.Controls.Add(Me.textImageWidth)
        Me.groupBox3.Controls.Add(Me.textSerialNum)
        Me.groupBox3.Controls.Add(Me.textFWVersion)
        Me.groupBox3.Controls.Add(Me.textDeviceID)
        Me.groupBox3.Controls.Add(Me.textBrightness)
        Me.groupBox3.Controls.Add(Me.textGain)
        Me.groupBox3.Controls.Add(Me.textContrast)
        Me.groupBox3.Controls.Add(Me.label12)
        Me.groupBox3.Controls.Add(Me.label11)
        Me.groupBox3.Controls.Add(Me.label10)
        Me.groupBox3.Controls.Add(Me.label9)
        Me.groupBox3.Controls.Add(Me.label8)
        Me.groupBox3.Controls.Add(Me.label7)
        Me.groupBox3.Controls.Add(Me.label6)
        Me.groupBox3.Controls.Add(Me.label5)
        Me.groupBox3.Controls.Add(Me.label13)
        Me.groupBox3.Location = New System.Drawing.Point(8, 8)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(264, 248)
        Me.groupBox3.TabIndex = 41
        Me.groupBox3.TabStop = False
        Me.groupBox3.Text = "DeviceInfo"
        '
        'textImageDPI
        '
        Me.textImageDPI.Enabled = False
        Me.textImageDPI.Location = New System.Drawing.Point(96, 144)
        Me.textImageDPI.Name = "textImageDPI"
        Me.textImageDPI.Size = New System.Drawing.Size(152, 20)
        Me.textImageDPI.TabIndex = 66
        '
        'textImageHeight
        '
        Me.textImageHeight.Enabled = False
        Me.textImageHeight.Location = New System.Drawing.Point(96, 120)
        Me.textImageHeight.Name = "textImageHeight"
        Me.textImageHeight.Size = New System.Drawing.Size(152, 20)
        Me.textImageHeight.TabIndex = 65
        '
        'textImageWidth
        '
        Me.textImageWidth.Enabled = False
        Me.textImageWidth.Location = New System.Drawing.Point(96, 96)
        Me.textImageWidth.Name = "textImageWidth"
        Me.textImageWidth.Size = New System.Drawing.Size(152, 20)
        Me.textImageWidth.TabIndex = 64
        '
        'textSerialNum
        '
        Me.textSerialNum.Enabled = False
        Me.textSerialNum.Location = New System.Drawing.Point(96, 72)
        Me.textSerialNum.Name = "textSerialNum"
        Me.textSerialNum.Size = New System.Drawing.Size(152, 20)
        Me.textSerialNum.TabIndex = 63
        '
        'textFWVersion
        '
        Me.textFWVersion.Enabled = False
        Me.textFWVersion.Location = New System.Drawing.Point(96, 48)
        Me.textFWVersion.Name = "textFWVersion"
        Me.textFWVersion.Size = New System.Drawing.Size(152, 20)
        Me.textFWVersion.TabIndex = 62
        '
        'textDeviceID
        '
        Me.textDeviceID.Enabled = False
        Me.textDeviceID.Location = New System.Drawing.Point(96, 24)
        Me.textDeviceID.Name = "textDeviceID"
        Me.textDeviceID.Size = New System.Drawing.Size(152, 20)
        Me.textDeviceID.TabIndex = 61
        '
        'textBrightness
        '
        Me.textBrightness.Enabled = False
        Me.textBrightness.Location = New System.Drawing.Point(96, 168)
        Me.textBrightness.Name = "textBrightness"
        Me.textBrightness.Size = New System.Drawing.Size(152, 20)
        Me.textBrightness.TabIndex = 58
        '
        'textGain
        '
        Me.textGain.Enabled = False
        Me.textGain.Location = New System.Drawing.Point(96, 216)
        Me.textGain.Name = "textGain"
        Me.textGain.Size = New System.Drawing.Size(152, 20)
        Me.textGain.TabIndex = 57
        '
        'textContrast
        '
        Me.textContrast.Enabled = False
        Me.textContrast.Location = New System.Drawing.Point(96, 192)
        Me.textContrast.Name = "textContrast"
        Me.textContrast.Size = New System.Drawing.Size(152, 20)
        Me.textContrast.TabIndex = 56
        '
        'label12
        '
        Me.label12.Location = New System.Drawing.Point(16, 216)
        Me.label12.Name = "label12"
        Me.label12.Size = New System.Drawing.Size(72, 16)
        Me.label12.TabIndex = 55
        Me.label12.Text = "Gain"
        Me.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label11
        '
        Me.label11.Location = New System.Drawing.Point(16, 192)
        Me.label11.Name = "label11"
        Me.label11.Size = New System.Drawing.Size(72, 16)
        Me.label11.TabIndex = 54
        Me.label11.Text = "Contrast"
        Me.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label10
        '
        Me.label10.Location = New System.Drawing.Point(16, 168)
        Me.label10.Name = "label10"
        Me.label10.Size = New System.Drawing.Size(72, 16)
        Me.label10.TabIndex = 53
        Me.label10.Text = "Brightness"
        Me.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label9
        '
        Me.label9.Location = New System.Drawing.Point(16, 144)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(72, 16)
        Me.label9.TabIndex = 51
        Me.label9.Text = "Image DPI"
        Me.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label8
        '
        Me.label8.Location = New System.Drawing.Point(16, 72)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(72, 16)
        Me.label8.TabIndex = 49
        Me.label8.Text = "Serial #"
        Me.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label7
        '
        Me.label7.Location = New System.Drawing.Point(16, 48)
        Me.label7.Name = "label7"
        Me.label7.Size = New System.Drawing.Size(72, 16)
        Me.label7.TabIndex = 47
        Me.label7.Text = "F/W Version"
        Me.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label6
        '
        Me.label6.Location = New System.Drawing.Point(16, 120)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(72, 16)
        Me.label6.TabIndex = 45
        Me.label6.Text = "Image Height"
        Me.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label5
        '
        Me.label5.Location = New System.Drawing.Point(16, 96)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(72, 16)
        Me.label5.TabIndex = 43
        Me.label5.Text = "Image Width"
        Me.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'label13
        '
        Me.label13.Location = New System.Drawing.Point(16, 24)
        Me.label13.Name = "label13"
        Me.label13.Size = New System.Drawing.Size(72, 16)
        Me.label13.TabIndex = 41
        Me.label13.Text = "Device ID"
        Me.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tabPage3
        '
        Me.tabPage3.Controls.Add(Me.BtnVerify)
        Me.tabPage3.Controls.Add(Me.BtnRegister)
        Me.tabPage3.Controls.Add(Me.groupBox6)
        Me.tabPage3.Controls.Add(Me.groupBox2)
        Me.tabPage3.Controls.Add(Me.groupBox1)
        Me.tabPage3.Location = New System.Drawing.Point(4, 22)
        Me.tabPage3.Name = "tabPage3"
        Me.tabPage3.Size = New System.Drawing.Size(478, 510)
        Me.tabPage3.TabIndex = 2
        Me.tabPage3.Text = "Register/Verify"
        Me.tabPage3.Visible = False
        '
        'BtnVerify
        '
        Me.BtnVerify.BackColor = System.Drawing.SystemColors.Desktop
        Me.BtnVerify.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.BtnVerify.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnVerify.Location = New System.Drawing.Point(280, 308)
        Me.BtnVerify.Name = "BtnVerify"
        Me.BtnVerify.Size = New System.Drawing.Size(108, 23)
        Me.BtnVerify.TabIndex = 34
        Me.BtnVerify.Text = "Verify"
        Me.BtnVerify.UseVisualStyleBackColor = False
        Me.BtnVerify.Visible = False
        '
        'BtnRegister
        '
        Me.BtnRegister.BackColor = System.Drawing.SystemColors.Desktop
        Me.BtnRegister.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.BtnRegister.Location = New System.Drawing.Point(52, 308)
        Me.BtnRegister.Name = "BtnRegister"
        Me.BtnRegister.Size = New System.Drawing.Size(132, 23)
        Me.BtnRegister.TabIndex = 33
        Me.BtnRegister.Text = "Register"
        Me.BtnRegister.UseVisualStyleBackColor = False
        Me.BtnRegister.Visible = False
        '
        'groupBox6
        '
        Me.groupBox6.Location = New System.Drawing.Point(8, 8)
        Me.groupBox6.Name = "groupBox6"
        Me.groupBox6.Size = New System.Drawing.Size(392, 56)
        Me.groupBox6.TabIndex = 30
        Me.groupBox6.TabStop = False
        Me.groupBox6.Visible = False
        '
        'groupBox2
        '
        Me.groupBox2.Location = New System.Drawing.Point(264, 76)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(136, 220)
        Me.groupBox2.TabIndex = 29
        Me.groupBox2.TabStop = False
        Me.groupBox2.Visible = False
        '
        'groupBox1
        '
        Me.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.groupBox1.Location = New System.Drawing.Point(8, 76)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(244, 220)
        Me.groupBox1.TabIndex = 28
        Me.groupBox1.TabStop = False
        Me.groupBox1.Visible = False
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictureBox1.Location = New System.Drawing.Point(12, 12)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(292, 396)
        Me.pictureBox1.TabIndex = 5
        Me.pictureBox1.TabStop = False
        '
        'OpenDeviceBtn
        '
        Me.OpenDeviceBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.OpenDeviceBtn.Location = New System.Drawing.Point(284, 729)
        Me.OpenDeviceBtn.Name = "OpenDeviceBtn"
        Me.OpenDeviceBtn.Size = New System.Drawing.Size(80, 24)
        Me.OpenDeviceBtn.TabIndex = 14
        Me.OpenDeviceBtn.Text = "Ini"
        Me.OpenDeviceBtn.UseVisualStyleBackColor = False
        Me.OpenDeviceBtn.Visible = False
        '
        'BtnCapture2
        '
        Me.BtnCapture2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.BtnCapture2.Location = New System.Drawing.Point(128, 176)
        Me.BtnCapture2.Name = "BtnCapture2"
        Me.BtnCapture2.Size = New System.Drawing.Size(104, 23)
        Me.BtnCapture2.TabIndex = 24
        Me.BtnCapture2.Text = "Capture R2"
        Me.BtnCapture2.UseVisualStyleBackColor = False
        '
        'BtnCapture1
        '
        Me.BtnCapture1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.BtnCapture1.Location = New System.Drawing.Point(8, 176)
        Me.BtnCapture1.Name = "BtnCapture1"
        Me.BtnCapture1.Size = New System.Drawing.Size(104, 23)
        Me.BtnCapture1.TabIndex = 23
        Me.BtnCapture1.Text = "Capture R1"
        Me.BtnCapture1.UseVisualStyleBackColor = False
        '
        'pictureBoxR1
        '
        Me.pictureBoxR1.BackColor = System.Drawing.SystemColors.Window
        Me.pictureBoxR1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictureBoxR1.Location = New System.Drawing.Point(8, 24)
        Me.pictureBoxR1.Name = "pictureBoxR1"
        Me.pictureBoxR1.Size = New System.Drawing.Size(104, 128)
        Me.pictureBoxR1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBoxR1.TabIndex = 26
        Me.pictureBoxR1.TabStop = False
        '
        'pictureBoxR2
        '
        Me.pictureBoxR2.BackColor = System.Drawing.SystemColors.Window
        Me.pictureBoxR2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictureBoxR2.Location = New System.Drawing.Point(128, 24)
        Me.pictureBoxR2.Name = "pictureBoxR2"
        Me.pictureBoxR2.Size = New System.Drawing.Size(104, 128)
        Me.pictureBoxR2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBoxR2.TabIndex = 27
        Me.pictureBoxR2.TabStop = False
        '
        'progressBar_R1
        '
        Me.progressBar_R1.Location = New System.Drawing.Point(8, 152)
        Me.progressBar_R1.Name = "progressBar_R1"
        Me.progressBar_R1.Size = New System.Drawing.Size(104, 12)
        Me.progressBar_R1.TabIndex = 28
        '
        'progressBar_R2
        '
        Me.progressBar_R2.Location = New System.Drawing.Point(128, 152)
        Me.progressBar_R2.Name = "progressBar_R2"
        Me.progressBar_R2.Size = New System.Drawing.Size(104, 12)
        Me.progressBar_R2.TabIndex = 29
        '
        'comboBox1
        '
        Me.comboBox1.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBox1.Location = New System.Drawing.Point(48, -40)
        Me.comboBox1.Name = "comboBox1"
        Me.comboBox1.Size = New System.Drawing.Size(88, 21)
        Me.comboBox1.TabIndex = 30
        '
        'BtnCapture3
        '
        Me.BtnCapture3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.BtnCapture3.Location = New System.Drawing.Point(16, 176)
        Me.BtnCapture3.Name = "BtnCapture3"
        Me.BtnCapture3.Size = New System.Drawing.Size(104, 23)
        Me.BtnCapture3.TabIndex = 27
        Me.BtnCapture3.Text = "Capture V1"
        Me.BtnCapture3.UseVisualStyleBackColor = False
        '
        'pictureBoxV1
        '
        Me.pictureBoxV1.BackColor = System.Drawing.SystemColors.Window
        Me.pictureBoxV1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pictureBoxV1.Location = New System.Drawing.Point(16, 24)
        Me.pictureBoxV1.Name = "pictureBoxV1"
        Me.pictureBoxV1.Size = New System.Drawing.Size(104, 128)
        Me.pictureBoxV1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pictureBoxV1.TabIndex = 29
        Me.pictureBoxV1.TabStop = False
        '
        'progressBar_V1
        '
        Me.progressBar_V1.Location = New System.Drawing.Point(16, 152)
        Me.progressBar_V1.Name = "progressBar_V1"
        Me.progressBar_V1.Size = New System.Drawing.Size(104, 12)
        Me.progressBar_V1.TabIndex = 31
        '
        'comboBoxSecuLevel_R
        '
        Me.comboBoxSecuLevel_R.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_R.Location = New System.Drawing.Point(80, 24)
        Me.comboBoxSecuLevel_R.Name = "comboBoxSecuLevel_R"
        Me.comboBoxSecuLevel_R.Size = New System.Drawing.Size(112, 21)
        Me.comboBoxSecuLevel_R.TabIndex = 21
        '
        'label4
        '
        Me.label4.Location = New System.Drawing.Point(8, 24)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(72, 24)
        Me.label4.TabIndex = 22
        '
        'label14
        '
        Me.label14.Location = New System.Drawing.Point(208, 24)
        Me.label14.Name = "label14"
        Me.label14.Size = New System.Drawing.Size(64, 24)
        Me.label14.TabIndex = 23
        '
        'comboBoxSecuLevel_V
        '
        Me.comboBoxSecuLevel_V.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_V.Location = New System.Drawing.Point(272, 24)
        Me.comboBoxSecuLevel_V.Name = "comboBoxSecuLevel_V"
        Me.comboBoxSecuLevel_V.Size = New System.Drawing.Size(112, 21)
        Me.comboBoxSecuLevel_V.TabIndex = 24
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.White
        Me.Button1.Location = New System.Drawing.Point(12, 23)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(138, 92)
        Me.Button1.TabIndex = 16
        Me.Button1.Text = "1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button2.ForeColor = System.Drawing.Color.White
        Me.Button2.Location = New System.Drawing.Point(156, 23)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(138, 92)
        Me.Button2.TabIndex = 17
        Me.Button2.Text = "2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button3.ForeColor = System.Drawing.Color.White
        Me.Button3.Location = New System.Drawing.Point(300, 23)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(138, 92)
        Me.Button3.TabIndex = 18
        Me.Button3.Text = "3"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button4.ForeColor = System.Drawing.Color.White
        Me.Button4.Location = New System.Drawing.Point(12, 121)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(138, 71)
        Me.Button4.TabIndex = 19
        Me.Button4.Text = "4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button5.ForeColor = System.Drawing.Color.White
        Me.Button5.Location = New System.Drawing.Point(156, 121)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(138, 71)
        Me.Button5.TabIndex = 20
        Me.Button5.Text = "5"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button6.ForeColor = System.Drawing.Color.White
        Me.Button6.Location = New System.Drawing.Point(300, 121)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(138, 71)
        Me.Button6.TabIndex = 21
        Me.Button6.Text = "6"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button7.ForeColor = System.Drawing.Color.White
        Me.Button7.Location = New System.Drawing.Point(12, 199)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(138, 76)
        Me.Button7.TabIndex = 22
        Me.Button7.Text = "7"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button8.ForeColor = System.Drawing.Color.White
        Me.Button8.Location = New System.Drawing.Point(156, 199)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(138, 76)
        Me.Button8.TabIndex = 23
        Me.Button8.Text = "8"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button9.ForeColor = System.Drawing.Color.White
        Me.Button9.Location = New System.Drawing.Point(300, 199)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(138, 76)
        Me.Button9.TabIndex = 24
        Me.Button9.Text = "9"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button10.ForeColor = System.Drawing.Color.White
        Me.Button10.Location = New System.Drawing.Point(156, 281)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(138, 86)
        Me.Button10.TabIndex = 25
        Me.Button10.Text = "0"
        Me.Button10.UseVisualStyleBackColor = True
        '
        'txtempleado
        '
        Me.txtempleado.Enabled = False
        Me.txtempleado.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtempleado.Location = New System.Drawing.Point(616, 357)
        Me.txtempleado.Name = "txtempleado"
        Me.txtempleado.Size = New System.Drawing.Size(142, 26)
        Me.txtempleado.TabIndex = 27
        '
        'Button11
        '
        Me.Button11.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button11.ForeColor = System.Drawing.Color.White
        Me.Button11.Location = New System.Drawing.Point(156, 384)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(138, 86)
        Me.Button11.TabIndex = 28
        Me.Button11.Text = "Limpiar"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.Enabled = False
        Me.Button12.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button12.ForeColor = System.Drawing.Color.White
        Me.Button12.Location = New System.Drawing.Point(12, 385)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(138, 86)
        Me.Button12.TabIndex = 30
        Me.Button12.Text = "Ingresa huella Empleado"
        Me.Button12.UseVisualStyleBackColor = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Button14)
        Me.GroupBox5.Controls.Add(Me.Button12)
        Me.GroupBox5.Controls.Add(Me.Button11)
        Me.GroupBox5.Controls.Add(Me.txtempleado)
        Me.GroupBox5.Controls.Add(Me.Button10)
        Me.GroupBox5.Controls.Add(Me.Button9)
        Me.GroupBox5.Controls.Add(Me.Button8)
        Me.GroupBox5.Controls.Add(Me.Button7)
        Me.GroupBox5.Controls.Add(Me.Button6)
        Me.GroupBox5.Controls.Add(Me.Button5)
        Me.GroupBox5.Controls.Add(Me.Button4)
        Me.GroupBox5.Controls.Add(Me.Button3)
        Me.GroupBox5.Controls.Add(Me.Button2)
        Me.GroupBox5.Controls.Add(Me.Button1)
        Me.GroupBox5.Location = New System.Drawing.Point(310, 68)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(448, 485)
        Me.GroupBox5.TabIndex = 15
        Me.GroupBox5.TabStop = False
        '
        'Button14
        '
        Me.Button14.Enabled = False
        Me.Button14.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button14.ForeColor = System.Drawing.Color.White
        Me.Button14.Location = New System.Drawing.Point(304, 384)
        Me.Button14.Name = "Button14"
        Me.Button14.Size = New System.Drawing.Size(138, 86)
        Me.Button14.TabIndex = 31
        Me.Button14.Text = "Elimina huella Empleado"
        Me.Button14.UseVisualStyleBackColor = False
        '
        'txtnombre
        '
        Me.txtnombre.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtnombre.Enabled = False
        Me.txtnombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnombre.Location = New System.Drawing.Point(6, 19)
        Me.txtnombre.Name = "txtnombre"
        Me.txtnombre.Size = New System.Drawing.Size(436, 26)
        Me.txtnombre.TabIndex = 29
        '
        'GroupBox7
        '
        Me.GroupBox7.Controls.Add(Me.txtnombre)
        Me.GroupBox7.Location = New System.Drawing.Point(310, 8)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(448, 53)
        Me.GroupBox7.TabIndex = 30
        Me.GroupBox7.TabStop = False
        '
        'chkadministrador
        '
        Me.chkadministrador.AutoSize = True
        Me.chkadministrador.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkadministrador.ForeColor = System.Drawing.Color.White
        Me.chkadministrador.Location = New System.Drawing.Point(12, 422)
        Me.chkadministrador.Name = "chkadministrador"
        Me.chkadministrador.Size = New System.Drawing.Size(126, 24)
        Me.chkadministrador.TabIndex = 31
        Me.chkadministrador.Text = "Administrador"
        Me.chkadministrador.UseVisualStyleBackColor = True
        '
        'Button13
        '
        Me.Button13.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button13.ForeColor = System.Drawing.Color.White
        Me.Button13.Location = New System.Drawing.Point(12, 453)
        Me.Button13.Name = "Button13"
        Me.Button13.Size = New System.Drawing.Size(292, 86)
        Me.Button13.TabIndex = 32
        Me.Button13.Text = "Cerrar"
        Me.Button13.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(952, 759)
        Me.Controls.Add(Me.Button13)
        Me.Controls.Add(Me.chkadministrador)
        Me.Controls.Add(Me.GroupBox7)
        Me.Controls.Add(Me.OpenDeviceBtn)
        Me.Controls.Add(Me.EnumerateBtn)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.comboBoxDeviceName)
        Me.Controls.Add(Me.pictureBox1)
        Me.Controls.Add(Me.StatusBar)
        Me.Controls.Add(Me.tabControl1)
        Me.Controls.Add(Me.GroupBox5)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Construplaza"
        Me.tabControl1.ResumeLayout(False)
        Me.tabPage2.ResumeLayout(False)
        Me.GroupBox8.ResumeLayout(False)
        CType(Me.BrightnessUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.groupBox4.ResumeLayout(False)
        Me.groupBox4.PerformLayout()
        Me.tabPage1.ResumeLayout(False)
        Me.groupBox3.ResumeLayout(False)
        Me.groupBox3.PerformLayout()
        Me.tabPage3.ResumeLayout(False)
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBoxR1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBoxR2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBoxV1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

#End Region

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.BackColor = Color.Gray





        m_LedOn = False
        comboBoxSecuLevel_R.SelectedIndex = 4
        comboBoxSecuLevel_V.SelectedIndex = 3
        EnableButtons(False)

        m_FPM = New SGFingerPrintManager
        EnumerateBtn_Click(sender, e)

        OpenDeviceBtn_Click(sender, e)


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

    Private Sub SetLedOnBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        m_LedOn = Not m_LedOn
        m_FPM.SetLedOn(m_LedOn)
    End Sub

    Private Sub GetImageBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetImageBtn.Click
        Dim fp_image() As Byte
        Dim iError As Int32
        Dim elap_time As Int32

        ReDim fp_image(m_ImageWidth * m_ImageHeight)

        elap_time = Environment.TickCount

        iError = m_FPM.GetImage(fp_image)

        If (iError = SGFPMError.ERROR_NONE) Then
            elap_time = Environment.TickCount - elap_time
            DrawImage(fp_image, pictureBox1)
            StatusBar.Text = "Capture Time : " + Convert.ToString(elap_time) + " ms"
        Else
            DisplayError("GetImage", iError)
        End If

    End Sub

    'Private Sub GetLiveImageBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetLiveImageBtn.Click



    '    If txtnombre.Text.Trim.Length > 0 Then

    '        Dim fp_image() As Byte
    '        Dim iError As Int32
    '        Dim img_qlty As Int32
    '        ReDim fp_image(m_ImageWidth * m_ImageHeight)

    '        iError = m_FPM.GetImage(fp_image)

    '        If iError = SGFPMError.ERROR_NONE Then
    '            DrawImage(fp_image, pictureBox1)
    '            m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, img_qlty)
    '            progressBar_R1.Value = img_qlty
    '            iError = m_FPM.CreateTemplate(fp_image, m_RegMin1)

    '            If (iError = SGFPMError.ERROR_NONE) Then
    '                StatusBar.Text = "First image is captured"
    '            Else
    '                DisplayError("CreateTemplate", iError)
    '            End If
    '        Else
    '            DisplayError("GetImage", iError)
    '        End If

    '    Else

    '        MsgBox("Debe de digitar un # de empleado", MsgBoxStyle.Critical)

    '    End If



    'End Sub

    Private Sub ConfigBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConfigBtn.Click
        m_FPM.Configure(Handle.ToInt32())
    End Sub

    Private Sub BtnCapture1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCapture1.Click
        Dim fp_image() As Byte
        Dim iError As Int32
        Dim img_qlty As Int32
        ReDim fp_image(m_ImageWidth * m_ImageHeight)

        iError = m_FPM.GetImage(fp_image)

        If iError = SGFPMError.ERROR_NONE Then
            DrawImage(fp_image, pictureBoxR1)
            m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, img_qlty)
            progressBar_R1.Value = img_qlty
            iError = m_FPM.CreateTemplate(fp_image, m_RegMin1)

            If (iError = SGFPMError.ERROR_NONE) Then
                StatusBar.Text = "First image is captured"
            Else
                DisplayError("CreateTemplate", iError)
            End If
        Else
            DisplayError("GetImage", iError)
        End If

    End Sub

    Private Sub BtnCapture2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCapture2.Click
        Dim fp_image() As Byte
        Dim iError As Int32
        Dim img_qlty As Int32
        ReDim fp_image(m_ImageWidth * m_ImageHeight)

        iError = m_FPM.GetImage(fp_image)

        If iError = SGFPMError.ERROR_NONE Then
            DrawImage(fp_image, pictureBoxR2)
            m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, img_qlty)
            progressBar_R2.Value = img_qlty
            iError = m_FPM.CreateTemplate(fp_image, m_RegMin2)

            If (iError = SGFPMError.ERROR_NONE) Then
                StatusBar.Text = "Second image  is captured"
            Else
                DisplayError("CreateTemplate", iError)
            End If
        Else
            DisplayError("GetImage", iError)
        End If


    End Sub

    Private Sub BtnCapture3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCapture3.Click
        Dim fp_image() As Byte
        Dim iError As Int32
        Dim img_qlty As Int32
        ReDim fp_image(m_ImageWidth * m_ImageHeight)

        iError = m_FPM.GetImage(fp_image)

        If iError = SGFPMError.ERROR_NONE Then
            DrawImage(fp_image, pictureBoxV1)
            m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, img_qlty)
            progressBar_V1.Value = img_qlty
            iError = m_FPM.CreateTemplate(fp_image, m_VrfMin)

            If (iError = SGFPMError.ERROR_NONE) Then
                StatusBar.Text = "Image for verification is captured"
            Else
                DisplayError("CreateTemplate", iError)
            End If
        Else
            DisplayError("GetImage", iError)
        End If

    End Sub

    Private Sub BtnRegister_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRegister.Click
        Dim iError As Int32
        Dim matched As Boolean
        Dim match_score As Int32
        Dim secu_level As SGFPMSecurityLevel


        matched = False
        match_score = 0

        secu_level = comboBoxSecuLevel_R.SelectedIndex

        iError = m_FPM.MatchTemplate(m_RegMin1, m_RegMin2, secu_level, matched)
        iError = m_FPM.GetMatchingScore(m_RegMin1, m_RegMin2, match_score)

        If (iError = SGFPMError.ERROR_NONE) Then
            If (matched) Then
                StatusBar.Text = "Registration Success, Matching Score: " + Convert.ToString(match_score)
            Else
                StatusBar.Text = "Registration Failed"
            End If

        Else
            DisplayError("GetMatchingScore", iError)
        End If

    End Sub

    Private Sub BtnVerify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnVerify.Click
        Dim iError As Int32
        Dim matched1 As Boolean
        Dim matched2 As Boolean
        Dim secu_level As SGFPMSecurityLevel

        secu_level = comboBoxSecuLevel_V.SelectedIndex

        iError = m_FPM.MatchTemplate(m_RegMin1, m_VrfMin, secu_level, matched1)
        iError = m_FPM.MatchTemplate(m_RegMin2, m_VrfMin, secu_level, matched2)

        If (iError = SGFPMError.ERROR_NONE) Then
            If (matched1 And matched2) Then
                StatusBar.Text = "Verification Success"
            Else
                StatusBar.Text = "Verification Failed"
            End If

        Else
            DisplayError("MatchTemplate", iError)
        End If

    End Sub

    Private Sub GetBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GetBtn.Click
        Dim pInfo As SGFPMDeviceInfoParam
        Dim iError As Int32

        pInfo = New SGFPMDeviceInfoParam
        iError = m_FPM.GetDeviceInfo(pInfo)

        If (iError = SGFPMError.ERROR_NONE) Then
            m_ImageWidth = pInfo.ImageWidth
            m_ImageHeight = pInfo.ImageHeight

            textDeviceID.Text = Convert.ToString(pInfo.DeviceID)
            textFWVersion.Text = Convert.ToString(pInfo.FWVersion, 16)

            'Device Serial Number
            Dim encoding As New ASCIIEncoding
            textSerialNum.Text = encoding.GetString(pInfo.DeviceSN)

            textImageDPI.Text = Convert.ToString(pInfo.ImageDPI)
            textImageHeight.Text = Convert.ToString(pInfo.ImageHeight)
            textImageWidth.Text = Convert.ToString(pInfo.ImageWidth)
            textBrightness.Text = Convert.ToString(pInfo.Brightness)
            textContrast.Text = Convert.ToString(pInfo.Contrast)
            textGain.Text = Convert.ToString(pInfo.Gain)

            BrightnessUpDown.Value = pInfo.Brightness
        End If


    End Sub

    Private Sub SetBrightnessBtn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetBrightnessBtn.Click
        Dim iError As Int32
        Dim brightness As Int32

        brightness = BrightnessUpDown.Value
        iError = m_FPM.SetBrightness(brightness)
        If (iError = SGFPMError.ERROR_NONE) Then
            StatusBar.Text = "SetBrightness success"
            GetBtn_Click(sender, e)
        Else
            DisplayError("SetBrightness", iError)
        End If

    End Sub

    Private Sub EnableButtons(ByVal enable As Boolean)
        ConfigBtn.Enabled = enable
        GetImageBtn.Enabled = enable
        '  GetLiveImageBtn.Enabled = enable
        BtnCapture1.Enabled = enable
        BtnCapture2.Enabled = enable
        BtnCapture3.Enabled = enable
        BtnRegister.Enabled = enable
        BtnVerify.Enabled = enable
        GetBtn.Enabled = enable
        SetBrightnessBtn.Enabled = enable
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

    Private Sub CheckBoxAutoOn_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxAutoOn.CheckedChanged
        If (CheckBoxAutoOn.Checked) Then
            m_FPM.EnableAutoOnEvent(True, Me.Handle.ToInt32())
        Else
            m_FPM.EnableAutoOnEvent(False, 0)
        End If
    End Sub


    Protected Overrides Sub WndProc(ByRef msg As Message)
        If (msg.Msg = SGFPMMessages.DEV_AUTOONEVENT) Then
            If (msg.WParam.ToInt32() = SGFPMAutoOnEvent.FINGER_ON) Then
                StatusBar.Text = "Device Message: Finger On"
            ElseIf (msg.WParam.ToInt32() = SGFPMAutoOnEvent.FINGER_OFF) Then
                StatusBar.Text = "Device Message: Finger Off"
            End If
        End If

        MyBase.WndProc(msg)
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







    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        contadores = contadores + 1
        label1.Text = contadores

        txtempleado.Text = txtempleado.Text & Button1.Text
        If contadores >= 4 Then
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
            Button12.Enabled = True
            Button14.Enabled = True



            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleado.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next


        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        contadores = contadores + 1
        label1.Text = contadores

        txtempleado.Text = txtempleado.Text & Button2.Text
        If contadores >= 4 Then
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
            Button12.Enabled = True
            Button14.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleado.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next

        End If

    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        contadores = contadores + 1
        label1.Text = contadores

        txtempleado.Text = txtempleado.Text & Button3.Text
        If contadores >= 4 Then
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
            Button12.Enabled = True
            Button14.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleado.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next


        End If

    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        contadores = contadores + 1
        label1.Text = contadores

        txtempleado.Text = txtempleado.Text & Button4.Text
        If contadores >= 4 Then
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
            Button12.Enabled = True
            Button14.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleado.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next



        End If

    End Sub
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        contadores = contadores + 1
        label1.Text = contadores

        txtempleado.Text = txtempleado.Text & Button5.Text
        If contadores >= 4 Then
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
            Button12.Enabled = True
            Button14.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleado.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next


        End If

    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        contadores = contadores + 1
        label1.Text = contadores

        txtempleado.Text = txtempleado.Text & Button6.Text
        If contadores >= 4 Then
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
            Button12.Enabled = True
            Button14.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleado.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next

        End If

    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        contadores = contadores + 1
        label1.Text = contadores

        txtempleado.Text = txtempleado.Text & Button7.Text
        If contadores >= 4 Then
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
            Button12.Enabled = True
            Button14.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleado.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next


        End If

    End Sub
    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        contadores = contadores + 1
        label1.Text = contadores

        txtempleado.Text = txtempleado.Text & Button8.Text
        If contadores >= 4 Then
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
            Button12.Enabled = True
            Button14.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleado.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next


        End If

    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        contadores = contadores + 1
        label1.Text = contadores

        txtempleado.Text = txtempleado.Text & Button9.Text
        If contadores >= 4 Then
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
            Button12.Enabled = True
            Button14.Enabled = True


            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleado.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next

        End If

    End Sub
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        contadores = contadores + 1
        label1.Text = contadores

        txtempleado.Text = txtempleado.Text & Button10.Text
        If contadores >= 4 Then
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
            Button12.Enabled = True
            Button14.Enabled = True



            cmdconsulta = New SqlClient.SqlCommand
            daconsulta = New SqlClient.SqlDataAdapter
            dsconsulta = New DataSet
            With cmdconsulta
                .Connection = conexion
                .CommandText = "SELECT NOMBRE FROM EXACTUS.digema.EMPLEADO WITH(NOLOCK) WHERE EMPLEADO='" & txtempleado.Text & "' "
                .CommandType = CommandType.Text
            End With
            daconsulta.SelectCommand = cmdconsulta
            daconsulta.Fill(dsconsulta)


            For z = 0 To dsconsulta.Tables(0).Rows.Count - 1
                txtnombre.Text = dsconsulta.Tables(0).Rows(z).Item("NOMBRE")
            Next


        End If

    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
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
        Button12.Enabled = False
        Button14.Enabled = False


        pictureBox1.Image = Nothing
        pictureBoxR1.Image = Nothing
        pictureBoxR2.Image = Nothing
        pictureBoxV1.Image = Nothing
        contadores = 0


    End Sub

    Shared Function Imagen_Bytes(ByVal Foto As Image) As Byte()
        If Not Foto Is Nothing Then
            Dim Codi As New IO.MemoryStream
            Foto.Save(Codi, Imaging.ImageFormat.Jpeg)
            Return Codi.GetBuffer
        Else
            Return Nothing
        End If
    End Function


    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click

        Dim bandera As Boolean = False
        Dim i As Integer
        Dim bandera_error As Boolean = False



        '  System.Threading.Thread.Sleep(1000)



        '---------------------------------------------------------------------------------------




        cmdconsulta = New SqlClient.SqlCommand
        daconsulta = New SqlClient.SqlDataAdapter
        dsconsulta = New DataSet
        With cmdconsulta
            .Connection = conexion
            .CommandText = "select EMPLEADO from ASISTENCIA.DBO.EMPLEADO_HUELLA WITH(NOLOCK) where EMPLEADO='" & txtempleado.Text & "'"
            .CommandType = CommandType.Text
        End With
        daconsulta.SelectCommand = cmdconsulta
        daconsulta.Fill(dsconsulta)
        For i = 0 To dsconsulta.Tables(0).Rows.Count - 1
            If dsconsulta.Tables(0).Rows(i).Item("EMPLEADO") = txtempleado.Text Then

                bandera = True
            End If

        Next




        '   bandera_error = False



        If txtnombre.Text.Trim.Length > 0 Then

            Dim fp_image() As Byte
            Dim iError As Int32
            Dim img_qlty As Int32
            ReDim fp_image(m_ImageWidth * m_ImageHeight)

            iError = m_FPM.GetImage(fp_image)

            If iError = SGFPMError.ERROR_NONE Then
                DrawImage(fp_image, pictureBox1)
                m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, img_qlty)
                progressBar_R1.Value = img_qlty
                iError = m_FPM.CreateTemplate(fp_image, m_RegMin1)

                If (iError = SGFPMError.ERROR_NONE) Then
                    StatusBar.Text = "First image is captured"
                Else
                    bandera = True
                    DisplayError("CreateTemplate", iError)
                    bandera_error = True
                End If
            Else
                bandera = True
                DisplayError("GetImage", iError)
                bandera_error = True
            End If

        Else

            MsgBox("Debe de digitar un # de empleado", MsgBoxStyle.Critical)

        End If





        If bandera = False And txtnombre.Text.Trim.Length > 0 And bandera_error = False Then

            'Dim vFoto As New Bitmap(pictureBox1.Width, pictureBox1.Height)
            'pictureBox1.DrawToBitmap(vFoto, New Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height))
            'vFoto.Save("C:\huella.jpg", Imaging.ImageFormat.Jpeg)


            Dim fi As New System.IO.FileInfo("c:\huella.JPG")
            Dim fs As System.IO.FileStream = fi.OpenRead

            Dim lBytes As Long = fs.Length
            Dim myImage(lBytes) As Byte

            fs.Read(myImage, 0, lBytes)

            fs.Close()



            ' Insert binary data into database
            Using myConn As New Data.SqlClient.SqlConnection("Server=192.168.101.215\BDAPP01;Database=ASISTENCIA;User Id=SA;Password=Igoloncet!2;")
                Dim myCommand As New Data.SqlClient.SqlCommand("INSERT INTO ASISTENCIA.DBO.EMPLEADO_HUELLA(EMPLEADO,IMAGEN_1,IMAGEN_2,TEMPLATE_1,TEMPLATE_2) VALUES (@empleado,@img_data,@img_data2,@img_template1,@img_template2)", myConn)
                myCommand.Parameters.AddWithValue("@img_data", myImage)
                myCommand.Parameters.AddWithValue("@img_data2", myImage)
                myCommand.Parameters.AddWithValue("@img_template1", m_RegMin1)
                myCommand.Parameters.AddWithValue("@img_template2", m_RegMin1)
                myCommand.Parameters.Add("@empleado", SqlDbType.NVarChar).Value = txtempleado.Text



                If chkadministrador.Checked = True Then

                    If conexion.State = ConnectionState.Closed Then
                        conexion.Open()
                    End If

                    transaccion = conexion.BeginTransaction
                    cmdincluir = New SqlClient.SqlCommand

                    parametros = "'" & txtempleado.Text & "'," & activado & ""

                    With cmdincluir
                        .Connection = conexion
                        .CommandText = "INSERT INTO ASISTENCIA.DBO.SUPERVISOR(EMPLEADO,ACTIVO) values (" & parametros & ")"
                        .CommandType = CommandType.Text
                        .Transaction = transaccion
                    End With
                    iregincluir = cmdincluir.ExecuteNonQuery
                    transaccion.Commit()




                End If



                myConn.Open()
                myCommand.ExecuteNonQuery()
                myConn.Close()
            End Using




            MsgBox("Se ingreso satisfactoriamente el empleado:  " & txtempleado.Text & "   " & txtnombre.Text, MsgBoxStyle.Information, "Construplaza")




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
            '  Button12.Enabled = False
            '    Button12.Enabled = False

            pictureBox1.Image = Nothing
            pictureBoxR1.Image = Nothing
            pictureBoxR2.Image = Nothing
            pictureBoxV1.Image = Nothing
            contadores = 0



        End If


        If bandera = True Or txtnombre.Text.Trim.Length = 0 And contadores <> 0 Or bandera_error = True Then
            MsgBox("NO SE Ingreso el empleado:  " & txtempleado.Text & "   " & txtnombre.Text & "   " & " ya se ecuentra incluido o la huella no es clara", MsgBoxStyle.Critical, "Construplaza")
        End If


  

    End Sub

    Private Function ReadBinaryFile(image As Image) As Byte()
        Throw New NotImplementedException
    End Function


    Private Sub GroupBox5_Enter(sender As Object, e As EventArgs) Handles GroupBox5.Enter

    End Sub


    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        conexion.Close()

        Me.Close()

        Formmenu.BackColor = Color.Gray
        Formmenu.btnIngresar.BackColor = Color.Gray
        Formmenu.btnmarcar.BackColor = Color.Gray
        Formmenu.Button1.BackColor = Color.Gray

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click

        If conexion.State = ConnectionState.Closed Then
            conexion.Open()
        End If

        transaccion = conexion.BeginTransaction
        cmdincluir = New SqlClient.SqlCommand

        '  parametros = "'" & txtempleado.Text & "'," & activado & ""


        'ASISTENCIA.DBO.SUPERVISOR

        With cmdincluir
            .Connection = conexion
            .CommandText = "DELETE FROM ASISTENCIA.DBO.SUPERVISOR WHERE EMPLEADO='" & txtempleado.Text & "'"
            .CommandType = CommandType.Text
            .Transaction = transaccion
        End With
        iregincluir = cmdincluir.ExecuteNonQuery
        transaccion.Commit()



        '---------------------------------------------------------------------------------------------------------------------------------------


        If conexion.State = ConnectionState.Closed Then
            conexion.Open()
        End If

        transaccion = conexion.BeginTransaction
        cmdincluir = New SqlClient.SqlCommand

        '  parametros = "'" & txtempleado.Text & "'," & activado & ""


        'ASISTENCIA.DBO.SUPERVISOR

        With cmdincluir
            .Connection = conexion
            .CommandText = "DELETE FROM ASISTENCIA.DBO.EMPLEADO_HUELLA WHERE EMPLEADO='" & txtempleado.Text & "'"
            .CommandType = CommandType.Text
            .Transaction = transaccion
        End With
        iregincluir = cmdincluir.ExecuteNonQuery
        transaccion.Commit()


        Button14.Enabled = False
        MsgBox("Huella del empleado " & txtempleado.Text & " eliminada")
        Button11_Click(sender, e)




    End Sub
End Class
