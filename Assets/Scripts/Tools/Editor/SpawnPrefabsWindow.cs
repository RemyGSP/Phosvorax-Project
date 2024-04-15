using PlasticGui.WebApi.Responses;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPrefabsWindow : EditorWindow
{
    private GameObject prefab;
    private GameObject currentPrefab;
    private GameObject[] prefabs;
    private int selectedOptionIndex = 0;
    GUILayoutOption[] prefabOptions;
    private bool instantiating;
    private GameObject instantiatingFeedback;
    private bool enabled;
    private bool isMouseDragging;
    private GameObject lastInstantiated;
    private Vector2 lastMousePosition;

    private void OnGUI()
    {
        string[] prefabGUID = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Prefabs/Enemies" });
        int counter = 0;
        prefabs = new GameObject[prefabGUID.Length];
        foreach (string aux in prefabGUID)
        {
            prefabs[counter] = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(aux));
            counter++;

        }
        prefab = (GameObject)EditorGUILayout.ObjectField("ParentObject", prefab, typeof(GameObject), true);
        GUIContent prefabDropdown = new GUIContent("Prefab", "The prefab you want to instantiate");
        EditorGUILayout.BeginVertical();

        if (EditorGUILayout.DropdownButton(new GUIContent(prefabs[selectedOptionIndex].name), FocusType.Passive))
        {
            GenericMenu menu = new GenericMenu();

            // Add options to the menu
            for (int i = 0; i < prefabs.Length; i++)
            {
                int optionIndex = i;
                menu.AddItem(new GUIContent(prefabs[i].name), false, () => { selectedOptionIndex = optionIndex; });
            }

            menu.ShowAsContext();
        }

        EditorGUILayout.EndVertical();

        if (GUILayout.Button("Instantiate"))
        {
            instantiating = !instantiating;
        }

    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGui;
        enabled = true;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGui;
        enabled = false;
    }


    private void OnSceneGui(SceneView obj)
    {
        Event currentEvent = Event.current;

        if (currentEvent.type == EventType.MouseDown && instantiating)
        {
            if (currentEvent.button == 0)
            {
                isMouseDragging = true;
                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    lastInstantiated = Instantiate(prefabs[selectedOptionIndex], hit.point, Quaternion.identity);
                    if (prefab != null)
                        lastInstantiated.transform.SetParent(prefab.transform);
                    lastMousePosition = currentEvent.mousePosition;
                    Undo.RegisterCreatedObjectUndo(lastInstantiated, prefabs[selectedOptionIndex].name);
                    
                }

            }
        }
        else
        {
            if (instantiating && instantiatingFeedback == null)
            {

                GameObject aux = new GameObject(prefabs[selectedOptionIndex].name);
                GameObject canvas = new GameObject();
                canvas.AddComponent<Canvas>();

                Instantiate(canvas);

                Rect rec = new Rect(0, 0, 100, 100);
                instantiatingFeedback = Instantiate(aux);
                RectTransform trans = instantiatingFeedback.AddComponent<RectTransform>();
                trans.transform.SetParent(canvas.transform); // setting parent
                trans.localScale = Vector3.one;
                trans.anchoredPosition = new Vector2(0f, 0f); // setting position, will be on center
                trans.sizeDelta = new Vector2(150, 200); // custom size
                Image image = instantiatingFeedback.AddComponent<Image>();
                image.sprite = Sprite.Create(AssetPreview.GetMiniThumbnail(prefabs[selectedOptionIndex]), rec, new Vector2(50, 50), 1);


            }
        }
        if (currentEvent.type == EventType.MouseUp)
        {
            isMouseDragging = false;

        }
        if (currentEvent.type == EventType.MouseDrag)
        {
            if (isMouseDragging)
            {
                Vector2 currentMousePosition = currentEvent.mousePosition;
                Vector2 delta = currentMousePosition - lastMousePosition;
                float rotationAmount = delta.x;
                lastInstantiated.transform.Rotate(Vector3.up, rotationAmount);
                lastMousePosition = currentMousePosition;
                currentEvent.Use(); // Consume the event to prevent other GUI elements from using it
            }
        }



        Handles.BeginGUI();

        Handles.EndGUI();
    }
    [MenuItem("Tools/Prefab Instancier")]
    private static void OpenWindow()
    {
        SpawnPrefabsWindow window = new SpawnPrefabsWindow();
        window.titleContent = new GUIContent("Prefab Instancier");
        window.Show();
    }
}
