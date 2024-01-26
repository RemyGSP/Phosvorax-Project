using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour
{

    public Quaternion Rotate(Quaternion objectCurrentRotation, Vector3 direction)
    {
        Quaternion rotation = Quaternion.Lerp(objectCurrentRotation, Quaternion.LookRotation(direction, Vector3.up), 0.5f);
        rotation.x = 0;
        rotation.z = 0;
        return rotation;
    }

    public Quaternion Rotate(Quaternion objectCurrentRotation, Vector3 direction, float rotationSmoothness)
    {
        Quaternion rotation = Quaternion.Lerp(objectCurrentRotation, Quaternion.LookRotation(direction, Vector3.up), 0.5f);
        rotation.x = 0;
        rotation.z = 0;
        return rotation;
    }

    public Quaternion NonSmoothenedRotation(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        rotation.x = 0;
        rotation.z = 0;
        return rotation;
    }
}
