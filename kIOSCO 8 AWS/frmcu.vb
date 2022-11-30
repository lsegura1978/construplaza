Public Class frmcircular

    Private Sub btnIngresar_Click(sender As Object, e As EventArgs) Handles btnIngresar.Click
        Me.Close()

    End Sub

    Private Sub frmcircular_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.RichTextBox1.LoadFile("C:\circular\circular.txt", RichTextBoxStreamType.PlainText)

    End Sub

    Private Sub btncerrar_Click(sender As Object, e As EventArgs) Handles btncerrar.Click
        Me.Close()

    End Sub
End Class