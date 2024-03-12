using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilitys/ParryShieldAbility")]
public class ParryShieldAbility : Ability
{

    [SerializeField] private float shieldLength;
    [SerializeField] private float ShieldPercentage;
    private HealthBehaviour playerhealth;

    

    public override void OnEnterState(GameObject stateGameObject)
    {
        playerhealth = stateGameObject.GetComponent<HealthBehaviour>();
        playerhealth.SetDamageModifier(0);
        PlayerReferences.instance.shieldObject.SetActive(true);
        ChangeColor(new Color(255f / 255f, 255f / 255f, 255f / 255f, 50f / 255f));
    }

    public override void OnExitState()
    {
        if (playerhealth.GetParrydetector())
        {
            playerhealth.SetDamageModifier(1);
            //instanciaPlayerTimers.abilityTimers[1] = instanciaPlayerTimers.abilityCD[1];
            PlayerReferences.instance.shieldObject.SetActive(false);

        }
        else
        {
            MonoInstance.instance.StartCoroutine(EnableShield());
            ChangeColor(new Color(0f / 255f, 255f / 255f, 120f / 255f, 100f / 255f));
            playerhealth.SetDamageModifier(ShieldPercentage);
        }
        playerhealth.SetParrydetectorFalse();

    }

    public override void AbilityUpdate()
    {
        throw new System.NotImplementedException();
    }

    void ChangeColor(Color newColor)
    {
        // Establece el color del material al nuevo color
        PlayerReferences.instance.shieldObjectRenderer.material.color = newColor;
    }

    IEnumerator EnableShield()
    {
        yield return new WaitForSeconds(shieldLength);
        playerhealth.SetDamageModifier(1);
        PlayerReferences.instance.shieldObject.SetActive(false);
    }
}
