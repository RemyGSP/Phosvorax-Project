using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class States : ScriptableObject
{
    protected GameObject stateGameObject;
    protected List<States> states;
    public States(GameObject stateGameObject)
    {
    }

    public abstract States CheckTransitions();

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
