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
        'StartFenster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(517, 546)
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
End Class
