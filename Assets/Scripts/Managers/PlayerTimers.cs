using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimers : MonoBehaviour
{
    [Header("PlayerTimers")]
    static public PlayerTimers timer;
    public float playerBasicAttackTimer;
    public float playerBasicAttackCD;
    public float rollTimer;
    public float rollCD;

    public float[] abilityTimers;
    [SerializeField] public float[] abilityCD;

    private void Start()
    {
        abilityTimers = new float[4];
        if (timer == null)
        {
            timer = this;
        }
    }

    private void Update()
    {
        abilityTimers[0] += Time.deltaTime;
        playerBasicAttackTimer += Time.deltaTime;
        rollTimer += Time.deltaTime;
    }
}
