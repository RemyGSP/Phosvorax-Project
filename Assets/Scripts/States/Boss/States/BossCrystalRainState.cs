using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "BossStates/BossCrystalRainState")]
public class BossCrystalRainState : States
{

    #region Constructor
    public BossCrystalRainState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion


    #region Variables
    [SerializeField] LayerMask playerLayerMask;
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private Animator anim;
    private Animator animator;
    private GameObject crystalPrefab;


    [Header("Time")]
    [SerializeField] float timeToBeSpawning;
    private float currentTime;

    [Header("Spawn Values")]
    [SerializeField] float spawnInterval;
    private GameObject crystalSpawnPoint;
    private NavMeshAgent enemy;
    #endregion

    #region Methods

    public override States CheckTransitions()
    {
        return base.CheckTransitions();
    }

    public override void Start()
    {
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(true);
        base.Start();
        crystalPrefab = stateGameObject.GetComponent<BossReferences>().GetCrystalPrefab();
        crystalSpawnPoint = stateGameObject.GetComponent<BossReferences>().GetCrystalSpawnPoint();
        enemy.speed = 0;
        currentTime = 0;
        rigidBody.mass = Mathf.Infinity;
        ActivateFireAura();
        MonoInstance.instance.StartCoroutine(SpawnCrystalRoutine());
        Debug.Log("hfabfafca");

    }

    public override void Update()
    {
        currentTime += Time.deltaTime;


        if (currentTime >= timeToBeSpawning)
        {
            StopSpawningCrystals();
        }

        crystalSpawnPoint.transform.position = new Vector3(PlayerReferences.instance.GetPlayerCoordinates().x, crystalSpawnPoint.transform.position.y, PlayerReferences.instance.GetPlayerCoordinates().z);
    }

    IEnumerator SpawnCrystalRoutine()
    {
        Debug.Log("hfabfafca");
        while (true)
        {
            
            Instantiate(crystalPrefab, crystalSpawnPoint.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    public void StopSpawningCrystals()
    {
        MonoInstance.instance.StopCoroutine(SpawnCrystalRoutine());
    }

    public void ActivateFireAura()
    {

    }


    public override void OnExitState()
    {
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
        stateGameObject.GetComponent<BossReferences>().SetCanUseAbility(false);
        base.OnExitState();
    }
    #endregion
}

