using System;
using System.Collections;
using UnityEngine;

public class CrystalBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    private int crystalValue;
    [SerializeField] private float maxSpeed = 1000;
    [SerializeField] private AnimationCurve speedCurve;
    [SerializeField] private float startMovingDelay;
    private bool canStartMoving;
    private bool playerInRange;


    [Header("Healing Values")]
    [SerializeField] private bool isHealingCrystal;
    [SerializeField] private float healingAmount;

    private float elapsedTime = 0f;

    void Start()
    {
        StartCoroutine(_StartMoving());
        StartCoroutine(_Delete());
        canStartMoving = false;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (canStartMoving && playerInRange)
        {
            elapsedTime += Time.fixedDeltaTime;
            float curveValue = speedCurve.Evaluate(elapsedTime);
            float currentSpeed = curveValue * maxSpeed;

            Vector3 directionToPlayer = (PlayerReferences.instance.GetPlayerVisiblePoint() - transform.position).normalized;
            rb.velocity = (directionToPlayer * currentSpeed);
        }
        else
        {
            rb.AddForce(new Vector3(0f, -300f, 0f), ForceMode.Force);
        }
    }

    public void SetCrystalValue(int crystalValue)
    {
        this.crystalValue = crystalValue;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.TryGetComponent(out CrystalController cryst))
            {
                if (!isHealingCrystal)
                {
                    cryst.AddCrystals(crystalValue);
                }
                else
                {
                    collision.gameObject.GetComponent<HealthBehaviour>().Heal((int)healingAmount);
                }

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private IEnumerator _StartMoving()
    {
        yield return new WaitForSeconds(startMovingDelay);
        canStartMoving = true;
    }

    private IEnumerator _Delete()
    {
        yield return new WaitForSeconds(60f);
        if (this != null)
        {
            Destroy(gameObject);
        }
    }
}
