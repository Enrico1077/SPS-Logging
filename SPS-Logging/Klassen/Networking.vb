Imports System.Configuration
Module Networking

    'Diese Funktion testet alle IP-Addressen von Start-Ip bis End-Ip, ob sie
    'auf einen Ping antworten 
    Function searchForPLCs(StartIp As String, EndIp As String) As List(Of String)
        If Not areIPsValid(StartIp, EndIp) Then Return Nothing
        Dim CurIp = Net.IPAddress.Parse(StartIp)
        Dim StartAddr = CurIp.GetAddressBytes()
        Dim EndAddr = Net.IPAddress.Parse(EndIp).GetAddressBytes()
        Dim FoundIps As New List(Of String)
        Dim tasks As New List(Of Threading.Tasks.Task)
        While True
            Dim IpString As String = CurIp.ToString
            Dim t As Threading.Tasks.Task = Threading.Tasks.Task.Run(Sub() CheckIP(IpString, FoundIps))
            tasks.Add(t)
            If CurIp.ToString = New Net.IPAddress(EndAddr).ToString Then Exit While
            Dim higherIp As UInt32 = BitConverter.ToUInt32(CurIp.GetAddressBytes.Reverse().ToArray(), 0) + 1
            CurIp = New Net.IPAddress(BitConverter.GetBytes(higherIp).Reverse.ToArray)
        End While
        Threading.Tasks.Task.WaitAll(tasks.ToArray)
        SaveIps(FoundIps)
        Return FoundIps
    End Function

    'Diese Funktion prüft ob die Ip-Addressen richtige Ip-Addressen sind und ob die Ziel-Addresse
    'größer als Start-Addresse ist (evt. noch Fehlermanagment hinzufügen)
    Function areIPsValid(StartIp As String, EndIp As String) As Boolean
        Dim StartAddr As Net.IPAddress = New Net.IPAddress(0)
        Dim EndAddr As Net.IPAddress = New Net.IPAddress(0)
        If Not Net.IPAddress.TryParse(StartIp, StartAddr) Then Return False
        If Not Net.IPAddress.TryParse(EndIp, EndAddr) Then Return False
        Dim StartAddrBytes = StartAddr.GetAddressBytes()
        Dim EndAddrBytes = EndAddr.GetAddressBytes()
        For i As Integer = 0 To EndAddrBytes.Length - 1
            If StartAddrBytes(i) < EndAddrBytes(i) Then Exit For
            If StartAddrBytes(i) < EndAddrBytes(i) Then Return False
        Next
        Return True
    End Function

    'Diese Funktion prüft ob eine IP-Addresse auf einen Ping antwortet und fügt diese Addresse
    'der beigefügten Liste hinzu
    Sub CheckIP(ByVal ip As String, ByRef FoundIps As List(Of String))
        Dim erfolg As Boolean
        Try
            erfolg = My.Computer.Network.Ping(ip, 100)
        Catch ex As Exception
            erfolg = False
        End Try

        Console.WriteLine($"{ip}: {erfolg}")
        If erfolg Then
            Console.WriteLine($"Added {ip}")
            FoundIps.Add(ip)
        End If
    End Sub

    'Speichert die gefundenen IP-Addressen in Anwendungskonfigurationen,
    'welche auch nach einem Neustart der Anwendung noch verfügbar sind
    Sub SaveIps(IpAddr As List(Of String))
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        config.AppSettings.Settings.Clear()
        For i As Integer = 0 To IpAddr.Count - 1
            config.AppSettings.Settings.Add($"IP-Addresse{i}", IpAddr(i))
        Next
        config.Save(ConfigurationSaveMode.Modified)
        ConfigurationManager.RefreshSection("appSettings")

    End Sub

    'Die Funktion lädt di IP-Addressen, welche in der Anwendungskonfiguration gespeichert
    'wurden und gibt sie als String-Liste zurück
    Function loadSavedIps() As List(Of String)
        Dim IpAddr As New List(Of String)
        Dim IPCounter As Integer = 0
        While True
            Dim CurIP As String = ConfigurationManager.AppSettings($"IP-Addresse{IPCounter}")
            If CurIP Is Nothing Or CurIP = "" Then Exit While
            IpAddr.Add(CurIP)
            IPCounter += 1
        End While
        Return IpAddr
    End Function


End Module
