using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "BossStates/BossJumpfallState")]
public class BossJumpfallState : States
{
    #region Constructor
    public BossJumpfallState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    #endregion

    #region Variables
    [SerializeField] float jumpForce;
    [SerializeField] float timeFalling;

    private float startingMass;
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
        rigidBody.isKinematic = true;
        rigidBody.constraints &= ~RigidbodyConstraints.FreezePositionY;
        stateGameObject.GetComponent<BossTimers>().abilityTimers[2] = 0;
        stateGameObject.GetComponent<GetBestAbilityToUse>().ResetArrays();
        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(true);
        finalPosition = stateGameObject.transform.position;
        hasExecutedAttack = false;
        startingMass = rigidBody.mass;
        Debug.Log("Salta");
        DoJump();
    }

    private void DoJump()
    {
        stateGameObject.transform.DOJump(stateGameObject.transform.position, jumpForce, 1, 3.0f).OnComplete(() =>
        {
            rigidBody.isKinematic = false;
            stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
            hasExecutedAttack = true;
        });
    }

    //public override void FixedUpdate()
    //{
    //    Fall();
    //}

    //private void Jump()
    //{
    //    rigidBody = stateGameObject.GetComponent<Rigidbody>();
    //    rigidBody.mass = 0.1f;
    //    // Aplica una fuerza al Rigidbody solo en el eje Y para que el jefe salte
    //    rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    //}


    //private void Fall()
    //{
    //    // Calcula el desplazamiento hacia abajo
    //    float step = timeFalling * Time.deltaTime;
    //    Vector3 newPosition = Vector3.MoveTowards(stateGameObject.transform.position, finalPosition, step);

    //    // Actualiza la posición del jefe
    //    stateGameObject.transform.position = newPosition;

    //    // Si el jefe ha llegado a la posición final
    //    if (stateGameObject.transform.position.y <= finalPosition.y)
    //    {
    //        // El jefe ha terminado de caer
    //        stateGameObject.GetComponent<BossReferences>().SetIsUsingAbiliy(false);
    //        hasExecutedAttack = true;
    //    }
    //}

    public override void Update()
    {

    }
    #endregion
}