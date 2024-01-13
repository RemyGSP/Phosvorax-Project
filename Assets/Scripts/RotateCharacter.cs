using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour
{

    public Quaternion Rotate(Vector3 playerDirection, Vector3 direction)
    {
        Vector3 relative = (playerDirection + direction) - playerDirection;
        Quaternion rotation = Quaternion.LookRotation(relative, Vector3.up);
        return rotation;
    }
}
