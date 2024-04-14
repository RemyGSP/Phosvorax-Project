using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilitys/EzrealQAbility")]
public class EzrealQAbility : Ability
{
    private Rigidbody rigidBody;
    public GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDuration;
    private Transform playerTransform;
    private RotateCharacter rotateCharacter;
    public override void OnEnterState(GameObject stateGameObject)
    {
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        playerTransform = stateGameObject.GetComponent<Transform>();
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        Vector3 targetDir = PlayerReferences.instance.GetMouseTargetDir() - stateGameObject.transform.position;
        targetDir.y = 0;
        stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(targetDir);
        ShootProjectile(targetDir);
    }

    private void ShootProjectile(Vector3 direction)
    {
        AudioManager.Instance.CallOneShot("event:/Zap");
        GameObject b = Instantiate(bulletPrefab, PlayerReferences.instance.ShotingPoint.position, PlayerReferences.instance.ShotingPoint.rotation);
        b.GetComponent<Rigidbody>().velocity = direction.normalized * bulletSpeed;
        b.GetComponent<BulletBehaviour>().SetLifetime(bulletDuration);
        if (ShopManager.instance.GetDamageLevel() == 0)
        {
            b.GetComponent<BulletBehaviour>().SetDamage(abilityBaseDamage);
        }
        else
        {
            b.GetComponent<BulletBehaviour>().SetDamage((abilityBaseDamage * (ShopManager.instance.GetDamageLevel() - 1) * 0.7f));
        }
        AudioManager.Instance.CallOneShot("event:/EnemyHit");
    }

    public override void OnExitState()
    {
        
    }

    public override void AbilityUpdate()
    {

    }
}
