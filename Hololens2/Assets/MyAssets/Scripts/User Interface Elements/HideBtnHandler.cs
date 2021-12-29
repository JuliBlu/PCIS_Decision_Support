using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBtnHandler : MonoBehaviour
{
    public Material defaultMaterial;
    //public List<GameObject> cellObjects;
    public bool isactive = true;

    GameObject SymptomsDashboard;
    GameObject RootCausesDashboard;
    GameObject FaultsDashboard;
    GameObject CorrectiveActionsDashboard;


    public void Start() {
        SymptomsDashboard = GameObject.Find("Symptoms");
        RootCausesDashboard = GameObject.Find("RootCauses");
        FaultsDashboard = GameObject.Find("Faults");
        CorrectiveActionsDashboard = GameObject.Find("CorrectiveActions");
    }

    public void HideDecisionBoxes() {
        //TODO: Connect with button

        GameObject hidetext = GameObject.Find("NearMenuHideBtnText");


        if (isactive)
        {
            SymptomsDashboard.SetActive(false);
            //RootCausesDashboard.SetActive(false);
            FaultsDashboard.SetActive(false);
            //CorrectiveActionsDashboard.SetActive(false);
            isactive = false;
            hidetext.GetComponent<TMPro.TextMeshPro>().text = "Show Decision Boxes";

        } else if (isactive == false){

            SymptomsDashboard.SetActive(true);
            //RootCausesDashboard.SetActive(true);
            FaultsDashboard.SetActive(true);
            //CorrectiveActionsDashboard.SetActive(true);
            isactive = true;
            hidetext.GetComponent<TMPro.TextMeshPro>().text = "Hide Decision Boxes";

        }


    }

}
