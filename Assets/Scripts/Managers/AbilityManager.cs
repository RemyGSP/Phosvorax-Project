using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    static public AbilityManager instance;
    private int currentAbility;
    //[SerializeField] private Attack[] currentAbilityState;
    [SerializeField] private  AttackAreaVisualizer attAreaVisual;
    [SerializeField] private Ability[] abilities;
    public bool isCasting;
    public bool throwAbility;
    private void Start()
    {
        isCasting = false;
        throwAbility = false;
        instance = this;
    }

    //
    private void Update()
    {
        if (PlayerInputController.Instance.IsUsingAbility())
        {
            currentAbility = PlayerInputController.Instance.GetCurrentAbility();
            CallAbilityIndicator();
            isCasting = true;
        }
        else
        {
            attAreaVisual.DeactivateArea();
        }
        if (isCasting && !PlayerInputController.Instance.IsUsingAbility())
        {
            throwAbility = true;
        }
        else
        {
            throwAbility = false;
        }
    }
    //0 es el melee y de 1 a 4 son las habilidades en orden
    public int GetCurrentAbility()
    {
        return currentAbility;
    }

    public void CallAbilityIndicator()
    {
        attAreaVisual.ActivateArea();
        attAreaVisual.DrawAttackArea(abilities[currentAbility].abilityRange, abilities[currentAbility].abilityRange);
    }
    
    public void CastedAbility()
    {
        isCasting = false;
    }
}
