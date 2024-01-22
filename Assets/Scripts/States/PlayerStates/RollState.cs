using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

[CreateAssetMenu(menuName = "States/PlayerRollState")]
public class RollState : States
{
    [Header("States")]
    [SerializeField] private States idleState;
    [SerializeField] private States moveState;
    #region Variables
    private Rigidbody rigidBody;
    [Header("Dash Variables")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;
    private Vector3 dashFinalPos;
    private Animator animator;
    private Vector3 playerDirection;
    private float currentDashTime = 0f;
    #endregion

    #region Methods
    public RollState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    public override void Start()
    {
        currentDashTime = 0f;
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        if (stateGameObject.TryGetComponent<Animator>(out Animator objectAnimator))
        {
            animator = objectAnimator;
            animator.SetTrigger("roll");
        }
        playerDirection = PlayerInputController.Instance.GetPlayerInputDirection();
    }

 
    public override States CheckTransitions()
    {
        States newPlayerState = null;

        bool notChanged = true;
        int counter = 0;

        while (notChanged)
        {
            newPlayerState = stateTransitions[counter].GetExitState();
            if (newPlayerState != null)
            {
                notChanged = false;
                newPlayerState.InitializeState(stateGameObject);
                newPlayerState.Start();
                rigidBody.velocity = Vector3.zero;
                animator.SetBool("dashing", false);
            }
            if (counter < stateTransitions.Length - 1)
            {
                counter++;
            }
            else
            {
                notChanged = false;
            }
        }
        return newPlayerState;
    }

    public override void FixedUpdate()
    {
        if (dashTime >= currentDashTime)
        {
            Dash();
            currentDashTime += Time.deltaTime;
        }
        else
        {
            PlayerTimers.timer.rollTimer = 0;
        }
    }

    public void Dash()
    {
        if (playerDirection != Vector3.zero)
        {
            rigidBody.AddForce(playerDirection * dashForce * Time.deltaTime, ForceMode.Impulse);
        }
        //Esto es por si el usuario no tienes ninguna direccion pulsada, asi que pillara la rotacion actual del personaje
        else
            rigidBody.AddForce(stateGameObject.transform.rotation * Vector3.forward * dashForce * Time.deltaTime, ForceMode.Impulse);
    }


    public override void Update()
    {
        return;
    }
    #endregion
}