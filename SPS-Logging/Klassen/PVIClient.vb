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

    Sub connectService()
        If CurService Is Nothing Then
            CurService = New Service("service")
            AddHandler CurService.Error, AddressOf service_error
            AddHandler CurService.Connected, AddressOf service_Connected
        End If
        CurService.Connect()
    End Sub

    Sub service_error(sender As Object, e As PviEventArgs)
        StartFenster.setLCpuConnectedText($"Error: {e.ErrorText}")
    End Sub

    Sub service_Connected()
        If CurCPU Is Nothing Then
            CurCPU = New Cpu(CurService, "cpu")
            AddHandler CurCPU.Connected, AddressOf cpu_connected
            CurCPU.Connection.DeviceType = DeviceType.ANSLTcp
            CurCPU.Connection.ANSLTcp.DestinationIpAddress = CpuIP
        End If
        CurCPU.Connect()
    End Sub

    Private Sub cpu_connected(sender As Object, e As PviEventArgs)
        StartFenster.setLCpuConnectedText($"{CurCPU.HardwareInfo} is connected")  'Nochmal mit echter SPS testen
        AddHandler CurCPU.Tasks.Uploaded, AddressOf Cpu_Tasks_Uploaded
        CurCPU.Variables.Upload()
        CurCPU.Tasks.Upload()
    End Sub

    Private Sub Cpu_Tasks_Uploaded(sender As Object, e As PviEventArgs)
        For Each Task As DictionaryEntry In CurCPU.Tasks
            Dim tmpTask As Task = CurCPU.Tasks(Task.Key)
            AddHandler tmpTask.Connected, AddressOf Task_Connected
            tmpTask.Connect()
        Next
    End Sub

    Private Sub Task_Connected(sender As Object, e As PviEventArgs)
        Dim tmpTask As Task = sender
        AddHandler tmpTask.Variables.Uploaded, AddressOf Task_Variables_Uploaded
        tmpTask.Variables.Upload()
    End Sub

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
