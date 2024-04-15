using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SelectPlayerTool : Editor
{
    [MenuItem("Tools/Select Player #&C")]
     public static void SelectPlayer()
    {
        Selection.activeGameObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
