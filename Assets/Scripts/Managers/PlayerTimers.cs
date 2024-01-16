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

    public float ability1Timer;
    public float ability1CD;

    public float ability2Timer;
    public float ability2CD;

    public float ability3Timer;
    public float ability3CD;

    public float ability4Timer;
    public float ability4CD;
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
