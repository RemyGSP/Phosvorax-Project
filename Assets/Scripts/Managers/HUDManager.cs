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
    private Vector2[] rectTransforms;

    private void Start()
    {
        instance = this;
        int i = 0;
        rectTransforms = new Vector2[HUDAbilities.Length];
        foreach (Image image in HUDAbilities)
        {
            rectTransforms[i] = image.rectTransform.sizeDelta;
                i++;
        }
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
        HUDAbilities[abilityNumber].rectTransform.sizeDelta = rectTransforms[abilityNumber];
         StartCoroutine(DoCooldown(HUDAbilities[abilityNumber], abilitiesVisualCD[abilityNumber], cooldown,abilityNumber));
    }

    private IEnumerator DoCooldown(Image ability,TextMeshProUGUI abilityVisualCD, float cooldown, int abilityNumber)
    {
        float timer = 0.0f;
        
        //Debug.Log(PlayerTimers.Instance.abilityTimers[abilityNumber]);
        //Debug.Log(cooldown);
        
        ability.gameObject.SetActive(true);
        abilityVisualCD.gameObject.SetActive(true);
        Vector2 size = ability.rectTransform.sizeDelta;
        while (timer < cooldown)
        {
            ability.rectTransform.sizeDelta = new Vector2(size.x  -(size.x *  (timer / cooldown)),size.y);
            abilityVisualCD.text = (cooldown - timer).ToString("0.0");
            // Increment the timer by the time passed since the last frame
            timer += Time.deltaTime;
            // Wait for the next frame
            yield return null;
        }
        ability.gameObject.SetActive(false);
        abilityVisualCD.gameObject.SetActive(false);



    }


}
