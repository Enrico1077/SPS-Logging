Public Class StartFenster

    Dim PVIC As PVIClient
    Dim FTPC As FTP_Client
    Dim rs As Resizer = New Resizer
    Dim isMaximized As Boolean = False
    Dim FTPTimer As Timer


    Sub New()
        InitializeComponent()
        CB_IPAddressen.Items.AddRange(loadSavedIps.toArray)
        If CB_IPAddressen.Items.Count > 0 Then CB_IPAddressen.SelectedIndex = 0
        If CB_LogMode.Items.Count > 0 Then CB_LogMode.SelectedIndex = 0
        If CB_DownloadModi.Items.Count > 0 Then CB_DownloadModi.SelectedIndex = 0
        Dim HomeDir = IO.Directory.GetCurrentDirectory
        If Not My.Computer.FileSystem.FileExists(HomeDir + configName) Then saveDefaultXML(HomeDir + configName)
        ReadConfig(HomeDir + configName)



    End Sub




#Region "PublicSetter"

    Sub setLCpuConnectedText(Text As String)
        L_CpuConnected.Text = Text
    End Sub

    Sub addTreeNode(ByRef AdderNode As TreeNode)
        TV_PVIVars.Nodes.Add(AdderNode)
    End Sub

    Sub setValCountText(Optional aktNum As Integer = -1, Optional MaxNum As Integer = -1)
        If Not aktNum = -1 AndAlso Not MaxNum = -1 Then L_VarCount.Text = $"Anzahl: {aktNum}/{MaxNum}" : Exit Sub
        Dim CountParts As String() = L_VarCount.Text.Split(New Char() {":", "/"})
        If Not aktNum = -1 Then L_VarCount.Text = $"Anzahl: {aktNum}/{CountParts(2)}" : Exit Sub
        If Not MaxNum = -1 Then L_VarCount.Text = $"Anzahl: {CountParts(1)}/{MaxNum}"
    End Sub
#End Region

#Region "Visuals"
    Private Sub StartFenster_Load(sender As Object, e As EventArgs) Handles Me.Load
        rs.FindAllControls(Me)
    End Sub

    Private Sub StartFenster_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        rs.ResizeAllControls(Me)
    End Sub

    Private Sub StartFenster_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If WindowState = FormWindowState.Maximized Then
            isMaximized = True
            rs.ResizeAllControls(Me)
        End If

        If isMaximized AndAlso WindowState = FormWindowState.Normal Then
            isMaximized = False
            rs.ResizeAllControls(Me)
        End If
    End Sub

#End Region

#Region "UserInput"
    Private Sub B_SPSSuche_Click(sender As Object, e As EventArgs) Handles B_SPSSuche.Click
        CB_IPAddressen.Items.AddRange(searchForPLCs("192.168.0.140", "192.168.0.150").ToArray)
        If CB_IPAddressen.Items.Count > 1 Then CB_IPAddressen.SelectedIndex = 0
    End Sub

    Private Sub B_ConnectCpu_Click(sender As Object, e As EventArgs) Handles B_ConnectCpu.Click
        If PVIC Is Nothing Then PVIC = New PVIClient(Me, CB_IPAddressen.Text)
        PVIC.connectService()
    End Sub

    Private Sub TV_PVIVars_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TV_PVIVars.AfterSelect
        If e.Node.Nodes.Count > 0 Then Exit Sub
        Dim CountParts As String() = L_VarCount.Text.Split(New Char() {":", "/"})
        If CInt(CountParts(2)) = CInt(CountParts(1)) Then Exit Sub
        Dim Name As String = PVIC.TreeNodeToVarName(e.Node)
        If Not LB_ChoosenObj.Items.Contains(Name) Then LB_ChoosenObj.Items.Add(Name)
        setValCountText(LB_ChoosenObj.Items.Count)
    End Sub

    Private Sub LB_ChoosenObj_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_ChoosenObj.SelectedIndexChanged
        LB_ChoosenObj.Items.Remove(LB_ChoosenObj.SelectedItem)
        setValCountText(LB_ChoosenObj.Items.Count)
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
        PVIC.StartLogger(CurConfig.DataRecoderName, LB_ChoosenObj.Items, TB_SampTime.Text, CB_LogMode.SelectedIndex)
    End Sub

    Private Sub B_LoggerStop_Click(sender As Object, e As EventArgs) Handles B_LoggerStop.Click
        PVIC.StopLogger(CurConfig.DataRecoderName)
    End Sub

    Private Sub b_CpuDisconnect_Click(sender As Object, e As EventArgs) Handles b_CpuDisconnect.Click
        PVIC.DisconnectService()
        TV_PVIVars.Nodes.Clear()
    End Sub

    Private Sub B_FTPStart_Click(sender As Object, e As EventArgs) Handles B_FTPStart.Click
        If FTPC Is Nothing Then FTPC = New FTP_Client(CurConfig.FTP_IPAdress, CurConfig.FTP_UserName, CurConfig.FTP_Password)
        If CB_DownloadModi.SelectedIndex = 0 Then
            PVIC.LookOnFileName(CurConfig.DataRecoderName)
        Else
            FTPTimer = New Timer
            FTPTimer.Interval = CInt(TB_DownloadTime.Text) * 1000
            AddHandler FTPTimer.Tick, AddressOf NewCsvFile
            FTPTimer.Start()

        End If
    End Sub



    Private Sub B_StopFTP_Click(sender As Object, e As EventArgs) Handles B_StopFTP.Click
        If CB_DownloadModi.SelectedIndex = 0 Then
            PVIC.StopLookingOnFileName()
        Else
            FTPTimer.Stop()
        End If

    End Sub

    Private Sub CB_DownloadModi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_DownloadModi.SelectedIndexChanged
        If CB_DownloadModi.SelectedIndex = 0 Then
            L_DownloadTime.Enabled = False
            TB_DownloadTime.Enabled = False
        Else
            L_DownloadTime.Enabled = True
            TB_DownloadTime.Enabled = True
        End If
    End Sub

    Private Sub B_OpenFBD_Click(sender As Object, e As EventArgs) Handles B_OpenFBD.Click
        FBD_FTPSave.ShowDialog()
        If FBD_FTPSave.SelectedPath Is Nothing Then Exit Sub
        TB_DirPath.Text = FBD_FTPSave.SelectedPath
    End Sub

#End Region

    Sub NewCsvFile()
        FTPC.DownloadFile(FTPC.FindLatestFile(), TB_DirPath.Text)
    End Sub


End Class
