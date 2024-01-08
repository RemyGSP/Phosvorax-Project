using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyStates/EnemyIdleState")]

public class EnemyIdleState : States
{
    [Header("States")]
    [SerializeField] private States EnemyChaseState;
    [SerializeField] private States EnemyAttackState;
    [SerializeField] private States basicAttackState;

    public override States CheckTransitions()
    {
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
