using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "States/ProjectileAbilityState")]
public class ProjectileAbilityState : Ability
{
    private Rigidbody rigidBody;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float bulletDuration;
    [SerializeField] private float damage;
    private Transform playerTransform;
    private RotateCharacter rotateCharacter;
    public ProjectileAbilityState(GameObject stateGameObject) : base(stateGameObject)
    {
    }


    private void ShootProjectile(Vector3 direction)
    {
        GameObject b = Instantiate(bulletPrefab, PlayerReferences.instance.ShotingPoint.position, PlayerReferences.instance.ShotingPoint.rotation);

        b.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
        b.GetComponent<BulletBehaviour>().SetLifetime(bulletDuration);
        b.GetComponent<BulletBehaviour>().SetDamage(damage);
    }
    public override void Start()
    {
        AbilityManager.instance.CastedAbility();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        playerTransform = stateGameObject.GetComponent<Transform>();
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        Vector3 targetDir = PlayerReferences.instance.GetMouseTargetDir() - stateGameObject.transform.position;

        stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(targetDir);
        ShootProjectile(targetDir);


    }




    public override void FixedUpdate()
    {

    }
    public override void Update()
    {
        return;
    }

    public override void OnExitState()
    {
        return;
    }
}