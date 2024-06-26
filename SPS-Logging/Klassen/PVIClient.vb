﻿Imports BR.AN.PviServices
Public Class PVIClient
    Dim CurService As Service
    Dim CurCPU As Cpu
    Dim CpuIP As String
    Dim StartFenster As StartFenster
    Dim tmpTreeNode As TreeNode
    Dim CurFileNameVar As Variable
    Dim VarRegOk As Variable

    Sub New(SF As Form, CIP As String)
        StartFenster = SF
        CpuIP = CIP
        Dim PviManTimer As New Timer() With {.Interval = (6 / 5) * 10 ^ 6, .Enabled = True}
        RestartPvi(PviManTimer, Nothing)
        AddHandler PviManTimer.Tick, AddressOf RestartPvi
    End Sub

    'Beim Start der Anwendung und dann im 20 Minuten Schritten wir geprüft ob der PviManager bereits länger als 100 Minuten 
    'auf einem nicht B&R-IPC läuft. Falls ja wird der PviManager neugestartet
    Private Sub RestartPvi(sender As Timer, e As EventArgs)
        Dim PviLic As New BR.AN.BRLicenseInfo
        If Not PviLic.BRIPCState.ToString = "INVALID" Then sender.Stop() : Exit Sub
        Dim pviManProcesses As Process() = Process.GetProcessesByName("PviMan")
        Dim pviManProcess As Process = pviManProcesses.FirstOrDefault()
        If pviManProcess Is Nothing Then Exit Sub
        Dim startTime As Date = pviManProcess.StartTime
        Dim age As TimeSpan = Date.Now - startTime
        If age.TotalMinutes < 100 Then Exit Sub
        pviManProcess.Kill()
        pviManProcess.WaitForExit()
        Process.Start("PviMan")
    End Sub

    'Diese Funktion löscht alle Verweise welche durch diese Anwendung in der
    'PVI-Session angelgt wurden 
    Public Sub DisconnectService()
        If CurService Is Nothing Then Exit Sub
        CurService.Remove()
        CurService = Nothing
        CurCPU = Nothing
    End Sub

    'Erstellt einen Service und verbindet diesen 
    Sub connectService()
        If CurService Is Nothing Then
            CurService = New Service("service")
            AddHandler CurService.Error, AddressOf service_error
            AddHandler CurService.Connected, AddressOf service_Connected
        End If
        CurService.Connect()
    End Sub

    'Stellt einen Fehler in der PVI-Verbindung auf dem Startfenster dar
    Sub service_error(sender As Object, e As PviEventArgs)
        StartFenster.setLCpuConnectedText($"Error: {e.ErrorText}")
    End Sub

    'Sobald der Service verbunden wurde wird eine CPU mit der im Startfenster festgelegten IP verbunden 
    Sub service_Connected()
        If CurCPU Is Nothing Then
            CurCPU = New Cpu(CurService, "cpu")
            AddHandler CurCPU.Connected, AddressOf cpu_connected
            CurCPU.Connection.DeviceType = DeviceType.ANSLTcp
            CurCPU.Connection.ANSLTcp.DestinationIpAddress = CpuIP
        End If
        CurCPU.Connect()
    End Sub

    'Wenn die CPU erfolgreich verbunden wurde wird dies im Startfenster angezeigt.
    'Auch werden alle Tasks und Variablen der CPU auf in die Anwendung hochgeladen
    Private Sub cpu_connected(sender As Object, e As PviEventArgs)
        If CurCPU Is Nothing Then Exit Sub
        StartFenster.setLCpuConnectedText($"{CurCPU.Name} is connected")  'Nochmal mit echter SPS testen
        AddHandler CurCPU.Tasks.Uploaded, AddressOf Cpu_Tasks_Uploaded
        AddHandler CurCPU.Variables.Uploaded, AddressOf Cpu_Variables_Uploaded
        CurCPU.Variables.Upload()
        CurCPU.Tasks.Upload()
    End Sub

    'Sobald alle globalen Variablen von der SPS hpchgeladen wurden, werden diese Verbunden und aktiviert    
    Private Sub Cpu_Variables_Uploaded(sender As Object, e As PviEventArgs)
        For Each Variable As DictionaryEntry In CurCPU.Variables
            Dim tmpVariable As Variable = CurCPU.Variables(Variable.Key)
            AddHandler tmpVariable.ValueChanged, AddressOf Cpu_Variable_Connected
            tmpVariable.Active = True
            tmpVariable.Connect()
        Next
    End Sub

    'Sobald eine globale Variable verbunden wurde wird sie und die erste unterstehende Ebene                
    'der TreeView im Startfenster hinzugefügt
    Private Sub Cpu_Variable_Connected(sender As Object, e As PviEventArgs)
        Dim Config As ConfigurationData = ReadConfig(IO.Directory.GetCurrentDirectory + ConfigName)
        Dim tmpVariable As Variable = sender
        Dim rootNode As New TreeNode(tmpVariable.Name) With {
            .Name = tmpVariable.Name,
            .ImageIndex = 2
        }
        If tmpVariable.Members IsNot Nothing Then
            For Each child As Variable In tmpVariable.Members.Values
                rootNode.Nodes.Add(child.Name, child.Name, 2)
            Next
        End If
        rootNode.ImageIndex = 2
        StartFenster.addTreeNode(rootNode)
        If tmpVariable.Name = Config.DataRecoderName Then
            LookOnLoggerStats(CurConfig.DataRecoderName)
            Try
                StartFenster.setValCountText(MaxNum:=tmpVariable.Value("In.Variable").ArrayData.Length)
            Catch ex As System.Exception
                Console.WriteLine("Fehler: " + ex.ToString)
            End Try

            Exit Sub
        End If
        RemoveHandler tmpVariable.ValueChanged, AddressOf Cpu_Variable_Connected
        tmpVariable.Disconnect()
    End Sub

    'Wenn die Tasks hochgeladen wurden, werden sie verbunden
    Private Sub Cpu_Tasks_Uploaded(sender As Object, e As PviEventArgs)
        For Each Task As DictionaryEntry In CurCPU.Tasks
            Dim tmpTask As Task = CurCPU.Tasks(Task.Key)
            AddHandler tmpTask.Connected, AddressOf Task_Connected
            tmpTask.Connect()
        Next
    End Sub

    'Wenn ein Task verbunden wurde, werden alle Variablen des Tasks von der SPS an die 
    'Anwendung hochgeladen
    Private Sub Task_Connected(sender As Object, e As PviEventArgs)
        Dim tmpTask As Task = sender
        AddHandler tmpTask.Variables.Uploaded, AddressOf Task_Variables_Uploaded
        tmpTask.Variables.Upload()

    End Sub

    'Wenn die Variablen eines Tasks hochgeladen wurden, werden sie als TreeNode an das Startfenster
    'weitergegeben
    Private Sub Task_Variables_Uploaded(sender As Object, e As PviEventArgs)
        Dim tmpTask As Task = sender.Parent
        'If tmpTask.Name = "LoggerTest" Then Stop
        Dim tmpTreeNode As New TreeNode(tmpTask.Name) With {
            .ImageIndex = 0,
            .Name = tmpTask.Name
        }
        For Each Var As DictionaryEntry In tmpTask.Variables
            Dim tmpVar As Variable = tmpTask.Variables(Var.Key)
            tmpTreeNode.Nodes.Add(tmpVar.Name, tmpVar.Name, 1)
        Next
        StartFenster.addTreeNode(tmpTreeNode)
    End Sub

    'Diese Funktion soll die nächste Ebene der Treeview füllen sobald die darüberliegende aufgerufen wird
    Public Sub ExpandNodeFurther(ByRef rootNode As TreeNode)
        If rootNode.Nodes.Count = 0 Then Exit Sub
        tmpTreeNode = rootNode
        Dim tmpTask = CurCPU.Tasks(rootNode.Text)
        Dim tmpGlobal = CurCPU.Variables(getRootNode(rootNode).Text)

        If tmpGlobal IsNot Nothing Then                                         'GlobaleVariablen
            Dim tmpVar As Variable = StringtoVar(TreeNodeToVarName(rootNode))
            If tmpVar Is Nothing Then Stop
            For Each childVar As Variable In tmpVar.Members.Values
                AddHandler childVar.ValueChanged, AddressOf TmpVar_Connected
                childVar.Active = True
                childVar.Connect()
            Next
            Exit Sub
        End If


        If tmpTask Is Nothing Then                                              'TaskVariablen
            Dim realtmpTask As Task = CurCPU.Tasks(getRootNode(rootNode).Text)
            Dim tmpVar As Variable = StringtoVar(TreeNodeToVarName(rootNode, 1), realtmpTask)
            If tmpVar Is Nothing Then Stop
            For Each childVar As Variable In tmpVar.Members.Values
                AddHandler childVar.ValueChanged, AddressOf TmpVar_Connected
                childVar.Active = True
                childVar.Connect()
            Next
            Exit Sub
        End If

        For Each Var As DictionaryEntry In tmpTask.Variables                      'Tasks
            Dim tmpVar As Variable = tmpTask.Variables(Var.Key)
            AddHandler tmpVar.ValueChanged, AddressOf TmpVar_Connected
            tmpVar.Active = True
            tmpVar.Connect()
        Next
    End Sub

    'Diese Funktion fügt dem TreeNode ChildNodes mit dem Namen der Untervariablen hinzu,
    'solbald die "Root-"Variable verbunden ist
    Private Sub TmpVar_Connected(sender As Variable, e As PviEventArgs)
        If sender.Members Is Nothing Then Exit Sub
        For Each childVar As Variable In sender.Members.Values
            If tmpTreeNode.Nodes(sender.Name) Is Nothing Then Exit Sub
            tmpTreeNode.Nodes(sender.Name).Nodes.Add(childVar.Name, childVar.Name, 1)
        Next

        If sender.Members.Values.Count = 0 AndAlso sender.Value.ArrayLength > 1 Then
            For i As Integer = sender.Value.ArrayMinIndex To sender.Value.ArrayMaxIndex
                If tmpTreeNode.Nodes(sender.Name) Is Nothing Then Exit Sub
                tmpTreeNode.Nodes(sender.Name).Nodes.Add($"[{i}]", $"[{i}]", 1)
            Next
        End If

        sender.Disconnect()
    End Sub

    'Diese Funktion ermittelt aus einer Treenode den internen Variablennamen und gibt
    'diesen zurück
    Public Function TreeNodeToVarName(aktNode As TreeNode, Optional StartLayer As Integer = 0) As String
        Dim NodePuffer As New List(Of String)
        Dim IsVarPuffer As New List(Of Boolean)
        NodePuffer.Add(aktNode.Text)
        IsVarPuffer.Add(Convert.ToBoolean(aktNode.ImageIndex))

        While aktNode.Parent IsNot Nothing
            aktNode = aktNode.Parent
            NodePuffer.Add(aktNode.Text)
            IsVarPuffer.Add(Convert.ToBoolean(aktNode.ImageIndex))
        End While

        Dim outString As String = NodePuffer(NodePuffer.Count - 1 - StartLayer)
        For i As Integer = NodePuffer.Count - 2 - StartLayer To 0 Step -1
            If IsVarPuffer(i + 1) Then
                outString += $".{NodePuffer(i)}"
            Else
                outString += $":{NodePuffer(i)}"
            End If
        Next

        If outString.Last = "]"c Then outString = outString.Remove(outString.LastIndexOf("."), 1)

        Return outString
    End Function

    'Diese Funktion ermittelt den höchstgeordneten TreeNode eines mitgegebenen
    'Treenodes
    Function getRootNode(childNode As TreeNode) As TreeNode
        Dim CurNode As TreeNode = childNode
        While CurNode.Parent IsNot Nothing
            CurNode = CurNode.Parent
        End While
        Return CurNode
    End Function

    'Diese Funktion konfiguriert den Logger und startet ihn
    Public Sub StartLogger(LoggerName As String, ProzzesData As ListBox.ObjectCollection, SampTime As Integer, RecMode As Integer)
        Dim LoggerVar As Variable = CurCPU.Variables(LoggerName)
        If Not LoggerVar.DataValid Then Exit Sub
        LoggerVar.WriteValueAutomatic = False
        LoggerVar.Value("In.AuswahlRecorderMode") = RecMode + 1
        LoggerVar.Value("In.SamplingTime") = SampTime
        For i As Integer = 0 To LoggerVar.Members("In").Members("Variable").Value.ArrayLength - 1 'ProzzesData.Count - 1
            If i < ProzzesData.Count Then
                LoggerVar.Value($"In.Variable[{i}]") = New Value(ProzzesData(i))
            Else
                LoggerVar.Value($"In.Variable[{i}]") = New Value("")
            End If

        Next
        LoggerVar.WriteValue()
        LoggerVar.WriteValueAutomatic = True
        Dim startIn1s As Timer = New Timer
        startIn1s.Interval = 2000
        startIn1s.Start()
        AddHandler startIn1s.Tick, AddressOf LateLoggerStart
        '--'
    End Sub

    'Funktion stellt das Timer.Tick Event. Wird nach einer gewissen Zeit nach dem Loggerstartklick ausgeführt.
    'Bei sofortige ausfürung gingen Variablen verloren
    Private Sub LateLoggerStart(sender As Timer, e As EventArgs)
        Dim LoggerVar As Variable = CurCPU.Variables(CurConfig.DataRecoderName)
        If Not LoggerVar.DataValid Then Exit Sub
        LoggerVar.WriteValueAutomatic = False
        LoggerVar.Value("In.AufzeichnungStart") = True
        LoggerVar.WriteValue()
        LoggerVar.WriteValueAutomatic = True
        sender.Stop()
    End Sub

    'Diese Funktion stop den Logger
    Public Sub StopLogger(LoggerName As String)
        Dim LoggerVar As Variable = CurCPU.Variables(LoggerName)
        If Not LoggerVar.DataValid Then Exit Sub
        LoggerVar.WriteValueAutomatic = False
        LoggerVar.Value("In.AufzeichnungStart") = False
        'LoggerVar.Value("In.AuswahlRecorderMode") = 0
        LoggerVar.WriteValue()
        LoggerVar.WriteValueAutomatic = True
    End Sub

    'Diese Funktion findet zu einer Internen Variablenbezeichnung die Variable im mitgegebenen
    'Task, unabhängig ob die Variable Teil sonstiger komplexer Strukturen ist
    Private Function StringtoVar(Name As String, Optional Task As Task = Nothing) As Variable
        Dim VarNames As String() = Name.Split((New Char() {".", ":"}))
        Dim CurVar As Variable = Nothing
        If Task IsNot Nothing Then CurVar = Task.Variables(VarNames(0)) Else CurVar = CurCPU.Variables(VarNames(0))
        For i As Integer = 1 To VarNames.Length - 1
            If CurVar Is Nothing Then Return Nothing
            For Each Var As Variable In CurVar.Members.Values
                If Var.Name = VarNames(i) Then
                    CurVar = Var
                    Exit For
                End If
            Next
        Next
        Return CurVar
    End Function

    'Diese Funktion verbindet die Variable welche den aktuellen FileName beinhaltet
    'Und fügt einen Handler bei Wertänderung der Variable hinzu
    Public Sub LookOnLoggerStats(LoggerName As String)
        Dim LoggerVar As Variable = CurCPU.Variables(LoggerName)
        If Not LoggerVar.DataValid Then Exit Sub
        Dim LoggerStartet As Variable = LoggerVar.Members("In").Members("AufzeichnungStart")            'DataRecorder.In.AufzeichnungStart
        Dim LoggerOutVar As Variable = LoggerVar.Members("Out")                                         'DataRecorder.Out
        Dim LoggerFileOutVar As Variable = LoggerOutVar.Members("AktuellerDateiname")                               'DataRecorder.Out.AktuellerDateiname
        Dim LoggerVarOK As Variable = LoggerOutVar.Members("VariablenRegistierungOk")                                   'DataRecorder.Out.VariablenRegistrierungOK
        LoggerStartet.Active = True
        LoggerStartet.Connect()
        LoggerFileOutVar.Active = True
        LoggerFileOutVar.Connect()
        LoggerVarOK.Active = True
        LoggerVarOK.Connect()
        AddHandler LoggerVarOK.ValueChanged, AddressOf LogVarOkChange
        AddHandler LoggerFileOutVar.ValueChanged, AddressOf NewFileName
        AddHandler LoggerStartet.ValueChanged, AddressOf LoggerStartetChanged
        CurFileNameVar = LoggerFileOutVar
        VarRegOk = LoggerVarOK
        StartFenster.setCurCsvFile(LoggerFileOutVar.Value)
    End Sub

    'Diese Funktion wird aufgerufen wenn der Wert von DataRecoder.In.AufzeichnungStart sich ändert und gibt den Wert
    'von AufzeichnungStart und Variable an das Startfenster weiter
    'Sollte der Logger aktiv sein, wird der FtpDownloader gestartet
    Private Sub LoggerStartetChanged(sender As Variable, e As PviEventArgs)
        StartFenster.setLLoggerStart(sender.Value)
        Dim LoggerVar As Variable = CurCPU.Variables(CurConfig.DataRecoderName)
        Dim LoggerInVarsVar As Variable = LoggerVar.Members("In").Members("Variable") 'DataRecorder.In.Variable
        StartFenster.setLBChoosenObjItems(LoggerInVarsVar.Value.ArrayData)
        If sender.Value.ToIECString = "True" Then StartFenster.startFtpDownloader()
    End Sub


    Private Sub LogVarOkChange(sender As Variable, e As PviEventArgs)
        StartFenster.setVarOk(sender.Value)
    End Sub

    'Diese Funktion gibt bei der Änderung des Dateinamens, die Information, dass
    'der Name geändert wurde an das StartFenster weiter
    Private Sub NewFileName(sender As Variable, e As PviEventArgs)
        StartFenster.NewCsvFile(CurFileNameVar.Value)
    End Sub

    'Diese Funktion löst die Verbindung zu der Variablen welchen den aktuellen DateiNamen beeinhaltet auf
    Public Sub StopLookingOnFileName()
        Try
            CurFileNameVar.Active = False
            CurFileNameVar.Disconnect()
            VarRegOk.Active = False
            VarRegOk.Disconnect()
        Catch ex As system.Exception
            Console.WriteLine("Hier könnte ihr Fehler stehen")
        End Try
    End Sub

End Class
