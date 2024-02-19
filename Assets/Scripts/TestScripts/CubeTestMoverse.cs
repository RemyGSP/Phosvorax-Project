using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTestMoverse : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float shootingInterval = 2f;
    public float projectileForce = 10f;

    void Start()
    {
        StartCoroutine(ShootRoutine());
    }

    IEnumerator ShootRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootingInterval);
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

        if (projectileRb != null)
        {
            projectileRb.AddForce(transform.forward * projectileForce, ForceMode.Impulse);
        }
    }
}
    

