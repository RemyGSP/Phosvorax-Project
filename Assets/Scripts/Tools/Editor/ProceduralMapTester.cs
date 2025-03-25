using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class ProceduralMapTester : EditorWindow
{
    // Número de veces que se ejecutará la generación
    private int iterations = 10;
    private int currentIteration = 0;
    // Lista para guardar mensajes de error que se produzcan
    private List<string> errorLog = new List<string>();
    private bool testing = false;

    [MenuItem("Tools/Probador de Mapa Procedural")]
    public static void ShowWindow()
    {
        GetWindow<ProceduralMapTester>("Probador de Mapa Procedural");
    }

    void OnGUI()
    {
        GUILayout.Label("Configuración de Prueba", EditorStyles.boldLabel);
        iterations = EditorGUILayout.IntField("Número de Iteraciones", iterations);

        if (!testing && GUILayout.Button("Iniciar Prueba"))
        {
            // Reinicia contadores y registros
            errorLog.Clear();
            currentIteration = 0;
            testing = true;
            // Suscribirse para capturar mensajes de error y excepciones
            Application.logMessageReceived += HandleLog;
            EditorApplication.update += RunTestIteration;
        }

        GUILayout.Space(10);
        GUILayout.Label("Iteración actual: " + currentIteration + " / " + iterations);

        GUILayout.Space(10);
        GUILayout.Label("Errores encontrados:", EditorStyles.boldLabel);
        foreach (string error in errorLog)
        {
            GUILayout.Label(error);
        }
    }

    void RunTestIteration()
    {
        if (currentIteration >= iterations)
        {
            testing = false;
            EditorApplication.update -= RunTestIteration;
            Application.logMessageReceived -= HandleLog;
            Debug.Log("Prueba completada. Iteraciones: " + iterations + ". Errores detectados: " + errorLog.Count);
            return;
        }

        // Crear un GameObject temporal para ejecutar la generación de mapa
        GameObject testGO = new GameObject("ProceduralMapGenerator_Test_" + currentIteration);
        // Añadir el componente del generador de mapa
        ProceduralMatrixGenerator generator = testGO.AddComponent<ProceduralMatrixGenerator>();

        // Configurar parámetros de prueba (ajusta según necesites)
        generator.MapSize = new Vector2Int(10, 10);
        generator.RoomQuantity = 20;
        generator.minDeadEnds = 2;

        // La generación se ejecuta en Start() del ProceduralMatrixGenerator.
        // Usamos delayCall para esperar un frame y luego destruir el objeto,
        // permitiendo que se ejecute el método Start y se recojan posibles errores.
        EditorApplication.delayCall += () =>
        {
            if (testGO != null)
            {
                DestroyImmediate(testGO);
            }
            currentIteration++;
            Repaint();
        };

        // Forzamos repaint para actualizar la ventana
        Repaint();
    }

    // Captura de errores y excepciones durante la prueba
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            errorLog.Add(logString);
        }
    }
}
