Imports System.Threading
Imports NAudio.Wave

Public Class about
    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HT_CAPTION As Integer = &H2
    Private waveStream As WaveStream
    Private wavePlayer As WaveOutEvent
    Private Sub NotificationForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Set initial position and opacity
        Me.Left = Screen.PrimaryScreen.WorkingArea.Width - Me.Width - 20
        Me.Top = Screen.PrimaryScreen.WorkingArea.Height - Me.Height - 20
        Me.Opacity = 0.6
        If My.Settings.sounds = True Then
            Dim audioFile As New WaveFileReader(My.Resources.notification)
            waveStream = audioFile

            wavePlayer = New WaveOutEvent()
            wavePlayer.Init(waveStream)


            wavePlayer.Play()
        End If

        ' Set round corners
        MakeSlightlyRoundForm(30)

        ' Move up and start a timer
        MoveUp()
        Timer1.Start()
    End Sub
    Private Sub MakeSlightlyRoundForm(radius As Integer)
        ' Create a GraphicsPath to define the shape of the form (rounded rectangle in this case)
        Dim path As New System.Drawing.Drawing2D.GraphicsPath()
        path.AddLine(radius, 0, Me.Width - radius, 0)
        path.AddArc(Me.Width - radius, 0, radius, radius, 270, 90)
        path.AddLine(Me.Width, radius, Me.Width, Me.Height - radius)
        path.AddArc(Me.Width - radius, Me.Height - radius, radius, radius, 0, 90)
        path.AddLine(Me.Width - radius, Me.Height, radius, Me.Height)
        path.AddArc(0, Me.Height - radius, radius, radius, 90, 90)
        path.AddLine(0, Me.Height - radius, 0, radius)
        path.AddArc(0, 0, radius, radius, 180, 90)

        ' Set the Region property of the form to the created GraphicsPath
        Me.Region = New Region(path)

    End Sub
    Private Sub MoveUp()
        Dim targetTop As Integer = Me.Top - 50
        Dim stepSize As Integer = 2

        Dim moveUpThread As New Thread(
            Sub()
                For i As Integer = Me.Top To targetTop Step -stepSize
                    Me.Invoke(
                        Sub()
                            Me.Top = i
                        End Sub
                    )
                    Thread.Sleep(20)
                Next
            End Sub
        )

        moveUpThread.Start()
    End Sub

    Private Sub CloseForm()

        Me.Close()

    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        MyBase.WndProc(m)
        If m.Msg = WM_NCLBUTTONDOWN AndAlso m.WParam.ToInt32() = HT_CAPTION Then

            ReleaseCapture()
            SendMessage(Me.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0)
        End If
    End Sub

    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function ReleaseCapture() As Boolean
    End Function

    <System.Runtime.InteropServices.DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function
    Public men As Boolean = False
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If men = False Then
            men = True
        Else
            Try
                wavePlayer?.Stop()
                wavePlayer?.Dispose()
                waveStream?.Dispose()
            Catch ex As Exception

            End Try
            Me.Close()
            Timer1.Stop()
        End If

    End Sub
    Public Sub SetNotificationContent(ip As String, shit As String, fake As String, ico As Image)
        ' Set the content of the notification here
        Label1.Text = ip
        Label2.Text = shit
        Label3.Text = fake
        Guna2CirclePictureBox1.Image = ico
    End Sub
End Class
