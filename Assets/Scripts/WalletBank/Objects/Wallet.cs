using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="Wallet", menuName="Money/Wallet/Create a Wallet", order = 1)]

public class Wallet : ScriptableObject{
    public int money {get; protected set;}

    public bool setMoney(string op, uint value){
        if(op == "-"){
            var result = money - (int)value;
            if(result >= 0){
                for (int i = 0; i < value; i++){
                    money -= 1;
                }
                return true;
            }
            else{
                return false;
            }
        }
        else if(op == "="){
            money = (int)value;
            return true;
        }
        else if(op == "+"){
            for (int i = 0; i < value; i++){
                return true;
                money += 1;
            }
        }
        return false;
    }
    
}