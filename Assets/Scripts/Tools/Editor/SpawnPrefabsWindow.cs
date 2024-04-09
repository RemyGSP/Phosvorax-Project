using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;

public class SpawnPrefabsWindow : EditorWindow
{
    private GameObject prefab;
    GUILayoutOption[] prefabOptions;
    private void OnGUI()
    {
        prefabOptions = new GUILayoutOption[] {}
        prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", prefab, typeof(GameObject), false);
        GUIContent prefabDropdown = new GUIContent("Prefab", "The prefab you want to instantiate");
        EditorGUILayout.DropdownButton(prefabDropdown,false, );

    }

    [MenuItem("Tools/Prefab Instancier")]
    private static void OpenWindow()
    {
        SpawnPrefabsWindow window = new SpawnPrefabsWindow();
        window.titleContent = new GUIContent("Prefab Instancier");
        window.Show();
    }
}
