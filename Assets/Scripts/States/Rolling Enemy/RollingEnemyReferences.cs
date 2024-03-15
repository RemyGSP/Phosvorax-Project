using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEnemyReferences : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool isStunned;
    [SerializeField] private bool isRolling;
    [SerializeField] private float rollTimer;
    [SerializeField] private bool isStuned;
    private bool playerSeen;
    private bool startRollTimer;
    void Start() 
    {
        isStunned = false;
    }

    public void Roll()
    {
        isRolling = true;
    }
    public void StopRolling()
    {
        isRolling = false;
    }
    public bool IsRolling()
    {
        return isRolling;
    }

    void Update()
    {

    }

    public void StartRollTimer()
    {
        startRollTimer = true;
    }
    public void StopRollTimer()
    {
        startRollTimer = false;
    }
    public void SetRollTime(float attackTime)
    {
        this.rollTimer = attackTime;
    }

    public bool CheckIfStunned()
    {
        return isStunned;
    }
    public void Stun(float stunTime)
    {
        isStunned = true;
        StartCoroutine(_StopStun(stunTime));
    }

    private IEnumerator _StopStun(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Colision con: " + other.name);
    }
    public float GetDamage()
    {
        return damage;
    }

    public bool GetPlayerSeen()
    {
        return playerSeen;
    }

    public void SetPlayerSeen(bool aux)
    {
        playerSeen = aux;
    }

    public void SetIsStunned(bool stun)
    {
        isStuned = stun;
    }

    public bool GetIsStunned()
    {
        return isStunned;
    }
}
