using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearEvidenceBtnHandler : MonoBehaviour
{


    public void OnClearEvidence(){

        int evidenceID = Phase2BtnHandler.evidenceid;
        PanelHandler.DisableDecisionSupport();
        StageMessages.StageClearEvidence(evidenceID);
        Phase2Visualization.VisualizeJsonPhase2(Phase1Visualization.messageContent, Phase1BtnHandler.clickeditem, "", "");

    }


}
