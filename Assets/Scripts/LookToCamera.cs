using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{

    private Vector3 desiredRotation = new Vector3(35f, 45f, 0f);

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(desiredRotation);

    }
}
