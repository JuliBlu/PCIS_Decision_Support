using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Windows;

public class Phase3Visualization : MonoBehaviour
{



    public static List<State> failuresListPhase3;
    public static List<Child> rootCausesListPhase3;
    public static List<Child> symptomsListPhase3;


    void Start()
    {
        
    }


    /**
    * Visualizes the message on the decision Support boxes
    * and colors the faulted cells.
    *
    * @param     mi   the message to visualize.
    * @param     cells   the list of cells of the production line.
    * @param     evidenceType   the name of the set evidence from phase 2.
    * @param     evidenceName   the type of the set evidence from phase 2.
    * @return    void
    */
    public static void VisualizeJsonPhase3(MessageInformation mi, List<Cell> cells, string evidenceType, string evidenceName)
    {
        PanelHandler.ClearPanels(true, true, true, true);
        //enableDecisionSupport();

        List<Child> symptoms = new List<Child>();
        List<Child> rootCauses = new List<Child>();
        List<State> correctiveActions = new List<State>();
        List<State> faults = new List<State>();
        List<string> processes = new List<string>();

        int processcounter = 0;

        if (processcounter < mi.children.Length)
        {

            while (mi.children[processcounter].states is null)
            {
                processes.Add(mi.children[processcounter].name.Substring(0, 3));
                processcounter++;
            }
        }

        foreach (State state in mi.states)
        {
            faults.Add(state);
        }

        foreach (State state in mi.children[processcounter].states)
        {
            correctiveActions.Add(state);
        }

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





        List<Cell> relevantcells = CellColoring.getCell(processes, cells);
        CellColoring.ColorCellsRed(relevantcells);
        CellColoring.SetArrowWaypointer(relevantcells[0]);

        GameObject RootCauses = GameObject.Find("RootCauses");
        GameObject CorrectiveActions = GameObject.Find("CorrectiveActions");
        RootCauses.transform.localScale = new Vector3((float)1.5, (float)1.5, (float)1.5);
        CorrectiveActions.transform.localScale = new Vector3((float)1.5, (float)1.5, (float)1.5);

        Vector3 cellposition = CellColoring.GetCellPositionByName(relevantcells[0].cellname);
        RootCauses.transform.position = cellposition + new Vector3((float)-0.7, -1, 0);
        CorrectiveActions.transform.position = cellposition + new Vector3((float)0.7, -1, 0);



        ProcessSymptomsPhase3(symptoms);


        ProcessRootCausesPhase3(rootCauses);


        ProcessFaultsPhase3(faults);


        ProcessCorrectiveActionsPhase3(correctiveActions);


        ProcessEvidencePhase3(evidenceName, evidenceType);

        failuresListPhase3 = faults;
        symptomsListPhase3 = symptoms;
        rootCausesListPhase3 = rootCauses;

    }

    /**
    * Case evidence was selected, visualize the evidence with 100% Present probability.
    *
    * @param     symptoms  the list of symptoms  
    * @return    void
    */
    public static void ProcessEvidencePhase3(string evidenceName, string evidenceType)
    {

        if (evidenceType == "rootcause")
        {
            PanelHandler.ClearPanels(false, false, false, true);
            GameObject rootCausePanel = GameObject.Find("RootCausesPanel");
            Transform rootCauseTransform = rootCausePanel.transform;
            GameObject rootCausePrefab = Resources.Load("SymptomPanel") as GameObject;
            GameObject g = Instantiate(rootCausePrefab, rootCauseTransform);
            GameObject nameLabel = g.transform.GetChild(0).gameObject;
            GameObject slider = g.transform.GetChild(1).gameObject;
            GameObject percentageLabel = g.transform.GetChild(2).gameObject;

            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().text = evidenceName;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 24;
            slider.GetComponent<Slider>().maxValue = 100;
            slider.GetComponent<Slider>().value = 100;
            percentageLabel.GetComponent<TMPro.TextMeshProUGUI>().text = 100 + "% Present";
        }
        else if (evidenceType == "failure")
        {
            PanelHandler.ClearPanels(false, false, true, false);

            GameObject faultPanel = GameObject.Find("FaultsPanel");
            Transform faultTransform = faultPanel.transform;
            GameObject faultPrefab = Resources.Load("SymptomPanel") as GameObject;
            GameObject g = Instantiate(faultPrefab, faultTransform);
            GameObject nameLabel = g.transform.GetChild(0).gameObject;
            GameObject slider = g.transform.GetChild(1).gameObject;
            GameObject percentageLabel = g.transform.GetChild(2).gameObject;

            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().text = evidenceName;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 24;
            slider.GetComponent<Slider>().maxValue = 100;
            slider.GetComponent<Slider>().value = 0;
            percentageLabel.GetComponent<TMPro.TextMeshProUGUI>().text = 0 + "% Present";


        }
        else if (evidenceType == "symptom")
        {
            PanelHandler.ClearPanels(true, false, false, false);
            GameObject SymptomsPanel = GameObject.Find("SymptomsPanel");
            Transform symptomstransform = SymptomsPanel.transform;
            GameObject symptomPrefab = Resources.Load("SymptomPanel") as GameObject;
            GameObject g = Instantiate(symptomPrefab, symptomstransform);
            GameObject nameLabel = g.transform.GetChild(0).gameObject;
            GameObject slider = g.transform.GetChild(1).gameObject;
            GameObject percentageLabel = g.transform.GetChild(2).gameObject;


            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().text = evidenceName;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 24;
            slider.GetComponent<Slider>().maxValue = 100;
            slider.GetComponent<Slider>().value = 100;
            percentageLabel.GetComponent<TMPro.TextMeshProUGUI>().text = 100 + "% Present";

        }

    }


    /**
    * Processes the symptoms
    *
    * @param     symptoms  the list of symptoms  
    * @return    void
    */
    public static void ProcessSymptomsPhase3(List<Child> symptoms)
    {

        GameObject SymptomsPanel = GameObject.Find("SymptomsPanel");
        Transform symptomstransform = SymptomsPanel.transform;

        GameObject g;

        GameObject symptomPrefab = Resources.Load("SymptomPanel") as GameObject;

        foreach (Child symptom in symptoms)
        {

            g = Instantiate(symptomPrefab, symptomstransform);
            GameObject nameLabel = g.transform.GetChild(0).gameObject;
            GameObject slider = g.transform.GetChild(1).gameObject;
            GameObject percentageLabel = g.transform.GetChild(2).gameObject;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().text = symptom.name;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 24;
            slider.GetComponent<Slider>().maxValue = 100;
            slider.GetComponent<Slider>().value = symptom.states[0].probability;
            percentageLabel.GetComponent<TMPro.TextMeshProUGUI>().text = symptom.states[0].probability.ToString() + "% Present";

        }

    }

    /**
   * Processes the rootcauses
   *
   * @param     rootCauses  the list of rootcauses  
   * @return    void
   */
    public static void ProcessRootCausesPhase3(List<Child> rootCauses)
    {

        GameObject rootCausePanel = GameObject.Find("RootCausesPanel");
        Transform rootCauseTransform = rootCausePanel.transform;


        GameObject rootCausePrefab = Resources.Load("SymptomPanel") as GameObject;

        //Debug.Log("rootCausePrefab Nullcheck: " + rootCausePrefab == null);
        //Debug.Log("rootCauseTransform Nullcheck: " + rootCauseTransform == null);

        foreach (Child rootCause in rootCauses)
        {

            GameObject g = Instantiate(rootCausePrefab, rootCauseTransform);
            GameObject nameLabel = g.transform.GetChild(0).gameObject;
            GameObject slider = g.transform.GetChild(1).gameObject;
            GameObject percentageLabel = g.transform.GetChild(2).gameObject;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().text = rootCause.name;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 24;
            slider.GetComponent<Slider>().maxValue = 100;
            slider.GetComponent<Slider>().value = rootCause.states[0].probability;
            percentageLabel.GetComponent<TMPro.TextMeshProUGUI>().text = rootCause.states[0].probability.ToString() + "% Present";



        }

    }

    /**
   * Processes the correctiveActions
   *
   * @param     correctiveActions  the list of correctiveActions  
   * @return    void
   */
    public static void ProcessCorrectiveActionsPhase3(List<State> correctiveActions)
    {
        GameObject correctiveActionPanel = GameObject.Find("CorrectiveActionsPanel");
        Transform correctiveActionTransform = correctiveActionPanel.transform;

        GameObject g;

        GameObject correctiveActionPrefab = Resources.Load("SymptomPanel") as GameObject;

        foreach (State correctiveAction in correctiveActions)
        {

            g = Instantiate(correctiveActionPrefab, correctiveActionTransform);
            GameObject nameLabel = g.transform.GetChild(0).gameObject;
            GameObject slider = g.transform.GetChild(1).gameObject;
            GameObject percentageLabel = g.transform.GetChild(2).gameObject;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().text = correctiveAction.name;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 24;
            slider.GetComponent<Slider>().maxValue = 100;
            int probability = 100 - correctiveAction.probability;
            slider.GetComponent<Slider>().value = probability;
            percentageLabel.GetComponent<TMPro.TextMeshProUGUI>().text = probability.ToString() + "% Present";

        }

    }

    /**
    * Processes the faults
    *
    * @param     faults  the list of faults  
    * @return    void
    */
    public static void ProcessFaultsPhase3(List<State> faults)
    {
        GameObject faultPanel = GameObject.Find("FaultsPanel");
        Transform faultTransform = faultPanel.transform;
        GameObject faultPrefab = Resources.Load("SymptomPanel") as GameObject;

        foreach (State fault in faults)
        {

            GameObject g = Instantiate(faultPrefab, faultTransform);
            GameObject nameLabel = g.transform.GetChild(0).gameObject;
            GameObject slider = g.transform.GetChild(1).gameObject;
            GameObject percentageLabel = g.transform.GetChild(2).gameObject;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().text = fault.name;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 24;
            slider.GetComponent<Slider>().maxValue = 100;
            int probability = 100 - fault.probability;
            slider.GetComponent<Slider>().value = probability;
            percentageLabel.GetComponent<TMPro.TextMeshProUGUI>().text = probability.ToString() + "% Present";


        }

    }





}
