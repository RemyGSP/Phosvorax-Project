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

    bool isUsingAbility = false;
    public GameObject GetCrystalSpawnPoint()
    {
        Debug.Log(cristalSpawnPoint);
        return cristalSpawnPoint;
    }

    public GameObject GetCrystalPrefab()
    {
        Debug.Log(crystalPrefab);
        return crystalPrefab;
    }

    public GameObject GetEnemyToSpawn()
    {
        Debug.Log(enemyToSpawn);
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

    public void SetCanUseAbility(bool aux)
    {
        canUseAbility = aux;
    }

    public void SetIsUsingAbiliy(bool aux)
    {
        isUsingAbility = aux;
    }
    public bool GetIsUsingAbiliy()
    {
        return isUsingAbility;
    }
}
