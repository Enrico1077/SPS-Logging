Imports System.Net.NetworkInformation

Module Networking

    'Diese Funktion testet alle IP-Addressen von Start-Ip bis End-Ip, ob sie
    'auf einen Ping antworten 
    Function searchForPLCs(StartIp As String, EndIp As String) As List(Of String)
        If Not areIPsValid(StartIp, EndIp) Then Return Nothing
        Dim StartAddr = Net.IPAddress.Parse(StartIp).GetAddressBytes()
        Dim EndAddr = Net.IPAddress.Parse(EndIp).GetAddressBytes()
        Dim FoundIps As New List(Of String)
        Dim tasks As New List(Of Threading.Tasks.Task)
        While True
            Dim CurIp As String = New Net.IPAddress(StartAddr).ToString
            Dim t As Threading.Tasks.Task = Threading.Tasks.Task.Run(Sub() CheckIP(CurIp, FoundIps))
            tasks.Add(t)

            If CurIp = New Net.IPAddress(EndAddr).ToString Then Exit While
            StartAddr(3) += 1
            If StartAddr(3) = 255 Then
                StartAddr(2) += 1
                StartAddr(3) = 0
                If StartAddr(2) = 255 Then
                    StartAddr(1) += 1
                    StartAddr(2) = 0
                    If StartAddr(1) = 255 Then
                        StartAddr(0) += 1
                        StartAddr(1) = 0
                    End If
                End If
            End If
        End While
        Threading.Tasks.Task.WaitAll(tasks.ToArray())
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
        Dim erfolg = My.Computer.Network.Ping(ip, 100)
        Console.WriteLine($"{ip}: {erfolg}")
        If erfolg Then
            Console.WriteLine($"Added {ip}")
            FoundIps.Add(ip)
        End If
    End Sub

End Module
