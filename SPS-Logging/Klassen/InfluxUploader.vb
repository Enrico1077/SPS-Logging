Imports System.Globalization
Imports InfluxDB.Client
Imports InfluxDB.Client.Api.Domain
Imports InfluxDB.Client.Writes


Public Class InfluxClient
    Private ReadOnly InfluxClient As InfluxDBClient
    Private ReadOnly Comp As String
    Private ReadOnly DBBucket As String

    'Der Konstruktor erstellt einen InfluxClienten und Speichert die Company und den Bucket in 
    'Klassenvariablen
    Sub New(ApiToken As String, InfluxAddr As String, Company As String, Bucket As String)
        InfluxClient = New InfluxDBClient(InfluxAddr, ApiToken)
        Comp = Company
        DBBucket = Bucket
    End Sub

    'Diese Funktion schreibt einen Datenpunkt in die InfluxDB
    Public Sub WritetoInflux(Of T)(Meas As String, VarValue As T, RecordTime As Date, writeApi As WriteApi)
        Dim point = PointData.Measurement(Meas).
                    Field("value", VarValue).
                    Timestamp(RecordTime, WritePrecision.Ns)
        writeApi.WritePoint(point, DBBucket, Comp)
    End Sub

    'Diese Funktion nimmt eine Liste an Spaltennamen und eine Liste an Reihen entgegen und
    'gibt die Werte geeignet an die Funktion WriteToInflux weiter
    Public Sub writeCSVToInflux(CsvData As (List(Of String), List(Of String())))
        Dim ColumnNames As List(Of String) = CsvData.Item1
        Dim ColumnValues As List(Of String()) = CsvData.Item2
        Dim writeApi = InfluxClient.GetWriteApi()

        For i As Integer = 0 To ColumnValues.Count - 1
            Dim RecTime As Date = Date.ParseExact(ColumnValues(i)(0), "yyyy MM dd HH:mm:ss:fff", CultureInfo.InvariantCulture)
            For j As Integer = 1 To ColumnNames.Count - 1
                WritetoInflux(ColumnNames(j), convertToRightBasicType(ColumnValues(i)(j)), RecTime, writeApi)
            Next
        Next

    End Sub

    'Diese Funktion Konvertiert den StringWert aus der CSV-Datei wenn möglich in einen Double oder in ein Bool
    Public Function convertToRightBasicType(Input As String)
        Dim DoubleValue As Double
        Dim BoolValue As Boolean
        If Double.TryParse(Input, DoubleValue) Then Return DoubleValue
        If Boolean.TryParse(Input, BoolValue) Then Return BoolValue
        Return Input
    End Function


    'Diese Funktion zerlegt eine CSV Datei in Listen. Eine Liste mit dem Namen der Spalten und einer Liste, von der 
    'jeder Eintrag eine Reihe an Daten beschreibt
    Public Function AnalyseCSV(Path As String) As (List(Of String), List(Of String()))
        Dim MyReader = My.Computer.FileSystem.OpenTextFieldParser(Path)
        MyReader.TextFieldType = FileIO.FieldType.Delimited
        MyReader.SetDelimiters(";")
        Dim LineCounter As Integer = 0
        Dim columnNames As New List(Of String)
        Dim DataArray As New List(Of String())

        Dim currentRow As String()
        While Not MyReader.EndOfData
            LineCounter += 1
            Try
                currentRow = MyReader.ReadFields
                currentRow = Array.FindAll(currentRow, Function(field) Not String.IsNullOrEmpty(field))
                If LineCounter = 1 Then Continue While
                If LineCounter = 2 Then
                    columnNames.AddRange(currentRow)
                    Continue While
                End If
                If Not currentRow.Length = columnNames.Count Then
                    Console.WriteLine("CSVFehler: Differenz in der Spaltenanzahl") 'Fehlermanagment
                    Continue While
                End If
                DataArray.Add(currentRow)
            Catch ex As FileIO.MalformedLineException       'Fehlermanagment
                MsgBox("Line " & ex.Message &
                "is not valid and will be skipped.")
            End Try
        End While
        MyReader.Close()
        Return (columnNames, DataArray)
        Stop
    End Function

End Class
