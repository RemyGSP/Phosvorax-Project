using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName ="States/DeathState")]
public class DeathState : States
{
    [SerializeField] private GameObject gameOver;
    public DeathState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    public override void OnExitState()
    {
        return;
    }

    public override void Update()
    {
        return;
    }

    public override void Start()
    {
        base.Start();
        Instantiate(gameOver);
        stateGameObject.GetComponent<StateMachine>().enabled = false;
    }


}
