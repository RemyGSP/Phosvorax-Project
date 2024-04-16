using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReferences : MonoBehaviour
{
    [Header("Crystal & SpawnPoint")]
    [SerializeField] private GameObject cristalSpawnPoint;
    [SerializeField] private GameObject crystalPrefab;

    [Header("SpawnEnemyAbility")]
    [SerializeField] private GameObject enemyToSpawn;
    public GameObject GetCrystalSpawnPoint()
    { 
        return cristalSpawnPoint;
    }

    public GameObject GetCrystalPrefab()
    { 
        return crystalPrefab;
    }

    public GameObject GetEnemyToSpawn()
    {
        return enemyToSpawn;
    }

}
