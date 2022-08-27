using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Day ", menuName = "Time/Create day",order = 1)]
public class CreateDay : ScriptableObject{
    public int day;
    public bool holiday;
}