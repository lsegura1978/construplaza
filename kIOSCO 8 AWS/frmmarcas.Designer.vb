<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmmarcas
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
        Me.btncerrar = New System.Windows.Forms.Button()
        Me.comboBoxSecuLevel_V = New System.Windows.Forms.ComboBox()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.comboBoxSecuLevel_R = New System.Windows.Forms.ComboBox()
        Me.OpenDeviceBtn = New System.Windows.Forms.Button()
        Me.EnumerateBtn = New System.Windows.Forms.Button()
        Me.GetBtn = New System.Windows.Forms.Button()
        Me.CheckBoxAutoOn = New System.Windows.Forms.CheckBox()
        Me.comboBoxDeviceName = New System.Windows.Forms.ComboBox()
        Me.StatusBar = New System.Windows.Forms.Label()
        Me.ListView1 = New System.Windows.Forms.ListView()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btncerrar
        '
        Me.btncerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 28.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncerrar.ForeColor = System.Drawing.Color.White
        Me.btncerrar.Location = New System.Drawing.Point(892, 774)
        Me.btncerrar.Name = "btncerrar"
        Me.btncerrar.Size = New System.Drawing.Size(179, 97)
        Me.btncerrar.TabIndex = 0
        Me.btncerrar.Text = "Cerrar"
        Me.btncerrar.UseVisualStyleBackColor = True
        '
        'comboBoxSecuLevel_V
        '
        Me.comboBoxSecuLevel_V.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_V.Location = New System.Drawing.Point(1338, 586)
        Me.comboBoxSecuLevel_V.Name = "comboBoxSecuLevel_V"
        Me.comboBoxSecuLevel_V.Size = New System.Drawing.Size(112, 21)
        Me.comboBoxSecuLevel_V.TabIndex = 95
        Me.comboBoxSecuLevel_V.Text = "NORMAL"
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pictureBox1.Location = New System.Drawing.Point(1386, 191)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(141, 200)
        Me.pictureBox1.TabIndex = 94
        Me.pictureBox1.TabStop = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button1.Location = New System.Drawing.Point(1341, 582)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 24)
        Me.Button1.TabIndex = 93
        Me.Button1.Text = "Get"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'comboBoxSecuLevel_R
        '
        Me.comboBoxSecuLevel_R.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_R.Location = New System.Drawing.Point(731, 418)
        Me.comboBoxSecuLevel_R.Name = "comboBoxSecuLevel_R"
        Me.comboBoxSecuLevel_R.Size = New System.Drawing.Size(112, 21)
        Me.comboBoxSecuLevel_R.TabIndex = 92
        Me.comboBoxSecuLevel_R.Text = "NORMAL"
        '
        'OpenDeviceBtn
        '
        Me.OpenDeviceBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.OpenDeviceBtn.Location = New System.Drawing.Point(1300, 661)
        Me.OpenDeviceBtn.Name = "OpenDeviceBtn"
        Me.OpenDeviceBtn.Size = New System.Drawing.Size(80, 24)
        Me.OpenDeviceBtn.TabIndex = 91
        Me.OpenDeviceBtn.Text = "Ini"
        Me.OpenDeviceBtn.UseVisualStyleBackColor = False
        Me.OpenDeviceBtn.Visible = False
        '
        'EnumerateBtn
        '
        Me.EnumerateBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.EnumerateBtn.Location = New System.Drawing.Point(1308, 703)
        Me.EnumerateBtn.Name = "EnumerateBtn"
        Me.EnumerateBtn.Size = New System.Drawing.Size(72, 24)
        Me.EnumerateBtn.TabIndex = 90
        Me.EnumerateBtn.Text = "Enumerate"
        Me.EnumerateBtn.UseVisualStyleBackColor = False
        Me.EnumerateBtn.Visible = False
        '
        'GetBtn
        '
        Me.GetBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GetBtn.Location = New System.Drawing.Point(731, 377)
        Me.GetBtn.Name = "GetBtn"
        Me.GetBtn.Size = New System.Drawing.Size(96, 24)
        Me.GetBtn.TabIndex = 89
        Me.GetBtn.Text = "Get"
        Me.GetBtn.UseVisualStyleBackColor = False
        '
        'CheckBoxAutoOn
        '
        Me.CheckBoxAutoOn.Enabled = False
        Me.CheckBoxAutoOn.Location = New System.Drawing.Point(675, 346)
        Me.CheckBoxAutoOn.Name = "CheckBoxAutoOn"
        Me.CheckBoxAutoOn.Size = New System.Drawing.Size(248, 16)
        Me.CheckBoxAutoOn.TabIndex = 88
        Me.CheckBoxAutoOn.Text = "Enable AutoOn Event (FDU03, FDU04)"
        '
        'comboBoxDeviceName
        '
        Me.comboBoxDeviceName.Location = New System.Drawing.Point(759, 368)
        Me.comboBoxDeviceName.Name = "comboBoxDeviceName"
        Me.comboBoxDeviceName.Size = New System.Drawing.Size(148, 21)
        Me.comboBoxDeviceName.TabIndex = 87
        '
        'StatusBar
        '
        Me.StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StatusBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar.ForeColor = System.Drawing.Color.White
        Me.StatusBar.Location = New System.Drawing.Point(0, 917)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(1083, 35)
        Me.StatusBar.TabIndex = 96
        Me.StatusBar.Visible = False
        '
        'ListView1
        '
        Me.ListView1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.Location = New System.Drawing.Point(12, 14)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(1058, 754)
        Me.ListView1.TabIndex = 97
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'frmmarcas
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gray
        Me.ClientSize = New System.Drawing.Size(1083, 952)
        Me.Controls.Add(Me.StatusBar)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.comboBoxSecuLevel_V)
        Me.Controls.Add(Me.pictureBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.comboBoxSecuLevel_R)
        Me.Controls.Add(Me.OpenDeviceBtn)
        Me.Controls.Add(Me.EnumerateBtn)
        Me.Controls.Add(Me.GetBtn)
        Me.Controls.Add(Me.CheckBoxAutoOn)
        Me.Controls.Add(Me.comboBoxDeviceName)
        Me.Controls.Add(Me.btncerrar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmmarcas"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Marcas "
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btncerrar As System.Windows.Forms.Button
    Friend WithEvents comboBoxSecuLevel_V As System.Windows.Forms.ComboBox
    Friend WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents comboBoxSecuLevel_R As System.Windows.Forms.ComboBox
    Friend WithEvents OpenDeviceBtn As System.Windows.Forms.Button
    Friend WithEvents EnumerateBtn As System.Windows.Forms.Button
    Friend WithEvents GetBtn As System.Windows.Forms.Button
    Friend WithEvents CheckBoxAutoOn As System.Windows.Forms.CheckBox
    Friend WithEvents comboBoxDeviceName As System.Windows.Forms.ComboBox
    Friend WithEvents StatusBar As System.Windows.Forms.Label
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
End Class
