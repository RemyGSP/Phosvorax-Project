using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTimers : MonoBehaviour
{
    public float basicAttackTimer;
    public float basicAttackCD;

    public float[] abilityTimers;
    [SerializeField] public float[] abilityCD;
    private void Start()
    {
        abilityTimers = new float[4];
    }

    private void Update()
    {

        abilityTimers[0] += Time.deltaTime;
        abilityTimers[1] += Time.deltaTime;
        abilityTimers[2] += Time.deltaTime;
        abilityTimers[3] += Time.deltaTime;
        basicAttackTimer += Time.deltaTime;
    }
}
