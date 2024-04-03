Public Class StartFenster

    Dim PVIC As PVIClient

    Sub New()
        InitializeComponent()
        CB_IPAddressen.Items.AddRange(loadSavedIps.toArray)
        If CB_IPAddressen.Items.Count > 1 Then CB_IPAddressen.SelectedIndex = 0

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
        CB_IPAddressen.Items.AddRange(searchForPLCs("126.255.255.150", "127.0.0.10").ToArray)
        If CB_IPAddressen.Items.Count > 1 Then CB_IPAddressen.SelectedIndex = 0
    End Sub

    Private Sub B_ConnectCpu_Click(sender As Object, e As EventArgs) Handles B_ConnectCpu.Click
        If PVIC Is Nothing Then PVIC = New PVIClient(Me, CB_IPAddressen.Text)
        PVIC.connectService()
    End Sub

    Private Sub TV_PVIVars_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TV_PVIVars.AfterSelect
        TVtoLV(e.Node)
    End Sub

    Private Sub LB_ChoosenObj_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_ChoosenObj.SelectedIndexChanged
        LB_ChoosenObj.Items.Remove(LB_ChoosenObj.SelectedItem)
    End Sub
#End Region

    Private Sub TVtoLV(aktNode As TreeNode)
        If aktNode.Nodes.Count > 0 Then Exit Sub
        Dim outString As String = aktNode.Text
        While True
            If aktNode.Parent Is Nothing Then Exit While
            aktNode = aktNode.Parent
            outString = aktNode.Text + ":" + outString
        End While
        If Not LB_ChoosenObj.Items.Contains(outString) Then LB_ChoosenObj.Items.Add(outString)

    End Sub


End Class
