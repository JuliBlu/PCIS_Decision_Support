using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Phase2BtnHandler : MonoBehaviour
{

    public static string evidencename;
    public static string evidencetype;
    public static int evidenceid;

    /**
    * Selected isPresent to be true.
    * Reports back to backend.
    * @param     none
    * @return    void.
    */
    public void OnClickGreen()
    {

        GameObject g = this.gameObject;

        GameObject parent = g.transform.parent.gameObject;

        GameObject nameLabel = parent.transform.GetChild(0).gameObject;

        string name = nameLabel.GetComponent<TMPro.TextMeshProUGUI>().text;
        int id = -1;

        List<Tuple<int, string, int>> symptomsList = Phase2Visualization.symptomsListGlobal;
        List<Tuple<int, string, int>> rootCausesList = Phase2Visualization.rootCausesListGlobal;
        List<Tuple<int, string, int>> failuresList = Phase2Visualization.failuresListGlobal;


        id = FindIdOfElement(symptomsList, name);

        if (id == -1)
        {
            id = FindIdOfElement(rootCausesList, name);
        }
        if (id == -1)
        {
            id = FindIdOfElement(failuresList, name);
        }

        //Debug.Log("Selected: " + name);
        evidencename = name;
        evidencetype = FindTypeOfElement(Phase1Visualization.messageContent, name);
        evidenceid = id;


        string statename = FindStateOfElement(Phase1Visualization.messageContent, true, id);

        StageMessages.StageSetEvidence(statename, id, true);

        Phase2Visualization.symptomsListGlobal = null;
        Phase2Visualization.rootCausesListGlobal = null;
        Phase2Visualization.failuresListGlobal = null;

        PanelHandler.EnableDecisionSupport();
        PanelHandler.ClearPanels(true, true, true, true);
        PanelHandler.WaitingPanels();
        PanelHandler.DisableClearEvidence();




    }

    /**
* Finds the type of a selected element.
* 
* @param     content the content of the json file.
* @param     elementname the name of the element
* @return    string, the type of the element.
*/
    public static string FindTypeOfElement(Content content, string elementname) {

        string type = "";

        foreach (MessageInformation mi in content.content)
        {

            if (mi.name.Equals(elementname))
            {
                type = "failure";
            }

            foreach (State state in mi.states)
            {
                
                if (state.name != null) {
                if (state.name.Equals(elementname))
                {
                    type = "failure";
                }
            }
            }


            foreach (Child child in mi.children) {

                if (child.name.Equals(elementname) && child.type == 2) {

                    type = "symptom";

                } else if (child.name.Equals(elementname) && child.type == 4) {

                    type = "rootcause";

                }

            }

        }

        return type;

    }

    public string FindStateOfElement(Content content, bool ispresent, int id) {
        string s = "none";
        foreach (MessageInformation mi in content.content) {
            if (mi.id == id) {
                s = mi.states[0].name;
            }

            foreach (Child child in mi.children)
            {

                if (child.id == id) {
                    if (ispresent) {
                        if (child.states != null)
                        {
                            s = child.states[0].name;
                        }
                    }else
                    {
                        if (child.states != null)
                        {
                            s = child.states[1].name;
                        }
                    }
                }

            }
        }

        return s;
    }


    /**
 * Finds the id of a selected evidece.
 * 
 * @param     list the list of candidates.
 * @param     s the element to search the id for.
 * @return    int the id of the element.
 */
    public int FindIdOfElement(List<Tuple<int, string, int>> candidateList, string elementName)
    {
        int id = -1;


        if (candidateList != null)
        {
            foreach (Tuple<int, string, int> tuple in candidateList)
            {

                if (tuple.Item2.Equals(elementName))
                {
                    id = tuple.Item1;
                }

            }
        }

        return id;
    }

    /**
    * Selected isPresent to be false.
    * Reports back to backend.
    * @param     none
    * @return    void.
    */
    public void OnClickRed()
    {

        GameObject g = this.gameObject;

        GameObject parent = g.transform.parent.gameObject;

        GameObject nameLabel = parent.transform.GetChild(0).gameObject;

        string name = nameLabel.GetComponent<TMPro.TextMeshProUGUI>().text;
        int id = -1;

        List<Tuple<int, string, int>> symptomsList = Phase2Visualization.symptomsListGlobal;
        List<Tuple<int, string, int>> rootCausesList = Phase2Visualization.rootCausesListGlobal;
        List<Tuple<int, string, int>> failuresList = Phase2Visualization.failuresListGlobal;


        id = FindIdOfElement(symptomsList, name);

        if (id == -1)
        {
            id = FindIdOfElement(rootCausesList, name);
        }
        if (id == -1)
        {
            id = FindIdOfElement(failuresList, name);
        }

        //Debug.Log("Selected: " + name);
        evidencename = name;
        evidencetype = FindTypeOfElement(Phase1Visualization.messageContent, name);
        evidenceid = id;


        string statename = FindStateOfElement(Phase1Visualization.messageContent, false, id);

        StageMessages.StageSetEvidence(statename, id, false);

        Phase2Visualization.symptomsListGlobal = null;
        Phase2Visualization.rootCausesListGlobal = null;
        Phase2Visualization.failuresListGlobal = null;

        PanelHandler.EnableDecisionSupport();
        PanelHandler.ClearPanels(true, true, true, true);
        PanelHandler.WaitingPanels();
        PanelHandler.DisableClearEvidence();

    }

}
