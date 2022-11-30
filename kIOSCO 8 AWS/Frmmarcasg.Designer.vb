<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmmarcasg
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.BtnSolicitar = New System.Windows.Forms.Button()
        Me.Btncerrar = New System.Windows.Forms.Button()
        Me.DTFecha_final = New System.Windows.Forms.DateTimePicker()
        Me.DtFecha_inicial = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtnombre = New System.Windows.Forms.TextBox()
        Me.txtempleado = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblvacaciones_pendientes = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.comboBoxSecuLevel_V = New System.Windows.Forms.ComboBox()
        Me.StatusBar = New System.Windows.Forms.Label()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.comboBoxSecuLevel_R = New System.Windows.Forms.ComboBox()
        Me.OpenDeviceBtn = New System.Windows.Forms.Button()
        Me.EnumerateBtn = New System.Windows.Forms.Button()
        Me.GetBtn = New System.Windows.Forms.Button()
        Me.CheckBoxAutoOn = New System.Windows.Forms.CheckBox()
        Me.comboBoxDeviceName = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.BtnSolicitar)
        Me.GroupBox2.Controls.Add(Me.Btncerrar)
        Me.GroupBox2.Location = New System.Drawing.Point(897, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(328, 460)
        Me.GroupBox2.TabIndex = 100
        Me.GroupBox2.TabStop = False
        '
        'BtnSolicitar
        '
        Me.BtnSolicitar.Font = New System.Drawing.Font("Microsoft Sans Serif", 45.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnSolicitar.ForeColor = System.Drawing.Color.White
        Me.BtnSolicitar.Location = New System.Drawing.Point(20, 22)
        Me.BtnSolicitar.Name = "BtnSolicitar"
        Me.BtnSolicitar.Size = New System.Drawing.Size(286, 155)
        Me.BtnSolicitar.TabIndex = 9
        Me.BtnSolicitar.Text = "Solicitar"
        Me.BtnSolicitar.UseVisualStyleBackColor = True
        '
        'Btncerrar
        '
        Me.Btncerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 45.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btncerrar.ForeColor = System.Drawing.Color.White
        Me.Btncerrar.Location = New System.Drawing.Point(15, 289)
        Me.Btncerrar.Name = "Btncerrar"
        Me.Btncerrar.Size = New System.Drawing.Size(291, 154)
        Me.Btncerrar.TabIndex = 8
        Me.Btncerrar.Text = "Cerrar"
        Me.Btncerrar.UseVisualStyleBackColor = True
        '
        'DTFecha_final
        '
        Me.DTFecha_final.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTFecha_final.Location = New System.Drawing.Point(464, 153)
        Me.DTFecha_final.Name = "DTFecha_final"
        Me.DTFecha_final.Size = New System.Drawing.Size(395, 32)
        Me.DTFecha_final.TabIndex = 11
        '
        'DtFecha_inicial
        '
        Me.DtFecha_inicial.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtFecha_inicial.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.DtFecha_inicial.Location = New System.Drawing.Point(22, 153)
        Me.DtFecha_inicial.Name = "DtFecha_inicial"
        Me.DtFecha_inicial.Size = New System.Drawing.Size(390, 32)
        Me.DtFecha_inicial.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(461, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(155, 20)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Día final del permiso:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(19, 119)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(161, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Día inicio del permiso:"
        '
        'txtnombre
        '
        Me.txtnombre.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtnombre.Enabled = False
        Me.txtnombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtnombre.Location = New System.Drawing.Point(271, 38)
        Me.txtnombre.Name = "txtnombre"
        Me.txtnombre.Size = New System.Drawing.Size(588, 32)
        Me.txtnombre.TabIndex = 5
        '
        'txtempleado
        '
        Me.txtempleado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtempleado.Enabled = False
        Me.txtempleado.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtempleado.Location = New System.Drawing.Point(173, 38)
        Me.txtempleado.Name = "txtempleado"
        Me.txtempleado.Size = New System.Drawing.Size(92, 32)
        Me.txtempleado.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(19, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Empleado:"
        '
        'lblvacaciones_pendientes
        '
        Me.lblvacaciones_pendientes.AutoSize = True
        Me.lblvacaciones_pendientes.ForeColor = System.Drawing.Color.White
        Me.lblvacaciones_pendientes.Location = New System.Drawing.Point(19, 434)
        Me.lblvacaciones_pendientes.Name = "lblvacaciones_pendientes"
        Me.lblvacaciones_pendientes.Size = New System.Drawing.Size(191, 20)
        Me.lblvacaciones_pendientes.TabIndex = 2
        Me.lblvacaciones_pendientes.Text = "lblvacaciones_pendientes"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.DTFecha_final)
        Me.GroupBox1.Controls.Add(Me.DtFecha_inicial)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtnombre)
        Me.GroupBox1.Controls.Add(Me.txtempleado)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblvacaciones_pendientes)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.White
        Me.GroupBox1.Location = New System.Drawing.Point(14, 8)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(877, 464)
        Me.GroupBox1.TabIndex = 99
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Solicitud de permisos sin goce y citas médicas"
        '
        'comboBoxSecuLevel_V
        '
        Me.comboBoxSecuLevel_V.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_V.Location = New System.Drawing.Point(1282, 436)
        Me.comboBoxSecuLevel_V.Name = "comboBoxSecuLevel_V"
        Me.comboBoxSecuLevel_V.Size = New System.Drawing.Size(112, 21)
        Me.comboBoxSecuLevel_V.TabIndex = 98
        Me.comboBoxSecuLevel_V.Text = "NORMAL"
        '
        'StatusBar
        '
        Me.StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StatusBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar.ForeColor = System.Drawing.Color.White
        Me.StatusBar.Location = New System.Drawing.Point(0, 543)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(1236, 35)
        Me.StatusBar.TabIndex = 97
        Me.StatusBar.Visible = False
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pictureBox1.Location = New System.Drawing.Point(1311, 10)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(217, 264)
        Me.pictureBox1.TabIndex = 96
        Me.pictureBox1.TabStop = False
        '
        'comboBoxSecuLevel_R
        '
        Me.comboBoxSecuLevel_R.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_R.Location = New System.Drawing.Point(1296, 409)
        Me.comboBoxSecuLevel_R.Name = "comboBoxSecuLevel_R"
        Me.comboBoxSecuLevel_R.Size = New System.Drawing.Size(112, 21)
        Me.comboBoxSecuLevel_R.TabIndex = 94
        Me.comboBoxSecuLevel_R.Text = "NORMAL"
        '
        'OpenDeviceBtn
        '
        Me.OpenDeviceBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.OpenDeviceBtn.Location = New System.Drawing.Point(1364, 290)
        Me.OpenDeviceBtn.Name = "OpenDeviceBtn"
        Me.OpenDeviceBtn.Size = New System.Drawing.Size(80, 24)
        Me.OpenDeviceBtn.TabIndex = 93
        Me.OpenDeviceBtn.Text = "Ini"
        Me.OpenDeviceBtn.UseVisualStyleBackColor = False
        Me.OpenDeviceBtn.Visible = False
        '
        'EnumerateBtn
        '
        Me.EnumerateBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.EnumerateBtn.Location = New System.Drawing.Point(1367, 463)
        Me.EnumerateBtn.Name = "EnumerateBtn"
        Me.EnumerateBtn.Size = New System.Drawing.Size(72, 24)
        Me.EnumerateBtn.TabIndex = 92
        Me.EnumerateBtn.Text = "Enumerate"
        Me.EnumerateBtn.UseVisualStyleBackColor = False
        Me.EnumerateBtn.Visible = False
        '
        'GetBtn
        '
        Me.GetBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GetBtn.Location = New System.Drawing.Point(1298, 290)
        Me.GetBtn.Name = "GetBtn"
        Me.GetBtn.Size = New System.Drawing.Size(96, 24)
        Me.GetBtn.TabIndex = 91
        Me.GetBtn.Text = "Get"
        Me.GetBtn.UseVisualStyleBackColor = False
        '
        'CheckBoxAutoOn
        '
        Me.CheckBoxAutoOn.Enabled = False
        Me.CheckBoxAutoOn.Location = New System.Drawing.Point(1296, 330)
        Me.CheckBoxAutoOn.Name = "CheckBoxAutoOn"
        Me.CheckBoxAutoOn.Size = New System.Drawing.Size(248, 16)
        Me.CheckBoxAutoOn.TabIndex = 90
        Me.CheckBoxAutoOn.Text = "Enable AutoOn Event (FDU03, FDU04)"
        '
        'comboBoxDeviceName
        '
        Me.comboBoxDeviceName.Location = New System.Drawing.Point(1296, 352)
        Me.comboBoxDeviceName.Name = "comboBoxDeviceName"
        Me.comboBoxDeviceName.Size = New System.Drawing.Size(148, 21)
        Me.comboBoxDeviceName.TabIndex = 89
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button1.Location = New System.Drawing.Point(1296, 379)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 24)
        Me.Button1.TabIndex = 95
        Me.Button1.Text = "Get"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'Frmmarcasg
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gray
        Me.ClientSize = New System.Drawing.Size(1236, 578)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.comboBoxSecuLevel_V)
        Me.Controls.Add(Me.StatusBar)
        Me.Controls.Add(Me.pictureBox1)
        Me.Controls.Add(Me.comboBoxSecuLevel_R)
        Me.Controls.Add(Me.OpenDeviceBtn)
        Me.Controls.Add(Me.EnumerateBtn)
        Me.Controls.Add(Me.GetBtn)
        Me.Controls.Add(Me.CheckBoxAutoOn)
        Me.Controls.Add(Me.comboBoxDeviceName)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frmmarcasg"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Frmmarcasg"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnSolicitar As System.Windows.Forms.Button
    Friend WithEvents Btncerrar As System.Windows.Forms.Button
    Friend WithEvents DTFecha_final As System.Windows.Forms.DateTimePicker
    Friend WithEvents DtFecha_inicial As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtnombre As System.Windows.Forms.TextBox
    Friend WithEvents txtempleado As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblvacaciones_pendientes As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents comboBoxSecuLevel_V As System.Windows.Forms.ComboBox
    Friend WithEvents StatusBar As System.Windows.Forms.Label
    Friend WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents comboBoxSecuLevel_R As System.Windows.Forms.ComboBox
    Friend WithEvents OpenDeviceBtn As System.Windows.Forms.Button
    Friend WithEvents EnumerateBtn As System.Windows.Forms.Button
    Friend WithEvents GetBtn As System.Windows.Forms.Button
    Friend WithEvents CheckBoxAutoOn As System.Windows.Forms.CheckBox
    Friend WithEvents comboBoxDeviceName As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
