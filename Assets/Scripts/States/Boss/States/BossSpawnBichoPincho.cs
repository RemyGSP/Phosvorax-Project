using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "BossStates/BossSpawnEnemies")]
public class BossSpawnBichoPincho : States
{

    #region Constructor
    public BossSpawnBichoPincho(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Variables
    private GameObject prefabToSpawn;
    [SerializeField] private int numberOfSpawns = 2; // Número de veces que se spawneará el prefab
    public float spawnRadius = 5f; // Radio alrededor del enemigo para spawnear el objeto

    private Transform enemyTransform;

    #endregion
    public override void Start()
    {
        stateGameObject.GetComponent<Animator>().SetBool("rain", true);
        stateGameObject.GetComponent<BossTimers>().abilityTimers[0] = 0;
        stateGameObject.GetComponent<GetBestAbilityToUse>().ResetArrays();
        prefabToSpawn = stateGameObject.GetComponent<BossReferences>().GetEnemyToSpawn();
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(true);
        // Obtener la referencia al transform del enemigo
        enemyTransform = stateGameObject.transform;
        // Spawnear el prefab varias veces
        for (int i = 0; i < numberOfSpawns; i++)
        {
            SpawnPrefab();
            if (i == 1)
            {
                stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
            }
        }
    }


    public override void Update()
    {

    }
    void SpawnPrefab()
    {
        // Calcular una posición aleatoria dentro del radio especificado
        Vector3 spawnPosition = enemyTransform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 15f;

        // Instanciar el prefab en la posición calculada
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

    public override void OnExitState()
    {
        stateGameObject.GetComponent<Animator>().SetBool("rain", false);
        stateGameObject.GetComponent<BossReferences>().ResetCurrentTime();
        stateGameObject.GetComponent<BossReferences>().SetCanUseAbility(false);
        base.OnExitState();
    }
}
