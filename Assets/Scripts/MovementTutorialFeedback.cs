using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTutorialFeedback : MonoBehaviour
{
    [SerializeField] private GameObject[] wasdFeedback;
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject[] joystickFeedback;
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
        if (PlayerInputController.Instance.IsUsingKeyboard())
        {
            foreach (var wasd in wasdFeedback)
            {
                wasd.SetActive(true);
            }
        }
        else
        {
            joystick.SetActive(true);
            foreach (var wasd in joystickFeedback)
            {
                wasd.SetActive(true);
            }
        }

    }
    public void DeactivateFeedback(int index)
    {
        if (PlayerInputController.Instance.IsUsingKeyboard())
        {
            wasdFeedback[index].GetComponent<Animator>().SetTrigger("pressed");
        }
        else
        {
            joystickFeedback[index].GetComponent<Animator>().SetTrigger("pressed");
        }
    }
}
