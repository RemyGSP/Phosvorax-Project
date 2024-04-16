using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "EnemyStates/EntryState")]
public class EnemyEntryState : States
{
    #region Constructor

    public EnemyEntryState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    #endregion

    #region Variables

    [SerializeField] private float timeToStartEnemies;
    private float currentElapsedTime;
    private bool canStartCountdown;
    [SerializeField] private GameObject spawnParticles;
    GameObject particles = null;
    #endregion

    #region Methods

    public override void Start()
    {
        currentElapsedTime = 0f;
        canStartCountdown = false;
    }

    public override void Update()
    {
        if(canStartCountdown)
        {
            if (particles == null)
            {
                particles = Instantiate(spawnParticles, stateGameObject.transform.position, Quaternion.identity);
                ParticleSystem.MainModule module = particles.GetComponent<ParticleSystem>().main;
                module.startLifetime = timeToStartEnemies;
                module.duration = timeToStartEnemies;

            }
            currentElapsedTime += Time.deltaTime;

            if (currentElapsedTime >= timeToStartEnemies)
            {
                Debug.Log(stateGameObject.GetComponent<Collider>() + "Ha sido activado");
                Destroy(particles);
                stateGameObject.GetComponent<EnemyReferences>().SetCanBeStarted(true);
            }
        }
        else
        {
            Vector3 playerPos = PlayerReferences.instance.GetPlayerCoordinates();
            float distanceToStartCountdown = stateGameObject.GetComponent<EnemyReferences>().GetDistanceToStartCountdown();
            float totalDistance = Vector3.Distance(playerPos, stateGameObject.transform.position);

            if(totalDistance <= distanceToStartCountdown) 
            {
                canStartCountdown=true;
            }

        }

    }

    public override void OnExitState()
    {
    }
    #endregion
}
