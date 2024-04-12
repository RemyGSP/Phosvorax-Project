using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class GetBestAbilityToUse : MonoBehaviour
{
    [SerializeField] Abilities[] ability;
    [SerializeField] int number;
    [SerializeField] float distanceToBeConsideredFarAway;
    private Vector3 playerPos;
    private int bestAbilty;

    bool canDecide;
    void Start()
    {
        ability = new Abilities[ability.Length];
        bestAbilty = 0;
        canDecide = true;
    }

    void Update()
    {
        playerPos = PlayerReferences.instance.GetPlayerCoordinates();

        if(canDecide)
        {
            StartCoroutine(SelectBestAbility(playerPos));
            canDecide=false;
        }
    }
    private IEnumerator SelectBestAbility(Vector3 playerPos)
    {
        yield return new WaitForSeconds(3f);
        canDecide = true;
        float distance =  this.gameObject.transform.position.magnitude - playerPos.magnitude;

        if(distance < distanceToBeConsideredFarAway)
        {

        }
        else
        {

        }
    }



    public void SetBestAbility(int aux)
    {
        bestAbilty = aux;   

    }
    
    public int GetBestAbility()
    {
        return bestAbilty;
    }
}









//ESTA CLASE SOLO ES PARA LA INFORMACIÓN QUE VA A CONSULTAR EL BOSS PARA DECIDIR CUAL DE LAS HABILIDADES ES MEJOR EN X MOMENTO
[System.Serializable]
public class Abilities
{
    public int damage;
    public float range;
    public bool isAreaAbility;
    public float cooldown;
    public float duration;

    [Header("Información de editor")]
    // esta string es para que quine coloque las habilidades en el array ponga cual es y asi no se mezclan o hay malentendidos
    public string name;

    public Abilities(int damage, float range, bool isAreaAbility, float cooldown, float duration, string name)
    {
        this.damage = damage;
        this.range = range;
        this.isAreaAbility = isAreaAbility;
        this.cooldown = cooldown;
        this.duration = duration;
        this.name = name;
    }
}
