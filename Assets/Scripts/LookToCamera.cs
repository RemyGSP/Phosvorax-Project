using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookToCamera : MonoBehaviour
{

    private Vector3 desiredRotation = new Vector3(35f, 45f, 0f);
    [SerializeField]private  Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        Vector3 finalRotation = desiredRotation + offset;
        transform.rotation = Quaternion.Euler(finalRotation);

    }
}
