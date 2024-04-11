using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilitys/ChoGathQAbility")]
public class ChoGathQAbility : Ability
{
    private RotateCharacter rotateCharacter;
    private Vector3 targetDir;
    public GameObject prefabToInstantiate; // Asigna el prefab que deseas instanciar en el editor
    public float bulletLifetime;


    private Vector3 mousePosition;

    public override void AbilityUpdate()
    {

    }

    public override void OnEnterState(GameObject stateGameObject)
    {
        AudioManager.Instance.CallOneShot("event:/StoneAttack");
        targetDir = PlayerReferences.instance.GetMouseTargetDir();
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(targetDir);
    }

    public override void OnExitState()
    {
        targetDir.y = -0.7f;
        GameObject projectile = Instantiate(prefabToInstantiate, targetDir, Quaternion.identity);


        DamageOnHit damageOnHit= projectile.GetComponent<DamageOnHit>();
        if (damageOnHit != null)
        {
            damageOnHit.SetDamage(abilityBaseDamage + (abilityBaseDamage * ShopManager.instance.GetDamageLevel() * 0.7f));
        }
    }
}
