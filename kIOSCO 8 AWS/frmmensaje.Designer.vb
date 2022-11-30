<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmmensaje
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmmensaje))
        Me.txtmensaje = New System.Windows.Forms.TextBox()
        Me.BTNCERRAR = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtmensaje
        '
        Me.txtmensaje.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtmensaje.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmensaje.Location = New System.Drawing.Point(0, 5)
        Me.txtmensaje.Multiline = True
        Me.txtmensaje.Name = "txtmensaje"
        Me.txtmensaje.Size = New System.Drawing.Size(841, 132)
        Me.txtmensaje.TabIndex = 0
        Me.txtmensaje.Text = resources.GetString("txtmensaje.Text")
        '
        'BTNCERRAR
        '
        Me.BTNCERRAR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTNCERRAR.Font = New System.Drawing.Font("Microsoft Sans Serif", 17.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNCERRAR.Location = New System.Drawing.Point(667, 150)
        Me.BTNCERRAR.Name = "BTNCERRAR"
        Me.BTNCERRAR.Size = New System.Drawing.Size(174, 67)
        Me.BTNCERRAR.TabIndex = 1
        Me.BTNCERRAR.Text = "Cerrar"
        Me.BTNCERRAR.UseVisualStyleBackColor = True
        '
        'frmmensaje
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(847, 229)
        Me.Controls.Add(Me.BTNCERRAR)
        Me.Controls.Add(Me.txtmensaje)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmmensaje"
        Me.Text = "Construplaza"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtmensaje As System.Windows.Forms.TextBox
    Friend WithEvents BTNCERRAR As System.Windows.Forms.Button
End Class
