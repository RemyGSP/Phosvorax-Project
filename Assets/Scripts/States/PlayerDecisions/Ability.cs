using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : States
{
    [SerializeField] public float abilityRange;
    protected Ability(GameObject stateGameObject) : base(stateGameObject)
    {
    }

}
