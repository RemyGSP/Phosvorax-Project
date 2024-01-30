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
    public UnityEvent<float> onHit;
    public UnityEvent onDeath;
    public UnityEvent onRevive;

    private void Start()
    {
        currentHealth =maxHealth;
    }
    public bool Damage(float damage)
    {
        bool aux = false;
        if (currentHealth > minHealth)
        {
            currentHealth -= damage;
            onHit.Invoke(currentHealth);
            CheckIdDead();
            aux = true;
        }
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


    public bool CheckIdDead()
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
