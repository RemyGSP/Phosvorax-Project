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

    void Update()
    {
        if (collided)
        {

            if (pathPoints.Length == 0)
                return;

            // Calculate the direction towards the current target point
            Vector3 direction = (pathPoints[currentPointIndex].position - transform.position).normalized;
            Debug.Log(direction);
            // Move towards the target point
            player.velocity = speed * Time.deltaTime * direction;

            // Check if the object has reached the current point
            if (Vector3.Distance(transform.position, pathPoints[currentPointIndex].position) < arrivalThreshold)
            {
                // Move to the next point in the path
                currentPointIndex = (currentPointIndex + 1) % pathPoints.Length;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            collided = true;
            player = collider.gameObject.GetComponent<Rigidbody>();

        }
    }
}