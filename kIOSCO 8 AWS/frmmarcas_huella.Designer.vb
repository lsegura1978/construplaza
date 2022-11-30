<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmmarcas_huella
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
        Me.comboBoxSecuLevel_V = New System.Windows.Forms.ComboBox()
        Me.comboBoxSecuLevel_R = New System.Windows.Forms.ComboBox()
        Me.OpenDeviceBtn = New System.Windows.Forms.Button()
        Me.EnumerateBtn = New System.Windows.Forms.Button()
        Me.GetBtn = New System.Windows.Forms.Button()
        Me.CheckBoxAutoOn = New System.Windows.Forms.CheckBox()
        Me.comboBoxDeviceName = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.StatusBar = New System.Windows.Forms.Label()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.btncerrar = New System.Windows.Forms.Button()
        Me.lblhora = New System.Windows.Forms.Label()
        Me.lblfecha = New System.Windows.Forms.Label()
        Me.txttexto = New System.Windows.Forms.TextBox()
        Me.lblestados = New System.Windows.Forms.Label()
        Me.lblnombre = New System.Windows.Forms.Label()
        Me.texto2 = New System.Windows.Forms.TextBox()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pictureBox1.Location = New System.Drawing.Point(9, 9)
        Me.pictureBox1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(311, 371)
        Me.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.pictureBox1.TabIndex = 7
        Me.pictureBox1.TabStop = False
        '
        'comboBoxSecuLevel_V
        '
        Me.comboBoxSecuLevel_V.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_V.Location = New System.Drawing.Point(1094, 72)
        Me.comboBoxSecuLevel_V.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.comboBoxSecuLevel_V.Name = "comboBoxSecuLevel_V"
        Me.comboBoxSecuLevel_V.Size = New System.Drawing.Size(85, 21)
        Me.comboBoxSecuLevel_V.TabIndex = 55
        Me.comboBoxSecuLevel_V.Text = "NORMAL"
        '
        'comboBoxSecuLevel_R
        '
        Me.comboBoxSecuLevel_R.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_R.Location = New System.Drawing.Point(1080, 271)
        Me.comboBoxSecuLevel_R.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.comboBoxSecuLevel_R.Name = "comboBoxSecuLevel_R"
        Me.comboBoxSecuLevel_R.Size = New System.Drawing.Size(85, 21)
        Me.comboBoxSecuLevel_R.TabIndex = 54
        Me.comboBoxSecuLevel_R.Text = "NORMAL"
        '
        'OpenDeviceBtn
        '
        Me.OpenDeviceBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.OpenDeviceBtn.Location = New System.Drawing.Point(1092, 109)
        Me.OpenDeviceBtn.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.OpenDeviceBtn.Name = "OpenDeviceBtn"
        Me.OpenDeviceBtn.Size = New System.Drawing.Size(60, 18)
        Me.OpenDeviceBtn.TabIndex = 53
        Me.OpenDeviceBtn.Text = "Ini"
        Me.OpenDeviceBtn.UseVisualStyleBackColor = False
        Me.OpenDeviceBtn.Visible = False
        '
        'EnumerateBtn
        '
        Me.EnumerateBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.EnumerateBtn.Location = New System.Drawing.Point(1096, 109)
        Me.EnumerateBtn.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.EnumerateBtn.Name = "EnumerateBtn"
        Me.EnumerateBtn.Size = New System.Drawing.Size(54, 18)
        Me.EnumerateBtn.TabIndex = 52
        Me.EnumerateBtn.Text = "Enumerate"
        Me.EnumerateBtn.UseVisualStyleBackColor = False
        Me.EnumerateBtn.Visible = False
        '
        'GetBtn
        '
        Me.GetBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GetBtn.Location = New System.Drawing.Point(1080, 139)
        Me.GetBtn.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.GetBtn.Name = "GetBtn"
        Me.GetBtn.Size = New System.Drawing.Size(72, 18)
        Me.GetBtn.TabIndex = 51
        Me.GetBtn.Text = "Get"
        Me.GetBtn.UseVisualStyleBackColor = False
        '
        'CheckBoxAutoOn
        '
        Me.CheckBoxAutoOn.Enabled = False
        Me.CheckBoxAutoOn.Location = New System.Drawing.Point(1082, 169)
        Me.CheckBoxAutoOn.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.CheckBoxAutoOn.Name = "CheckBoxAutoOn"
        Me.CheckBoxAutoOn.Size = New System.Drawing.Size(186, 12)
        Me.CheckBoxAutoOn.TabIndex = 50
        Me.CheckBoxAutoOn.Text = "Enable AutoOn Event (FDU03, FDU04)"
        '
        'comboBoxDeviceName
        '
        Me.comboBoxDeviceName.Location = New System.Drawing.Point(1080, 186)
        Me.comboBoxDeviceName.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.comboBoxDeviceName.Name = "comboBoxDeviceName"
        Me.comboBoxDeviceName.Size = New System.Drawing.Size(112, 21)
        Me.comboBoxDeviceName.TabIndex = 49
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button1.Location = New System.Drawing.Point(1080, 223)
        Me.Button1.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(72, 18)
        Me.Button1.TabIndex = 56
        Me.Button1.Text = "Get"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'StatusBar
        '
        Me.StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StatusBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar.ForeColor = System.Drawing.Color.White
        Me.StatusBar.Location = New System.Drawing.Point(0, 706)
        Me.StatusBar.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(953, 69)
        Me.StatusBar.TabIndex = 57
        '
        'PictureBox2
        '
        Me.PictureBox2.BackColor = System.Drawing.SystemColors.ControlLight
        Me.PictureBox2.Location = New System.Drawing.Point(324, 9)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(305, 370)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage
        Me.PictureBox2.TabIndex = 58
        Me.PictureBox2.TabStop = False
        '
        'btncerrar
        '
        Me.btncerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btncerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 28.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncerrar.ForeColor = System.Drawing.Color.White
        Me.btncerrar.Location = New System.Drawing.Point(742, 558)
        Me.btncerrar.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.btncerrar.Name = "btncerrar"
        Me.btncerrar.Size = New System.Drawing.Size(200, 124)
        Me.btncerrar.TabIndex = 59
        Me.btncerrar.Text = "CERRAR"
        Me.btncerrar.UseVisualStyleBackColor = True
        '
        'lblhora
        '
        Me.lblhora.AutoSize = True
        Me.lblhora.Font = New System.Drawing.Font("Microsoft Sans Serif", 45.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblhora.ForeColor = System.Drawing.Color.White
        Me.lblhora.Location = New System.Drawing.Point(321, 558)
        Me.lblhora.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblhora.Name = "lblhora"
        Me.lblhora.Size = New System.Drawing.Size(209, 69)
        Me.lblhora.TabIndex = 60
        Me.lblhora.Text = "Label1"
        Me.lblhora.Visible = False
        '
        'lblfecha
        '
        Me.lblfecha.AutoSize = True
        Me.lblfecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 45.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblfecha.ForeColor = System.Drawing.Color.White
        Me.lblfecha.Location = New System.Drawing.Point(17, 558)
        Me.lblfecha.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblfecha.Name = "lblfecha"
        Me.lblfecha.Size = New System.Drawing.Size(209, 69)
        Me.lblfecha.TabIndex = 62
        Me.lblfecha.Text = "Label1"
        Me.lblfecha.Visible = False
        '
        'txttexto
        '
        Me.txttexto.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.txttexto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txttexto.Enabled = False
        Me.txttexto.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txttexto.ForeColor = System.Drawing.Color.Navy
        Me.txttexto.Location = New System.Drawing.Point(634, 9)
        Me.txttexto.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txttexto.Multiline = True
        Me.txttexto.Name = "txttexto"
        Me.txttexto.Size = New System.Drawing.Size(312, 234)
        Me.txttexto.TabIndex = 0
        '
        'lblestados
        '
        Me.lblestados.AutoSize = True
        Me.lblestados.Font = New System.Drawing.Font("Microsoft Sans Serif", 45.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblestados.ForeColor = System.Drawing.Color.White
        Me.lblestados.Location = New System.Drawing.Point(17, 480)
        Me.lblestados.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblestados.Name = "lblestados"
        Me.lblestados.Size = New System.Drawing.Size(209, 69)
        Me.lblestados.TabIndex = 64
        Me.lblestados.Text = "Label1"
        Me.lblestados.Visible = False
        '
        'lblnombre
        '
        Me.lblnombre.AutoSize = True
        Me.lblnombre.Font = New System.Drawing.Font("Microsoft Sans Serif", 32.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblnombre.ForeColor = System.Drawing.Color.White
        Me.lblnombre.Location = New System.Drawing.Point(17, 392)
        Me.lblnombre.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.lblnombre.Name = "lblnombre"
        Me.lblnombre.Size = New System.Drawing.Size(152, 51)
        Me.lblnombre.TabIndex = 65
        Me.lblnombre.Text = "Label1"
        '
        'texto2
        '
        Me.texto2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.texto2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.texto2.Enabled = False
        Me.texto2.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.texto2.ForeColor = System.Drawing.Color.Navy
        Me.texto2.Location = New System.Drawing.Point(634, 247)
        Me.texto2.Margin = New System.Windows.Forms.Padding(2)
        Me.texto2.Multiline = True
        Me.texto2.Name = "texto2"
        Me.texto2.Size = New System.Drawing.Size(312, 133)
        Me.texto2.TabIndex = 66
        '
        'frmmarcas_huella
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(953, 775)
        Me.Controls.Add(Me.texto2)
        Me.Controls.Add(Me.lblnombre)
        Me.Controls.Add(Me.txttexto)
        Me.Controls.Add(Me.lblestados)
        Me.Controls.Add(Me.lblfecha)
        Me.Controls.Add(Me.lblhora)
        Me.Controls.Add(Me.btncerrar)
        Me.Controls.Add(Me.PictureBox2)
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
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "frmmarcas_huella"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Construplaza"
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents comboBoxSecuLevel_V As System.Windows.Forms.ComboBox
    Friend WithEvents comboBoxSecuLevel_R As System.Windows.Forms.ComboBox
    Friend WithEvents OpenDeviceBtn As System.Windows.Forms.Button
    Friend WithEvents EnumerateBtn As System.Windows.Forms.Button
    Friend WithEvents GetBtn As System.Windows.Forms.Button
    Friend WithEvents CheckBoxAutoOn As System.Windows.Forms.CheckBox
    Friend WithEvents comboBoxDeviceName As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents StatusBar As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents btncerrar As System.Windows.Forms.Button
    Friend WithEvents lblhora As System.Windows.Forms.Label
    Friend WithEvents lblfecha As System.Windows.Forms.Label
    Friend WithEvents txttexto As System.Windows.Forms.TextBox
    Friend WithEvents lblestados As System.Windows.Forms.Label
    Friend WithEvents lblnombre As System.Windows.Forms.Label
    Friend WithEvents texto2 As System.Windows.Forms.TextBox
End Class
