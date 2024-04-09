using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.XR;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private States entryState;
    [SerializeField] private States currentState;
    [SerializeField] private States stunnedState;
    // Start is called before the first frame update
    void Start()
    {
        currentState = Instantiate(entryState);
        currentState.InitializeState(this.gameObject);
        currentState.OnEnterState();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.Update();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }
    private void LateUpdate()
    {
        States newState = currentState.CheckTransitions();
        if (newState is not null)
        {
            ChangeState(newState);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        currentState.OnCollisionEnter(collision);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);

    }

    public void ChangeState(States newState)
    {
        currentState.OnExitState();
        currentState = Instantiate(newState);
        currentState.InitializeState(this.gameObject);
        currentState.OnEnterState();
    }
    public void ReceivedDamage()
    {
        //Si no se puede stunear, esto no hace nada y ya
        if (stunnedState != null)
        {
            ChangeState(stunnedState);
        }
    }
}