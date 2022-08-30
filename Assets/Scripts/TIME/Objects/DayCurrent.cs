using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Day Current", menuName="Time/Day Current", order = 2)]
public class DayCurrent : ScriptableObject{
    public int minutesCurrent;
    public int hourCurrent;
    public int dayCurrent;

}