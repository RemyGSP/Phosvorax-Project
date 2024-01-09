using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour
{

    public Quaternion Rotate(Quaternion objectCurrentRotation, Vector3 direction)
    {
        Vector3 relative = direction;
        Quaternion rotation = Quaternion.Lerp(objectCurrentRotation,Quaternion.LookRotation(relative, Vector3.up), 0.5f);
        return rotation;
    }

    public Quaternion Rotate(Quaternion objectCurrentRotation, Vector3 direction, float rotationSmoothness)
    {
        Vector3 relative = direction;
        Quaternion rotation = Quaternion.Lerp(objectCurrentRotation, Quaternion.LookRotation(relative, Vector3.up), rotationSmoothness);
        return rotation;
    }
}
