using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerOnDamageState")]
public class OnDamageState : States
{
    [SerializeField] private float invulnerabilityTime;
    [SerializeField] private int blinkCount = 5; // Número de parpadeos
    private bool isBlinking = false;
    private HealthBehaviour playerHealth;
    private Renderer playerRenderer;

    public OnDamageState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override void Start()
    {
        AudioManager.Instance.CallOneShot("event:/PlayerHit");
        playerHealth = stateGameObject.GetComponent<HealthBehaviour>();
        stateGameObject.GetComponent<OnHitColorFeedback>().PlayHitFeedback(invulnerabilityTime);
        //playerRenderer = PlayerReferences.instance.playerRenderer;
        MonoInstance.instance.StartCoroutine(InvulnerabilityCoroutine());
    }

    public override void FixedUpdate()
    {
    }

    public override void OnExitState()
    {
        base.OnExitState();

    }

    public override void Update()
    {
    }

    IEnumerator InvulnerabilityCoroutine()
    {
        playerHealth.SetDamageModifier(0);



            yield return new WaitForSeconds(invulnerabilityTime);
        

        playerHealth.SetDamageModifier(1);
    }
}
