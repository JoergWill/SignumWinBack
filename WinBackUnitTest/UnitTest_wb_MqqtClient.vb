﻿Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports uPLibrary.Networking.M2Mqtt
Imports uPLibrary.Networking.M2Mqtt.Messages

<TestClass()> Public Class UnitTest_wb_MqqtClient
    'Public WithEvents client As New MqttClient("mqtt.winback.de", 8883, True, Nothing, Nothing, MqttSslProtocols.SSLv3)

    '<TestMethod()> Public Sub TestMethod1()
    '    AddHandler client.MqttMsgPublishReceived, AddressOf client_MqttMsgPublishReceived
    '    AddHandler client.MqttMsgPublished, AddressOf client_MqttMsgPublished
    '    AddHandler client.MqttMsgSubscribed, AddressOf client_MqttMsgSubscribed

    '    'Verbindung aufbauen
    '    Try
    '        client.ProtocolVersion = MqttProtocolVersion.Version_3_1
    '        client.Connect(Guid.NewGuid().ToString(), "W1nB8ck", "kmdqx?s1PuN5")
    '    Catch e As Exception
    '        MsgBox("Fehler bei Connect " & e.Message)
    '    End Try

    '    'Daten abbonieren
    '    client.Subscribe({"Test"}, {MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE})

    '    System.Console.ReadLine()
    'End Sub

    'Private Sub client_MqttMsgPublishReceived(Sender As Object, e As MqttMsgPublishEventArgs)
    '    Dim s As String = System.Text.Encoding.Default.GetString(e.Message)
    '    Debug.Print(s)
    'End Sub

    'Private Sub client_MqttMsgPublished(Sender As Object, e As MqttMsgPublishedEventArgs)
    '    Dim s As String = e.MessageId
    '    Debug.Print(s)
    'End Sub

    'Private Sub client_MqttMsgSubscribed(Sender As Object, e As MqttMsgSubscribedEventArgs)
    '    Dim s As String = e.MessageId
    '    Debug.Print(s)
    'End Sub
End Class