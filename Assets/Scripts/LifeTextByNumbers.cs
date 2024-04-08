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
        healthText.text =currentHealth + " / " + healthBehaviour.GetMaxHealth();
    }
}
