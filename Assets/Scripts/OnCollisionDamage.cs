using UnityEngine;

public class OnCollisionDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter(Collider other)
    {
        HandleCollision(other.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void HandleCollision(GameObject otherGameObject)
    {
        HealthBehaviour healthBehaviour = otherGameObject.GetComponent<HealthBehaviour>();
        if (healthBehaviour != null)
        {
            healthBehaviour.Damage(damage);
        }
    }
}
