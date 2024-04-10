Public Class StartFenster

    Dim PVIC As PVIClient
    Dim FTPC As FTP_Client
    Dim rs As Resizer = New Resizer
    Dim isMaximized As Boolean = False
    Dim FTPTimer As Timer
    Dim CurCsvFile As String


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

    Sub setCurCsvFile(CsvFile As String)
        CurCsvFile = CsvFile
    End Sub

    'Diese Funktion zeigt den StatusText im Fenster an
    Sub setLCpuConnectedText(Text As String)
        L_CpuConnected.Text = Text
    End Sub

    'Diese Funktion fügt der TreeView einen TreeNode hinzu
    Sub addTreeNode(ByRef AdderNode As TreeNode)
        TV_PVIVars.Nodes.Add(AdderNode)
    End Sub

    'Diese Funktion stellt dar wie viele Variablen markiert sind und wie viele
    'maximal markiert werden können 
    Sub setValCountText(Optional aktNum As Integer = -1, Optional MaxNum As Integer = -1)
        If Not aktNum = -1 AndAlso Not MaxNum = -1 Then L_VarCount.Text = $"Anzahl: {aktNum}/{MaxNum}" : Exit Sub
        Dim CountParts As String() = L_VarCount.Text.Split(New Char() {":", "/"})
        If Not aktNum = -1 Then L_VarCount.Text = $"Anzahl: {aktNum}/{CountParts(2)}" : Exit Sub
        If Not MaxNum = -1 Then L_VarCount.Text = $"Anzahl: {CountParts(1)}/{MaxNum}"
    End Sub
#End Region

#Region "Visuals"
    'Wenn das Fenster geladen wird werden alle Komponenten in der Resizer Klasse gespeichert
    Private Sub StartFenster_Load(sender As Object, e As EventArgs) Handles Me.Load
        rs.FindAllControls(Me)
    End Sub

    'Wenn die Größe des Fensters angepasst wurde wird die Größe alle Komponenten entsprechend mit angepasst
    Private Sub StartFenster_ResizeEnd(sender As Object, e As EventArgs) Handles Me.ResizeEnd
        rs.ResizeAllControls(Me)
    End Sub

    'Wenn das Fenster Maximiert oder Mininimiert wurde wird die Größe aller Komponente entsprechend angepasst
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

    'Wenn der Such-Knopf gedrückt wird wird ein fester Ip-Bereich angepinngt und
    'die Netzwerkpartner dargestellt
    Private Sub B_SPSSuche_Click(sender As Object, e As EventArgs) Handles B_SPSSuche.Click
        CB_IPAddressen.Items.AddRange(searchForPLCs("192.168.0.140", "192.168.0.150").ToArray)
        If CB_IPAddressen.Items.Count > 1 Then CB_IPAddressen.SelectedIndex = 0
    End Sub

    'Wenn der Connect CPU Knopf gedrückt wird, wird die SPS an der angegebenden IP-Addresse verbunden
    Private Sub B_ConnectCpu_Click(sender As Object, e As EventArgs) Handles B_ConnectCpu.Click
        If PVIC Is Nothing Then PVIC = New PVIClient(Me, CB_IPAddressen.Text)
        PVIC.connectService()
    End Sub

    'Wird ein Element in der untersten Ebene in der TreeView angeklickt wird es
    'geeignet Formatiert In der ListBox dargestellt
    Private Sub TV_PVIVars_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TV_PVIVars.AfterSelect
        If e.Node.Nodes.Count > 0 Then Exit Sub
        Dim CountParts As String() = L_VarCount.Text.Split(New Char() {":", "/"})
        If CInt(CountParts(2)) = CInt(CountParts(1)) Then Exit Sub
        Dim Name As String = PVIC.TreeNodeToVarName(e.Node)
        If Not LB_ChoosenObj.Items.Contains(Name) Then LB_ChoosenObj.Items.Add(Name)
        setValCountText(LB_ChoosenObj.Items.Count)
    End Sub

    'Wird ein Element in der ListBox angeklickt wird es aus dieser entfernt
    Private Sub LB_ChoosenObj_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_ChoosenObj.SelectedIndexChanged
        LB_ChoosenObj.Items.Remove(LB_ChoosenObj.SelectedItem)
        setValCountText(LB_ChoosenObj.Items.Count)
    End Sub

    'Bei einem Klick auf den Sortieren Knopf wird die TreeView alphabetisch sortiert
    Private Sub B_Sort_Click(sender As Object, e As EventArgs) Handles B_Sort.Click
        TV_PVIVars.BeginUpdate()
        TV_PVIVars.Sort()
        TV_PVIVars.EndUpdate()
    End Sub

    'Wird ein Knoten der TreeView geöffnet wird die Ebene unter der neu geöffneten Ebene für alle
    'betroffenden Knoten geladen 
    Private Sub TV_PVIVars_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles TV_PVIVars.BeforeExpand
        PVIC.ExpandNodeFurther(e.Node)
    End Sub

    'Bei einem Klick auf den Knopf LoggerStart wird der Logger wie gewünscht konfiguriert und 
    'anschließend gestartet
    Private Sub B_LoggerStart_Click(sender As Object, e As EventArgs) Handles B_LoggerStart.Click
        PVIC.StartLogger(CurConfig.DataRecoderName, LB_ChoosenObj.Items, TB_SampTime.Text, CB_LogMode.SelectedIndex)
    End Sub

    'Bei einem Klick auf den Knopf Logger stoppen wird der Logger gestoppt =)
    Private Sub B_LoggerStop_Click(sender As Object, e As EventArgs) Handles B_LoggerStop.Click
        PVIC.StopLogger(CurConfig.DataRecoderName)
    End Sub

    'Bei einem Klick auf den Knopf CPU Trennen werden alle PVI-Verweise gelöscht und die TreeView geleert
    Private Sub b_CpuDisconnect_Click(sender As Object, e As EventArgs) Handles b_CpuDisconnect.Click
        PVIC.DisconnectService()
        TV_PVIVars.Nodes.Clear()
    End Sub

    'Bei einem Klickt auf den Knopf FTP Download wird wie gewünscht entweder ein Timer gestartet welcher nach X Sekunden
    'die letzte csv Datei herunterlädt oder eine PVI_variable verbunden, welche den aktuellen CSV-File_Name beihaltet und 
    'bei änderung des File-Names die File heruntergeladen 
    Private Sub B_FTPStart_Click(sender As Object, e As EventArgs) Handles B_FTPStart.Click
        If FTPC Is Nothing Then FTPC = New FTP_Client(CurConfig.FTP_IPAdress, CurConfig.FTP_UserName, CurConfig.FTP_Password)
        If CB_DownloadModi.SelectedIndex = 0 Then
            PVIC.LookOnFileName(CurConfig.DataRecoderName)
        Else
            FTPTimer = New Timer
            FTPTimer.Interval = CInt(TB_DownloadTime.Text) * 1000
            AddHandler FTPTimer.Tick, AddressOf DownloadNewstCsvFile
            FTPTimer.Start()
        End If
    End Sub

    'Wird auf den Knopf FTP-Download stoppen gedrückt werden keine weiteren CSV-Dateien heruntergeladen
    Private Sub B_StopFTP_Click(sender As Object, e As EventArgs) Handles B_StopFTP.Click
        If CB_DownloadModi.SelectedIndex = 0 Then
            PVIC.StopLookingOnFileName()
        Else
            FTPTimer.Stop()
        End If

    End Sub

    'Wird der DownloadModi in der ComboBox geändert wird die Zeitzeile passend enabled oder disabled
    Private Sub CB_DownloadModi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CB_DownloadModi.SelectedIndexChanged
        If CB_DownloadModi.SelectedIndex = 0 Then
            L_DownloadTime.Enabled = False
            TB_DownloadTime.Enabled = False
        Else
            L_DownloadTime.Enabled = True
            TB_DownloadTime.Enabled = True
        End If
    End Sub

    'Bei einem Klick auf den Knopf mit den drei Punkten wird ein FolderBrowserDialog geöffnet, in welchem 
    'das ZielVerzeichnis für die heruntergeladenen CSV-Dateien festgelegt werden kann
    Private Sub B_OpenFBD_Click(sender As Object, e As EventArgs) Handles B_OpenFBD.Click
        FBD_FTPSave.ShowDialog()
        If FBD_FTPSave.SelectedPath Is Nothing Then Exit Sub
        TB_DirPath.Text = FBD_FTPSave.SelectedPath
    End Sub

#End Region

    'Diese Funktion lässt den FTP-Client die angegebende CSV-Datei herunterladen
    Sub NewCsvFile(csvFile As String)
        FTPC.DownloadFile(CurCsvFile, TB_DirPath.Text)
        CurCsvFile = csvFile
    End Sub

    'Diese Funktion lässt den FTP-Client die aktuellste CSV-Datei herunterladen
    Sub DownloadNewstCsvFile()
        'ToDo: Es sollte nicht nur die aktuellste Datei runtergeladen werden, sondern 
        '      beim Dateinwechsel auch noch einmal die voraktuellste
        FTPC.DownloadFile(FTPC.FindLatestFile(), TB_DirPath.Text)
    End Sub

    Private Sub B_startUpload_Click(sender As Object, e As EventArgs) Handles B_startUpload.Click
        Dim InfluxC As InfluxClient = New InfluxClient(
                        "RIdhMT3ZXRFT8Lk1cHYMpwCpASQyva_fOUUf7nDPJGflYnNAnGmxIR78a68j76Kgof8tnJYJGNdRnfI30q4ccw==",
                        "http://localhost:8087",
                        "123",
                        "Test"
        )
        InfluxC.writeCSVToInflux(InfluxC.AnalyseCSV("C:\Temp\FTPDateien\Datalog_2024_04_09_14_13_55.csv"))
        Console.WriteLine("data has been uploaded")

    End Sub
End Class
