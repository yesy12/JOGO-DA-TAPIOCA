using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class TimeController: MonoBehaviour{

    [Header("------List of day------")]
    public List<CreateDay> List_day = new List<CreateDay>();

    [Header("------Time------")]

    public float day;
    public float hour;
    public float minutes;

    public TMP_Text tempText;


    void Awake(){
       addDayOnList();
       var date = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm:ss");
       Debug.Log(date);
    }

    void Update(){
        string temp_date = "";
        temp_date += hour < 10 ? $"0{hour}:" : $"{hour}:";
        temp_date += Mathf.Floor(minutes) < 10 ? $"0{Mathf.Floor(minutes)}" : $"{Mathf.Floor(minutes)}";

        tempText.text = temp_date;

        minutes += (Time.deltaTime);

        if(minutes > 59){
            hour += 1;
            minutes = 0;
        }

        if(hour > 23){
            var date = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm:ss");
            Debug.Log(date);
            day += 1;
            hour = 0;
            addDayOnList();
        }
    
    }

    void addDayOnList(){
        /*
        Pega de forma automática todos os objetos do tipo ScriptableObject, armazenado
        no diretorio path_assets e adiciona na Lista 
        */
        string path_assets = "Assets/Objects/Time/Day";
        var assets_scriptableObejcts = AssetDatabase.FindAssets("",new[]{path_assets});

        for(int i = 0; i < assets_scriptableObejcts.Length; i++){
            string pathNameAsset = AssetDatabase.GUIDToAssetPath(assets_scriptableObejcts[i]);
            CreateDay assetDay = (CreateDay) AssetDatabase.LoadAssetAtPath(pathNameAsset, typeof(CreateDay));
            
            bool result = List_day.Exists(day => day == assetDay);
            if(!result){
                List_day.Add(assetDay);
            }
        }
    }
}