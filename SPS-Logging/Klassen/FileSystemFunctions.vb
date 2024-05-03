Imports System.IO

Module FileSystemFunctions

    'Diese Funktion öffnet ein FolderBrowserDialog und speichert die übergebenen LoggerKonfigurationen 
    'in einer Datei am gewählten Pfad. Der Name der Datei ist "LoggerKonfig_{Timestamp}.txt" 
    Public Sub SaveLoggerConfig(Vars As ListBox.ObjectCollection, SampTime As String, LogMode As String)
        Dim FolderDia As New FolderBrowserDialog
        FolderDia.ShowDialog()
        If FolderDia.SelectedPath Is Nothing OrElse FolderDia.SelectedPath = "" Then Exit Sub
        Dim Path As String = FolderDia.SelectedPath
        Dim FileName As String = $"/LoggerKonfig_{Date.Now}.txt".Replace(":", "_")
        Dim FileWriter = My.Computer.FileSystem.OpenTextFileWriter(Path + FileName, False)
        Dim StringVars As String = String.Join(",", (From item In Vars Select value = item.ToString).ToArray())
        FileWriter.WriteLine($"Auflösung:{SampTime}")
        FileWriter.WriteLine($"Modus:{LogMode}")
        FileWriter.WriteLine($"Variablen:{StringVars}")
        FileWriter.Close()
    End Sub

    'Die Funktion öffnet ein OpenFileDialog und liest die LoggerKonfigurationen aus der ausgewählten Datei aus
    'Anschließend werden die Konfigurationen im übergebenen StartFenster gesetzt
    Public Sub LoadLoggerConfig(MainFrame As StartFenster)
        Dim FileDia As New OpenFileDialog
        FileDia.ShowDialog()
        If FileDia.FileName Is Nothing OrElse FileDia.FileName = "" Then Exit Sub
        Dim MyReader = My.Computer.FileSystem.OpenTextFieldParser(FileDia.FileName)
        MyReader.TextFieldType = FileIO.FieldType.Delimited
        MyReader.SetDelimiters(":")

        While Not MyReader.EndOfData
            Dim CurrentRow = MyReader.ReadFields
            If Not CurrentRow.Length = 2 Then Continue While
            If CurrentRow(0) = "Auflösung" Then MainFrame.setTBSampTime(CurrentRow(1))
            If CurrentRow(0) = "Modus" Then MainFrame.setCBLogMode(CurrentRow(1))
            If CurrentRow(0) = "Variablen" Then MainFrame.setLBChoosenObjItems(CurrentRow(1).Split(","))
        End While
        MyReader.Close()

    End Sub


End Module
