using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGameOver : MonoBehaviour
{

    private void Start()
    {
        StopGame();
    }
    public void StopGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
