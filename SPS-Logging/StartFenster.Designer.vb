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
        Me.IL_TreeView = New System.Windows.Forms.ImageList(Me.components)
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
        Me.CB_IPAddressen.Location = New System.Drawing.Point(24, 58)
        Me.CB_IPAddressen.Name = "CB_IPAddressen"
        Me.CB_IPAddressen.Size = New System.Drawing.Size(166, 21)
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
        Me.B_ConnectCpu.Location = New System.Drawing.Point(24, 87)
        Me.B_ConnectCpu.Name = "B_ConnectCpu"
        Me.B_ConnectCpu.Size = New System.Drawing.Size(251, 23)
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
        Me.CB_LogMode.Items.AddRange(New Object() {"Modus1", "Modus2"})
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
        'IL_TreeView
        '
        Me.IL_TreeView.ImageStream = CType(resources.GetObject("IL_TreeView.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.IL_TreeView.TransparentColor = System.Drawing.Color.Transparent
        Me.IL_TreeView.Images.SetKeyName(0, "Task.png")
        Me.IL_TreeView.Images.SetKeyName(1, "Var.png")
        Me.IL_TreeView.Images.SetKeyName(2, "Global.png")
        '
        'StartFenster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(627, 546)
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
End Class
