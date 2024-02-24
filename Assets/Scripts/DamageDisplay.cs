using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageDisplay : MonoBehaviour
{
    [SerializeField] private HealthBehaviour healthBehaviour;
    [SerializeField] private TextMeshProUGUI damageText;
    private float damage;
    private float maxHealth;
    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = healthBehaviour.GetMaxHealth();
        currentHealth = maxHealth;
    }
    public void DisplayDamage(float currentHealth)
    {
        damage = this.currentHealth - currentHealth;
        damageText.text = damage.ToString();
        damageText.gameObject.SetActive(true);
        damageText.gameObject.GetComponent<Animator>().Play("DamageText");
        StartCoroutine(_DeactivateDamageDisplay());
        this.currentHealth = currentHealth;

    }


    private IEnumerator _DeactivateDamageDisplay()
    {
        yield return new WaitForSeconds(damageText.gameObject.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
        damageText.gameObject.SetActive(false);
    }
}
