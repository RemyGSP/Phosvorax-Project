using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerOnDamageState")]
public class OnDamageState : States
{
    public OnDamageState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    public override void Start()
    {
        //Debug.Log("meda�olmao");
    }

    public override void OnExitState()
    {
        
    }

    public override void Update()
    {
        
    }
}
