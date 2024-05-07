using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossActivate2ndPhase : MonoBehaviour
{
    [Header("Seconds to subtract to cooldowns")]
    [SerializeField] private float secondsToSubtract;
    private float  health;
    private float currentHealth;
    [SerializeField] private GameObject exclamation;
    [Header("Fire Aura Game Objects")]
    [SerializeField] GameObject[] fireAura;
    private bool hasActivated2ndPhase;
    [SerializeField] private GameObject feedbackPos;
    private void Start()
    {
        hasActivated2ndPhase = false;
        health = this.gameObject.GetComponent<HealthBehaviour>().GetMaxHealth();
        currentHealth = this.gameObject.GetComponent<HealthBehaviour>().GetCurrentHealth();
    }

    private void Update()
    {
        if (gameObject.GetComponent<HealthBehaviour>().HasBeenHit())
        {
            currentHealth = gameObject.GetComponent<HealthBehaviour>().GetCurrentHealth();
        }
        

        if (!hasActivated2ndPhase)
        {
            if (health / 2 >= currentHealth)
            {
                Activate2ndPhase();
                ActivateFireAura();
                hasActivated2ndPhase = true;
            }
        }
        else
        {
            gameObject.GetComponent<BossReferences>().SetCanUseAbility(true);
        }
        
    }

    private void Activate2ndPhase()
    {
        GameObject gameObject = Instantiate(exclamation,feedbackPos.transform.position, Quaternion.identity);
        gameObject.GetComponent<FollowPlayer>().SetPlayer(this.gameObject);
        gameObject.GetComponent<FollowPlayer>().SetOffset(new Vector3(0,feedbackPos.transform.position.y,0));

        float[] cooldownArray = this.gameObject.GetComponent<BossTimers>().GetCooldownArray();
        for (int i = 0; i < cooldownArray.Length; i++)
        {
            cooldownArray[i] -= secondsToSubtract;
        }
    }

    private void ActivateFireAura()
    {
        for(int i = 0; i < fireAura.Length; i++)
        {
            fireAura[i].SetActive(true);
        }
    }
}
