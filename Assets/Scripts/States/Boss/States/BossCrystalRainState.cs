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
    private bool spawningCrystals = false;

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
        stateGameObject.GetComponent<GetBestAbilityToUse>().ResetArrays();
        spawningCrystals = true;
        enemy = stateGameObject.GetComponent<NavMeshAgent>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(true);
        base.Start();
        crystalPrefab = stateGameObject.GetComponent<BossReferences>().GetCrystalPrefab();
        crystalSpawnPoint = stateGameObject.GetComponent<BossReferences>().GetCrystalSpawnPoint();
        enemy.speed = 0;
        currentTime = 0;
        rigidBody.mass = Mathf.Infinity;
        ActivateFireAura();
        MonoInstance.instance.StartCoroutine(SpawnCrystalRoutine());

    }

    public override void Update()
    {
        currentTime += Time.deltaTime;


        if (currentTime >= timeToBeSpawning)
        {
            spawningCrystals = false;
            stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
            StopSpawningCrystals();
        }

        crystalSpawnPoint.transform.position = new Vector3(PlayerReferences.instance.GetPlayerCoordinates().x, crystalSpawnPoint.transform.position.y, PlayerReferences.instance.GetPlayerCoordinates().z);
    }

    IEnumerator SpawnCrystalRoutine()
    {
        while (spawningCrystals)
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

