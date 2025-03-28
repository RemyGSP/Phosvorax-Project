using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class BulletBehaviour : MonoBehaviour
{
    private float lifetime;
    [SerializeField] private float damage;
    private float timer;
    private void Start()
    {
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void Update()
    {

        if (lifetime != 0)
        {
            timer += Time.deltaTime;
            if (timer > lifetime)
            {
                Destroy(gameObject);
            }
        }
    }

    // Update is called once per frame

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (other.gameObject.TryGetComponent<HealthBehaviour>(out HealthBehaviour healthBehaviour))
        {

            healthBehaviour.Damage(damage);
        }
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetLifetime(float lifeTime)
    {
        lifetime = lifeTime;
    }
}
