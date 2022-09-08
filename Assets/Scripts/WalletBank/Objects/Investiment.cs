using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="Investiment",menuName="Money/Investiment/Create a Investiment", order = 1 )]
public class Investiment : ScriptableObject{
    public int id;
    public string name;
    public float rate;
    public int timeMax;
    public int valueMin;
    public int valueCurrent;
    public bool expired;
}