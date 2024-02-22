using System.Collections;
using UnityEngine;

public class SlashGenerator : MonoBehaviour
{
    [SerializeField] private GameObject slashGameObject;
    private Material slashMaterial;
    [SerializeField] private float duration;

    private void Start()
    {
        slashMaterial = slashGameObject.GetComponent<Renderer>().material;
    }

    public void GenerateSlash()
    {
        slashGameObject.SetActive(true);
        StartCoroutine(DecreaseScrollValueOverTime());
    }

    private IEnumerator DecreaseScrollValueOverTime()
    {
        float startScrollValue = 5f;
        float targetScrollValue = -10f;
        duration = 0.5f; // Duración en segundos para el cambio gradual

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newScrollValue = Mathf.Lerp(startScrollValue, targetScrollValue, elapsedTime / duration);
            slashMaterial.SetFloat("_Scroll", newScrollValue);
            elapsedTime += Time.deltaTime; // Incrementar elapsedTime
            yield return null;
        }

        // Asegúrate de que el valor final sea exactamente el objetivo
        slashMaterial.SetFloat("Scroll", targetScrollValue);
        slashGameObject.SetActive(false);
    }
}
