Public Class StartFenster
    Private Sub B_SPSSuche_Click(sender As Object, e As EventArgs) Handles B_SPSSuche.Click
        CB_IPAddressen.Items.AddRange(searchForPLCs("192.168.0.100", "192.168.0.150").ToArray)
        If CB_IPAddressen.Items.Count > 0 Then CB_IPAddressen.SelectedIndex = 0
    End Sub
End Class
