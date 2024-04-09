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

    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(_StartMoving());
        StartCoroutine(_Delete());
        canStartMoving = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canStartMoving && playerInRange)
        {
            // Increment elapsed time
            elapsedTime += Time.fixedDeltaTime;

            // Evaluate speed using animation curve
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
                cryst.AddCrystals(crystalValue);
                Destroy(this.gameObject);
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
            Destroy(this.gameObject);
        }
    }
}