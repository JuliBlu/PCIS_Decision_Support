using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class InitiateBtnHandler : MonoBehaviour
{
    public void initiate() {

        PanelHandler.WaitingPanels();
        
        GameObject Symptoms = GameObject.Find("Symptoms");
        
        GameObject Faults = GameObject.Find("Faults");

        GameObject RootCauses = GameObject.Find("RootCauses");
        GameObject CorrectiveActions = GameObject.Find("CorrectiveActions");


        //RootCauses.SetActive(false);
        //CorrectiveActions.SetActive(false);

        GameObject InitiateBtn = GameObject.Find("InitiateBtn");


        StageMessages.StageNetworkInit();

        RootCauses.transform.localScale = new Vector3(0,0,0);
        CorrectiveActions.transform.localScale = new Vector3(0,0,0);

        //GameObject titlebarbtns = titlebar.transform.GetChild(2).gameObject;
        //GameObject pressablefollowmebtn = titlebarbtns.transform.GetChild(0).gameObject;
        //Symptoms.GetComponent<FollowMeToggle>().ToggleFollowMeBehavior();
    }
}
