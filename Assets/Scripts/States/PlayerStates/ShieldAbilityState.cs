using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/SecondAbilityState")]
public class ShieldAbilityState : Ability
{
    private Rigidbody rigidBody;
    [SerializeField] private float parryLength;
    [SerializeField] private float shieldLength;
    float currentParryTime;
    private HealthBehaviour playerhealth;
    PlayerTimers instanciaPlayerTimers;
    [SerializeField] private float ShieldPercentage;



    public ShieldAbilityState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    public override States CheckTransitions()
    {
        States newGameState = null;
        if (currentParryTime > parryLength)
        {
            bool notChanged = true;
            int counter = 0;
            AfterParry();

            while (notChanged)
            {
                newGameState = stateTransitions[counter].GetExitState();
                if (newGameState != null)
                {
                    notChanged = false;
                    newGameState.InitializeState(stateGameObject);
                    newGameState.Start();
                    rigidBody.velocity = Vector3.zero;
                }
                if (counter < stateTransitions.Length - 1)
                {
                    counter++;
                }
                else
                {
                    notChanged = false;
                }
            }
        }
            
        return newGameState;
    }
    void ExecuteParry()
    {
        playerhealth.SetDamageModifier(0);
        PlayerReferences.instance.shieldObject.SetActive(true);
        ChangeColor(new Color(255f / 255f, 255f / 255f, 255f / 255f, 50f / 255f));

    }
    void AfterParry()
    {
        if (playerhealth.GetParrydetector())
        {
            playerhealth.SetDamageModifier(1);  
            instanciaPlayerTimers.abilityTimers[1] = instanciaPlayerTimers.abilityCD[1];
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

    void ChangeColor(Color newColor)
    {
        // Establece el color del material al nuevo color
        PlayerReferences.instance.shieldObjectRenderer.material.color = newColor;
    }
    public override void Start() {
        AbilityManager.instance.CastedAbility();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        playerhealth = stateGameObject.GetComponent<HealthBehaviour>();
        instanciaPlayerTimers = PlayerTimers.Instance;
        currentParryTime = 0;

        ExecuteParry();
    }
    public override void FixedUpdate()
    {
        currentParryTime += Time.deltaTime;
    }
    public override void Update()
    {
        return;
    }

    IEnumerator EnableShield()
    {
        yield return new WaitForSeconds(shieldLength);
        playerhealth.SetDamageModifier(1);
        PlayerReferences.instance.shieldObject.SetActive(false);
    }
}
