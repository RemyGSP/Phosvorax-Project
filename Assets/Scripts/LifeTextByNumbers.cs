using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeTextByNumbers : MonoBehaviour
{
    [SerializeField] private HealthBehaviour healthBehaviour;
    [SerializeField] private TextMeshProUGUI healthText;
    void Start()
    {
        ChangeText(healthBehaviour.GetMaxHealth());
    }

    void Update()
    {
        
    }

    public void ChangeText(float currentHealth)
    {
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthText.GetComponent<Animator>().SetTrigger("hit");
        healthText.text = currentHealth + " / " + healthBehaviour.GetMaxHealth();

    }
}
