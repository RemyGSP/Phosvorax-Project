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
        // Obtener la referencia al transform del enemigo
        enemyTransform = stateGameObject.transform;

        // Spawnear el prefab varias veces
        for (int i = 0; i < numberOfSpawns; i++)
        {
            SpawnPrefab();
        }
    }


    public override void Update()
    {

    }
    void SpawnPrefab()
    {
        // Calcular una posición aleatoria dentro del radio especificado
        Vector3 spawnPosition = enemyTransform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.y = 0;

        // Instanciar el prefab en la posición calculada
        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }

    /*float GetGroundHeight(Vector3 position)
    {
        // Raycast desde arriba hacia abajo para encontrar la altura del suelo en la posición especificada
        RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            return hit.point.y;
        }
        return 0f; // Si no se encuentra el suelo, devolver 0 como altura
    }*/

    //EN CASO DE QUE NO VAYA BIEN SPAWNEAR EN Y 0

    public override void OnExitState()
    {
        base.OnExitState();
    }
}
