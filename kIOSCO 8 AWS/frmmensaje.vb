Public Class frmmensaje

    Private Sub BTNCERRAR_Click(sender As Object, e As EventArgs) Handles BTNCERRAR.Click
        Me.Close()
        frmmarcas_huella.Close()
        Formmenu.Button1.Enabled = True

    End Sub
End Class