using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnHit : MonoBehaviour
{
    private bool hasDoneDamage;
    private float damage;

    private void Start()
    {
        hasDoneDamage = false;
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public float GetDamage()
    {
        return this.damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<HealthBehaviour>(out HealthBehaviour healthBehaviour) && !hasDoneDamage)
        {
            healthBehaviour.Damage(damage);
            hasDoneDamage = true;
        }
    }
}
