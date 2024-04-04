Imports System.Xml.Linq

'Speicherobjekt für die Gesamte Konfiguration
Public Class ConfigurationData
    Public DataRecoderName As String

End Class

Module XMLFunktionen

    Public configName = "\config.xml"
    Public CurConfig As ConfigurationData = ReadConfig(IO.Directory.GetCurrentDirectory + configName)

    'Speichert die Konfigurationen als XML-Datei am angegebenen Pfad
    Public Sub WriteConfig(path As String, ConfigData As ConfigurationData)
        Dim root As New XElement("Konfiguration")

        Dim SpsConfig As New XElement("SPS-EINSTELLUNGEN",
                                      New XElement("DataRecorderVariable", ConfigData.DataRecoderName)
        )
        root.Add(SpsConfig)
        Dim doc As New XDocument(New XDeclaration("1.0", "utf-8", "yes"), root)
        doc.Save(path)
    End Sub

    'Liest die Konfigurationen einer XML-Datei aus und speichert diese in der
    'mitgegebenen ConfigurationData-Klasse
    Public Function ReadConfig(path As String)
        Dim doc As XDocument = XDocument.Load(path)
        Dim ConfigClass As New ConfigurationData

        Dim SpsConfig = doc.Root.Element("SPS-EINSTELLUNGEN")
        ConfigClass.DataRecoderName = SpsConfig.Element("DataRecorderVariable").Value

        Return ConfigClass
    End Function

    'Füllt die ConfigurationDataKlasse mit StandardWerten
    Public Sub SetDefaultSettings(ByRef ConfigData As ConfigurationData)
        ConfigData.DataRecoderName = "DataRecorder"
    End Sub

    'Diese Funktion Erstellt eine config.xml Datei mit Default-Werten
    Public Sub saveDefaultXML(path As String)
        Dim DefaultConfig = New ConfigurationData
        SetDefaultSettings(DefaultConfig)
        WriteConfig(path, DefaultConfig)
    End Sub

End Module
