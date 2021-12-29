using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionSupportBtnHandler : MonoBehaviour
{

    GameObject decisionSupportCorrectBtn;
    GameObject decisionSupportIncorrectBtn;
    Content c;

    /**
    * Finds the UI Elements in the scene.
    * @param     cells the list of cells of the productionline
    * @return    void.
    */
    void Start()
    {
        decisionSupportCorrectBtn = GameObject.Find("CorrectBtn");
        decisionSupportIncorrectBtn = GameObject.Find("IncorrectBtn");

    }

    /**
    * The user selected correct decision support.
    * reports to backend.
    * @param     none
    * @return    void.
    */
    public void DescisionSupportCorrect() {

        StageMessages.StageDecisionSupport(true);
        CellColoring.ColorCellsBlack(Main.cells);
        Content c = Phase1Visualization.messageContent;

        Phase2Visualization.VisualizeJsonPhase2(c, Phase1BtnHandler.clickeditem, Phase2BtnHandler.evidencename, Phase2BtnHandler.evidencetype);
        PanelHandler.DisableDecisionSupport();

        GameObject RootCauses = GameObject.Find("RootCauses");
        GameObject CorrectiveActions = GameObject.Find("CorrectiveActions");
        RootCauses.transform.localScale = new Vector3(0, 0, 0);
        CorrectiveActions.transform.localScale = new Vector3(0, 0, 0);

    }


    /**
    * The user selected incorrect decision support.
    * reports to backend.
    * 
    * TODO:
    * Implement a View to so send faults
    * back to the backend to process.
    * 
    * 
    * @param     none
    * @return    void.
    */
    public void DecisionSupportIncorrect() {
        StageMessages.StageDecisionSupport(false);
        ResetSystem();
    }


    /**
    * resets the system to the original state. 
    * @param     none.
    * @return    void.
    */
    public void ResetSystem() {

        CellColoring.ColorCellsBlack(Main.cells);
        Content c = Phase1Visualization.messageContent;
        Phase1Visualization.VisualizeJsonPhase1(c);
        PanelHandler.DisableDecisionSupport();

        GameObject RootCauses = GameObject.Find("RootCauses");
        GameObject CorrectiveActions = GameObject.Find("CorrectiveActions");
        RootCauses.transform.localScale = new Vector3(0, 0, 0);
        CorrectiveActions.transform.localScale = new Vector3(0, 0, 0);

    }

    
}