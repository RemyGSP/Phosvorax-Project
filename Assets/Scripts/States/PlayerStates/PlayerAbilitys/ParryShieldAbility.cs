using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilitys/ParryShieldAbility")]
public class ParryShieldAbility : Ability
{

    [SerializeField] private float shieldLength;
    [SerializeField] private float ShieldPercentage;
    private HealthBehaviour playerhealth;

    

    public override void OnEnterState(GameObject stateGameObject)
    {
        Material disolveMaterial = PlayerReferences.instance.shieldObject.GetComponent<MeshRenderer>().sharedMaterial;
        disolveMaterial.SetFloat("_Dissolve", -0.3f);
        Debug.Log("Start");
        playerhealth = stateGameObject.GetComponent<HealthBehaviour>();
        playerhealth.SetDamageModifier(0);
        PlayerReferences.instance.shieldObject.SetActive(true);
        MonoInstance.instance.StartCoroutine(Dissolve());
        ChangeColor(new Color(255f / 255f, 255f / 255f, 255f / 255f, 50f / 255f));
    }
    


    public override void OnExitState()
    {
        if (playerhealth.GetParrydetector())
        {
            playerhealth.SetDamageModifier(1);
            PlayerTimers.Instance.abilityTimers[0] = PlayerTimers.Instance.abilityCD[0];
            Debug.Log("parry");
        }
        MonoInstance.instance.StartCoroutine(EnableShield());
        ChangeColor(new Color(0f / 255f, 255f / 255f, 120f / 255f, 100f / 255f));
        playerhealth.SetDamageModifier(ShieldPercentage);
        playerhealth.SetParrydetectorFalse();
    }

    public override void AbilityUpdate()
    {

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
        MonoInstance.instance.StartCoroutine(ReverseDissolve());
    }
    private IEnumerator Dissolve()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("Before");
        float elapsedTime = 1.2f;
        //0 es invisible 1 es visible
        float currentDissolve = 1.2f;
        Material disolveMaterial = PlayerReferences.instance.shieldObject.GetComponent<MeshRenderer>().sharedMaterial;
        Color previousColor = disolveMaterial.GetColor("_DIssolveEdgeColor");
        disolveMaterial.SetFloat("_Dissolve", currentDissolve);

        while (currentDissolve > -0.3f)
        {
            currentDissolve -= 0.02f;
            disolveMaterial.SetFloat("_Dissolve", currentDissolve);
            Debug.Log("CurrenDisolve " + currentDissolve);
            elapsedTime += Time.deltaTime;
        }
        disolveMaterial.SetFloat("_Dissolve", -0.3f);
        Debug.Log("Hola");
        disolveMaterial.SetColor("_DIssolveEdgeColor", Color.cyan);

    }
    private IEnumerator ReverseDissolve()
    {
        float elapsedTime = 1.2f;
        //0 es invisible 1 es visible
        float currentDissolve = -0.3f;
        Material disolveMaterial = PlayerReferences.instance.shieldObject.GetComponent<MeshRenderer>().sharedMaterial;


        while (currentDissolve < 1.2f)
        {
            currentDissolve += 0.02f;
            disolveMaterial.SetFloat("_Dissolve", currentDissolve);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        disolveMaterial.SetFloat("_Dissolve", 1.2f);
        PlayerReferences.instance.shieldObject.SetActive(false);

    }
}
