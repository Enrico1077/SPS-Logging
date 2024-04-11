<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StartFenster
    Inherits System.Windows.Forms.Form

    'Das Formular überschreibt den Löschvorgang, um die Komponentenliste zu bereinigen.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Wird vom Windows Form-Designer benötigt.
    Private components As System.ComponentModel.IContainer

    'Hinweis: Die folgende Prozedur ist für den Windows Form-Designer erforderlich.
    'Das Bearbeiten ist mit dem Windows Form-Designer möglich.  
    'Das Bearbeiten mit dem Code-Editor ist nicht möglich.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StartFenster))
        Me.L_SPSSuche = New System.Windows.Forms.Label()
        Me.CB_IPAddressen = New System.Windows.Forms.ComboBox()
        Me.B_SPSSuche = New System.Windows.Forms.Button()
        Me.B_ConnectCpu = New System.Windows.Forms.Button()
        Me.L_CpuConnected = New System.Windows.Forms.Label()
        Me.TV_PVIVars = New System.Windows.Forms.TreeView()
        Me.IL_TreeView = New System.Windows.Forms.ImageList(Me.components)
        Me.L_TreeView = New System.Windows.Forms.Label()
        Me.LB_ChoosenObj = New System.Windows.Forms.ListBox()
        Me.L_ChosenObj = New System.Windows.Forms.Label()
        Me.B_LoggerStart = New System.Windows.Forms.Button()
        Me.B_Sort = New System.Windows.Forms.Button()
        Me.B_LoggerStop = New System.Windows.Forms.Button()
        Me.L_LoggerKonfig = New System.Windows.Forms.Label()
        Me.L_SampTime = New System.Windows.Forms.Label()
        Me.L_RecMode = New System.Windows.Forms.Label()
        Me.CB_LogMode = New System.Windows.Forms.ComboBox()
        Me.TB_SampTime = New System.Windows.Forms.TextBox()
        Me.b_CpuDisconnect = New System.Windows.Forms.Button()
        Me.L_VarCount = New System.Windows.Forms.Label()
        Me.L_Ftp = New System.Windows.Forms.Label()
        Me.L_FTP_Dir = New System.Windows.Forms.Label()
        Me.TB_DirPath = New System.Windows.Forms.TextBox()
        Me.B_OpenFBD = New System.Windows.Forms.Button()
        Me.L_DownloadModus = New System.Windows.Forms.Label()
        Me.CB_DownloadModi = New System.Windows.Forms.ComboBox()
        Me.B_FTPStart = New System.Windows.Forms.Button()
        Me.B_StopFTP = New System.Windows.Forms.Button()
        Me.L_DownloadTime = New System.Windows.Forms.Label()
        Me.TB_DownloadTime = New System.Windows.Forms.TextBox()
        Me.FBD_FTPSave = New System.Windows.Forms.FolderBrowserDialog()
        Me.L_ImportToInflux = New System.Windows.Forms.Label()
        Me.B_startUpload = New System.Windows.Forms.Button()
        Me.L_CSVFIles = New System.Windows.Forms.Label()
        Me.B_ChooseCsv = New System.Windows.Forms.Button()
        Me.L_Bucket = New System.Windows.Forms.Label()
        Me.TB_Bucket = New System.Windows.Forms.TextBox()
        Me.OFD_CsvData = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'L_SPSSuche
        '
        Me.L_SPSSuche.AutoSize = True
        Me.L_SPSSuche.Location = New System.Drawing.Point(24, 29)
        Me.L_SPSSuche.Name = "L_SPSSuche"
        Me.L_SPSSuche.Size = New System.Drawing.Size(229, 26)
        Me.L_SPSSuche.TabIndex = 0
        Me.L_SPSSuche.Text = "Bitte die IP-Addresse der Steuerung auswählen" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "oder eintragen"
        '
        'CB_IPAddressen
        '
        Me.CB_IPAddressen.FormattingEnabled = True
        Me.CB_IPAddressen.Location = New System.Drawing.Point(27, 58)
        Me.CB_IPAddressen.Name = "CB_IPAddressen"
        Me.CB_IPAddressen.Size = New System.Drawing.Size(163, 21)
        Me.CB_IPAddressen.TabIndex = 1
        '
        'B_SPSSuche
        '
        Me.B_SPSSuche.Location = New System.Drawing.Point(196, 58)
        Me.B_SPSSuche.Name = "B_SPSSuche"
        Me.B_SPSSuche.Size = New System.Drawing.Size(79, 23)
        Me.B_SPSSuche.TabIndex = 2
        Me.B_SPSSuche.Text = "Suchen"
        Me.B_SPSSuche.UseVisualStyleBackColor = True
        '
        'B_ConnectCpu
        '
        Me.B_ConnectCpu.Location = New System.Drawing.Point(27, 87)
        Me.B_ConnectCpu.Name = "B_ConnectCpu"
        Me.B_ConnectCpu.Size = New System.Drawing.Size(248, 23)
        Me.B_ConnectCpu.TabIndex = 3
        Me.B_ConnectCpu.Text = "CPU Verbinden"
        Me.B_ConnectCpu.UseVisualStyleBackColor = True
        '
        'L_CpuConnected
        '
        Me.L_CpuConnected.AutoSize = True
        Me.L_CpuConnected.Location = New System.Drawing.Point(24, 117)
        Me.L_CpuConnected.Name = "L_CpuConnected"
        Me.L_CpuConnected.Size = New System.Drawing.Size(0, 13)
        Me.L_CpuConnected.TabIndex = 4
        '
        'TV_PVIVars
        '
        Me.TV_PVIVars.ImageIndex = 0
        Me.TV_PVIVars.ImageList = Me.IL_TreeView
        Me.TV_PVIVars.Location = New System.Drawing.Point(27, 159)
        Me.TV_PVIVars.Name = "TV_PVIVars"
        Me.TV_PVIVars.SelectedImageIndex = 0
        Me.TV_PVIVars.Size = New System.Drawing.Size(247, 329)
        Me.TV_PVIVars.TabIndex = 5
        '
        'IL_TreeView
        '
        Me.IL_TreeView.ImageStream = CType(resources.GetObject("IL_TreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.IL_TreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.IL_TreeView.Images.SetKeyName(0, "Task.png")
        Me.IL_TreeView.Images.SetKeyName(1, "Var.png")
        Me.IL_TreeView.Images.SetKeyName(2, "Global.png")
        '
        'L_TreeView
        '
        Me.L_TreeView.AutoSize = True
        Me.L_TreeView.Location = New System.Drawing.Point(24, 143)
        Me.L_TreeView.Name = "L_TreeView"
        Me.L_TreeView.Size = New System.Drawing.Size(114, 13)
        Me.L_TreeView.TabIndex = 6
        Me.L_TreeView.Text = "Gefundene PVI-Ojekte"
        '
        'LB_ChoosenObj
        '
        Me.LB_ChoosenObj.FormattingEnabled = True
        Me.LB_ChoosenObj.Location = New System.Drawing.Point(306, 159)
        Me.LB_ChoosenObj.Name = "LB_ChoosenObj"
        Me.LB_ChoosenObj.Size = New System.Drawing.Size(247, 329)
        Me.LB_ChoosenObj.TabIndex = 7
        '
        'L_ChosenObj
        '
        Me.L_ChosenObj.AutoSize = True
        Me.L_ChosenObj.Location = New System.Drawing.Point(303, 143)
        Me.L_ChosenObj.Name = "L_ChosenObj"
        Me.L_ChosenObj.Size = New System.Drawing.Size(128, 13)
        Me.L_ChosenObj.TabIndex = 8
        Me.L_ChosenObj.Text = "Ausgewählte PVI-Objekte"
        '
        'B_LoggerStart
        '
        Me.B_LoggerStart.Location = New System.Drawing.Point(306, 494)
        Me.B_LoggerStart.Name = "B_LoggerStart"
        Me.B_LoggerStart.Size = New System.Drawing.Size(123, 23)
        Me.B_LoggerStart.TabIndex = 9
        Me.B_LoggerStart.Text = "Logger Starten"
        Me.B_LoggerStart.UseVisualStyleBackColor = True
        '
        'B_Sort
        '
        Me.B_Sort.Location = New System.Drawing.Point(200, 136)
        Me.B_Sort.Name = "B_Sort"
        Me.B_Sort.Size = New System.Drawing.Size(75, 23)
        Me.B_Sort.TabIndex = 10
        Me.B_Sort.Text = "Sortieren"
        Me.B_Sort.UseVisualStyleBackColor = True
        '
        'B_LoggerStop
        '
        Me.B_LoggerStop.Location = New System.Drawing.Point(430, 494)
        Me.B_LoggerStop.Name = "B_LoggerStop"
        Me.B_LoggerStop.Size = New System.Drawing.Size(123, 23)
        Me.B_LoggerStop.TabIndex = 11
        Me.B_LoggerStop.Text = "Logger Stoppen"
        Me.B_LoggerStop.UseVisualStyleBackColor = True
        '
        'L_LoggerKonfig
        '
        Me.L_LoggerKonfig.AutoSize = True
        Me.L_LoggerKonfig.Location = New System.Drawing.Point(373, 29)
        Me.L_LoggerKonfig.Name = "L_LoggerKonfig"
        Me.L_LoggerKonfig.Size = New System.Drawing.Size(105, 13)
        Me.L_LoggerKonfig.TabIndex = 12
        Me.L_LoggerKonfig.Text = "Logger-Konfiguration" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'L_SampTime
        '
        Me.L_SampTime.AutoSize = True
        Me.L_SampTime.Location = New System.Drawing.Point(303, 66)
        Me.L_SampTime.Name = "L_SampTime"
        Me.L_SampTime.Size = New System.Drawing.Size(82, 13)
        Me.L_SampTime.TabIndex = 14
        Me.L_SampTime.Text = "Abtastrate in ms"
        '
        'L_RecMode
        '
        Me.L_RecMode.AutoSize = True
        Me.L_RecMode.Location = New System.Drawing.Point(303, 97)
        Me.L_RecMode.Name = "L_RecMode"
        Me.L_RecMode.Size = New System.Drawing.Size(80, 13)
        Me.L_RecMode.TabIndex = 15
        Me.L_RecMode.Text = "Logging-Modus"
        '
        'CB_LogMode
        '
        Me.CB_LogMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_LogMode.FormattingEnabled = True
        Me.CB_LogMode.Items.AddRange(New Object() {"Zeitlich Bedingt", "Änderungs Bedingt"})
        Me.CB_LogMode.Location = New System.Drawing.Point(391, 94)
        Me.CB_LogMode.Name = "CB_LogMode"
        Me.CB_LogMode.Size = New System.Drawing.Size(162, 21)
        Me.CB_LogMode.TabIndex = 16
        '
        'TB_SampTime
        '
        Me.TB_SampTime.Location = New System.Drawing.Point(391, 63)
        Me.TB_SampTime.Name = "TB_SampTime"
        Me.TB_SampTime.Size = New System.Drawing.Size(162, 20)
        Me.TB_SampTime.TabIndex = 17
        Me.TB_SampTime.Text = "100"
        '
        'b_CpuDisconnect
        '
        Me.b_CpuDisconnect.Location = New System.Drawing.Point(27, 494)
        Me.b_CpuDisconnect.Name = "b_CpuDisconnect"
        Me.b_CpuDisconnect.Size = New System.Drawing.Size(247, 23)
        Me.b_CpuDisconnect.TabIndex = 18
        Me.b_CpuDisconnect.Text = "CPU Trennen"
        Me.b_CpuDisconnect.UseVisualStyleBackColor = True
        '
        'L_VarCount
        '
        Me.L_VarCount.AutoSize = True
        Me.L_VarCount.Location = New System.Drawing.Point(479, 143)
        Me.L_VarCount.Name = "L_VarCount"
        Me.L_VarCount.Size = New System.Drawing.Size(62, 13)
        Me.L_VarCount.TabIndex = 19
        Me.L_VarCount.Text = "Anzahl: 0/0"
        '
        'L_Ftp
        '
        Me.L_Ftp.AutoSize = True
        Me.L_Ftp.Location = New System.Drawing.Point(675, 29)
        Me.L_Ftp.Name = "L_Ftp"
        Me.L_Ftp.Size = New System.Drawing.Size(92, 13)
        Me.L_Ftp.TabIndex = 20
        Me.L_Ftp.Text = "FTP-Konfiguration"
        '
        'L_FTP_Dir
        '
        Me.L_FTP_Dir.AutoSize = True
        Me.L_FTP_Dir.Location = New System.Drawing.Point(577, 66)
        Me.L_FTP_Dir.Name = "L_FTP_Dir"
        Me.L_FTP_Dir.Size = New System.Drawing.Size(106, 13)
        Me.L_FTP_Dir.TabIndex = 21
        Me.L_FTP_Dir.Text = "Speicher Verzeichnis"
        '
        'TB_DirPath
        '
        Me.TB_DirPath.Location = New System.Drawing.Point(689, 63)
        Me.TB_DirPath.Name = "TB_DirPath"
        Me.TB_DirPath.Size = New System.Drawing.Size(157, 20)
        Me.TB_DirPath.TabIndex = 22
        Me.TB_DirPath.Text = "C:\Temp\FTPDateien"
        '
        'B_OpenFBD
        '
        Me.B_OpenFBD.Location = New System.Drawing.Point(852, 63)
        Me.B_OpenFBD.Name = "B_OpenFBD"
        Me.B_OpenFBD.Size = New System.Drawing.Size(24, 20)
        Me.B_OpenFBD.TabIndex = 23
        Me.B_OpenFBD.Text = "..."
        Me.B_OpenFBD.UseVisualStyleBackColor = True
        '
        'L_DownloadModus
        '
        Me.L_DownloadModus.AutoSize = True
        Me.L_DownloadModus.Location = New System.Drawing.Point(577, 102)
        Me.L_DownloadModus.Name = "L_DownloadModus"
        Me.L_DownloadModus.Size = New System.Drawing.Size(90, 13)
        Me.L_DownloadModus.TabIndex = 24
        Me.L_DownloadModus.Text = "Download-Modus"
        '
        'CB_DownloadModi
        '
        Me.CB_DownloadModi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CB_DownloadModi.FormattingEnabled = True
        Me.CB_DownloadModi.Items.AddRange(New Object() {"Download bei neuer Datei", "Download nach X Sekunden"})
        Me.CB_DownloadModi.Location = New System.Drawing.Point(689, 97)
        Me.CB_DownloadModi.Name = "CB_DownloadModi"
        Me.CB_DownloadModi.Size = New System.Drawing.Size(187, 21)
        Me.CB_DownloadModi.TabIndex = 25
        '
        'B_FTPStart
        '
        Me.B_FTPStart.Location = New System.Drawing.Point(580, 159)
        Me.B_FTPStart.Name = "B_FTPStart"
        Me.B_FTPStart.Size = New System.Drawing.Size(140, 23)
        Me.B_FTPStart.TabIndex = 26
        Me.B_FTPStart.Text = "FTP-Downloader starten"
        Me.B_FTPStart.UseVisualStyleBackColor = True
        '
        'B_StopFTP
        '
        Me.B_StopFTP.Location = New System.Drawing.Point(736, 159)
        Me.B_StopFTP.Name = "B_StopFTP"
        Me.B_StopFTP.Size = New System.Drawing.Size(140, 23)
        Me.B_StopFTP.TabIndex = 27
        Me.B_StopFTP.Text = "FTP-Downloader stoppen"
        Me.B_StopFTP.UseVisualStyleBackColor = True
        '
        'L_DownloadTime
        '
        Me.L_DownloadTime.AutoSize = True
        Me.L_DownloadTime.Enabled = False
        Me.L_DownloadTime.Location = New System.Drawing.Point(577, 136)
        Me.L_DownloadTime.Name = "L_DownloadTime"
        Me.L_DownloadTime.Size = New System.Drawing.Size(183, 13)
        Me.L_DownloadTime.TabIndex = 28
        Me.L_DownloadTime.Text = "Sekunden zwischen zwei Downloads"
        '
        'TB_DownloadTime
        '
        Me.TB_DownloadTime.Enabled = False
        Me.TB_DownloadTime.Location = New System.Drawing.Point(766, 133)
        Me.TB_DownloadTime.Name = "TB_DownloadTime"
        Me.TB_DownloadTime.Size = New System.Drawing.Size(110, 20)
        Me.TB_DownloadTime.TabIndex = 29
        Me.TB_DownloadTime.Text = "1"
        '
        'L_ImportToInflux
        '
        Me.L_ImportToInflux.AutoSize = True
        Me.L_ImportToInflux.Location = New System.Drawing.Point(634, 237)
        Me.L_ImportToInflux.Name = "L_ImportToInflux"
        Me.L_ImportToInflux.Size = New System.Drawing.Size(181, 13)
        Me.L_ImportToInflux.TabIndex = 30
        Me.L_ImportToInflux.Text = "CSV-Dateien in die Datenbank laden"
        '
        'B_startUpload
        '
        Me.B_startUpload.Enabled = False
        Me.B_startUpload.Location = New System.Drawing.Point(637, 335)
        Me.B_startUpload.Name = "B_startUpload"
        Me.B_startUpload.Size = New System.Drawing.Size(154, 23)
        Me.B_startUpload.TabIndex = 31
        Me.B_startUpload.Text = "Upload starten"
        Me.B_startUpload.UseVisualStyleBackColor = True
        '
        'L_CSVFIles
        '
        Me.L_CSVFIles.AutoSize = True
        Me.L_CSVFIles.Enabled = False
        Me.L_CSVFIles.Location = New System.Drawing.Point(577, 269)
        Me.L_CSVFIles.Name = "L_CSVFIles"
        Me.L_CSVFIles.Size = New System.Drawing.Size(71, 13)
        Me.L_CSVFIles.TabIndex = 32
        Me.L_CSVFIles.Text = "CSV-Dateien:"
        '
        'B_ChooseCsv
        '
        Me.B_ChooseCsv.Location = New System.Drawing.Point(658, 264)
        Me.B_ChooseCsv.Name = "B_ChooseCsv"
        Me.B_ChooseCsv.Size = New System.Drawing.Size(218, 23)
        Me.B_ChooseCsv.TabIndex = 33
        Me.B_ChooseCsv.Text = "CSV-Dateien auswählen"
        Me.B_ChooseCsv.UseVisualStyleBackColor = True
        '
        'L_Bucket
        '
        Me.L_Bucket.AutoSize = True
        Me.L_Bucket.Enabled = False
        Me.L_Bucket.Location = New System.Drawing.Point(577, 301)
        Me.L_Bucket.Name = "L_Bucket"
        Me.L_Bucket.Size = New System.Drawing.Size(75, 13)
        Me.L_Bucket.TabIndex = 34
        Me.L_Bucket.Text = "Bucket Name:"
        '
        'TB_Bucket
        '
        Me.TB_Bucket.Location = New System.Drawing.Point(658, 298)
        Me.TB_Bucket.Name = "TB_Bucket"
        Me.TB_Bucket.Size = New System.Drawing.Size(218, 20)
        Me.TB_Bucket.TabIndex = 35
        Me.TB_Bucket.Text = "Test"
        '
        'OFD_CsvData
        '
        Me.OFD_CsvData.DefaultExt = "csv"
        Me.OFD_CsvData.FileName = "OpenFileDialog1"
        Me.OFD_CsvData.Multiselect = True
        '
        'StartFenster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(894, 546)
        Me.Controls.Add(Me.TB_Bucket)
        Me.Controls.Add(Me.L_Bucket)
        Me.Controls.Add(Me.B_ChooseCsv)
        Me.Controls.Add(Me.L_CSVFIles)
        Me.Controls.Add(Me.B_startUpload)
        Me.Controls.Add(Me.L_ImportToInflux)
        Me.Controls.Add(Me.TB_DownloadTime)
        Me.Controls.Add(Me.L_DownloadTime)
        Me.Controls.Add(Me.B_StopFTP)
        Me.Controls.Add(Me.B_FTPStart)
        Me.Controls.Add(Me.CB_DownloadModi)
        Me.Controls.Add(Me.L_DownloadModus)
        Me.Controls.Add(Me.B_OpenFBD)
        Me.Controls.Add(Me.TB_DirPath)
        Me.Controls.Add(Me.L_FTP_Dir)
        Me.Controls.Add(Me.L_Ftp)
        Me.Controls.Add(Me.L_VarCount)
        Me.Controls.Add(Me.b_CpuDisconnect)
        Me.Controls.Add(Me.TB_SampTime)
        Me.Controls.Add(Me.CB_LogMode)
        Me.Controls.Add(Me.L_RecMode)
        Me.Controls.Add(Me.L_SampTime)
        Me.Controls.Add(Me.L_LoggerKonfig)
        Me.Controls.Add(Me.B_LoggerStop)
        Me.Controls.Add(Me.B_Sort)
        Me.Controls.Add(Me.B_LoggerStart)
        Me.Controls.Add(Me.L_ChosenObj)
        Me.Controls.Add(Me.LB_ChoosenObj)
        Me.Controls.Add(Me.L_TreeView)
        Me.Controls.Add(Me.TV_PVIVars)
        Me.Controls.Add(Me.L_CpuConnected)
        Me.Controls.Add(Me.B_ConnectCpu)
        Me.Controls.Add(Me.B_SPSSuche)
        Me.Controls.Add(Me.CB_IPAddressen)
        Me.Controls.Add(Me.L_SPSSuche)
        Me.Name = "StartFenster"
        Me.Text = "SPS-Logger"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents L_SPSSuche As Label
    Friend WithEvents CB_IPAddressen As ComboBox
    Friend WithEvents B_SPSSuche As Button
    Friend WithEvents B_ConnectCpu As Button
    Friend WithEvents L_CpuConnected As Label
    Friend WithEvents TV_PVIVars As TreeView
    Friend WithEvents L_TreeView As Label
    Friend WithEvents LB_ChoosenObj As ListBox
    Friend WithEvents L_ChosenObj As Label
    Friend WithEvents B_LoggerStart As Button
    Friend WithEvents B_Sort As Button
    Friend WithEvents B_LoggerStop As Button
    Friend WithEvents L_LoggerKonfig As Label
    Friend WithEvents L_SampTime As Label
    Friend WithEvents L_RecMode As Label
    Friend WithEvents CB_LogMode As ComboBox
    Friend WithEvents TB_SampTime As TextBox
    Friend WithEvents IL_TreeView As ImageList
    Friend WithEvents b_CpuDisconnect As Button
    Friend WithEvents L_VarCount As Label
    Friend WithEvents L_Ftp As Label
    Friend WithEvents L_FTP_Dir As Label
    Friend WithEvents TB_DirPath As TextBox
    Friend WithEvents B_OpenFBD As Button
    Friend WithEvents L_DownloadModus As Label
    Friend WithEvents CB_DownloadModi As ComboBox
    Friend WithEvents B_FTPStart As Button
    Friend WithEvents B_StopFTP As Button
    Friend WithEvents L_DownloadTime As Label
    Friend WithEvents TB_DownloadTime As TextBox
    Friend WithEvents FBD_FTPSave As FolderBrowserDialog
    Friend WithEvents L_ImportToInflux As Label
    Friend WithEvents B_startUpload As Button
    Friend WithEvents L_CSVFIles As Label
    Friend WithEvents B_ChooseCsv As Button
    Friend WithEvents L_Bucket As Label
    Friend WithEvents TB_Bucket As TextBox
    Friend WithEvents OFD_CsvData As OpenFileDialog
End Class
