using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class StunBar : MonoBehaviour
{
    [SerializeField] GameObject stunThingsContainer;
    [SerializeField] private Image stunBar;
    [SerializeField] private float fillSpeed;
    public void StartStun(float stun)
    {
        stunBar.fillAmount = 1;
        stunThingsContainer.SetActive(true);
        StartCoroutine(_StunBar(stun));
    }

    private IEnumerator _StunBar(float stunTime)
    {
        float reductionRate = 0.1f;
        while (Mathf.Abs(stunBar.fillAmount - reductionRate) > 0.01f)
        {
            stunBar.fillAmount = stunBar.fillAmount - reductionRate;

            yield return new WaitForSeconds(stunTime / 10);
        }

        stunBar.fillAmount = 0;
        stunThingsContainer.SetActive(false);
    }


}
