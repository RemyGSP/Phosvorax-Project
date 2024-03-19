using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[CreateAssetMenu(menuName = "Abilitys/AreaDamageAbility")]
public class AreaDamageAbility : Ability
{

    private Transform playerTransform;
    [SerializeField] LayerMask enemyLayerMask;
    public override void OnEnterState(GameObject stateGameObject)
    {
        playerTransform = stateGameObject.GetComponent<Transform>();
        AreaDamage();
        //Debug.Log("nikinikole si o no?");
    }

    public override void OnExitState()
    {
        

    }

    public override void AbilityUpdate()
    {
        
    }

    private void AreaDamage()
    {
       
        Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, abilityRange / 2, enemyLayerMask, QueryTriggerInteraction.UseGlobal);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<HealthBehaviour>(out HealthBehaviour healthBehaviour))
            {
                healthBehaviour.Damage(abilityBaseDamage);
            }
            Debug.Log("Impacto con: " + hitCollider.gameObject.name);
        }
    }
}
