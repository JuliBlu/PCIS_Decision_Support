using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Windows;

public class PanelHandler : MonoBehaviour
{
    //public GameObject displayPrefab;
    public static GameObject correctBtn;
    public static GameObject incorrectBtn;
    public static GameObject clearEvidenceBtn;
    public static GameObject decisionSupportLabel;

    public void Start()
    {

        correctBtn = GameObject.Find("DecisionSupportCorrectBtn");

        incorrectBtn = GameObject.Find("DecisionSupportIncorrectBtn");

        clearEvidenceBtn = GameObject.Find("ClearEvidenceBtn");

        decisionSupportLabel = GameObject.Find("DecisionSupportLabel");

        DisableClearEvidence();

    }

    /**
    * Shows the waiting screeen on all panels.
    * @param     none
    * @return    void
    */
    public static void WaitingPanels()
    {

        GameObject failurePanel = GameObject.Find("FaultsPanel");
        Transform failureTransform = failurePanel.transform;

        GameObject symptomsPanel = GameObject.Find("SymptomsPanel");
        Transform symptomsTransform = symptomsPanel.transform;

        GameObject rootcausesPanel = GameObject.Find("RootCausesPanel");
        Transform rootcausesTransform = rootcausesPanel.transform;

        GameObject correctiveActionPanel = GameObject.Find("CorrectiveActionsPanel");
        Transform correctiveActionTransform = correctiveActionPanel.transform;

        GameObject waitingPrefab = Resources.Load("WaitingPanel") as GameObject;

        GameObject g0 = Instantiate(waitingPrefab, failureTransform);
        GameObject g1 = Instantiate(waitingPrefab, symptomsTransform);
        GameObject g2 = Instantiate(waitingPrefab, rootcausesTransform);
        GameObject g3 = Instantiate(waitingPrefab, correctiveActionTransform);

    }

    /**
    * Clears the content of all panels.
    *
    * @param     symptoms   if the symptomspanel should be cleared
    * @param     correctiveActions   if the correctiveActions should be cleared
    * @param     faults   if the faultspanel should be cleared
    * @param     rootcauses   if the  rootcausespanel should be cleared
    * @return    void
    */
    public static void ClearPanels(bool symptoms, bool correctiveActions, bool faults, bool rootcauses)
    {

        GameObject failurePanel = GameObject.Find("FaultsPanel");
        Transform failureTransform = failurePanel.transform;

        GameObject symptomsPanel = GameObject.Find("SymptomsPanel");
        Transform symptomsTransform = symptomsPanel.transform;

        GameObject rootcausesPanel = GameObject.Find("RootCausesPanel");
        Transform rootcausesTransform = rootcausesPanel.transform;

        GameObject correctiveActionPanel = GameObject.Find("CorrectiveActionsPanel");
        Transform correctiveActionTransform = correctiveActionPanel.transform;

        if (symptoms)
        {

            foreach (Transform child in symptomsTransform)
            {
                GameObject.Destroy(child.gameObject);
            }

        }

        if (correctiveActions)
        {

            foreach (Transform child in correctiveActionTransform)
            {
                GameObject.Destroy(child.gameObject);
            }

        }

        if (faults)
        {

            foreach (Transform child in failureTransform)
            {
                GameObject.Destroy(child.gameObject);
            }

        }

        if (rootcauses)
        {

            foreach (Transform child in rootcausesTransform)
            {
                GameObject.Destroy(child.gameObject);
            }

        }

    }



    /**
    * Disables the correct & false Buttons from the decision support near menu
    * Disables the text of the decision support near menu
    * @param     none
    * @return    void
    */
    public static void DisableDecisionSupport()
    {

        correctBtn.SetActive(false);
        incorrectBtn.SetActive(false);
        decisionSupportLabel.SetActive(false);

    }

    /**
   * Enables the correct & false Buttons from the decision support near menu
   * Enables the text of the decision support near menu
   * @param     none
   * @return    void
   */
    public static void EnableDecisionSupport()
    {

        correctBtn.SetActive(true);
        incorrectBtn.SetActive(true);
        decisionSupportLabel.SetActive(true);

    }


    /**
  * Disables the Clear Evidence Button on the Decision Support Menu
  * @param     none
  * @return    void
  */
    public static void DisableClearEvidence()
    {

        clearEvidenceBtn.SetActive(false);

    }

    /**
  * Enables the Clear Evidence Button on the Decision Support Menu
  * @param     none
  * @return    void
  */
    public static void EnableClearEvidence()
    {

        clearEvidenceBtn.SetActive(true);

    }



}
