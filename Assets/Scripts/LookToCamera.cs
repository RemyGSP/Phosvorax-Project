using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {

        Vector3 directionToCamera = Camera.main.transform.position - transform.position;

        // Undo the rotation of the camera
        Quaternion inverseRotation = Quaternion.Euler(-Camera.main.transform.rotation.eulerAngles);

        // Rotate the direction vector based on the inverse camera rotation
        Vector3 adjustedDirection = inverseRotation * directionToCamera;

        // Calculate the target position for LookAt
        Vector3 targetPosition = transform.position + adjustedDirection;

        // Make the object look at the modified target position
        transform.LookAt(targetPosition);

    }
}
