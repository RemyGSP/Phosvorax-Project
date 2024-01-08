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

    private void Start()
    {
        if (timer == null)
            timer = this;
    }
    private void Update()
    {
        playerBasicAttackTimer += Time.deltaTime;
        rollTimer += Time.deltaTime;
    } 
}
