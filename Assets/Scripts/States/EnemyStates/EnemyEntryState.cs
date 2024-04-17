using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.CompilerServices;
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
    [SerializeField] private bool dissolves;
    GameObject particles = null;
    private float dissolveDuration;
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
                if (dissolves)
                {
                    MonoInstance.instance.StartCoroutine(Dissolve(stateGameObject.GetComponent<EnemyReferences>().GetEnemy()));
                }
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

    private IEnumerator Dissolve(GameObject dissolveObject)
    {
        float elapsedTime = 0;
        //0 es invisible 1 es visible
        float currentDissolve = 0;
        Material disolveMaterial = dissolveObject.GetComponent<EnemyReferences>().GetVisuals().GetComponent<SkinnedMeshRenderer>().sharedMaterial;


        while (currentDissolve < 1)
        {
            currentDissolve += 0.005f;
            disolveMaterial.SetFloat("_Dissolve", currentDissolve);

            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

    }
    public override void OnExitState()
    {
    }
    #endregion
}
