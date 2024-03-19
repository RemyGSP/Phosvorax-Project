using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerOnDamageState")]
public class OnDamageState : States
{
    [SerializeField] private float invulnerabilityTime;
    public AnimationCurve blinkCurve; // Curva de animación para el parpadeo
    private bool isBlinking = false;
    private HealthBehaviour playerhealth;

    public OnDamageState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    
    public override void Start()
    {
        playerhealth = stateGameObject.GetComponent<HealthBehaviour>();
        MonoInstance.instance.StartCoroutine(InvulnerabilityCorutine());
    }

    public override void FixedUpdate()
    {
   
    }

    public override void OnExitState()
    {
        
    }

    public override void Update()
    {
        
    }

    IEnumerator InvulnerabilityCorutine()
    {
        isBlinking = true;
        float timer = 0f;
        playerhealth.SetDamageModifier(0);
        while (timer < invulnerabilityTime)
        {
            float blinkValue = blinkCurve.Evaluate(timer / invulnerabilityTime);

            // Cambia la visibilidad del jugador
            PlayerReferences.instance.playerRenderer.enabled = !PlayerReferences.instance.playerRenderer.enabled;

            yield return null;
            timer += Time.deltaTime;

        }

        PlayerReferences.instance.playerRenderer.enabled = true; // Asegúrate de que el jugador esté visible al final del parpadeo
        playerhealth.SetDamageModifier(1);
        isBlinking = false;
    }

}
