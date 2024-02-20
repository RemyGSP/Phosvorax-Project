using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/ThirdAbilityState")]
public class ProjectileAbilityState : Ability
{
    private Rigidbody rigidBody;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public float bulletDuration;
    private Transform playerTransform;
    private RotateCharacter rotateCharacter;
    public ProjectileAbilityState(GameObject stateGameObject) : base(stateGameObject)
    {
    }

    private void ShootProjectile()
    {
        Instantiate(bulletPrefab, PlayerReferences.instance.ShotingPoint.position, PlayerReferences.instance.ShotingPoint.rotation);
    }
    public override void Start()
    {
        AbilityManager.instance.CastedAbility();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        playerTransform = stateGameObject.GetComponent<Transform>();
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        Vector3 targetDir = PlayerReferences.instance.GetMouseTargetDir() - stateGameObject.transform.position;

        stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(targetDir);
        ShootProjectile();


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

    }

}
