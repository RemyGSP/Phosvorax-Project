using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timers : MonoBehaviour
{
    static public Timers timer;
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
