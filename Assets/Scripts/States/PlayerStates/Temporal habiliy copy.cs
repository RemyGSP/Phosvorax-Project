using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temporalhabiliycopy : MonoBehaviour
{
    /*
     * 
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



     public override States CheckTransitions()
    {
        if (currentAttackTime > animationLength)
        {
            return base.CheckTransitions();
        }
        else
        {
            return null;
        }


    }

    public override void Start()
    {
        Debug.Log("StartAbility");
        AbilityManager.instance.CastedAbility();
        anim = PlayerReferences.instance.GetPlayerAnimator();
        rotateCharacter = stateGameObject.GetComponent<RotateCharacter>();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        playerTransform = stateGameObject.GetComponent<Transform>();

        //Lo que tardara en ejecutarse el ataque comparado con la animacion
        currentAttackDelay = attackDelay;
        Vector3 targetDir = PlayerReferences.instance.GetMouseTargetDir();

        stateGameObject.transform.rotation = rotateCharacter.NonSmoothenedRotation(targetDir);
        if (stateGameObject.TryGetComponent<AttackAreaVisualizer>(out AttackAreaVisualizer attAreaVisual))
        {
            if (PlayerInputController.Instance.IsUsingKeyboard())
            {
                attackPosition = attAreaVisual.GetCursorPositionInsideBounds(PlayerReferences.instance.GetMouseTargetDir() - PlayerReferences.instance.GetPlayerCoordinates()) + PlayerReferences.instance.GetPlayerCoordinates();
            }
            else
            {
                attackPosition = attAreaVisual.GetCursorPositionInsideBounds(PlayerReferences.instance.GetMouseTargetDir() - PlayerReferences.instance.GetPlayerCoordinates()) + PlayerReferences.instance.GetPlayerCoordinates();
            }
        }


        ExecuteAnim();
    }

    private void ExecuteAnim()
    {
        anim.SetTrigger("attack");
        currentFeedback = Instantiate(abilityFeedback, attackPosition, Quaternion.identity);
        currentFeedback.transform.position = attackPosition;
        currentAttackTime = 0;
        animationLength = anim.GetCurrentAnimatorClipInfo(0).Length;
        //stateGameObject.transform.rotation = Quaternion.Euler(stateGameObject.transform.rotation.x , stateGameObject.transform.rotation.y + animOffsetRotation, stateGameObject.transform.rotation.z );
        currentAttackDelay = attackDelay;
        canAttack = true;
    }

    void ExecuteAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(attackPosition, sphereSize / 2, enemyLayerMask, QueryTriggerInteraction.UseGlobal);

        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.TryGetComponent<HealthBehaviour>(out HealthBehaviour healthBehaviour))
            {
                healthBehaviour.Damage(attackDamage);
            }
            Debug.Log("Impacto con: " + hitCollider.gameObject.name);
        }
        canAttack = false;
    }

    public override void FixedUpdate()
    {
        currentAttackDelay -= Time.deltaTime;
        currentAttackTime += Time.deltaTime;
        if (currentAttackDelay <= 0 && canAttack)
        {
            ExecuteAttack();
            PlayerTimers.Instance.playerBasicAttackTimer = 0;
        }

    }






     private Rigidbody rigidBody;
    [SerializeField] private float parryLength;
    [SerializeField] private float shieldLength;
    float currentParryTime;
    private HealthBehaviour playerhealth;
    PlayerTimers instanciaPlayerTimers;
    [SerializeField] private float ShieldPercentage;



    public ShieldAbilityState(GameObject stateGameObject) : base(stateGameObject)
    {
    }
    public override States CheckTransitions()
    {
        Debug.Log("CurrentParryTime: " + currentParryTime + " ParryLength: " + parryLength);
        States newGameState = null;
        if (currentParryTime > parryLength)
        {

            AfterParry();
            newGameState = base.CheckTransitions();
        }

        return newGameState;
    }
    void ExecuteParry()
    {
        playerhealth.SetDamageModifier(0);
        PlayerReferences.instance.shieldObject.SetActive(true);
        ChangeColor(new Color(255f / 255f, 255f / 255f, 255f / 255f, 50f / 255f));

    }
    void AfterParry()
    {
        if (playerhealth.GetParrydetector())
        {
            playerhealth.SetDamageModifier(1);
            instanciaPlayerTimers.abilityTimers[1] = instanciaPlayerTimers.abilityCD[1];
            PlayerReferences.instance.shieldObject.SetActive(false);

        }
        else
        {
            MonoInstance.instance.StartCoroutine(EnableShield());
            ChangeColor(new Color(0f / 255f, 255f / 255f, 120f / 255f, 100f / 255f));
            playerhealth.SetDamageModifier(ShieldPercentage);
        }
        playerhealth.SetParrydetectorFalse();

    }

    void ChangeColor(Color newColor)
    {
        // Establece el color del material al nuevo color
        PlayerReferences.instance.shieldObjectRenderer.material.color = newColor;
    }
    public override void Start()
    {
        AbilityManager.instance.CastedAbility();
        rigidBody = stateGameObject.GetComponent<Rigidbody>();
        playerhealth = stateGameObject.GetComponent<HealthBehaviour>();
        instanciaPlayerTimers = PlayerTimers.Instance;
        currentParryTime = 0;

        ExecuteParry();
    }
    public override void FixedUpdate()
    {
        currentParryTime += Time.deltaTime;
    }
    public override void Update()
    {
        return;
    }

    IEnumerator EnableShield()
    {
        yield return new WaitForSeconds(shieldLength);
        playerhealth.SetDamageModifier(1);
        PlayerReferences.instance.shieldObject.SetActive(false);
    }

    public override void OnExitState()
    {
        return;
    } */


}
