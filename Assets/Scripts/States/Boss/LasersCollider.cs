using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasersCollider : MonoBehaviour
{
    [SerializeField] private float damage;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthBehaviour>().Damage(damage);
        }
    }
}
