/*
The MIT License (MIT)

Copyright (c) 2018 Giovanni Paolo Vigano'

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

/// <summary>
/// Examples for the M2MQTT library (https://github.com/eclipse/paho.mqtt.m2mqtt),
/// </summary>

/// <summary>
/// Script for testing M2MQTT with a Unity UI
/// </summary>
public class MQTT: M2MqttUnityClient
{
    
    public InputField consoleInputField;
    public Toggle encryptedToggle;
    public InputField addressInputField;
    public InputField portInputField;
    public Button connectButton;
    public Button disconnectButton;
    public Button testPublishButton;
    public Button clearButton;

    public static GameObject MQTTDisplay;

    private List<Tuple<string, string>> eventMessages = new List<Tuple<string, string>>();
    private String currentTopic = "none";
    private bool updateUI = false;

    public void TestPublish()
    {
        client.Publish("topicalisala", System.Text.Encoding.UTF8.GetBytes("Testerinoooooooo"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Test message published");
    }

    public void PublishMessage(string topic, string message)
    {
        Debug.Log("---Publishing--- Topic: " + topic + " Message: " + message+ " ---Publishing---");
        client.Publish(topic, System.Text.Encoding.UTF8.GetBytes(message), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
    }

    public void SetBrokerAddress(string brokerAddress)
    {
            this.brokerAddress = brokerAddress;
    }

    public void SetBrokerPort(string brokerPort)
    {
            int.TryParse(brokerPort, out this.brokerPort);
    }

    public void SetEncrypted(bool isEncrypted)
    {
        this.isEncrypted = isEncrypted;
    }

    protected override void OnConnecting()
    {
        base.OnConnecting();
        //Debug.Log("Connecting to broker on " + brokerAddress + ":" + brokerPort.ToString() + "...\n");
    }

    protected override void OnConnected()
    {
        base.OnConnected();
        Debug.Log("Connected to broker on " + brokerAddress + "\n");
        SubscribeTopics();
        PanelHandler.DisableDecisionSupport();

    }

    protected override void SubscribeTopics()
    {
        String topic = "topiclukasnaba";
        client.Subscribe(new string[] {topic}, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

        String topic2 = "topicNetworkInitFrontend";
        client.Subscribe(new string[] { topic2 }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

        String topic3 = "topicSetEvidenceFrontend";
        client.Subscribe(new string[] { topic3 }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    protected override void UnsubscribeTopics()
    {
        client.Unsubscribe(new string[] { "topiclukasnaba" });
    }

    protected override void OnConnectionFailed(string errorMessage)
    {
        Debug.Log("CONNECTION FAILED! " + errorMessage);
    }

    protected override void OnDisconnected()
    {
        Debug.Log("Disconnected.");
    }

    protected override void OnConnectionLost()
    {
        Debug.Log("CONNECTION LOST!");
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void DecodeMessage(string topic, byte[] message)
    {

        string msg = System.Text.Encoding.UTF8.GetString(message);
        currentTopic = topic;
        StoreMessage(topic, msg);

       
    }

    private void StoreMessage(string topic, string eventMsg)
    {
        eventMessages.Add(Tuple.Create(topic, eventMsg));
    }

    private void ProcessMessage(string topic, string msg)
    {

        Debug.Log("---Received--- Topic: " + topic + " Message: " + msg+ " ---Received---");

        
       if (topic == "topiclukasnaba")
       {
           Disconnect();
       }
       else if (topic == "topicNetworkInitFrontend")
       {
            //JSONHandler.saveJson(msg, "/MyAssets/Messages/Phase1Message.json");
            //Content c = JSONHandler.readJsonContentFromFile(msg);
            //PublishMessage("topicSetEvidenceBackend", "Request");

            Content c = JSONHandler.ReadJsonContentFromString(msg);
            Phase1Visualization.VisualizeJsonPhase1(c);


       }
       else if (topic == "topicSetEvidenceFrontend") 
       {
            MessageInformation mi = JSONHandler.ReadJsonFromString(msg);
            Phase3Visualization.VisualizeJsonPhase3(mi, Main.cells, Phase2BtnHandler.evidencetype, Phase2BtnHandler.evidencename);
       }
       



        //Debug.Log("Received: " + msg);
        //MQTTDisplay = GameObject.Find("MQTTDisplay");
        //MQTTDisplay.GetComponent<TMPro.TextMeshProUGUI>().text += "\n" + msg;

       
        

    }

    protected override void Update()
    {
        base.Update(); // call ProcessMqttEvents()
       
        if (eventMessages.Count > 0)
        {
            //Debug.Log("Eventmessage Count0: " + eventMessages.Count);
            foreach (Tuple<string, string> msg in eventMessages)
            {
                ProcessMessage(msg.Item1, msg.Item2);
            }
            //Debug.Log("Eventmessage Count1: " + eventMessages.Count);
            eventMessages.Clear();
            //Debug.Log("Eventmessage Count2: " + eventMessages.Count);
        }

    }

    private void OnDestroy()
    {
        Disconnect();
    }

    private void OnValidate()
    {
            autoConnect = true;
    }
}

