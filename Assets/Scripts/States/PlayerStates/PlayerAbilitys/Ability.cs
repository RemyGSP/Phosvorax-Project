using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject{
    [SerializeField] protected float abilityRange;
    [SerializeField] protected float abilityBaseDamage;
    [SerializeField] protected float abilityScaling;
    [SerializeField] protected float abilityCD;

    public abstract void OnEnterState();
    public abstract void AbilityUpdate();
    public abstract void OnExitState();
    


}
