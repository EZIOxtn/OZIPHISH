Public Class Metainf
    Private img As String
    Private Sub Guna2GradientButton3_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton3.Click
        Dim dg As New OpenFileDialog()
        dg.Title = "Select an image"
        dg.Filter = "Image Files|*,jpg;*.jpeg;*.png"
        If dg.ShowDialog() = DialogResult.OK Then
            img = dg.FileName
            PictureBox1.Image = Image.FromFile(dg.FileName)
            IO.File.Copy(dg.FileName, Application.StartupPath + My.Settings.indexfolder + "\logo.jpg")
        End If
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        TextBox5.Text = TextBox4.Text
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        Dim fol As String = My.Settings.indexfolder
        Dim indexp As String = fol + My.Settings.indexPath
        Dim cntedit As String = My.Resources.Metainfo
        If IO.File.Exists(indexp) Then
            IO.File.Delete(indexp)
            IO.File.Create(indexp)

            cntedit = cntedit.Replace("[desc]", TextBox4.Text)
            cntedit = cntedit.Replace("[title]", TextBox1.Text)
            cntedit = cntedit.Replace("[url]", TextBox3.Text)
            cntedit = cntedit.Replace("[ogdesc]", TextBox5.Text)
            cntedit = cntedit.Replace("[sitename]", TextBox2.Text)
            If img IsNot Nothing Then
                cntedit = cntedit.Replace("[img]", img)
            Else
                cntedit = cntedit.Replace("[img]", "null")
            End If


        End If
        Dim strg As String = My.Resources.sarahahEN.Replace("[metacnt]", cntedit)
        IO.File.WriteAllText(indexp, strg)
    End Sub
End Class