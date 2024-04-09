Imports System.Xml.Linq

'Speicherobjekt für die Gesamte Konfiguration
Public Class ConfigurationData
    Public DataRecoderName As String
    Public FTP_IPAdress As String
    Public FTP_UserName As String
    Public FTP_Password As String

End Class

Module XMLFunktionen

    Public configName = "\config.xml"
    Public CurConfig As ConfigurationData

    'Speichert die Konfigurationen als XML-Datei am angegebenen Pfad
    Public Sub WriteConfig(path As String, ConfigData As ConfigurationData)
        Dim root As New XElement("Konfiguration")

        Dim SpsConfig As New XElement("SPS-EINSTELLUNGEN",
                                      New XElement("DataRecorderVariable", ConfigData.DataRecoderName)
        )
        Dim FTPConfig As New XElement("FTP-Konfigurationen",
                                      New XElement("IP-Addresse", ConfigData.FTP_IPAdress),
                                      New XElement("User-Name", ConfigData.FTP_UserName),
                                      New XElement("Password", ConfigData.FTP_Password)
)
        root.Add(SpsConfig, FTPConfig)
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

        Dim FtpConfig = doc.Root.Element("FTP-Konfigurationen")
        ConfigClass.FTP_IPAdress = FtpConfig.Element("IP-Addresse")
        ConfigClass.FTP_UserName = FtpConfig.Element("User-Name")
        ConfigClass.FTP_Password = FtpConfig.Element("Password")

        CurConfig = ConfigClass
        Return ConfigClass
    End Function

    'Füllt die ConfigurationDataKlasse mit StandardWerten
    Public Sub SetDefaultSettings(ByRef ConfigData As ConfigurationData)
        ConfigData.DataRecoderName = "DataRecorder"
        ConfigData.FTP_IPAdress = "192.168.0.147"
        ConfigData.FTP_UserName = "ftpuser"
        ConfigData.FTP_Password = "4711"
    End Sub

    'Diese Funktion Erstellt eine config.xml Datei mit Default-Werten
    Public Sub saveDefaultXML(path As String)
        Dim DefaultConfig = New ConfigurationData
        SetDefaultSettings(DefaultConfig)
        WriteConfig(path, DefaultConfig)
    End Sub

End Module
