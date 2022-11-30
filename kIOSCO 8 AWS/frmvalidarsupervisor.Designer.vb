<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmvalidarsupervisor
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
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.comboBoxSecuLevel_V = New System.Windows.Forms.ComboBox()
        Me.comboBoxSecuLevel_R = New System.Windows.Forms.ComboBox()
        Me.OpenDeviceBtn = New System.Windows.Forms.Button()
        Me.EnumerateBtn = New System.Windows.Forms.Button()
        Me.GetBtn = New System.Windows.Forms.Button()
        Me.CheckBoxAutoOn = New System.Windows.Forms.CheckBox()
        Me.comboBoxDeviceName = New System.Windows.Forms.ComboBox()
        Me.StatusBar = New System.Windows.Forms.Label()
        Me.btningresar = New System.Windows.Forms.Button()
        Me.btnsalir = New System.Windows.Forms.Button()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pictureBox1.Location = New System.Drawing.Point(27, 19)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(303, 386)
        Me.pictureBox1.TabIndex = 8
        Me.pictureBox1.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button1.Location = New System.Drawing.Point(597, 238)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 24)
        Me.Button1.TabIndex = 64
        Me.Button1.Text = "Get"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'comboBoxSecuLevel_V
        '
        Me.comboBoxSecuLevel_V.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_V.Location = New System.Drawing.Point(640, 40)
        Me.comboBoxSecuLevel_V.Name = "comboBoxSecuLevel_V"
        Me.comboBoxSecuLevel_V.Size = New System.Drawing.Size(112, 21)
        Me.comboBoxSecuLevel_V.TabIndex = 63
        Me.comboBoxSecuLevel_V.Text = "NORMAL"
        '
        'comboBoxSecuLevel_R
        '
        Me.comboBoxSecuLevel_R.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_R.Location = New System.Drawing.Point(606, 298)
        Me.comboBoxSecuLevel_R.Name = "comboBoxSecuLevel_R"
        Me.comboBoxSecuLevel_R.Size = New System.Drawing.Size(112, 21)
        Me.comboBoxSecuLevel_R.TabIndex = 62
        Me.comboBoxSecuLevel_R.Text = "NORMAL"
        '
        'OpenDeviceBtn
        '
        Me.OpenDeviceBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.OpenDeviceBtn.Location = New System.Drawing.Point(638, 88)
        Me.OpenDeviceBtn.Name = "OpenDeviceBtn"
        Me.OpenDeviceBtn.Size = New System.Drawing.Size(80, 24)
        Me.OpenDeviceBtn.TabIndex = 61
        Me.OpenDeviceBtn.Text = "Ini"
        Me.OpenDeviceBtn.UseVisualStyleBackColor = False
        Me.OpenDeviceBtn.Visible = False
        '
        'EnumerateBtn
        '
        Me.EnumerateBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.EnumerateBtn.Location = New System.Drawing.Point(644, 89)
        Me.EnumerateBtn.Name = "EnumerateBtn"
        Me.EnumerateBtn.Size = New System.Drawing.Size(72, 24)
        Me.EnumerateBtn.TabIndex = 60
        Me.EnumerateBtn.Text = "Enumerate"
        Me.EnumerateBtn.UseVisualStyleBackColor = False
        Me.EnumerateBtn.Visible = False
        '
        'GetBtn
        '
        Me.GetBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GetBtn.Location = New System.Drawing.Point(649, 130)
        Me.GetBtn.Name = "GetBtn"
        Me.GetBtn.Size = New System.Drawing.Size(96, 24)
        Me.GetBtn.TabIndex = 59
        Me.GetBtn.Text = "Get"
        Me.GetBtn.UseVisualStyleBackColor = False
        '
        'CheckBoxAutoOn
        '
        Me.CheckBoxAutoOn.Enabled = False
        Me.CheckBoxAutoOn.Location = New System.Drawing.Point(624, 167)
        Me.CheckBoxAutoOn.Name = "CheckBoxAutoOn"
        Me.CheckBoxAutoOn.Size = New System.Drawing.Size(248, 16)
        Me.CheckBoxAutoOn.TabIndex = 58
        Me.CheckBoxAutoOn.Text = "Enable AutoOn Event (FDU03, FDU04)"
        '
        'comboBoxDeviceName
        '
        Me.comboBoxDeviceName.Location = New System.Drawing.Point(597, 189)
        Me.comboBoxDeviceName.Name = "comboBoxDeviceName"
        Me.comboBoxDeviceName.Size = New System.Drawing.Size(148, 21)
        Me.comboBoxDeviceName.TabIndex = 57
        '
        'StatusBar
        '
        Me.StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StatusBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar.ForeColor = System.Drawing.Color.White
        Me.StatusBar.Location = New System.Drawing.Point(0, 433)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(551, 35)
        Me.StatusBar.TabIndex = 65
        Me.StatusBar.Visible = False
        '
        'btningresar
        '
        Me.btningresar.Enabled = False
        Me.btningresar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btningresar.ForeColor = System.Drawing.Color.White
        Me.btningresar.Location = New System.Drawing.Point(349, 19)
        Me.btningresar.Name = "btningresar"
        Me.btningresar.Size = New System.Drawing.Size(190, 167)
        Me.btningresar.TabIndex = 66
        Me.btningresar.Text = "Ingresar empleados"
        Me.btningresar.UseVisualStyleBackColor = True
        '
        'btnsalir
        '
        Me.btnsalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnsalir.ForeColor = System.Drawing.Color.White
        Me.btnsalir.Location = New System.Drawing.Point(349, 236)
        Me.btnsalir.Name = "btnsalir"
        Me.btnsalir.Size = New System.Drawing.Size(190, 169)
        Me.btnsalir.TabIndex = 67
        Me.btnsalir.Text = "Salir"
        Me.btnsalir.UseVisualStyleBackColor = True
        '
        'frmvalidarsupervisor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gray
        Me.ClientSize = New System.Drawing.Size(551, 468)
        Me.Controls.Add(Me.btnsalir)
        Me.Controls.Add(Me.btningresar)
        Me.Controls.Add(Me.StatusBar)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.comboBoxSecuLevel_V)
        Me.Controls.Add(Me.comboBoxSecuLevel_R)
        Me.Controls.Add(Me.OpenDeviceBtn)
        Me.Controls.Add(Me.EnumerateBtn)
        Me.Controls.Add(Me.GetBtn)
        Me.Controls.Add(Me.CheckBoxAutoOn)
        Me.Controls.Add(Me.comboBoxDeviceName)
        Me.Controls.Add(Me.pictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmvalidarsupervisor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmvalidarsupervisor"
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents comboBoxSecuLevel_V As System.Windows.Forms.ComboBox
    Friend WithEvents comboBoxSecuLevel_R As System.Windows.Forms.ComboBox
    Friend WithEvents OpenDeviceBtn As System.Windows.Forms.Button
    Friend WithEvents EnumerateBtn As System.Windows.Forms.Button
    Friend WithEvents GetBtn As System.Windows.Forms.Button
    Friend WithEvents CheckBoxAutoOn As System.Windows.Forms.CheckBox
    Friend WithEvents comboBoxDeviceName As System.Windows.Forms.ComboBox
    Friend WithEvents StatusBar As System.Windows.Forms.Label
    Friend WithEvents btningresar As System.Windows.Forms.Button
    Friend WithEvents btnsalir As System.Windows.Forms.Button
End Class
