using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "BossStates/BossJumpfallState")]
public class BossJumpfallState : States
{
    #region Constructor
    public BossJumpfallState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Variables
    [Header("JumpValues")]
    [SerializeField] float timeToJump;
    [SerializeField] int numberOfJumps;
    [SerializeField] float jumpForce;
    private Animator anim;
    [Header("DamageValues")]
    [SerializeField] float sphereSize;
    [SerializeField] float attackDamage;
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] float maxDistanceToJumpToPlayer;
    private Rigidbody rigidBody;
    private Vector3 finalPosition;
    private bool hasExecutedAttack;
    #endregion

    #region Methods
    public override void OnExitState()
    {
        rigidBody.constraints |= RigidbodyConstraints.FreezePositionY;
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
        stateGameObject.GetComponent<BossReferences>().SetCanUseAbility(false);
        base.OnExitState();
    }

    public override void Start()
    {
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        anim = stateGameObject.GetComponent<Animator>();
        anim.SetTrigger("Jump");
        rigidBody.isKinematic = true;
        rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionY;
        stateGameObject.GetComponent<BossTimers>().abilityTimers[2] = 0;
        stateGameObject.GetComponent<GetBestAbilityToUse>().ResetArrays();
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(true);
        finalPosition = stateGameObject.transform.position;
        hasExecutedAttack = false;
        DoJump();
    }

    private void DoJump()
    {
        Vector3 distance = PlayerReferences.instance.GetPlayerCoordinates() - stateGameObject.transform.position;

        if (distance.magnitude >= maxDistanceToJumpToPlayer)
        {
            stateGameObject.transform.DOJump((PlayerReferences.instance.GetPlayerCoordinates()+ stateGameObject.transform.position) / 2, jumpForce, numberOfJumps, timeToJump).OnComplete(() =>
            {
                stateGameObject.GetComponent<BossJumpfallAreaAttack>().ActivateAreaEffect();
                stateGameObject.GetComponent<TraumaInducer>().setCanShakeCam(true);
                rigidBody.isKinematic = false;
                stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
                hasExecutedAttack = true;
            });

        }
        else
        {
            stateGameObject.transform.DOJump(PlayerReferences.instance.GetPlayerCoordinates(), jumpForce, numberOfJumps, timeToJump).OnComplete(() =>
            {
                //Hacer daño
                Vector3 attackPosition = stateGameObject.transform.position;
                Collider[] hitColliders = Physics.OverlapSphere(attackPosition, sphereSize / 2, playerLayerMask, QueryTriggerInteraction.UseGlobal);

                foreach (Collider hitCollider in hitColliders)
                {
                    Debug.Log(hitCollider.tag);
                    if (hitCollider.TryGetComponent<HealthBehaviour>(out HealthBehaviour healthBehaviour))
                    {

                        healthBehaviour.Damage(attackDamage);

                    }
                }
                stateGameObject.GetComponent<TraumaInducer>().setCanShakeCam(true);

                stateGameObject.GetComponent<TraumaInducer>().setCanShakeCam(true);
                rigidBody.isKinematic = false;
                stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
                hasExecutedAttack = true;
            });
        }

       
    }

    public override void Update()
    {
        stateGameObject.GetComponent<TraumaInducer>().setCanShakeCam(true);
    }
    #endregion
}