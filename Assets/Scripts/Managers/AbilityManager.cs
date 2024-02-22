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
    public bool smartCast;
    private void Start()
    {
        isCasting = false;
        throwAbility = false;
        instance = this;
    }
    //Esto es 100% chatGPT :)
    //Esto no es chatGPT esto lo hice yo
    //Si esta smartcast activado simplemente tira la habilidad, si no esta activado comprueba si esta pulsando le boton de habilidad con Instance.IsUsingAbility()
    //Luego
    private void Update()
    {
        currentAbility = PlayerInputController.Instance.GetCurrentAbility();
        if (!smartCast)
        {
            if (PlayerInputController.Instance.IsUsingAbility() && PlayerTimers.Instance.abilityTimers[currentAbility-1] > PlayerTimers.Instance.abilityCD[currentAbility-1])
            {
                CallAbilityIndicator();
                isCasting = true;
            }
            else
            {
                attAreaVisual.DeactivateArea();
            }
            if (PlayerInputController.Instance.IsCanceling())
            {
                isCasting = false;
                PlayerInputController.Instance.StopUsingAbility();
            }
            //Debug.Log(PlayerInputController.Instance.IsCanceling());
            if (isCasting && !PlayerInputController.Instance.IsUsingAbility())
            {
                throwAbility = true;
            }
            else
            {
                throwAbility = false;
            }
        }
        else
        {
            if (PlayerInputController.Instance.IsUsingAbility())
            {
                throwAbility = true;
            }
            else
            {
                throwAbility = false;
            }
        }
    }
    public void CastedAbility()
    {
        isCasting = false;
    }


    public void CallAbilityIndicator()
    {
        attAreaVisual.ActivateArea();
        attAreaVisual.DrawAttackArea(abilities[currentAbility].abilityRange, abilities[currentAbility].abilityRange);
    }
}
