using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/PlayerBasicAttackState")]
public class OnDamageState : States
{
    public OnDamageState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    public override void Start()
    {
        Debug.Log("medañolmao");
    }

    public override void OnExitState()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }
}
