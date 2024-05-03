Imports System.Windows.Forms
Imports System.Drawing.Drawing2D

Public Class RoundButton
    Inherits Button

    Protected Overrides Sub OnPaint(pevent As PaintEventArgs)
        Dim grpath As GraphicsPath = New GraphicsPath()
        grpath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height)
        Me.Region = New Region(grpath)
        MyBase.OnPaint(pevent)
    End Sub

End Class
