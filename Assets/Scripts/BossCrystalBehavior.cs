using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCrystalBehavior : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float timeToDestroy;
    [SerializeField] Rigidbody rb;
    [SerializeField] float downForce;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthBehaviour>().Damage(damage);
        }
        if (other.CompareTag("BossRoom"))
        {
            StartCoroutine(DestroyAfterDelay());
        }
    }

    private IEnumerator DestroyAfterDelay()
    {

        yield return new WaitForSeconds(timeToDestroy);

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * downForce, ForceMode.Impulse);
    }
}
