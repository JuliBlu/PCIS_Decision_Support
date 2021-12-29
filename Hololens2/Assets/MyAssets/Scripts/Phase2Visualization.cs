using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Windows;

public class Phase2Visualization : MonoBehaviour
{

    public static List<Tuple<int, string, int>> failuresListGlobal;
    public static List<Tuple<int, string, int>> rootCausesListGlobal;
    public static List<Tuple<int, string, int>> symptomsListGlobal;

    void Start()
    {
        
    }


    /**
   * Returns the type of an error that occurred.
   *
   * @param     failures  the list of failures.
   * @param     symptoms   the list of symptoms.
   * @param     rootCauses   the list of rootcauses.
   * @param     clickedItem   the error that occurred.
   * @return    string  the type of the error that occurred.
   */
    public static string GetItemType(List<MessageInformation> failures, List<Child> symptoms, List<Child> rootCauses, string clickeditem)
    {

        string type = "";

        foreach (MessageInformation mi in failures)
        {

            if (mi.name.Equals(clickeditem))
            {
                type = "failure";
            }

        }

        foreach (Child c in symptoms)
        {

            if (c.name.Equals(clickeditem))
            {
                type = "symptom";
            }

        }

        foreach (Child c in rootCauses)
        {

            if (c.name.Equals(clickeditem))
            {
                type = "rootCause";
            }

        }

        return type;

    }


    /**
    * Visualizes the message of possible evidence on the decision Support boxes.
    *
    * @param     content   the message content to visualize
    * @param     clickeditem   the error that occurred
    * @param     evidencename   dictates if the method is called from phase 1 or phase3
    * @return    void
    */
    public static void VisualizeJsonPhase2(Content content, string clickeditem, string evidencename, string evidencetype)
    {

        PanelHandler.ClearPanels(true, true, true, true);
        CellColoring.DisableArrowWaypointer();
        PanelHandler.EnableClearEvidence();

        List<MessageInformation> jsonObjects = new List<MessageInformation>();
        List<Child> symptoms = new List<Child>();
        List<Child> rootCauses = new List<Child>();


        foreach (MessageInformation mi in content.content)
        {
            jsonObjects.Add(mi);
        }

        foreach (MessageInformation mi in content.content)
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


        //Type of phase1 clicked button -> ("rootCause", "failure", "symptom")
        string type = GetItemType(jsonObjects, symptoms, rootCauses, clickeditem);

        ProcessFailuresPhase2(jsonObjects, clickeditem, type, evidencename, evidencetype);

        ProcessSymptomsAndRootCausesPhase2(jsonObjects, clickeditem, type, evidencename, evidencetype);



    }



    /**
    * Processes the failues
    *
    * @param     failures  the list of failures.
    * @param     clickeditem  the error that occurred.
    * @param     type type of the error that occurred.
    * @param     evidencename  the name of the evidence.
    * @param     evidencetype  the type of the evidence.
    * @return    void
    */
    public static void ProcessFailuresPhase2(List<MessageInformation> failures, string clickeditem, string type, string evidencename, string evidencetype)
    {
        GameObject failurePanel = GameObject.Find("FaultsPanel");
        Transform failureTransform = failurePanel.transform;

        GameObject g;

        GameObject failurePrefab = Resources.Load("EvidencePanel") as GameObject;

        //int: id, string: name, int: probability
        List<Tuple<int, string, int>> failureList = new List<Tuple<int, string, int>>();


        if (type.Equals("failure"))
        {

            foreach (MessageInformation failure in failures)
            {

                if (failure.name.Equals(clickeditem))
                {

                    foreach (State state in failure.states)
                    {

                        if (evidencetype == "")
                        {

                            failureList.Add(Tuple.Create(failure.id, state.name, state.probability));

                        }
                        else if (state.name.Equals(evidencename))
                        {

                            failureList.Add(Tuple.Create(failure.id, state.name, 100));

                        }
                        else if (evidencetype.Equals("symptom") ^ evidencetype.Equals("rootcause"))
                        {

                            failureList.Add(Tuple.Create(failure.id, state.name, state.probability));

                        }

                    }

                }

            }

        }
        else if (type.Equals("symptom"))
        {

            foreach (MessageInformation failure in failures)
            {

                foreach (Child child in failure.children)
                {

                    if (child.name.Equals(clickeditem))
                    {
                        foreach (State state in failure.states)
                        {

                            if (evidencetype == "")
                            {

                                failureList.Add(Tuple.Create(failure.id, state.name, state.probability));

                            }
                            else if (state.name.Equals(evidencename))
                            {

                                failureList.Add(Tuple.Create(failure.id, state.name, 100));

                            }
                            else if (evidencetype.Equals("symptom") ^ evidencetype.Equals("rootcause"))
                            {

                                failureList.Add(Tuple.Create(failure.id, state.name, state.probability));

                            }



                        }
                    }

                }

            }


        }

        else if (type.Equals("rootCause"))
        {

            foreach (MessageInformation failure in failures)
            {

                foreach (Child child in failure.children)
                {

                    if (child.name.Equals(clickeditem))
                    {

                        foreach (State state in failure.states)
                        {

                            if (evidencetype == "")
                            {

                                failureList.Add(Tuple.Create(failure.id, state.name, state.probability));

                            }
                            else if (state.name.Equals(evidencename))
                            {

                                failureList.Add(Tuple.Create(failure.id, state.name, 100));

                            }
                            else if (evidencetype.Equals("symptom") ^ evidencetype.Equals("rootcause"))
                            {

                                failureList.Add(Tuple.Create(failure.id, state.name, state.probability));

                            }

                        }
                    }

                }

            }


        }


        if (evidencetype.Equals("symptom"))
        {

            failureList = new List<Tuple<int, string, int>>();

            foreach (State s in Phase3Visualization.failuresListPhase3)
            {

                failureList.Add(Tuple.Create(0, s.name, s.probability));

            }

        }

        failuresListGlobal = failureList;

        foreach (Tuple<int, string, int> tuple in failureList)
        {

            int id = tuple.Item1;
            string name = tuple.Item2;
            int percentage = tuple.Item3;
            percentage = 100 - percentage;

            g = Instantiate(failurePrefab, failureTransform);
            GameObject nameLabel = g.transform.GetChild(0).gameObject;
            GameObject slider = g.transform.GetChild(1).gameObject;
            GameObject setTrueCheckBox = g.transform.GetChild(2).gameObject;
            GameObject setFalseCheckBox = g.transform.GetChild(3).gameObject;
            GameObject percentageLabel = g.transform.GetChild(4).gameObject;

            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().text = name;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 18;
            percentageLabel.GetComponent<TMPro.TextMeshProUGUI>().text = percentage + "% Present";
            slider.GetComponent<Slider>().maxValue = 100;
            slider.GetComponent<Slider>().value = percentage;

        }
    }


    /**
   * Processes the symptoms and Rootcauses
   *
   * @param     messageContent the contents of the message.
   * @param     clickeditem  the error that occurred.
   * @param     type type of the error that occurred.
   * @param     evidencename  the name of the evidence.
   * @param     evidencetype  the type of the evidence.
   * @return    void
   */
    public static void ProcessSymptomsAndRootCausesPhase2(List<MessageInformation> messageContent, string clickeditem, string type, string evidencename, string evidencetype)
    {

        GameObject symptomsPanel = GameObject.Find("SymptomsPanel");
        Transform symptomsTransform = symptomsPanel.transform;


        GameObject rootCausesPanel = GameObject.Find("RootCausesPanel");
        Transform rootCausesTransform = rootCausesPanel.transform;

        GameObject g;

        GameObject g2;

        GameObject symptomsPrefab = Resources.Load("EvidencePanel") as GameObject;
        GameObject rootCausesPrefab = Resources.Load("EvidencePanel") as GameObject;

        //int: id, string: name, int: probability
        List<Tuple<int, string, int>> symptomsList = new List<Tuple<int, string, int>>();
        List<Tuple<int, string, int>> rootCausesList = new List<Tuple<int, string, int>>();

        if (type.Equals("failure"))
        {

            foreach (MessageInformation failure in messageContent)
            {

                if (failure.name.Equals(clickeditem))
                {

                    foreach (Child child in failure.children)
                    {
                        if (child.type == 2)
                        {

                            //s[0] is green part

                            if (evidencetype == "")
                            {

                                symptomsList.Add(Tuple.Create(child.id, child.name, child.states[0].probability));

                            }
                            else if (child.name.Equals(evidencename))
                            {

                                symptomsList.Add(Tuple.Create(child.id, child.name, 100));

                            }
                            else if (evidencetype.Equals("failure") ^ evidencetype.Equals("rootcause"))
                            {

                                symptomsList.Add(Tuple.Create(child.id, child.name, child.states[0].probability));

                            }


                        }
                        else if (child.type == 4)
                        {

                            //s[0] is green part
                            if (evidencetype == "")
                            {

                                rootCausesList.Add(Tuple.Create(child.id, child.name, child.states[0].probability));

                            }
                            else if (child.name.Equals(evidencename))
                            {

                                rootCausesList.Add(Tuple.Create(child.id, child.name, 100));

                            }
                            else if (evidencetype.Equals("symptom") ^ evidencetype.Equals("failure"))
                            {

                                rootCausesList.Add(Tuple.Create(child.id, child.name, child.states[0].probability));

                            }

                        }
                    }
                }

            }

        }
        else if (type.Equals("symptom") ^ type.Equals("rootCause"))
        {

            foreach (MessageInformation failure in messageContent)
            {
                foreach (Child child in failure.children)
                {
                    if (child.name.Equals(clickeditem))
                    {

                        //TODO Filter out same items
                        foreach (Child child2 in failure.children)
                        {

                            if (child2.type == 2)
                            {


                                if (evidencetype == "")
                                {

                                    symptomsList.Add(Tuple.Create(child2.id, child2.name, child2.states[0].probability));

                                }
                                else if (child2.name.Equals(evidencename))
                                {

                                    symptomsList.Add(Tuple.Create(child2.id, child2.name, 100));

                                }
                                else if (evidencetype.Equals("failure") ^ evidencetype.Equals("rootcause"))
                                {

                                    symptomsList.Add(Tuple.Create(child2.id, child2.name, child2.states[0].probability));

                                }


                            }
                            else if (child2.type == 4)
                            {


                                if (evidencetype == "")
                                {

                                    rootCausesList.Add(Tuple.Create(child2.id, child2.name, child2.states[0].probability));

                                }
                                else if (child2.name.Equals(evidencename))
                                {

                                    rootCausesList.Add(Tuple.Create(child2.id, child2.name, 100));

                                }
                                else if (evidencetype.Equals("symptom") ^ evidencetype.Equals("failure"))
                                {

                                    rootCausesList.Add(Tuple.Create(child2.id, child2.name, child2.states[0].probability));

                                }


                            }

                        }
                    }

                }

            }


        }


        if (evidencetype.Equals("failure"))
        {

            symptomsList = new List<Tuple<int, string, int>>();

            foreach (Child c in Phase3Visualization.symptomsListPhase3)
            {

                symptomsList.Add(Tuple.Create(c.id, c.name, c.states[0].probability));

            }


        }


        symptomsListGlobal = symptomsList;
        rootCausesListGlobal = rootCausesList;

        foreach (Tuple<int, string, int> tuple in symptomsList)
        {

            int id = tuple.Item1;
            string name = tuple.Item2;
            int percentage = tuple.Item3;

            g = Instantiate(symptomsPrefab, symptomsTransform);
            GameObject nameLabel = g.transform.GetChild(0).gameObject;
            GameObject slider = g.transform.GetChild(1).gameObject;
            GameObject setTrueCheckBox = g.transform.GetChild(2).gameObject;
            GameObject setFalseCheckBox = g.transform.GetChild(3).gameObject;
            GameObject percentageLabel = g.transform.GetChild(4).gameObject;

            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().text = name;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 18;
            percentageLabel.GetComponent<TMPro.TextMeshProUGUI>().text = percentage + "% Present";
            slider.GetComponent<Slider>().maxValue = 100;
            slider.GetComponent<Slider>().value = percentage;

        }



        foreach (Tuple<int, string, int> tuple in rootCausesList)
        {

            int id = tuple.Item1;
            string name = tuple.Item2;
            int percentage = tuple.Item3;

            g2 = Instantiate(rootCausesPrefab, rootCausesTransform);
            GameObject nameLabel = g2.transform.GetChild(0).gameObject;
            GameObject slider = g2.transform.GetChild(1).gameObject;
            GameObject setTrueCheckBox = g2.transform.GetChild(2).gameObject;
            GameObject setFalseCheckBox = g2.transform.GetChild(3).gameObject;
            GameObject percentageLabel = g2.transform.GetChild(4).gameObject;

            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().text = name;
            nameLabel.GetComponent<TMPro.TextMeshProUGUI>().fontSize = 18;
            percentageLabel.GetComponent<TMPro.TextMeshProUGUI>().text = percentage + "% Present";
            slider.GetComponent<Slider>().maxValue = 100;
            slider.GetComponent<Slider>().value = percentage;

        }


    }



}
