using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    [SerializeField] private Transform[] pathPoints;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float arrivalThreshold = 0.1f;

    private int currentPointIndex = 0;
    private bool collided;
    private Rigidbody player;
    private bool canUse;
    private void Start()
    {
        canUse = true;
    }
    void Update()
    {
        Debug.Log(collided);
        if (collided)
        {
            if (currentPointIndex >= pathPoints.Length)
            {
                collided = false;
                canUse = false;
            }
            // Calculate the direction towards the current target point
            Vector3 direction = (pathPoints[currentPointIndex].position - transform.position).normalized;
            
            // Move towards the target point
            player.velocity = speed * Time.deltaTime * direction;
            Debug.Log("Distancia " + (Vector3.Distance(player.transform.position, pathPoints[currentPointIndex].position)));
            // Check if the object has reached the current point
            if (Vector3.Distance(player.transform.position, pathPoints[currentPointIndex].position) < arrivalThreshold)
            {
                Debug.Log("Funciona " );
                // Move to the next point in the path
                currentPointIndex++;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (canUse)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                collided = true;
                player = collider.gameObject.GetComponent<Rigidbody>();

            }
        }

    }
}