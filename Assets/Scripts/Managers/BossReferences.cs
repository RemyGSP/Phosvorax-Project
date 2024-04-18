using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossReferences : MonoBehaviour
{
    [Header( "Time to use abilities again")]
    [SerializeField] private float minTime;
    [SerializeField] private float maxTime;
    [SerializeField]  private float currentTime;
    [SerializeField] bool canUseAbility;
    private float time;


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

    private void Start()
    {
       time = SetTime();
    }
    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= time)
        {
            canUseAbility = true;
            currentTime = 0;
            SetTime();
        }
        else
        {
            canUseAbility = false;
        }
    }

    public bool GetCanUseAbility()
    {
        return canUseAbility;
    }

    private float SetTime()
    {
       float aux = Random.Range(minTime, maxTime);
        return aux;
    }
}
