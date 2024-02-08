using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class States : ScriptableObject
{
    [HideInInspector] public GameObject stateGameObject;
    protected List<States> states;
    [SerializeField] protected Transition[] stateTransitions;
    public States(GameObject stateGameObject)
    {
    }

    public virtual States CheckTransitions()
    {
        States newGameState = null;
        bool notChanged = true;
        int counter = 0;

        while (notChanged)
        {
            newGameState = stateTransitions[counter].GetExitState(stateGameObject.GetComponent<StateMachine>());
            if (newGameState != null)
            {
                notChanged = false;
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

        return newGameState;
    }

    public virtual void OnEnterState()
    {
        Start();
    }

    public abstract void OnExitState();

    //virtual para que cuando se utilize un objeto de tipo States pero que contenga un hijo 
    public virtual void InitializeState(GameObject gameObject)
    {
        stateGameObject = gameObject;
    }
    public virtual void Start()
    {

    }

    public virtual void OnTriggerEnter()
    {

    }

    public virtual void FixedUpdate()
    {

    }
    public abstract void Update();

}
