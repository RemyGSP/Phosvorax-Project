using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorialFeedback : MonoBehaviour
{
    [SerializeField] private GameObject[] wasdFeedback;
    void Start()
    {
            ActivateFeedback();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateFeedback()
    {
        foreach (var wasd in wasdFeedback)
        {
            wasd.SetActive(true);
        }
    }


    public void DeactivateFeedback(int index)
    {
        wasdFeedback[index].SetActive(false);   
    }
}
