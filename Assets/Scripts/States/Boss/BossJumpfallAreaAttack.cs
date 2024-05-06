using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJumpfallAreaAttack : MonoBehaviour
{
    [SerializeField] GameObject areaEffect;

    private Vector3 areaEffectStartSize;

    [Header("Damage Values")]
    [SerializeField] float damage;


    [Header("Area Activate Values")]
    [SerializeField] Vector3 areaEffectMaxSize;
    [SerializeField] float growthDuration = 1f;
    private void Start()
    {
        areaEffectStartSize = areaEffect.transform.localScale;
    }
    public void ActivateAreaEffect()
    {
        areaEffect.SetActive(true);
        StartCoroutine(AreaSizeGrow());
    }

    public void DeactivateAreaEffect()
    {
        areaEffect.SetActive(false);
        areaEffect.transform.localScale = areaEffectStartSize;
    }

    private IEnumerator AreaSizeGrow()
    {
        float timer = 0f;
        Vector3 initialSize = areaEffect.transform.localScale;

        // Guarda los valores iniciales de Y y luego establece Y en 1 para bloquear el cambio de escala en ese eje
        float initialY = initialSize.y;
        initialSize.y = 0.1f;

        while (timer < growthDuration)
        {
            timer += Time.deltaTime;
            float t = timer / growthDuration;
            float scaleFactorX = Mathf.Lerp(1f, areaEffectMaxSize.x / initialSize.x, t);
            float scaleFactorZ = Mathf.Lerp(1f, areaEffectMaxSize.z / initialSize.z, t);

            // Aplica el aumento de escala solo a los ejes X y Z
            Vector3 scaleFactor = new Vector3(scaleFactorX, 1f, scaleFactorZ);
            areaEffect.transform.localScale = Vector3.Scale(initialSize, scaleFactor);
            yield return null;
        }

        DeactivateAreaEffect();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthBehaviour>().Damage(damage);
        }
    }
}
