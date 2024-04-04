Public Class StartFenster

    Dim PVIC As PVIClient


    Sub New()
        InitializeComponent()
        CB_IPAddressen.Items.AddRange(loadSavedIps.toArray)
        If CB_IPAddressen.Items.Count > 0 Then CB_IPAddressen.SelectedIndex = 0
        Dim HomeDir = IO.Directory.GetCurrentDirectory
        If Not My.Computer.FileSystem.FileExists(HomeDir + ConfigName) Then saveDefaultXML(HomeDir + ConfigName)
    End Sub

#Region "PublicVisualSetter"
    Sub setLCpuConnectedText(Text As String)
        L_CpuConnected.Text = Text
    End Sub

    Sub addTreeNode(ByRef AdderNode As TreeNode)
        TV_PVIVars.Nodes.Add(AdderNode)
    End Sub
#End Region

#Region "UserInput"
    Private Sub B_SPSSuche_Click(sender As Object, e As EventArgs) Handles B_SPSSuche.Click
        CB_IPAddressen.Items.AddRange(searchForPLCs("126.255.255.150", "127.0.0.1").ToArray)
        If CB_IPAddressen.Items.Count > 1 Then CB_IPAddressen.SelectedIndex = 0
    End Sub

    Private Sub B_ConnectCpu_Click(sender As Object, e As EventArgs) Handles B_ConnectCpu.Click
        If PVIC Is Nothing Then PVIC = New PVIClient(Me, CB_IPAddressen.Text)
        PVIC.connectService()
    End Sub

    Private Sub TV_PVIVars_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TV_PVIVars.AfterSelect
        If e.Node.Nodes.Count > 0 Then Exit Sub
        Dim Name As String = PVIC.TreeNodeToVarName(e.Node)
        If Not LB_ChoosenObj.Items.Contains(Name) Then LB_ChoosenObj.Items.Add(Name)
    End Sub

    Private Sub LB_ChoosenObj_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_ChoosenObj.SelectedIndexChanged
        LB_ChoosenObj.Items.Remove(LB_ChoosenObj.SelectedItem)
    End Sub

    Private Sub B_Sort_Click(sender As Object, e As EventArgs) Handles B_Sort.Click
        TV_PVIVars.BeginUpdate()
        TV_PVIVars.Sort()
        TV_PVIVars.EndUpdate()
    End Sub

    Private Sub TV_PVIVars_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles TV_PVIVars.BeforeExpand
        PVIC.ExpandNodeFurther(e.Node)
    End Sub

    Private Sub B_LoggerStart_Click(sender As Object, e As EventArgs) Handles B_LoggerStart.Click
        PVIC.StartLogger(CurConfig.DataRecoderName, LB_ChoosenObj.Items)
    End Sub

    Private Sub B_LoggerStop_Click(sender As Object, e As EventArgs) Handles B_LoggerStop.Click
        PVIC.StopLogger(CurConfig.DataRecoderName)
    End Sub



#End Region




End Class
