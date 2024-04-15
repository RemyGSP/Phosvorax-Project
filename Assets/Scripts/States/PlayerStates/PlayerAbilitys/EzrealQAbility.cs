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
    private GameObject sgo;
    public override void OnEnterState(GameObject stateGameObject)
    {
        sgo = stateGameObject;
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        playerTransform = stateGameObject.GetComponent<Transform>();
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        RotatePlayerTowardsMouseTarget();
    }

    private void RotatePlayerTowardsMouseTarget()
    {
        if (!PlayerInputController.Instance.isGamepad){
            Vector3 targetDir = PlayerReferences.instance.GetMouseTargetDir() - sgo.transform.position;
            targetDir.y = 0;
            sgo.transform.rotation = rotateCharacter.NonSmoothenedRotation(targetDir);   
        }else if(PlayerInputController.Instance.GetPlayerInputDirection()!= Vector3.zero){           
            sgo.transform.rotation = rotateCharacter.NonSmoothenedRotation(PlayerInputController.Instance.GetPlayerInputDirection());
        }

        ShootProjectile();
        
    
    }

    

    private void ShootProjectile()
    {
        AudioManager.Instance.CallOneShot("event:/Zap");
        GameObject b = Instantiate(bulletPrefab, PlayerReferences.instance.ShotingPoint.position, PlayerReferences.instance.ShotingPoint.rotation);
        Vector3 playerForward = sgo.transform.forward;
        b.GetComponent<Rigidbody>().velocity = playerForward.normalized * bulletSpeed;
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
