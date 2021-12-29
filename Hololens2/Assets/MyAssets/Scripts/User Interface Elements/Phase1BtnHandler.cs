using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase1BtnHandler : MonoBehaviour
{
    public static string clickeditem;

    /**
    * Handles selected button.
    * Selected button represents the found error.
    * @param     none
    * @return    void.
    */
    public void OnClick() {
        GameObject g = this.gameObject;
        GameObject child0 = g.transform.GetChild(0).gameObject;
        string s = child0.GetComponent<Text>().text;
        //Debug.Log("Phase1; Selected: " + s);
        clickeditem = s;
        Phase2Visualization.VisualizeJsonPhase2(Phase1Visualization.messageContent, s, "", "");
        
    }

}
