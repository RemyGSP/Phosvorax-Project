using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackPosition : MonoBehaviour
{
    [SerializeField] private Transform spawnPos;

    public Vector3 GetFeedbackSpawnPos()
    {
        return spawnPos.position;
    }
}
