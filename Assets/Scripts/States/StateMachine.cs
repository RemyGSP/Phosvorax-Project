using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private States entryState;
    [SerializeField] private States currentState;
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
            currentState.OnExitState();
            currentState = Instantiate(newState);
            currentState.InitializeState(this.gameObject);
            currentState.OnEnterState();
        }
    }

}