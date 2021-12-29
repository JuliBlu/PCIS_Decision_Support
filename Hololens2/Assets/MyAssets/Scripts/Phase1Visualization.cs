using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Windows;

public class Phase1Visualization : MonoBehaviour
{
    public static Content messageContent;

    void Start()
    {
        
    }


    /**
  * Visualizes the message containing all possible errors.
  * @param     content   the message content to visualize
  * @return    void
  */
    public static void VisualizeJsonPhase1(Content content)
    {
        PanelHandler.ClearPanels(true, true, true, true);
        CellColoring.DisableArrowWaypointer();

        messageContent = content;

        List<MessageInformation> failures = new List<MessageInformation>();
        List<Child> symptoms = new List<Child>();
        List<Child> rootCauses = new List<Child>();

        foreach (MessageInformation mi in messageContent.content)
        {
            failures.Add(mi);
        }

        foreach (MessageInformation mi in messageContent.content)
        {

            foreach (Child child in mi.children)
            {
                if (child.type == 2)
                {
                    symptoms.Add(child);
                }
                else if (child.type == 4)
                {
                    rootCauses.Add(child);
                }
            }
        }

        ProcessFailuresPhase1(failures);

        ProcessSymptomsPhase1(symptoms);

        ProcessRootCausesPhase1(rootCauses);




    }


    /**
    * processes the failures.
    * @param     failures   the list of failures.
    * @return    void
    */
    public static void ProcessFailuresPhase1(List<MessageInformation> failures)
    {
        GameObject failurePanel = GameObject.Find("FaultsPanel");
        Transform failureTransform = failurePanel.transform;

        GameObject g;

        GameObject failurePrefab = Resources.Load("Button") as GameObject;

        foreach (MessageInformation failure in failures)
        {

            g = Instantiate(failurePrefab, failureTransform);
            GameObject child0 = g.transform.GetChild(0).gameObject;
            child0.GetComponent<Text>().text = failure.name;
            child0.GetComponent<Text>().fontSize = 18;

        }

    }


    /**
   * processes the symptoms.
   *
   * @param     symptoms   the list of symptoms.
   * @return    void
   */
    public static void ProcessSymptomsPhase1(List<Child> symptoms)
    {

        GameObject symptomsPanel = GameObject.Find("SymptomsPanel");
        Transform symptomsTransform = symptomsPanel.transform;

        GameObject g;

        GameObject symptomsPrefab = Resources.Load("Button") as GameObject;

        foreach (Child symptom in symptoms)
        {

            g = Instantiate(symptomsPrefab, symptomsTransform);
            GameObject child0 = g.transform.GetChild(0).gameObject;
            child0.GetComponent<Text>().text = symptom.name;
            child0.GetComponent<Text>().fontSize = 18;

        }

    }


    /**
  * processes the rootCauses.
  *
  * @param     rootcauses   the list of rootcauses.
  * @return    void
  */
    public static void ProcessRootCausesPhase1(List<Child> rootcauses)
    {

        GameObject rootcausesPanel = GameObject.Find("RootCausesPanel");
        Transform rootcausesTransform = rootcausesPanel.transform;

        GameObject g;

        GameObject rootcausesPrefab = Resources.Load("Button") as GameObject;

        foreach (Child rootcause in rootcauses)
        {

            g = Instantiate(rootcausesPrefab, rootcausesTransform);
            GameObject child0 = g.transform.GetChild(0).gameObject;
            child0.GetComponent<Text>().text = rootcause.name;
            child0.GetComponent<Text>().fontSize = 12;

        }

    }





}
