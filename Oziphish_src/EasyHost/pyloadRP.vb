Imports System.IO
Imports System.Reflection

Public Class pyloadRP
    Private Sub pyloadRP_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Public gex As String
    Public bol As Boolean = False
    Private Sub Guna2CustomGradientPanel1_Paint(sender As Object, e As DragEventArgs) Handles Guna2CustomGradientPanel1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())

            If files.Length > 0 Then
                Dim filePath As String = files(0)

                ' Display the file path in a label
                Label1.Text = "Ready"
                gex = filePath
                bol = True
                Guna2CustomGradientPanel1.Visible = False
                DrakeUIGroupBox1.Visible = True
                PictureBox1.Visible = True
                Parcer(gex)
            End If
        End If

    End Sub
    Private Sub Guna2CstomGradientPanel1_Paint(sender As Object, e As DragEventArgs) Handles Guna2CustomGradientPanel1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
        Guna2CustomGradientPanel1.FillColor = Color.Black
        Guna2CustomGradientPanel1.FillColor4 = Color.Black
    End Sub
    Private Sub Guna2CustomGradientPaint(sender As Object, e As DragEventArgs) Handles Label1.DragDrop
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            Dim files As String() = CType(e.Data.GetData(DataFormats.FileDrop), String())

            If files.Length > 0 Then
                Dim filePath As String = files(0)

                Label1.Text = "Ready"
                gex = filePath

                bol = True
                Guna2CustomGradientPanel1.Visible = False
                DrakeUIGroupBox1.Visible = True
                PictureBox1.Visible = True
                Parcer(gex)
            End If
        End If

    End Sub
    Private Sub Guna2CstomGradienl1_Paint(sender As Object, e As DragEventArgs) Handles Label1.DragEnter
        If e.Data.GetDataPresent(DataFormats.FileDrop) Then
            e.Effect = DragDropEffects.Copy
        Else
            e.Effect = DragDropEffects.None
        End If
        Guna2CustomGradientPanel1.FillColor = Color.Black
        Guna2CustomGradientPanel1.FillColor4 = Color.Black
    End Sub
    Private Sub Guna2CstomGradientPanel1_Pat(sender As Object, e As DragEventArgs) Handles Guna2CustomGradientPanel1.DragLeave
        Guna2CustomGradientPanel1.FillColor = Color.FromArgb(64, 0, 0)
        Guna2CustomGradientPanel1.FillColor4 = Color.FromArgb(0, 0, 64)
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

        Dim dg As New OpenFileDialog()
        dg.Title = "Select an image"
        If dg.ShowDialog() = DialogResult.OK Then
            gex = dg.FileName
            Guna2CustomGradientPanel1.Visible = False
            DrakeUIGroupBox1.Visible = True
            PictureBox1.Visible = True
            Parcer(gex)
        End If
    End Sub
    Private Sub Parcer(x As String)
        Dim fileType As String = Path.GetExtension(x).ToLower()
        MsgBox(x)
        If fileType = ".apk" Then
            PictureBox1.Image = My.Resources.apk
            Dim appInfo As New apkparce(x)
            Label3.Text = "application Name: " & appInfo.Name
            Label4.Text = "package name: " & appInfo.PackageName
            Label5.Text = "size: " & appInfo.Size
            PictureBox2.Image = appInfo.Icon
            Label6.Visible = False
        ElseIf fileType = ".exe" Then
            PictureBox1.Image = My.Resources.exe
            Dim assembly As Assembly = Assembly.LoadFrom(x)

            Label3.Text = "Name: " & assembly.GetName().Name & " size:" & ((x.Length / 1024 / 1024).ToString()).Substring(0, 5) & " MB"
            Label4.Text = "Version: " & assembly.GetName().Version.ToString()
            Label5.Text = "Culture: " & assembly.GetName().ContentType
            Label6.Text = "Processor Architecture: " & assembly.GetName().ProcessorArchitecture.ToString()

        ElseIf fileType = ".docx" Or fileType = ".doc" Then
            PictureBox1.Image = My.Resources.word
        ElseIf fileType = ".xlsx" Or fileType = ".xls" Then
            PictureBox1.Image = My.Resources.download
        ElseIf fileType = ".py" Then
            PictureBox1.Image = My.Resources.python
        ElseIf fileType = ".vbs" Then
            PictureBox1.Image = My.Resources.file
        ElseIf fileType = ".bat" Then
            PictureBox1.Image = My.Resources.console
        ElseIf fileType = ".zip" Then
            PictureBox1.Image = My.Resources.zip
        ElseIf fileType = ".pptx" Or fileType = ".ppt" Then
            PictureBox1.Image = My.Resources.powerpoint
        Else
            PictureBox1.Image = My.Resources.Error_512__1_
        End If
    End Sub

    Private Sub Guna2ImageButton2_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton2.Click
        Me.Close()
    End Sub
End Class