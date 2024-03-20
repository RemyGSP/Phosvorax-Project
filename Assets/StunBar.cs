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
        stunThingsContainer.SetActive(true);
        StartCoroutine(_StunBar(stun));
    }

    private IEnumerator _StunBar(float stunTime)
    {
        float reductionRate = 1f / stunTime;
        while (Mathf.Abs(stunBar.fillAmount - reductionRate) > 0.01f)
        {
            // Use Mathf.Lerp to interpolate between the current fill amount and the target fill amount
            stunBar.fillAmount = Mathf.Lerp(stunBar.fillAmount, 0, Time.deltaTime * fillSpeed);

            yield return null; // Wait for the end of frame before continuing the loop
        }

        // Ensure the health bar reaches exactly the target fill amount
        stunBar.fillAmount = 0;
        stunThingsContainer.SetActive(false);
    }


}
