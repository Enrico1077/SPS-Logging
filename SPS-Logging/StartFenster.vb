Public Class StartFenster

    Dim PVIC As PVIClient
    Dim FTPC As FTP_Client
    Dim rs As Resizer = New Resizer
    Dim isMaximized As Boolean = False
    Dim FTPTimer As Timer
    Dim CurCsvFile As String
    Dim FtpDownStarted As Boolean = False
    Dim InfluxUploadStarted As Boolean = False
    Dim lastSelTreeNode As TreeNode = Nothing
    Dim lastSelListBoxItem As Object = Nothing


    Sub New()
        InitializeComponent()
        CB_IPAddressen.Items.AddRange(loadSavedIps.ToArray)
        If CB_IPAddressen.Items.Count > 0 Then CB_IPAddressen.SelectedIndex = 0
        If CB_LogMode.Items.Count > 0 Then CB_LogMode.SelectedIndex = 0
        If CB_DownloadModi.Items.Count > 0 Then CB_DownloadModi.SelectedIndex = 0
        CBInfluxUploadModi.SelectedIndex = 1
        Dim HomeDir = IO.Directory.GetCurrentDirectory
        If Not My.Computer.FileSystem.FileExists(HomeDir + configName) Then saveDefaultXML(HomeDir + configName)
        ReadConfig(HomeDir + configName)
    End Sub

#Region "PublicSetter"

    'Mit dieser Funktion kann der Ftp-Downloader von außerhalb der Klasse gestartet werden 
    Sub startFtpDownloader()
        B_FTPStart.PerformClick()
    End Sub

    'Mit dieser Funktion lässt sich von außerhalb dieser Klasse die Textbox DirPath setzen
    Sub setFTPSpeicherPfad(Pfad As String)
        TB_DirPath.Text = Pfad
    End Sub

    'Mit dieser Funktion lässt sich von außerhalb der Klasse das Label L_ErgLogStart setzten 
    Sub setLLoggerStart(LogStart As Boolean)
        L_ErgLogStart.Text = LogStart
    End Sub

    'Mit dieser Funktion lässt sich von außerhalb der Klasse das LBChoosenObj mit
    'Items füllen 
    Sub setLBChoosenObjItems(varItems As String())
        varItems = varItems.Where(Function(s) Not String.IsNullOrEmpty(s)).ToArray()
        LB_ChoosenObj.Items.Clear()
        LB_ChoosenObj.Items.AddRange(varItems)
        setValCountText(aktNum:=LB_ChoosenObj.Items.Count)
    End Sub

    'Diese Funktion ist der Setter für das TBSampTime Textfeld
    Sub setTBSampTime(SampTime As String)
        TB_SampTime.Text = SampTime
    End Sub

    'Diese Funktion ist ein Setter für die CBLogMode Combobox
    'Es muss ein String übergeben, welcher gesettet wird falls
    'es in der CB ein Item mit diesem String gibt
    Sub setCBLogMode(Modus As String)
        If Not CB_LogMode.Items.Contains(Modus) Then Exit Sub
        CB_LogMode.SelectedIndex = CB_LogMode.FindString(Modus)
    End Sub

    'Die Funktion ist ein Setter für das L_LogPVIFehler label
    Sub setVarOk(VarOk As String)
        L_LogPVIFehler.Text = VarOk
    End Sub

    'Die Funktion ist ein Setter für das L_LogPVIFile label
    Sub setCurCsvFile(CsvFile As String)
        CurCsvFile = CsvFile
        L_LogPVIFile.Text = CsvFile
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

    'Diese Funktion deaktiviert je nach Auwahl des UploadModi die unpassenden Knöpfe
    Private Sub CBInfluxUploadModi_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBInfluxUploadModi.SelectedIndexChanged
        If CBInfluxUploadModi.SelectedIndex = 0 Then
            B_ChooseCsv.Enabled = False
            B_startUpload.Enabled = True
        Else
            B_ChooseCsv.Enabled = True
            B_startUpload.Enabled = False
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
    Private Sub TV_PVIVars_MouseDoubleClick(sender As TreeView, e As MouseEventArgs) Handles TV_PVIVars.DoubleClick
        If sender.SelectedNode Is Nothing Then Exit Sub
        Dim CountParts As String() = L_VarCount.Text.Split(New Char() {":", "/"})
        If CInt(CountParts(2)) = CInt(CountParts(1)) Then Exit Sub
        Dim Name As String = PVIC.TreeNodeToVarName(sender.SelectedNode)
        If Not LB_ChoosenObj.Items.Contains(Name) Then LB_ChoosenObj.Items.Add(Name)
        setValCountText(LB_ChoosenObj.Items.Count)
    End Sub

    'Wird ein Element in der ListBox angeklickt wird es aus dieser entfernt
    Private Sub LB_ChoosenObj_MouseDoubleClick(sender As Object, e As EventArgs) Handles LB_ChoosenObj.MouseDoubleClick
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
        If PVIC Is Nothing Then Exit Sub
        PVIC.StartLogger(CurConfig.DataRecoderName, LB_ChoosenObj.Items, TB_SampTime.Text, CB_LogMode.SelectedIndex)
        B_FTPStart.PerformClick()

    End Sub

    'Bei einem Klick auf den Knopf Logger stoppen wird der Logger gestoppt =)
    Private Sub B_LoggerStop_Click(sender As Object, e As EventArgs) Handles B_LoggerStop.Click
        If PVIC Is Nothing Then Exit Sub
        PVIC.StopLogger(CurConfig.DataRecoderName)
        B_StopFTP.PerformClick()
        setCurCsvFile("")
        setVarOk("-")
    End Sub

    'Bei einem Klick auf den Knopf CPU Trennen werden alle PVI-Verweise gelöscht und die TreeView geleert
    Private Sub b_CpuDisconnect_Click(sender As Object, e As EventArgs) Handles b_CpuDisconnect.Click
        If PVIC Is Nothing Then Exit Sub
        PVIC.DisconnectService()
        TV_PVIVars.Nodes.Clear()
    End Sub

    'Bei einem Klickt auf den Knopf FTP Download wird wie gewünscht entweder ein Timer gestartet welcher nach X Sekunden
    'die letzte csv Datei herunterlädt oder eine PVI_variable verbunden, welche den aktuellen CSV-File_Name beihaltet und 
    'bei änderung des File-Names die File heruntergeladen 
    Private Sub B_FTPStart_Click(sender As Object, e As EventArgs) Handles B_FTPStart.Click
        If CB_IPAddressen.Text Is Nothing Then Exit Sub
        L_ErgFTPStart.Text = "True"
        If FTPC Is Nothing Then FTPC = New FTP_Client(CB_IPAddressen.Text, CurConfig.FTP_UserName, CurConfig.FTP_Password)
        If CB_DownloadModi.SelectedIndex = 0 Then
            'PVIC.LookOnFileName(CurConfig.DataRecoderName)
            FtpDownStarted = True
        Else
            FTPTimer = New Timer With {.Interval = CInt(TB_DownloadTime.Text) * 1000}
            AddHandler FTPTimer.Tick, AddressOf DownloadNewstCsvFile
            FTPTimer.Start()
            FtpDownStarted = True
        End If
        CB_DownloadModi.Enabled = False
    End Sub

    'Wird auf den Knopf FTP-Download stoppen gedrückt werden keine weiteren CSV-Dateien heruntergeladen
    Private Sub B_StopFTP_Click(sender As Object, e As EventArgs) Handles B_StopFTP.Click
        If CB_DownloadModi.SelectedIndex = 0 Then
            'PVIC.StopLookingOnFileName()
        Else
            FTPTimer.Stop()
        End If
        DownloadNewstCsvFile()
        L_ErgFTPStart.Text = "False"
        CB_DownloadModi.Enabled = True
        FtpDownStarted = False
        If PVIC IsNot Nothing Then PVIC.StopLookingOnFileName()

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

    'Bei einem Klick auf den Knopf StartUpload werden die Daten welche in den CSV-Datei gespsiechert waren in die InfluxDB geuploaded
    Private Sub B_startUpload_Click(sender As Object, e As EventArgs) Handles B_startUpload.Click
        If OFD_CsvData.FileNames Is Nothing Then Exit Sub
        Dim InfluxC As InfluxClient = New InfluxClient(
                        CurConfig.Influx_API_Tok,
                        CurConfig.Influx_Address,
                        CurConfig.Influx_Comp,
                        TB_Bucket.Text
        )
        If CBInfluxUploadModi.SelectedIndex = 0 Then
            If Not FtpDownStarted Then Exit Sub
            InfluxUploadStarted = True
            InfluxC.writeCSVToInflux(InfluxC.AnalyseCSV($"{TB_DirPath.Text}\{CurCsvFile}"))
        Else
            For Each csvFile In OFD_CsvData.FileNames
                Dim FullPath = IO.Path.GetFullPath(csvFile)
                InfluxC.writeCSVToInflux(InfluxC.AnalyseCSV(FullPath))
            Next
            Console.WriteLine("data has been uploaded")
        End If


    End Sub

    'Bei einem Klick öffnet sich ein FileBrowseDialog in welche die CSV Datei für den Upload ausgewählt werden können 
    Private Sub B_ChooseCsv_Click(sender As Object, e As EventArgs) Handles B_ChooseCsv.Click
        If Not TB_DirPath.Text = "" Then OFD_CsvData.InitialDirectory = TB_DirPath.Text
        OFD_CsvData.ShowDialog()
        If OFD_CsvData.FileNames IsNot Nothing Then B_startUpload.Enabled = True
    End Sub

    'Bei einem Klick auf den LoggerConfig Laden Knopf, wird OpenFileDialog geöffnet und der Nutzer kann auswählen,
    'welche zuvorgespeicherte LoggerKonfiguration geladen werden soll
    Private Sub B_LogConfigLoad_Click(sender As Object, e As EventArgs) Handles B_LogConfigLoad.Click
        LoadLoggerConfig(Me)
    End Sub

    'Bei einem Klick auf den LoggerConfig Speichern Knopf, wird ein FolderBrowserDialog geöffnet und der Nutzer kann,
    'auswählen an welcher Stelle seine aktuelle LoggerKonfiguration gespeichert werden soll
    Private Sub B_LogConfigSave_Click(sender As Object, e As EventArgs) Handles B_LogConfigSave.Click
        SaveLoggerConfig(LB_ChoosenObj.Items, TB_SampTime.Text, CB_LogMode.Text, TB_DirPath.Text)
    End Sub



    'Die Funktion speichert bei einem Klick auf ein ListBox-Item das Item als KlassenVariable
    Private Sub LB_ChoosenObj_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LB_ChoosenObj.SelectedIndexChanged
        lastSelListBoxItem = LB_ChoosenObj.SelectedItem
    End Sub

    'Diese Funktionspeicher bei einem Klick auf eine TreeNode die Node als KlassenVariable
    Private Sub TV_PVIVars_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TV_PVIVars.AfterSelect
        lastSelTreeNode = TV_PVIVars.SelectedNode
    End Sub

    'Bei einem Klick auf den Links-Pfeil-Knopf wird das zuletzt ausgewählte Item der ListBox gelöscht
    Private Sub RB_Left_Click(sender As Object, e As EventArgs) Handles RB_Left.Click
        If lastSelListBoxItem Is Nothing Then Exit Sub
        If Not LB_ChoosenObj.Items.Contains(lastSelListBoxItem) Then Exit Sub
        LB_ChoosenObj.Items.Remove(lastSelListBoxItem)
        setValCountText(LB_ChoosenObj.Items.Count)
    End Sub

    'Bei einem Klick auf dem Rechtspfeil wird die zuletzt angelickte TreeNode der ListBox hinzugefügt
    Private Sub RB_Right_Click(sender As Object, e As EventArgs) Handles RB_Right.Click
        If lastSelTreeNode Is Nothing Then Exit Sub
        Dim CountParts As String() = L_VarCount.Text.Split(New Char() {":", "/"})
        If CInt(CountParts(2)) = CInt(CountParts(1)) Then Exit Sub
        Dim Name As String = PVIC.TreeNodeToVarName(lastSelTreeNode)
        If Not LB_ChoosenObj.Items.Contains(Name) Then LB_ChoosenObj.Items.Add(Name)
        setValCountText(LB_ChoosenObj.Items.Count)
    End Sub


#End Region


    'Diese Funktion lässt den FTP-Client die angegebende CSV-Datei herunterladen
    Sub NewCsvFile(csvFile As String)
        If FtpDownStarted Then FTPC.DownloadFile(CurCsvFile, TB_DirPath.Text)
        If InfluxUploadStarted Then B_startUpload.PerformClick()
        CurCsvFile = csvFile
    End Sub

    'Diese Funktion lässt den FTP-Client die aktuellste CSV-Datei herunterladen
    Sub DownloadNewstCsvFile()
        'ToDo: Es sollte nicht nur die aktuellste Datei runtergeladen werden, sondern 
        '      beim Dateinwechsel auch noch einmal die voraktuellste
        FTPC.DownloadFile(FTPC.FindLatestFile(), TB_DirPath.Text)
    End Sub

    Private Sub BStartInflux_Click(sender As Object, e As EventArgs) Handles BStartInflux.Click
        If Not My.Computer.FileSystem.FileExists(CurConfig.lokalInfluxPath) Then Exit Sub
        Process.Start(CurConfig.lokalInfluxPath, "--http-bind-address 127.0.0.1:8087")
    End Sub


End Class
