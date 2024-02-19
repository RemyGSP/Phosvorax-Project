using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    [SerializeField] GameObject HUD;
    [SerializeField] Image[] HUDAbilities;
    [SerializeField] TextMeshProUGUI[] abilitiesVisualCD;

    private void Start()
    {
        instance = this;
    }

    //Ability number puede ir del 0 al 3 este metodo se llama desde las decisiones de cada habilidad
    public void AbilityStartCooldown(int abilityNumber)
    {
        float cooldown = PlayerTimers.Instance.abilityCD[abilityNumber];
        PlayerTimers.Instance.abilityTimers[abilityNumber] = 0f;
        if (cooldown > 1)
        { 
            abilitiesVisualCD[abilityNumber].gameObject.SetActive(true);
        }
          // StartCoroutine(DoCooldown(HUDAbilities[abilityNumber], abilitiesVisualCD[abilityNumber], cooldown,abilityNumber));
    }

    private IEnumerator DoCooldown(Image ability,TextMeshProUGUI abilityVisualCD, float cooldown, int abilityNumber)
    {
        float timer = 0.0f;

        Debug.Log(PlayerTimers.Instance.abilityTimers[abilityNumber]);
        Debug.Log(cooldown);

        while (timer < cooldown)
        {
            Debug.Log("Cooldown Start");
            ability.fillAmount = timer / cooldown;
            abilityVisualCD.text = Mathf.Floor(cooldown - timer).ToString();
            // Increment the timer by the time passed since the last frame
            timer += Time.deltaTime;
            
            // Wait for the next frame
            yield return null;
        }
        abilitiesVisualCD[abilityNumber].gameObject.SetActive(false);
        ability.fillAmount = 1.0f;



    }


}
