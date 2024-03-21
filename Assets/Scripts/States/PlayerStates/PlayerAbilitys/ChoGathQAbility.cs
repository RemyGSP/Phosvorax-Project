using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilitys/ChoGathQAbility")]
public class ChoGathQAbility : Ability
{
    private RotateCharacter rotateCharacter;

    public GameObject prefabToInstantiate; // Asigna el prefab que deseas instanciar en el editor
    public float bulletLifetime;


    private Vector3 mousePosition;

    public override void AbilityUpdate()
    {

    }

    public override void OnEnterState(GameObject stateGameObject)
    {
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        Vector3 targetDir = PlayerReferences.instance.GetMouseTargetDir() - stateGameObject.transform.position;
        stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(targetDir);
    }

    public override void OnExitState()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.y; // Ajusta la posición Z al nivel de la cámara
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.y = 0; // Establece la altura en 0

        GameObject projectile = Instantiate(prefabToInstantiate, worldPosition, Quaternion.identity);

        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();

        BulletBehaviour bulletBehaviour = projectile.GetComponent<BulletBehaviour>();
        if (bulletBehaviour != null)
        {
            bulletBehaviour.SetLifetime(bulletLifetime);
            bulletBehaviour.SetDamage(abilityBaseDamage);
        }
    }
}
