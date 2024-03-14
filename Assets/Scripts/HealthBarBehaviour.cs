using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehaviour : MonoBehaviour
{
    [SerializeField] private HealthBehaviour healthBehaviour;
    [SerializeField] private float fillSpeed;
    [SerializeField] private TextMeshProUGUI damageText;

    private float damage;
    private float maxHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image delayBar;
    private float currentHealth;
    private float targetFillAmount;
    private float delayTargetFillAmount;
    void Start()
    {
        maxHealth = healthBehaviour.GetMaxHealth();
        healthBar.fillAmount = 1;
        targetFillAmount = 1;
        delayTargetFillAmount = 1;
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateHealthBar(float currentHealth)
    {
        damage = this.currentHealth - currentHealth;
        targetFillAmount = currentHealth / maxHealth;
        delayTargetFillAmount = currentHealth / maxHealth;
        this.currentHealth = currentHealth;
        // Start the fill animation coroutine
        StartCoroutine(_FillHealthBar());
        StartCoroutine(_FillDelayBar());
    }



    public void RestartHealthBar()
    {
        targetFillAmount = 1;
        StartCoroutine(_FillHealthBar());
    }
    private IEnumerator _FillHealthBar()
    {
        // Smoothly transition from the current fill amount to the target fill amount
        while (Mathf.Abs(healthBar.fillAmount - targetFillAmount) > 0.01f)
        {
            // Use Mathf.Lerp to interpolate between the current fill amount and the target fill amount
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, targetFillAmount, Time.deltaTime * fillSpeed);

            yield return null; // Wait for the end of frame before continuing the loop
        }

        // Ensure the health bar reaches exactly the target fill amount
        healthBar.fillAmount = targetFillAmount;
    }

    private IEnumerator _FillDelayBar()
    {
        yield return new WaitForSeconds(0.2f);
        // Smoothly transition from the current fill amount to the target fill amount
        while (Mathf.Abs(delayBar.fillAmount - delayTargetFillAmount) > 0.01f)
        {
            // Use Mathf.Lerp to interpolate between the current fill amount and the target fill amount
            delayBar.fillAmount = Mathf.Lerp(delayBar.fillAmount, delayTargetFillAmount, Time.deltaTime * fillSpeed);

            yield return null; // Wait for the end of frame before continuing the loop
        }

        // Ensure the health bar reaches exactly the target fill amount
        delayBar.fillAmount = delayTargetFillAmount;
    }
}
