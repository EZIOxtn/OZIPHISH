Imports System.Text.RegularExpressions

Public Class jsinject
    Private Const IndentationSize As Integer = 5
    ' Assuming the important lines are the first, second, and the last
    Private Function lastln() As Integer
        Dim textln As Integer = RichTextBox1.TextLength()
        Dim lst As Integer = RichTextBox1.GetLineFromCharIndex(textln)
        Return lst + 1
    End Function
    Private Sub jsinject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Text = My.Settings.jscode
        HighlightSyntax()
    End Sub

    Private Sub richTextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles RichTextBox1.KeyDown


        If e.KeyCode = Keys.Back OrElse e.KeyCode = Keys.Delete Then
            ' Prevent deletion of specific lines
            If IsCursorAtImportantLine() Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub richTextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles RichTextBox1.KeyPress
        ' Prevent character input if at important line (e.g., Enter key handling above will still work)
        If IsCursorAtImportantLine() AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged
        HighlightSyntax()
    End Sub
    Private Sub HighlightSyntax()
        ' Save the current selection
        Dim selectionStart As Integer = RichTextBox1.SelectionStart
        Dim selectionLength As Integer = RichTextBox1.SelectionLength

        ' Turn off redrawing to prevent flickering
        RichTextBox1.SuspendLayout()

        ' Save the text
        Dim text As String = RichTextBox1.Text

        ' Remove existing formatting
        RichTextBox1.SelectAll()
        RichTextBox1.SelectionColor = Color.GhostWhite

        ' Apply keyword highlighting
        Dim keywords As String() = {"var", "function", "if", "else", "for", "while", "return", "const", "let", "do"}
        For Each keyword As String In keywords
            HighlightWord(keyword, Color.Blue)
        Next
        Dim keywor As String() = {"(", ")", "{", "}", "=", "array", "[", "]", "&", "|", "!", ""}
        For Each keywordss As String In keywor
            HighlightWord(keywordss, Color.Red)
        Next

        ' Apply string highlighting
        Dim stringRegex As New Regex("""[^""]*""")
        HighlightPattern(stringRegex, Color.Brown)

        ' Apply comment highlighting
        Dim commentRegex As New Regex("//.*?$", RegexOptions.Multiline)
        HighlightPattern(commentRegex, Color.Green)

        ' Restore the original selection
        RichTextBox1.Select(selectionStart, selectionLength)

        ' Turn on redrawing
        RichTextBox1.ResumeLayout()
    End Sub

    Private Sub HighlightWord(word As String, color As Color)
        Dim regex As New Regex("\b" & Regex.Escape(word) & "\b")
        HighlightPattern(regex, color)
    End Sub

    Private Sub HighlightPattern(regex As Regex, color As Color)
        Dim matches As MatchCollection = regex.Matches(RichTextBox1.Text)
        For Each match As Match In matches
            RichTextBox1.Select(match.Index, match.Length)
            RichTextBox1.SelectionColor = color
        Next
    End Sub
    Private Function IsCursorAtImportantLine() As Boolean
        Dim currentLineIndex As Integer = RichTextBox1.GetLineFromCharIndex(RichTextBox1.SelectionStart)
        Dim ls As Integer = lastln()
        Dim ImportantLines As Integer() = {0, 1, ls}
        Return ImportantLines.Contains(currentLineIndex)
    End Function
    Dim isDragging As Boolean
    Dim clickPoint As Point
    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles Guna2ShadowPanel2.MouseDown
        If e.Button = MouseButtons.Left Then
            isDragging = True
            clickPoint = New Point(e.X, e.Y)
        End If
    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles Guna2ShadowPanel2.MouseMove
        If isDragging Then
            Dim newLocation As New Point(Me.Left + e.X - clickPoint.X, Me.Top + e.Y - clickPoint.Y)
            Me.Location = newLocation
        End If
    End Sub

    Private Sub Form1_MouseUp(sender As Object, e As MouseEventArgs) Handles Guna2ShadowPanel2.MouseUp
        If e.Button = MouseButtons.Left Then
            isDragging = False
        End If
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click

    End Sub

    Private Sub Guna2ImageButton2_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton2.Click
        Me.Close()
    End Sub

    Private Sub Guna2ImageButton3_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
    Private fsize As Integer = 11

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        If fsize > 10 Then
            fsize = fsize - 1
            Guna2GradientButton4.Text = fsize.ToString()
            RichTextBox1.SelectAll()
            RichTextBox1.SelectionFont = New Font(RichTextBox1.Font.FontFamily, fsize)
        End If
    End Sub

    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        If fsize < 26 Then
            fsize = fsize + 1
            Guna2GradientButton4.Text = fsize.ToString()
            RichTextBox1.SelectAll()
            RichTextBox1.SelectionFont = New Font(RichTextBox1.Font.FontFamily, fsize)
        End If
    End Sub

    Private Sub Guna2GradientButton3_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton3.Click
        My.Settings.jscode = RichTextBox1.Text
        My.Settings.Save()
        Dim htmlcontent = IO.File.ReadAllText(My.Settings.indexPath)
        Dim injectPosition As Integer = htmlContent.IndexOf("</body>", StringComparison.OrdinalIgnoreCase)
        If injectPosition >= 0 Then
            If Not htmlContent.Contains("OZIRIS") Then
                Dim modifiedHtmlContent As String = htmlcontent.Insert(injectPosition, "<script>" & vbCrLf & "document.addEventListener(""DOMContentLoaded"", function () {" & vbCrLf & My.Settings.jscode & vbCrLf & "</script>")
                IO.File.WriteAllText(My.Settings.indexPath, modifiedHtmlContent)
            End If

        Else
            MsgBox("Error injecting JavaScript. </body> not found in the HTML content.")

        End If
    End Sub
End Class