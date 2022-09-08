using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AssetObjectsController : MonoBehaviour{
    public static string[] returnAndsearchAssets(string path_name) {
        return  AssetDatabase.FindAssets("", new[] { path_name });
    }

}
