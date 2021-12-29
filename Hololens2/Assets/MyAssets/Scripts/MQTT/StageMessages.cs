using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageMessages : MonoBehaviour
{

    /**
     * Prepares the message for network init.
    * Calls MQTT Publish.
    * @param     none
    * @return    void
     */
    public static void StageNetworkInit() {

        GameObject mqttserver = GameObject.Find("MQTT");
        MQTT mqttinstance = mqttserver.GetComponent<MQTT>();
        mqttinstance.PublishMessage("topicNetworkInitBackend", "Request Phase 1");


    }


    /**
    * Prepares the message for set evidence.
    * Calls MQTT Publish.
    * @param     statename the name of the state.
    * @param     id the id of the evidence.
    * @param     isPresent is the evidence present.
    * @return    void
    */
    public static void StageSetEvidence(string statename, int id, bool isPresent)
    {

        GameObject mqttserver = GameObject.Find("MQTT");
        MQTT mqttinstance = mqttserver.GetComponent<MQTT>();


        string message = id + ", " + statename + ", " + isPresent;
        mqttinstance.PublishMessage("topicSetEvidenceBackend", message);

    }

    /**
    * Prepares the message to clear an evidence in the backend.
    * Calls MQTT Publish.
    * @param     id the id of the evidence to clear.
    * @return    void
    */
    public static void StageClearEvidence(int id) {

        GameObject mqttserver = GameObject.Find("MQTT");
        MQTT mqttinstance = mqttserver.GetComponent<MQTT>();

        string message = ""+id;
        mqttinstance.PublishMessage("topicClearEvidenceBackend", message);

    }



    /**
    * Prepares the message for decision suppport.
    * Calls MQTT Publish.
    * @param     wascorrect was the decision support correct?
    * @return    void
    */
    public static void StageDecisionSupport(bool wascorrect)
    {
        string message = ""; 

        if (wascorrect) {

            message = "DecisionSupport: True";

        } else if (!wascorrect){

            message = "DecisionSupport: False";

        }

        GameObject mqttserver = GameObject.Find("MQTT");
        MQTT mqttinstance = mqttserver.GetComponent<MQTT>();

        mqttinstance.PublishMessage("topicDecisionSupportBackend", message);

    }

}
