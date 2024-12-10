Imports System.Net
Public Class scannerN
    Public ipx As String = ""
    Public isip4 As Boolean = True
    Public prc As New Process()
    Private Sub Main()
        InitializeComponent()
    End Sub
    Private Async Sub scannernm(ipx)
        Try
            Dim ngrokExePath As String = "nmap.exe"
            Dim ngrokOptions As String = $"-v -A {ipx}"
            Dim ngrokProcessInfo As New ProcessStartInfo(ngrokExePath, ngrokOptions)
            ngrokProcessInfo.CreateNoWindow = True
            ngrokProcessInfo.RedirectStandardOutput = True
            ngrokProcessInfo.RedirectStandardError = True
            ngrokProcessInfo.UseShellExecute = False
            ngrokProcessInfo.WindowStyle = ProcessWindowStyle.Hidden
            Dim ngrokProcess As New Process()
            ngrokProcess.StartInfo = ngrokProcessInfo
            AddHandler ngrokProcess.OutputDataReceived, Sub(sender, e) logger(e.Data)
            AddHandler ngrokProcess.ErrorDataReceived, Sub(sender, e) logger(e.Data)
            ngrokProcess.Start()

            Await Task.Run(Sub() ngrokProcess.WaitForExit())

        Catch ex As Exception
            MsgBox("ERROR_HDL_CLOSING : TaskKill ngrok and php error")
        End Try
    End Sub
    Private Sub logger(x As String)
        If Not String.IsNullOrEmpty(x) Then
            Guna2TextBox2.Invoke(Sub() Guna2TextBox2.AppendText(x & Environment.NewLine))
        End If
    End Sub
    Private Sub nmapPrser()
        '############### the nmap string merger goes here
        Dim scr As String = Guna2TextBox1.Text()

    End Sub

    Private Sub scannerN_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If GetIPAddressType(ipx) = 4 Then
            isip4 = True
        Else
            isip4 = False
        End If
    End Sub
    Public Sub getip(x As String)
        ipx = x
    End Sub

    Function GetIPAddressType(ipx As String) As Integer
        Dim ipAddressType As Integer = -1

        If IPAddress.TryParse(ipx, Nothing) Then
            If IPAddress.Parse(ipx).AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                ipAddressType = 4 ' IPv4
            ElseIf IPAddress.Parse(ipx).AddressFamily = System.Net.Sockets.AddressFamily.InterNetworkV6 Then
                ipAddressType = 6 ' IPv6
            End If
        End If

        Return ipAddressType
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        nmapPrser()

        scannernm(ipx)
    End Sub
End Class