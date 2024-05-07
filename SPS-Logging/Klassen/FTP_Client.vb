Imports System.Globalization
Imports System.Net

Public Class FTP_Client
    Dim ServerName As String
    Dim Username As String
    Dim Password As String

    'Der Konstruktor des FTP-Clienten, hier müssen alle Daten übergeben werden, welche
    'für eine Verbindund mit dem FTP-Server benötigt werden 
    Sub New(Ip_address As String, User_Name As String, Pass As String)
        ServerName = $"ftp://{Ip_address}/"
        Username = User_Name
        Password = Pass
    End Sub

    'Diese Funktion die Datei am mitgegebenden Pfad herunter und Speichert sich anschließend
    'an dem Pfad StorePath unter gleichen Namen
    Public Sub DownloadFile(FilePath As String, StorePath As String)
        If FilePath Is Nothing OrElse FilePath = "" Then Exit Sub
        If Not My.Computer.FileSystem.DirectoryExists(StorePath) Then My.Computer.FileSystem.CreateDirectory(StorePath)
        Dim Request As FtpWebRequest = CType(WebRequest.Create(ServerName + FilePath), FtpWebRequest)
        Request.Method = WebRequestMethods.Ftp.DownloadFile
        Request.Credentials = New NetworkCredential(Username, Password)
        Dim response As FtpWebResponse = CType(Request.GetResponse(), FtpWebResponse)
        Dim ResponseStream As IO.Stream = response.GetResponseStream()
        Dim reader As IO.StreamReader = New IO.StreamReader(ResponseStream)
        Dim LocalFileName As String = $"{StorePath}\{FilePath}"
        Dim writer = My.Computer.FileSystem.OpenTextFileWriter(LocalFileName, False)
        writer.Write(reader.ReadToEnd)
        Console.WriteLine($"Download Complete , File: {FilePath} , status: {response.StatusDescription}")
        reader.Close()
        response.Close()
        writer.Close()

    End Sub

    'Diese Funktion sucht aus dem Stammverzeichnis der Ftp-Servers die Datei, welche zuletzt geändert wurde und
    'gibt ihren Pfad zurück
    'ACHTUNG: Die DateiNamen im Verzeichnis müssen wie folgt aufgebaut sein := "..._{Jahr}_..." (Das Jahr muss im Format yyyy nach dem ersten "_" stehen)
    Public Function FindLatestFile() As String
        Dim Request As FtpWebRequest = CType(WebRequest.Create(ServerName), FtpWebRequest)
        Request.Method = WebRequestMethods.Ftp.ListDirectoryDetails
        Request.Credentials = New NetworkCredential(Username, Password)
        Dim response As FtpWebResponse
        Try
            response = CType(Request.GetResponse(), FtpWebResponse)
        Catch ex As Exception
            Return Nothing
        End Try
        Dim ResponseStream As IO.Stream = response.GetResponseStream()
        Dim reader As IO.StreamReader = New IO.StreamReader(ResponseStream)
        Dim directoryDetails As String = reader.ReadToEnd()
        Dim DirDetailsArray As String() = directoryDetails.Split(vbNewLine)
        Dim newestDate As Date = #1/1/2000#
        Dim newestFile As String = Nothing
        For i As Integer = 1 To DirDetailsArray.Length - 1
            Dim LineParts As String() = DirDetailsArray(i).Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)
            If LineParts.Length < 8 Then Continue For
            Dim NameParts As String() = LineParts(8).Split("_")
            Dim FileDate As Date = Date.ParseExact($"{NameParts(1)} {LineParts(5)} {LineParts(6)} {LineParts(7)}", "yyyy MMM dd HH:mm", CultureInfo.InvariantCulture)
            If FileDate > newestDate Then
                newestDate = FileDate
                newestFile = LineParts(8)
            End If
        Next
        response.Close()
        Return newestFile
    End Function

End Class
