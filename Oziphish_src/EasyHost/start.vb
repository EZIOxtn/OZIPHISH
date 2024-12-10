Imports System.Runtime.InteropServices
Imports System.Threading

Public Class start
    Sub Main()
        InitializeComponent()
    End Sub
    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function AnimateWindow(ByVal hwnd As IntPtr, ByVal time As Integer, ByVal flags As AnimateWindowFlags) As Boolean
    End Function

    <Flags()>
    Private Enum AnimateWindowFlags
        AW_HOR_POSITIVE = &H1
        AW_HOR_NEGATIVE = &H2
        AW_VER_POSITIVE = &H4
        AW_VER_NEGATIVE = &H8
        AW_CENTER = &H10
        AW_HIDE = &H10000
        AW_ACTIVATE = &H20000
        AW_SLIDE = &H40000
        AW_BLEND = &H80000
    End Enum

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        MakeSlightlyRoundForm(15)
        _3asba()
    End Sub
    Private Sub MakeSlightlyRoundForm(radius As Integer)

        Dim path As New Drawing2D.GraphicsPath()
        path.AddLine(radius, 0, Me.Width - radius, 0)
        path.AddArc(Me.Width - radius, 0, radius, radius, 270, 90)
        path.AddLine(Me.Width, radius, Me.Width, Me.Height - radius)
        path.AddArc(Me.Width - radius, Me.Height - radius, radius, radius, 0, 90)
        path.AddLine(Me.Width - radius, Me.Height, radius, Me.Height)
        path.AddArc(0, Me.Height - radius, radius, radius, 90, 90)
        path.AddLine(0, Me.Height - radius, 0, radius)
        path.AddArc(0, 0, radius, radius, 180, 90)

        Me.Region = New Region(path)

    End Sub


    Private Sub _3asba()
        Dim originalLocation = Me.Location
        Dim varloc = originalLocation
        Dim varlocv = originalLocation
        varloc.X = -1500
        varlocv.X = 3500
        Dim cnt As Integer = -100
        Dim ctx As Double = 100
        Dim duration As Integer = 5000
        Dim timer As New System.Windows.Forms.Timer With {
            .Interval = 1
        }

        timer.Start()

        Dim thread As New Thread(
                   Sub()
                       Dim startTime As DateTime = DateTime.Now
                       Me.Opacity = 0.9

                       While cnt < originalLocation.X + 5000
                           Me.Invoke(
                               Sub()

                                   Me.Location = New Point(
                                       varloc.X + cnt,
                                        varloc.Y
                                   )
                               End Sub
                           )

                           cnt = cnt + 20
                           Thread.Sleep(1)
                       End While
                       While ctx < 8000
                           Me.Invoke(
                               Sub()

                                   Me.Location = New Point(
                                       varlocv.X - ctx,
                                        varlocv.Y
                                   )
                               End Sub
                           )

                           ctx = ctx + 20
                           Thread.Sleep(1)
                       End While

                       ctx = 0
                       Me.Opacity = 0
                       Me.Location = originalLocation
                       While ctx < 1
                           Me.Invoke(
                               Sub()

                                   Me.Opacity = ctx
                               End Sub
                           )

                           ctx = ctx + 0.03
                           Thread.Sleep(20)
                       End While
                       timer.Stop()

                       Me.Invoke(
                           Sub()
                               Me.Location = originalLocation
                               Me.Opacity = 1
                               Guna2PictureBox1.Visible = True
                               Dim gex As New welcome()
                               gex.Show()
                               Timer1.Start()
                           End Sub
                       )
                   End Sub
               )


        thread.Start()

    End Sub
    Private contx As Integer = 1
    Dim xtext As String = "OziPhish" + vbNewLine + vbNewLine + "oziphish is created by #OZIRIS# to help in penetration testing" + vbNewLine + " its not responsible for any misuse or illegal purposes" + vbNewLine + "==================="
    Dim outtext As String
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If contx <= 100 Then
            MetroSetProgressBar1.Value = contx

        Else

        End If
        contx = contx + 1
        If contx < xtext.Length Then
            outtext = xtext.Substring(0, contx)
            Label1.Text = outtext
        Else
            If My.Settings.firstTM Then
                Try
                    IO.Directory.CreateDirectory("resources")

                    IO.Directory.CreateDirectory("resources\php")

                Catch ex As Exception

                End Try
                FirstTM.Show()
                Me.WindowState = FormWindowState.Minimized
                Timer1.Stop()
            Else
                Form1.Show()
                Me.WindowState = FormWindowState.Minimized
                Timer1.Stop()
            End If


        End If
    End Sub


End Class