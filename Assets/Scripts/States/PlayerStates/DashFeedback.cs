using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFeedback : MonoBehaviour
{
    [SerializeField] private GameObject dashFeedback;

    public void ActivateFeedback()
    {
        dashFeedback.SetActive(true);
    }

    public void DeactivateFeedback()
    {
        dashFeedback.SetActive(false);
    }
}
