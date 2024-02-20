using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTimers : MonoBehaviour
{
    [Header("PlayerTimers")]
    static public PlayerTimers Instance;
    public float playerBasicAttackTimer;
    public float playerBasicAttackCD;
    public float rollTimer;
    public float rollCD;

    [HideInInspector]public float[] abilityTimers;
    [SerializeField] public float[] abilityCD;

    private void Start()
    {
        abilityTimers = new float[4];
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Update()
    {

        abilityTimers[0] += Time.deltaTime;
        abilityTimers[1] += Time.deltaTime;
        playerBasicAttackTimer += Time.deltaTime;
        rollTimer += Time.deltaTime;
    }
}
