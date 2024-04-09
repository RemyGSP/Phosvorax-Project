using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleActionTutorial : MonoBehaviour
{
    [SerializeField] private GameObject visualFeedback;


    public void PressButton()
    {
        visualFeedback.GetComponent<Animator>().SetTrigger("pressed");
    }
}
