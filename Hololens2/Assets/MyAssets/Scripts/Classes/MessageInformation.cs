using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class MessageInformation
{
    public int id;
    public string name;
    public State[] states;
    public int type;
    public string relation;
    public string[] cardinality;
    public Child[] children;
}
