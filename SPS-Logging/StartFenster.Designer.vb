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
        Me.SuspendLayout()
        '
        'L_SPSSuche
        '
        Me.L_SPSSuche.AutoSize = True
        Me.L_SPSSuche.Location = New System.Drawing.Point(21, 42)
        Me.L_SPSSuche.Name = "L_SPSSuche"
        Me.L_SPSSuche.Size = New System.Drawing.Size(300, 13)
        Me.L_SPSSuche.TabIndex = 0
        Me.L_SPSSuche.Text = "Bitte die IP-Addresse der Steuerung auswählen oder eintragen"
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
        Me.B_SPSSuche.Size = New System.Drawing.Size(75, 23)
        Me.B_SPSSuche.TabIndex = 2
        Me.B_SPSSuche.Text = "Suchen"
        Me.B_SPSSuche.UseVisualStyleBackColor = True
        '
        'B_ConnectCpu
        '
        Me.B_ConnectCpu.Location = New System.Drawing.Point(24, 87)
        Me.B_ConnectCpu.Name = "B_ConnectCpu"
        Me.B_ConnectCpu.Size = New System.Drawing.Size(247, 23)
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
        Me.TV_PVIVars.Location = New System.Drawing.Point(27, 159)
        Me.TV_PVIVars.Name = "TV_PVIVars"
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
        Me.B_LoggerStart.Size = New System.Drawing.Size(247, 23)
        Me.B_LoggerStart.TabIndex = 9
        Me.B_LoggerStart.Text = "Logger Starten"
        Me.B_LoggerStart.UseVisualStyleBackColor = True
        '
        'StartFenster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(627, 546)
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
End Class
