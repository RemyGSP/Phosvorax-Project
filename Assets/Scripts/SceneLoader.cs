using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject sceneTransitions;
    public static SceneLoader Instance { get; private set; }
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Ya existe otra instancia de SceneLoader");
            Destroy(gameObject);
        }

    }
    public void SceneLoad(string scene)
    {
        if (sceneTransitions != null)
        {
            StartCoroutine(LoadSceneAsync(scene));
        }
        else
        {
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
    }

    IEnumerator LoadSceneAsync(string scene)
    {
        Debug.Log(sceneTransitions);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);

        

        GameObject b = Instantiate(sceneTransitions);
        DontDestroyOnLoad (b);
        
        yield return asyncLoad; 
        
        while (!asyncLoad.isDone)
        {
            Debug.Log(PrefabRoomInstancier.isMapGenerated);
            yield return null;
        }
    }

    public void AddScene(string scene)
    {
        SceneManager.LoadScene(scene,LoadSceneMode.Additive);
    }

    public void RemoveScene(string scene) 
    {
        SceneManager.UnloadSceneAsync(scene);
    }
}
