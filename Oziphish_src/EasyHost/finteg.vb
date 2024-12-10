Imports System.IO
Imports System.IO.Compression
Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Module finteg
    Private ngpth As String = "cmd"
    Public Sub ExtractResourceToFile(resourceName As String, destinationPath As String)
        If Not File.Exists(destinationPath) Then
            Try
                Directory.CreateDirectory("resources")
                Directory.CreateDirectory("resources\webtest")
                Dim resourceObject As Object = My.Resources.ResourceManager.GetObject(resourceName)
                If resourceObject IsNot Nothing AndAlso TypeOf resourceObject Is Byte() Then
                    Dim resourceBytes As Byte() = DirectCast(resourceObject, Byte())
                    File.WriteAllBytes(destinationPath, resourceBytes)
                Else
                    MessageBox.Show($"Resource {resourceName} not found or is not a byte array.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            Catch ex As Exception
                MessageBox.Show($"Error extracting resource: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
    Public Sub startngrok(arguments As String)
        Try
            Dim cmdPath As String = "cmd.exe"
            Dim startInfo As New ProcessStartInfo(cmdPath, arguments)
            startInfo.WorkingDirectory = Application.StartupPath + "\resources\ngrok"
            startInfo.WindowStyle = ProcessWindowStyle.Normal
            Dim process As New Process()
            process.StartInfo = startInfo
            process.Start()
            process.WaitForExit()
            process.Close()
        Catch ex As Exception
            MessageBox.Show($"Error starting process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        End Try
    End Sub
    Public Async Sub Dwsvc()
        Try
            Await Task.Run(Sub()
                               Dim processInfo As New ProcessStartInfo()
                               processInfo.FileName = Application.StartupPath + "\DwService.exe"
                               processInfo.WindowStyle = ProcessWindowStyle.Hidden
                               Using process As Process = Process.Start(processInfo)
                                   process.WaitForExit()
                               End Using
                           End Sub)
        Catch ex As Exception
            MessageBox.Show($"Error starting process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
        End Try
    End Sub
    Async Function DownloadWebsite(url As String, outputPath As String) As Task
        If File.Exists(outputPath) Then
            File.Delete(outputPath)
        End If
        Using httpClient As New HttpClient()
            Try
                Dim response As HttpResponseMessage = Await httpClient.GetAsync(url)
                If response.IsSuccessStatusCode Then
                    Dim htmlContent As String = Await response.Content.ReadAsStringAsync()
                    File.WriteAllText(outputPath, htmlContent)
                Else
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}")
                End If
            Catch ex As Exception
                Console.WriteLine($"Exception: {ex.Message}")
            End Try
        End Using
    End Function
    Public Function ParseJsonAndExtractPublicUrl(jsonPath As String) As String
        Try
            Dim jsonContent As String = File.ReadAllText(jsonPath)
            Dim jsonObject As JObject = JObject.Parse(jsonContent)
            Dim publicUrl As String = jsonObject.SelectToken("tunnels[0].public_url").ToString()
            Return publicUrl
        Catch ex As Exception
            MsgBox($"Error parsing JSON: {ex.Message}  _  check your ngrok key")
            'grok.Show()
            Return String.Empty
        End Try
    End Function
    Sub ExtractZipFile(zipFilePath As String, extractDirectory As String)
        Try
            If File.Exists(zipFilePath) Then
                If Not Directory.Exists(extractDirectory) Then
                    Directory.CreateDirectory(extractDirectory)
                End If
                ZipFile.ExtractToDirectory(zipFilePath, extractDirectory)
            Else
                Console.WriteLine("Zip file not found.")
            End If
        Catch ex As Exception
            Console.WriteLine($"Error extracting zip file: {ex.Message}")
        End Try
    End Sub
    Function GetNamespaceName(type As Type) As String
        Dim namespaceName As String = type.Namespace
        Return If(namespaceName Is Nothing, "GlobalNamespace", namespaceName)
    End Function
    Public Function IPExistsInDataTable(dt As DataTable, ip As String) As Boolean

        For Each row As DataRow In dt.Rows
            If row("IP Address").ToString() = ip Then
                Return True
            End If
        Next
        Return False
    End Function
End Module