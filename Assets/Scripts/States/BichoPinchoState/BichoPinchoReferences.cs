using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BichoPinchoReferences : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float attackTimer;
    [SerializeField] private float attackTime;
    [SerializeField] private bool isStunned;
    [SerializeField] private bool isAttacking;
    private bool startAttackTimer;
    private bool isCrashing;
    void Start()
    {
        isStunned = false;
    }

    public void Attack()
    {
        isAttacking = true;
    }
    public void StopAttack()
    {
        isAttacking = false;
    }
    public bool IsAttacking()
    {
        return isAttacking;
    }
    // Update is called once per frame
    void Update()
    {
        if (startAttackTimer)
        {
            attackTimer += Time.deltaTime;
        }
    }

    public void RestartAttackTimer()
    {
        attackTimer = 0;
    }

    public void StartAttackTimer()
    {
        startAttackTimer = true;
    }
    public void StopAttackTimer()
    {
        startAttackTimer = false;
    }
    public void SetAttackTime(float attackTime)
    {
        this.attackTime = attackTime;
    }
    public bool CheckAttackTimer()
    {
        bool aux = false;
        if (attackTimer > attackTime)
        {
            aux = true;
        }
        return aux;
    }

    public bool CheckIfStunned()
    {
        return isStunned;
    }
    public void Stun(float stunTime)
    {
        isStunned = true;
        GetComponent<StunBar>().StartStun(stunTime);
        StartCoroutine(_StopStun(stunTime));
    }

    private IEnumerator _StopStun(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        isStunned=false;
    }

    public bool IsCrashing()
    {
        return isCrashing;
    }

    private void OnTriggerEnter(Collider other)
    {
        isCrashing = true;
    }
    private void OnTriggerExit(Collider other)
    {
        isCrashing = false;
    }

    public void StopCrashing()
    {
        isCrashing = false;
    }

    public float GetDamage()
    {
        return damage;
    }
}
