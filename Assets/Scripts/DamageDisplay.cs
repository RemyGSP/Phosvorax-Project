using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class DamageDisplay : MonoBehaviour
{

    [SerializeField] private HealthBehaviour healthBehaviour;
    [SerializeField] private GameObject[] damagePrefabs;
    private float damage;
    private float maxHealth;
    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = healthBehaviour.GetMaxHealth();
        currentHealth = maxHealth;
    }

    [CustomEditor(typeof(DamageDisplay))]
    public void DisplayDamage(float currentHealth)
    {
        damage = this.currentHealth - currentHealth;

        GameObject b = Instantiate(damagePrefabs[(int)Random.Range(0,3)]);
        b.GetComponent<RectTransform>().sizeDelta = new Vector2(1,1);
        b.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
        b.transform.position = transform.position;
        MonoInstance.instance.StartCoroutine(_DeactivateDamageDisplay(b.GetComponentInChildren<Animator>()));
        this.currentHealth = currentHealth;
        if (currentHealth == 0)
        {
            ResetHP();
        }

    }

    public void ResetHP()
    {
        currentHealth = maxHealth;
    }


    private IEnumerator _DeactivateDamageDisplay(Animator animator)
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0).Length);
        Destroy(animator.transform.parent.gameObject);
    }
}
