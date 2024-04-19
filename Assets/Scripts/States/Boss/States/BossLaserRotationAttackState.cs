using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[CreateAssetMenu(menuName = "BossStates/BossLaserRotatingAttackState")]

public class BossLaserRotationAttackState : States
{

    #region Constructor
    public BossLaserRotationAttackState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Variables
    [SerializeField] LayerMask playerLayerMask;
    private RotateCharacter rotateCharacter;
    private Rigidbody rigidBody;
    private Animator anim;

    private bool hasFinishedAttack;
    [Header("Rotation")]
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 rotationAxis = Vector3.left;


    [Header("Time")]
    [SerializeField] float attackTime;
    private float currentTime;
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
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(true);
        hasFinishedAttack = false;
        enemy.speed = 1;
        base.Start();
        currentTime = 0;

    }

    public override void Update()
    {
        currentTime += Time.deltaTime;
        Vector3 playerPos = PlayerReferences.instance.GetPlayerCoordinates();

        if (currentTime >= attackTime)
        {

            enemy.SetDestination(playerPos);
            stateGameObject.transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);

        }
        else
        {
            stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
            hasFinishedAttack = true;
        }

    }

    public override void OnExitState()
    {
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
        stateGameObject.GetComponent<BossReferences>().SetCanUseAbility(false);
        base.OnExitState();
    }
    #endregion
}


