using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilitys/MasteryQAbility")]
public class MasteryQAbility : Ability
{
    private Transform playerTransform;
    [SerializeField] LayerMask enemyLayerMask;
    private int maxTargets = 4; // M�ximo de objetivos afectados

    public override void OnEnterState(GameObject stateGameObject)
    {
        playerTransform = stateGameObject.GetComponent<Transform>();
        MonoInstance.instance.StartCoroutine(DrawGizmoCoroutine());
        QAbility();
    }

    public override void OnExitState()
    {
        // Puedes agregar l�gica de salida si es necesario
    }

    public override void AbilityUpdate()
    {
        // Puedes agregar l�gica de actualizaci�n si es necesario
    }

    private void QAbility()
    {
        Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, abilityRange / 2, enemyLayerMask, QueryTriggerInteraction.UseGlobal);

        int targetsAffected = Mathf.Min(hitColliders.Length, maxTargets); // Limita el n�mero de objetivos afectados

        for (int i = 0; i < targetsAffected; i++)
        {
            Collider hitCollider = hitColliders[i];

            if (hitCollider.TryGetComponent<HealthBehaviour>(out HealthBehaviour healthBehaviour))
            {
                // Da�o a un objetivo a la vez
                healthBehaviour.Damage(abilityBaseDamage);
            }
            Debug.Log("Impacto con: " + hitCollider.gameObject.name);
        }
    }

    private IEnumerator DrawGizmoCoroutine()
    {

        float duration = 0.5f; // Duraci�n en segundos
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            // Dibuja el gizmo para representar el �rea de la Physics.OverlapSphere
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerTransform.position, abilityRange / 2);
            yield return null; // Espera un frame
        }

    }
}
