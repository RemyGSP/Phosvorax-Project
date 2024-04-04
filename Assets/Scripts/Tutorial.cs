using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;


public class Tutorial : MonoBehaviour
{
    private bool hasDoneTutorial;
    private bool isTutorial;
    private int currentTutorial;
    private bool[] movedToDirections;
    [SerializeField] private GameObject movementTutorialFeedback;
    [SerializeField] private GameObject attackTutorialFeedback;
    [SerializeField] private GameObject dashTutorialFeedback;
    private GameObject currentFeedback;
    void Start()
    {
        if (PlayerPrefs.HasKey("HasDoneTutorial"))
        {
            if (PlayerPrefs.GetInt("HasDoneTutorial") == 1)
            {
                hasDoneTutorial = true;
            }
        }
        else
        {
            PlayerPrefs.SetInt("HasDoneTutorial", 0);
        }
        if (!hasDoneTutorial)
        {
            movedToDirections = new bool[4];
            currentTutorial = 0;
            isTutorial = true;
        }
    }

    void Update()
    {
        if (isTutorial)
        {
            switch (currentTutorial)
            {
                case 0: MovementTutorial(); break;
                case 1: AttackTutorial(); break;   
            }
        }
    }


    public void MovementTutorial()
    {
        Debug.Log(PlayerInputController.Instance.GetRawMovementInput());
        Vector3 currentInput = PlayerInputController.Instance.GetRawMovementInput();
        if (currentFeedback == null)
        {
            currentFeedback = Instantiate(movementTutorialFeedback);
        }

        if (currentInput.x > 0)
        {
            movedToDirections[0] = true;
            currentFeedback.GetComponent<MovementTutorialFeedback>().DeactivateFeedback(0);
        }
        else if (currentInput.x < 0)
        {
            movedToDirections[1] = true;
            currentFeedback.GetComponent<MovementTutorialFeedback>().DeactivateFeedback(1);

        }
        if (currentInput.z > 0 && currentInput.z != 0)
        {
            currentFeedback.GetComponent<MovementTutorialFeedback>().DeactivateFeedback(2);
            movedToDirections[2] = true;
        }
        else if (currentInput.z < 0)
        {
            currentFeedback.GetComponent<MovementTutorialFeedback>().DeactivateFeedback(3);
            movedToDirections[3] = true;
        }

        if (movedToDirections[0] && movedToDirections[1] && movedToDirections[2] && movedToDirections[3])
        {
            Destroy(currentFeedback);
            currentFeedback = null;
            currentTutorial++;
        }
    }

    public void AttackTutorial()
    {
        if (currentFeedback == null)
        {
            currentFeedback = Instantiate(attackTutorialFeedback);
        }
        if (PlayerInputController.Instance.IsAttacking())
        {
            Destroy(currentFeedback);
            currentFeedback = null;
            currentTutorial++;
        }
    }

    public void DashTutorial()
    {
        if (currentFeedback == null)
        {
            currentFeedback = Instantiate(dashTutorialFeedback);
        }
        if (PlayerInputController.Instance.IsRolling())
        {
            Destroy(currentFeedback);
            currentFeedback = null;
            currentTutorial++;
        }
    }

    public void FinishTutorial()
    {
        PlayerPrefs.SetInt("HasDoneTutorial", 1);
    }
}
