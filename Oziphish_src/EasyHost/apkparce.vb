Imports System.IO
Imports System.IO.Compression
Imports System.Xml

Public Class apkparce
    Public Property Name As String = "null"
    Public Property PackageName As String = "null"
    Public Property Size As String = "null"
    Public Property Icon As Image = My.Resources.Error_512__1_

    Public Sub New(apkPath As String)
        Try
            Dim apkFile As New FileInfo(apkPath)
            Size = (apkFile.Length / 1024 / 1024).ToString() & "MB"
            Using zip As ZipArchive = ZipFile.OpenRead(apkPath)
                Dim entry As ZipArchiveEntry = zip.GetEntry("AndroidManifest.xml")
                Dim stream As Stream = entry.Open()

                Dim xmlDoc As New XmlDocument()
                xmlDoc.Load(stream)
                Dim ns As XmlNamespaceManager = New XmlNamespaceManager(xmlDoc.NameTable)
                ns.AddNamespace("android", "http://schemas.android.com/apk/res/android")
                Dim nameAttr As XmlNode = xmlDoc.SelectSingleNode("/manifest/application/meta-data[@android:name='application-label']", ns)
                Name = nameAttr.Attributes("android:value").Value
                Dim packageAttr As XmlNode = xmlDoc.SelectSingleNode("/manifest", ns)
                PackageName = packageAttr.Attributes("package").Value

                Dim iconEntry As ZipArchiveEntry = zip.GetEntry("res/mipmap-hdpi-v4/ic_launcher.png")
                If iconEntry Is Nothing Then
                    iconEntry = zip.GetEntry("res/drawable-hdpi-v4/ic_launcher.png")
                End If
                Dim iconStream As Stream = iconEntry.Open()
                Icon = Image.FromStream(iconStream)
            End Using
        Catch ex As Exception

        End Try

    End Sub
End Class
