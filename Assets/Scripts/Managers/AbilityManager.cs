using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{

    static public AbilityManager instance;
    private int currentAbility;
    [SerializeField] private AttackAreaVisualizer attAreaVisual;
    private void Start()
    {
        instance = this;
    }


    //0 es el melee y de 1 a 4 son las habilidades en orden
    public int GetCurrentAbility()
    {
        if (PlayerInputController.IsUsingAbility())
            currentAbility = PlayerInputController.GetCurrentAbility();
        else
            currentAbility = 0;
        return currentAbility;
    }
    
}
