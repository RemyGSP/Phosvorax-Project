using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReferences : MonoBehaviour
{
    [Header("Crystal & SpawnPoint")]
    [SerializeField] private GameObject cristalSpawnPoint;
    [SerializeField] private GameObject crystalPrefab;

    public GameObject GetCrystalSpawnPoint()
    { 
        return cristalSpawnPoint;
    }

    public GameObject GetCrystalPrefab()
    { 
        return crystalPrefab;
    }

}
