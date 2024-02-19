using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [Header("Variables")]
    //Esto podria ser un int
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private float minHealth;
    [SerializeField] private float damageModifier;
    public UnityEvent<float> onHit;
    public UnityEvent onDeath;
    public UnityEvent onRevive;
    [SerializeField] private bool parryDamageDetector;
   
    private void Start()
    {
        currentHealth = maxHealth;
        damageModifier = 1;
        SetParrydetectorFalse();


    }
    public void SetDamageModifier(float newdamageModifier)
    {
        damageModifier = newdamageModifier;
    }
    public void SetParrydetectorFalse()
    {
        parryDamageDetector = false;
    }
    public void SetParrydetectorTrue()
    {
        parryDamageDetector = true;
    }
    public bool GetParrydetector()
    {
        return parryDamageDetector;
    }

    public bool Damage(float damage)
    {
        bool aux = false;
        if (currentHealth > minHealth)
        {
            float modifiedDamage = damage * damageModifier;
            currentHealth -= modifiedDamage;
            onHit.Invoke(currentHealth);
            CheckIfDeath();
            aux = true;
        }
        SetParrydetectorTrue();
        return aux;
    }

    public bool Heal(float healAmount)
    {
        bool aux = false;
        if (currentHealth > minHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + healAmount, minHealth, maxHealth);
            aux = true;
        }
        return aux;
    }


    public bool CheckIfDeath()
    {
        bool aux = false;
        if (currentHealth <= minHealth)
        {
            aux = true;
            onDeath.Invoke();
        }
        return aux;
    }

        public void Revive()
    {
        currentHealth = maxHealth;
        onRevive.Invoke();
    }

    public void Revive(float newHealth)
    {
        currentHealth = newHealth;
        onRevive.Invoke();
    }
}
