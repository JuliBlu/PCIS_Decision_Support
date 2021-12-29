using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public GameObject cellObject;
    public string description;
    public string cellname;
    public List<string> processes;

    public Cell(List<string> processes, string cellname, GameObject cellObject) {
        this.cellname = cellname;
        this.cellObject = cellObject;
        this.processes = processes;
    }

    public string getCellName() {
        return cellname;
    }

    public GameObject getCellObject()
    {
        return cellObject;
    }

    public List<string> getCellProcesses()
    {
        return processes;
    }

    public string toString() {
        string s = "";
        s += cellname += ", ";
        foreach (string process in processes) {
            s += "Process: " + process;
        }
        s += " ";

        return s;
    }



}
