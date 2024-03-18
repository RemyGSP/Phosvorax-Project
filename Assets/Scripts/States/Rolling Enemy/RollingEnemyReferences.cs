using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEnemyReferences : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool isStunned;

    private bool playerSeen;
    private bool startRollTimer;
    void Start() 
    {
        isStunned = false;
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
        isStunned = stun;
    }

    public bool GetIsStunned()
    {
        return isStunned;
    }
}
