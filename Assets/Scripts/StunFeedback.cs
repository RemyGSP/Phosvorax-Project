using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StunFeedback : MonoBehaviour
{
    [SerializeField] GameObject stunFeedback;

    public void StartStun(float stun)
    { 
        stunFeedback.SetActive(true);
        StartCoroutine(_StopStun(stun));
    }



    private IEnumerator _StopStun(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        stunFeedback.SetActive(false);
    }


}
