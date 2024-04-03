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

    Sub addTreeNode(AdderNode As TreeNode)
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
#End Region



End Class
