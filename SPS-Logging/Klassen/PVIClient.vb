Imports BR.AN.PviServices
Public Class PVIClient
    Dim CurService As Service
    Dim CurCPU As Cpu
    Dim CpuIP As String
    Dim StartFenster As StartFenster

    Sub New(SF As Form, CIP As String)
        StartFenster = SF
        CpuIP = CIP
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
        StartFenster.setLCpuConnectedText($"{CurCPU.HardwareInfo} is connected")  'Nochmal mit echter SPS testen
        AddHandler CurCPU.Tasks.Uploaded, AddressOf Cpu_Tasks_Uploaded
        AddHandler CurCPU.Variables.Uploaded, AddressOf Cpu_Variables_Uploaded
        CurCPU.Variables.Upload()
        CurCPU.Tasks.Upload()
    End Sub

    Private Sub Cpu_Variables_Uploaded(sender As Object, e As PviEventArgs)
        For Each Variable As DictionaryEntry In CurCPU.Variables
            Dim tmpVariable As Variable = CurCPU.Variables(Variable.Key)
            AddHandler tmpVariable.ValueChanged, AddressOf Cpu_Variable_Connected
            tmpVariable.Active = True
            tmpVariable.Connect()
        Next
    End Sub

    Private Sub Cpu_Variable_Connected(sender As Object, e As PviEventArgs)
        Dim tmpVariable As Variable = sender
        Dim rootNode = New TreeNode(tmpVariable.Name)
        If tmpVariable.Members IsNot Nothing Then
            For Each child As Variable In tmpVariable.Members.Values
                rootNode.Nodes.Add(child.Name)
            Next
        End If
        StartFenster.addTreeNode(rootNode)
        tmpVariable.Disconnect()
    End Sub

    Private Sub recVarDecoder(ByRef rootNode As TreeNode, Var As Variable)
        Dim childNode = New TreeNode(Var.Name)
        If Var.Members IsNot Nothing Then
            For Each child As Variable In Var.Members.Values
                recVarDecoder(childNode, child)
            Next
        End If
        rootNode.Nodes.Add(childNode)
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
        Dim tmpTreeNode As TreeNode = New TreeNode(tmpTask.Name)
        For Each Var As DictionaryEntry In tmpTask.Variables
            Dim tmpVar As Variable = tmpTask.Variables(Var.Key)
            tmpTreeNode.Nodes.Add(tmpVar.Name)
        Next
        StartFenster.addTreeNode(tmpTreeNode)
    End Sub

End Class
