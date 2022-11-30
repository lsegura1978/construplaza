<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmestadoquarzo
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtdisponible = New System.Windows.Forms.TextBox()
        Me.ListView2 = New System.Windows.Forms.ListView()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.StatusBar = New System.Windows.Forms.Label()
        Me.comboBoxSecuLevel_V = New System.Windows.Forms.ComboBox()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.comboBoxSecuLevel_R = New System.Windows.Forms.ComboBox()
        Me.OpenDeviceBtn = New System.Windows.Forms.Button()
        Me.EnumerateBtn = New System.Windows.Forms.Button()
        Me.GetBtn = New System.Windows.Forms.Button()
        Me.CheckBoxAutoOn = New System.Windows.Forms.CheckBox()
        Me.comboBoxDeviceName = New System.Windows.Forms.ComboBox()
        Me.btncerrar = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 13.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(13, 427)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(234, 26)
        Me.Label1.TabIndex = 127
        Me.Label1.Text = "Disponible para crédito"
        '
        'txtdisponible
        '
        Me.txtdisponible.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtdisponible.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtdisponible.Location = New System.Drawing.Point(18, 468)
        Me.txtdisponible.Margin = New System.Windows.Forms.Padding(4)
        Me.txtdisponible.Name = "txtdisponible"
        Me.txtdisponible.Size = New System.Drawing.Size(489, 41)
        Me.txtdisponible.TabIndex = 126
        Me.txtdisponible.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ListView2
        '
        Me.ListView2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView2.Location = New System.Drawing.Point(515, 13)
        Me.ListView2.Margin = New System.Windows.Forms.Padding(4)
        Me.ListView2.Name = "ListView2"
        Me.ListView2.Size = New System.Drawing.Size(1414, 378)
        Me.ListView2.TabIndex = 125
        Me.ListView2.UseCompatibleStateImageBehavior = False
        '
        'ListView1
        '
        Me.ListView1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ListView1.Location = New System.Drawing.Point(13, 13)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(4)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(494, 378)
        Me.ListView1.TabIndex = 124
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'StatusBar
        '
        Me.StatusBar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.StatusBar.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.StatusBar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusBar.ForeColor = System.Drawing.Color.White
        Me.StatusBar.Location = New System.Drawing.Point(0, 603)
        Me.StatusBar.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.Size = New System.Drawing.Size(1942, 43)
        Me.StatusBar.TabIndex = 123
        Me.StatusBar.Visible = False
        '
        'comboBoxSecuLevel_V
        '
        Me.comboBoxSecuLevel_V.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_V.Location = New System.Drawing.Point(1005, 819)
        Me.comboBoxSecuLevel_V.Margin = New System.Windows.Forms.Padding(4)
        Me.comboBoxSecuLevel_V.Name = "comboBoxSecuLevel_V"
        Me.comboBoxSecuLevel_V.Size = New System.Drawing.Size(148, 24)
        Me.comboBoxSecuLevel_V.TabIndex = 122
        Me.comboBoxSecuLevel_V.Text = "NORMAL"
        '
        'pictureBox1
        '
        Me.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLight
        Me.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pictureBox1.Location = New System.Drawing.Point(783, 732)
        Me.pictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(187, 246)
        Me.pictureBox1.TabIndex = 121
        Me.pictureBox1.TabStop = False
        Me.pictureBox1.Visible = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Button1.Location = New System.Drawing.Point(1024, 749)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(128, 30)
        Me.Button1.TabIndex = 120
        Me.Button1.Text = "Get"
        Me.Button1.UseVisualStyleBackColor = False
        Me.Button1.Visible = False
        '
        'comboBoxSecuLevel_R
        '
        Me.comboBoxSecuLevel_R.Items.AddRange(New Object() {"LOWEST", "LOWER", "LOW", "BELOW_NORMAL", "NORMAL", "ABOVE_NORMAL", "HIGH", "HIGHER", "HIGHEST"})
        Me.comboBoxSecuLevel_R.Location = New System.Drawing.Point(1024, 786)
        Me.comboBoxSecuLevel_R.Margin = New System.Windows.Forms.Padding(4)
        Me.comboBoxSecuLevel_R.Name = "comboBoxSecuLevel_R"
        Me.comboBoxSecuLevel_R.Size = New System.Drawing.Size(148, 24)
        Me.comboBoxSecuLevel_R.TabIndex = 119
        Me.comboBoxSecuLevel_R.Text = "NORMAL"
        Me.comboBoxSecuLevel_R.Visible = False
        '
        'OpenDeviceBtn
        '
        Me.OpenDeviceBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.OpenDeviceBtn.Location = New System.Drawing.Point(1430, 764)
        Me.OpenDeviceBtn.Margin = New System.Windows.Forms.Padding(4)
        Me.OpenDeviceBtn.Name = "OpenDeviceBtn"
        Me.OpenDeviceBtn.Size = New System.Drawing.Size(107, 30)
        Me.OpenDeviceBtn.TabIndex = 118
        Me.OpenDeviceBtn.Text = "Ini"
        Me.OpenDeviceBtn.UseVisualStyleBackColor = False
        Me.OpenDeviceBtn.Visible = False
        '
        'EnumerateBtn
        '
        Me.EnumerateBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.EnumerateBtn.Location = New System.Drawing.Point(1441, 816)
        Me.EnumerateBtn.Margin = New System.Windows.Forms.Padding(4)
        Me.EnumerateBtn.Name = "EnumerateBtn"
        Me.EnumerateBtn.Size = New System.Drawing.Size(96, 30)
        Me.EnumerateBtn.TabIndex = 117
        Me.EnumerateBtn.Text = "Enumerate"
        Me.EnumerateBtn.UseVisualStyleBackColor = False
        Me.EnumerateBtn.Visible = False
        '
        'GetBtn
        '
        Me.GetBtn.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.GetBtn.Location = New System.Drawing.Point(1273, 764)
        Me.GetBtn.Margin = New System.Windows.Forms.Padding(4)
        Me.GetBtn.Name = "GetBtn"
        Me.GetBtn.Size = New System.Drawing.Size(128, 30)
        Me.GetBtn.TabIndex = 116
        Me.GetBtn.Text = "Get"
        Me.GetBtn.UseVisualStyleBackColor = False
        Me.GetBtn.Visible = False
        '
        'CheckBoxAutoOn
        '
        Me.CheckBoxAutoOn.Enabled = False
        Me.CheckBoxAutoOn.Location = New System.Drawing.Point(1024, 689)
        Me.CheckBoxAutoOn.Margin = New System.Windows.Forms.Padding(4)
        Me.CheckBoxAutoOn.Name = "CheckBoxAutoOn"
        Me.CheckBoxAutoOn.Size = New System.Drawing.Size(331, 20)
        Me.CheckBoxAutoOn.TabIndex = 115
        Me.CheckBoxAutoOn.Text = "Enable AutoOn Event (FDU03, FDU04)"
        Me.CheckBoxAutoOn.Visible = False
        '
        'comboBoxDeviceName
        '
        Me.comboBoxDeviceName.Location = New System.Drawing.Point(1024, 716)
        Me.comboBoxDeviceName.Margin = New System.Windows.Forms.Padding(4)
        Me.comboBoxDeviceName.Name = "comboBoxDeviceName"
        Me.comboBoxDeviceName.Size = New System.Drawing.Size(196, 24)
        Me.comboBoxDeviceName.TabIndex = 114
        Me.comboBoxDeviceName.Visible = False
        '
        'btncerrar
        '
        Me.btncerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 30.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btncerrar.ForeColor = System.Drawing.Color.White
        Me.btncerrar.Location = New System.Drawing.Point(1130, 453)
        Me.btncerrar.Margin = New System.Windows.Forms.Padding(4)
        Me.btncerrar.Name = "btncerrar"
        Me.btncerrar.Size = New System.Drawing.Size(245, 117)
        Me.btncerrar.TabIndex = 113
        Me.btncerrar.Text = "Cerrar"
        Me.btncerrar.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(15, 577)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(36, 17)
        Me.Label2.TabIndex = 128
        Me.Label2.Text = "LSQ"
        '
        'Frmestadoquarzo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Gray
        Me.ClientSize = New System.Drawing.Size(1942, 646)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtdisponible)
        Me.Controls.Add(Me.ListView2)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.StatusBar)
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
        Me.Name = "Frmestadoquarzo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Estado de cuenta en linea"
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtdisponible As System.Windows.Forms.TextBox
    Friend WithEvents ListView2 As System.Windows.Forms.ListView
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents StatusBar As System.Windows.Forms.Label
    Friend WithEvents comboBoxSecuLevel_V As System.Windows.Forms.ComboBox
    Friend WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents comboBoxSecuLevel_R As System.Windows.Forms.ComboBox
    Friend WithEvents OpenDeviceBtn As System.Windows.Forms.Button
    Friend WithEvents EnumerateBtn As System.Windows.Forms.Button
    Friend WithEvents GetBtn As System.Windows.Forms.Button
    Friend WithEvents CheckBoxAutoOn As System.Windows.Forms.CheckBox
    Friend WithEvents comboBoxDeviceName As System.Windows.Forms.ComboBox
    Friend WithEvents btncerrar As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
