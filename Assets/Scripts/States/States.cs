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
        bool notChanged = true;
        int counter = 0;
        States newState = null;

        while (notChanged)
        {
            newState = stateTransitions[counter].GetExitState(stateGameObject.GetComponent<StateMachine>());
            if (newState != null)
            {
                notChanged = false;
                newState = Instantiate(newState);
                newState.InitializeState(stateGameObject);
                newState.Start();
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

        return newState;
    }

    //virtual para que cuando se utilize un objeto de tipo States pero que contenga un hijo 
    public virtual void InitializeState(GameObject gameObject)
    {
        stateGameObject = gameObject;
    }
    public virtual void Start()
    {

    }

    public virtual void OnExit()
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
