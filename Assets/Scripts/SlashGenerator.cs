using System.Collections;
using UnityEngine;

public class SlashGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] slashGameObject;
    private Material slashMaterial;
    [SerializeField] private float duration;

    private void Start()
    {
        slashMaterial = slashGameObject[0].GetComponent<Renderer>().material;
    }

    public void GenerateSlash(int currentSlash)
    {
        if (currentSlash == 3)
        {
            StartCoroutine(DecreaseScrollValueOverTimeFinalSlash(currentSlash));
            slashGameObject[currentSlash].SetActive(true);
            slashGameObject[currentSlash + 1].SetActive(true);
        }
        else
        {
            Debug.Log(currentSlash);
            slashMaterial = slashGameObject[currentSlash].GetComponent<Renderer>().material;
            slashGameObject[currentSlash].SetActive(true);
            StartCoroutine(DecreaseScrollValueOverTime(currentSlash));
        }

    }

    private IEnumerator DecreaseScrollValueOverTimeFinalSlash(int currentSlash)
    {
        float startScrollValue = -0.2f;
        float targetScrollValue = 1f;

        float elapsedTime = 0f;
        slashMaterial = slashGameObject[currentSlash].GetComponent<Renderer>().material;
        Material slashMaterial2 = slashGameObject[currentSlash].GetComponent<Renderer>().material;

        while (elapsedTime < duration)
        {
            float newScrollValue = Mathf.Lerp(startScrollValue, targetScrollValue, elapsedTime / duration);
            slashMaterial.SetFloat("_Dissolve", newScrollValue);
            slashMaterial2.SetFloat("_Dissolve", newScrollValue);
            elapsedTime += Time.deltaTime; // Incrementar elapsedTime
            yield return null;
        }

        // Asegúrate de que el valor final sea exactamente el objetivo
        slashMaterial.SetFloat("Scroll", targetScrollValue);
        slashMaterial2.SetFloat("Scroll", targetScrollValue);
        slashGameObject[currentSlash].SetActive(false);
        slashGameObject[currentSlash + 1].SetActive(false);
    }
    private IEnumerator DecreaseScrollValueOverTime(int currentSlash)
    {
        float startScrollValue = -0.2f;
        float targetScrollValue = 1f;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newScrollValue = Mathf.Lerp(startScrollValue, targetScrollValue, elapsedTime / duration);
            slashMaterial.SetFloat("_Dissolve", newScrollValue);
            elapsedTime += Time.deltaTime; // Incrementar elapsedTime
            yield return null;
        }

        // Asegúrate de que el valor final sea exactamente el objetivo
        slashMaterial.SetFloat("Scroll", targetScrollValue);
        slashGameObject[currentSlash].SetActive(false);
    }
}
