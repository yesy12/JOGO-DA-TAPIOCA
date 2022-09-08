using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class TimeController: MonoBehaviour{

    [Header("------List of day------")]
    public List<CreateDay> List_day = new List<CreateDay>();

    [Header("------Time------")]

    public DayCurrent dayCurrentAsset_;
    public float hour;
    public float minutes;
    public TMP_Text tempText;

    [Header("------Holidar------")]
    
    public bool isDayHoliday = false;
    public bool isDayPreHoliday = false;
    public bool whileNewDayPass = false;

    [Header("------Params------")]
    [Range(1,59)] public int minutesParams;
    [Range(1,48)] public int hourParams;

    void Awake(){
        addDayOnList();
        printDate();
    }

    void Update(){
        if(whileNewDayPass){
            time();
        }
        else{
            verifyDay();
            setParams();
        }
    }

    void printDate(){
        var date = System.DateTime.UtcNow.ToLocalTime().ToString("HH:mm:ss");
        print(date);
    }

    void time(){
        string temp_date = "";
        temp_date += hour < 10 ? $"0{hour}:" : $"{hour}:";
        temp_date += Mathf.Floor(minutes) < 10 ? $"0{Mathf.Floor(minutes)}" : $"{Mathf.Floor(minutes)}";

        tempText.text = temp_date;

        minutes += (Time.deltaTime);

        dayCurrentAsset_.minutesCurrent = (int)Mathf.Floor(minutes);
        dayCurrentAsset_.hourCurrent = (int)hour;

        if(minutes > minutesParams){
            hour += 1;
            minutes = 0;

        }

        if(hour >= hourParams){
            printDate();
            dayCurrentAsset_.dayCurrent += 1;
            hour = 0;
            whileNewDayPass = false;

            addDayOnList();
            verifyDay();
            setParams();
        }
    }     
    void setParams(){
        if(isDayHoliday){
            hourParams = 48;
        }
        else if(isDayPreHoliday){
            hourParams = 36;
        }
        else{
            hourParams = 24;
        }
    }

    void addDayOnList(){
        /*
        Pega de forma automática todos os objetos do tipo ScriptableObject, armazenado
        no diretorio path_assets e adiciona na Lista 
        */
        var assets_scriptableObejcts = AssetObjectsController.returnAndsearchAssets("Assets/Objects/Time/Day");

        for(int i = 0; i < assets_scriptableObejcts.Length; i++){
            string pathNameAsset = AssetDatabase.GUIDToAssetPath(assets_scriptableObejcts[i]);
            CreateDay assetDay = (CreateDay) AssetDatabase.LoadAssetAtPath(pathNameAsset, typeof(CreateDay));
            
            bool result = List_day.Exists(day => day == assetDay);
            if(!result){
                List_day.Add(assetDay);
            }
        }
    }
    void verifyDay(){
        int limitPass = 0;
        bool passWhile = false;
        bool end = false;
        bool results = false;
        while (end == false){
            
            if(!passWhile){
                results = List_day.Exists(day => day.day == dayCurrentAsset_.dayCurrent);
            }

            if(!results){
                bool result = List_day.Exists(day => day.day == (dayCurrentAsset_.dayCurrent - 1));
                bool result2 = List_day.Exists(day => day.day == (dayCurrentAsset_.dayCurrent + 1));

                results = result ? true : false;
                if(!results){
                    results = result2 ? true : false;
                }
                passWhile = true;
            }
            else{
                foreach(CreateDay day_single in List_day){
                    if(day_single.day == dayCurrentAsset_.dayCurrent && !whileNewDayPass){// Day Holiday
                        isDayHoliday = day_single.holiday ? true : false;
                        end = day_single.holiday ? true : false;
                        whileNewDayPass = day_single.holiday ? false : true;
                    }
                    else if(day_single.day == dayCurrentAsset_.dayCurrent + 1 && whileNewDayPass 
                    || day_single.day == dayCurrentAsset_.dayCurrent + 1 && passWhile){ // Day + 
                        isDayPreHoliday = day_single.holiday ? true: false;
                        end = day_single.holiday ? true : false;
                    }
                    else if(day_single.day == dayCurrentAsset_.dayCurrent - 1 && whileNewDayPass 
                    || day_single.day == dayCurrentAsset_.dayCurrent - 1 && passWhile){ // Day - 1
                        isDayPreHoliday = day_single.holiday ? true: false;
                        end = day_single.holiday ? true : false;
                    }
                }
            }
            limitPass += 1;
            if(limitPass == 10 || end){
                return ;
            }
        }
    }
}