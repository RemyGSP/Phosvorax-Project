using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerRollState")]
public class RollState : States
{
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
        stateGameObject.GetComponent<HealthBehaviour>().SetDamageModifier(0);
        animator = animator = PlayerReferences.instance.GetPlayerAnimator();
        //stateGameObject.GetComponent<Collider>().enabled = false;
        currentDashTime = 0f;
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        PlayerTimers.Instance.rollTimer = 0;
        playerDirection = PlayerInputController.Instance.GetPlayerInputDirection();
    }


    public override States CheckTransitions()
    {
        if (currentDashTime > dashTime)
        {
            return base.CheckTransitions();
        }
        else 
        {
            return null;
        }
    }

    public override void FixedUpdate()
    {
        if (dashTime >= currentDashTime)
        {
            Dash();
            currentDashTime += Time.deltaTime;
        }
    }

    public void Dash()
    {
        AudioManager.Instance.CallOneShot("event:/Dash");
        stateGameObject.GetComponent<DashFeedback>().ActivateFeedback();
        if (playerDirection != Vector3.zero)
        {
            rigidBody.AddForce(playerDirection * dashForce, ForceMode.Impulse);
        }
        //Esto es por si el usuario no tienes ninguna direccion pulsada, asi que pillara la rotacion actual del personaje
        else
        {
            rigidBody.AddForce(stateGameObject.transform.rotation * Vector3.forward * dashForce, ForceMode.Impulse);
        }
    }


    public override void Update()
    {
        return;
    }

    public override void OnExitState()
    {
        stateGameObject.GetComponent<DashFeedback>().DeactivateFeedback();
        stateGameObject.GetComponent<HealthBehaviour>().SetDamageModifier(1);
        rigidBody.velocity = Vector3.zero;
        //stateGameObject.GetComponent<Collider>().enabled = true;
        PlayerReferences.instance.GetPlayerAnimator().SetBool("dash", false);
        base.OnExitState();
    }

    public new void OnEnterState()
    {
        base.OnEnterState();
    }
    #endregion
}