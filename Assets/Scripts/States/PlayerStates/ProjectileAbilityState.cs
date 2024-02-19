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
    public override States CheckTransitions()
    {
        States newGameState = null;
        
        
            bool notChanged = true;
            int counter = 0;
           

            while (notChanged)
            {
                newGameState = stateTransitions[counter].GetExitState();
                if (newGameState != null)
                {
                    notChanged = false;
                    newGameState.InitializeState(stateGameObject);
                    newGameState.Start();
                    rigidBody.velocity = Vector3.zero;
                }
                if (counter < stateTransitions.Length - 1)
                {
                    counter++;
                }
                else
                {
                    notChanged = false;
                }
            }
        

        return newGameState;
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


}
