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
    private bool currentTutorialFinished;
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
                case 2: DashTutorial(); break;
            }
        }
    }


    public void MovementTutorial()
    {
        //Debug.Log(PlayerInputController.Instance.GetRawMovementInput());
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
            if (!currentTutorialFinished)
            {
                StartCoroutine(_DestroyFeedback(currentFeedback));
                currentTutorialFinished = true;
            }
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
            currentFeedback.GetComponent<SingleActionTutorial>().PressButton();
            if (!currentTutorialFinished)
            {
                StartCoroutine(_DestroyFeedback(currentFeedback));
                currentTutorialFinished = true;
            }

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
            currentFeedback.GetComponent<SingleActionTutorial>().PressButton();
            if (!currentTutorialFinished)
            {
                StartCoroutine(_DestroyFeedback(currentFeedback));
                currentTutorialFinished = true;
            }
        }
        FinishTutorial();
    }

    private IEnumerator _DestroyFeedback(GameObject objectToDestroy)
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(objectToDestroy);
        currentFeedback = null;
        currentTutorial++;
        currentTutorialFinished = false;
    }
    public void FinishTutorial()
    {
        PlayerPrefs.SetInt("HasDoneTutorial", 1);
    }
}
