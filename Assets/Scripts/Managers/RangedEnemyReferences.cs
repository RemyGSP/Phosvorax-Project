using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyReferences : MonoBehaviour
{
    #region Variables

    private bool canAttack;
    private bool canMoveAway;
    private bool canChase;
    [SerializeField] private float maxNearDistance;
    #endregion


    #region Methods

    private void Start()
    {
        canAttack = true;
        canMoveAway = false;
    }
    public void SetCanAttack(bool aux)
    {
        canAttack = aux;
    }

    public void SetCanMoveAway(bool aux)
    {
        canMoveAway = aux;
    }

    public bool GetCanAttack()
    {
        return canAttack;
    }

    public bool GetCanMoveAway()
    {
        return canMoveAway;
    }

    public float GetMaxNearDistance()
    {
        return maxNearDistance;
    }

    public void SetMaxNearDistance(float aux)
    {
       maxNearDistance = aux;   
    }

    public void SetCanChase(bool aux)
    {
        canChase = aux;
    }

    public bool GetCanChase()
    {
        return canChase;
    }
    #endregion
}

