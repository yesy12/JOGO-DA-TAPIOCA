using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class WalletController : MonoBehaviour{

    public Wallet Wallet;
    public TMP_Text money;
    
    public Investiment investiment;

    void Awake() {
        searchWallet();
        Wallet.setMoney("=",1000);
    }
    
    void Start(){
        Wallet.setMoney("-",40);
    }

    
    void Update(){
        money.text = $"{Wallet.money}";
        
    }
    void FixedUpdate(){
    }
        
    void searchWallet(){
        var assets_scriptableObejcts = AssetObjectsController.returnAndsearchAssets("Assets/Objects/Money/Wallet");

        for(int i = 0; i < assets_scriptableObejcts.Length; i++){
            string pathNameAsset = AssetDatabase.GUIDToAssetPath(assets_scriptableObejcts[i]);
            Wallet = (Wallet) AssetDatabase.LoadAssetAtPath(pathNameAsset, typeof(Wallet));
        }
    }

}