Imports System.Globalization
Imports InfluxDB.Client
Imports InfluxDB.Client.Api.Domain
Imports InfluxDB.Client.Writes
Imports Newtonsoft.Json.Linq


Public Class InfluxClient
    Private ReadOnly InfluxClient As InfluxDBClient
    Private ReadOnly Comp As String
    Private ReadOnly DBBucket As String

    Sub New(ApiToken As String, InfluxAddr As String, Company As String, Bucket As String)
        InfluxClient = New InfluxDBClient(InfluxAddr, ApiToken)
        Comp = Company
        DBBucket = Bucket
    End Sub

    Public Sub WritetoInflux(Meas As String, VarValue As String, RecordTime As Date, writeApi As WriteApi)
        Dim point = PointData.Measurement(Meas).
                    Field("value", VarValue).
                    Timestamp(RecordTime, WritePrecision.Ns)
        writeApi.WritePoint(point, DBBucket, Comp)
    End Sub

    Public Sub writeCSVToInflux(CsvData As (List(Of String), List(Of String())))
        Dim ColumnNames As List(Of String) = CsvData.Item1
        Dim ColumnValues As List(Of String()) = CsvData.Item2
        Dim writeApi = InfluxClient.GetWriteApi()

        For i As Integer = 0 To ColumnValues.Count - 1
            Dim RecTime As Date = Date.ParseExact(ColumnValues(i)(0), "yyyy MM dd HH:mm:ss:fff", CultureInfo.InvariantCulture)
            For j As Integer = 1 To ColumnNames.Count - 1
                WritetoInflux(ColumnNames(j), ColumnValues(i)(j), RecTime, writeApi)
            Next
        Next

    End Sub


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
        Return (columnNames, DataArray)
        Stop
    End Function

End Class
