using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class InvestimentController : MonoBehaviour{

    public List<Investiment> list;

    void Start(){
        searchInvestiment();
    }

    void Update(){
        
    }
    void searchInvestiment(){
        var assets_scriptableObejcts = AssetObjectsController.returnAndsearchAssets("Assets/Objects/Money/Investiment");

        for(int i = 0; i < assets_scriptableObejcts.Length; i++){
            string pathNameAsset = AssetDatabase.GUIDToAssetPath(assets_scriptableObejcts[i]);
            Investiment assetInvestiment = (Investiment) AssetDatabase.LoadAssetAtPath(pathNameAsset, typeof(Investiment));

            list.Add(assetInvestiment);

        }
    }

}