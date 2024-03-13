using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Abilitys/MasteryQAbility")]
public class MasteryQAbility : Ability
{
    private Transform playerTransform;
    [SerializeField] LayerMask enemyLayerMask;
    private int maxTargets = 4; // Máximo de objetivos afectados

    public override void OnEnterState(GameObject stateGameObject)
    {
        playerTransform = stateGameObject.GetComponent<Transform>();
        MonoInstance.instance.StartCoroutine(DrawGizmoCoroutine());
        QAbility();
    }

    public override void OnExitState()
    {
        // Puedes agregar lógica de salida si es necesario
    }

    public override void AbilityUpdate()
    {
        // Puedes agregar lógica de actualización si es necesario
    }

    private void QAbility()
    {
        Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, abilityRange / 2, enemyLayerMask, QueryTriggerInteraction.UseGlobal);

        int targetsAffected = Mathf.Min(hitColliders.Length, maxTargets); // Limita el número de objetivos afectados

        for (int i = 0; i < targetsAffected; i++)
        {
            Collider hitCollider = hitColliders[i];

            if (hitCollider.TryGetComponent<HealthBehaviour>(out HealthBehaviour healthBehaviour))
            {
                // Daño a un objetivo a la vez
                healthBehaviour.Damage(abilityBaseDamage);
            }
            Debug.Log("Impacto con: " + hitCollider.gameObject.name);
        }
    }

    private IEnumerator DrawGizmoCoroutine()
    {

        float duration = 0.5f; // Duración en segundos
        float startTime = Time.time;

        while (Time.time - startTime < duration)
        {
            // Dibuja el gizmo para representar el área de la Physics.OverlapSphere
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(playerTransform.position, abilityRange / 2);
            yield return null; // Espera un frame
        }

    }
}
