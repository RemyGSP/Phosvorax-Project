using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject[] HUDAbilities;

    private void Start()
    {
        instance = this;
    }

    //Ability number puede ir del 0 al 3
    //public void AbilityStartCooldown(int abilityNumber)
    //{
    //    DoCooldown(HUDAbilities[abilityNumber]);
    //}

    //private IEnumerator DoCooldown(GameObject ability)
    //{
    //    yield return new WaitForSeconds;
    //}
}
