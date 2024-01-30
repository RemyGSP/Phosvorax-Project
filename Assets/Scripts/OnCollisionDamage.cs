using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<HealthBehaviour>(out HealthBehaviour healthBehaviour))
        {
            healthBehaviour.Damage(damage);
        }
    }
}
