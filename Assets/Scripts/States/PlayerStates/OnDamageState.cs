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
        playerHealth = stateGameObject.GetComponent<HealthBehaviour>();
        playerRenderer = PlayerReferences.instance.playerRenderer;
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
        isBlinking = true;
        playerHealth.SetDamageModifier(0);

        float blinkInterval = invulnerabilityTime / (blinkCount * 2); // Calculamos el intervalo entre parpadeos

        for (int i = 0; i < blinkCount; i++)
        {
            // Cambiar la visibilidad del jugador
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }

        // Asegúrate de que el jugador esté visible al final del parpadeo
        playerRenderer.enabled = true;
        playerHealth.SetDamageModifier(1);
        isBlinking = false;
    }
}
