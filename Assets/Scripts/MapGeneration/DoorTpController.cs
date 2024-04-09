using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DoorTpController : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float delayBeforeFade = 0.1f;
    public GameObject destinationObject;
    private GameObject fadePanel;

    private bool isTeleporting = false;

    private void Start()
    {
        GameObject canvasObject = GameObject.Find("CanvasFundidoNegro");

        // Si no encuentra el canvas, muestra un mensaje de advertencia
        if (canvasObject == null)
        {
            Debug.LogError("No se encontró ningún objeto Canvas con el nombre 'Canvas1' en la escena.");
            return;
        }

        // Encuentra el primer hijo del canvas, que debería ser el panel de fundido
        fadePanel = canvasObject.transform.GetChild(0).gameObject;
    }

    private IEnumerator TeleportWithFade(Collider other)
    {
        isTeleporting = true;

        Image panelImage = fadePanel.GetComponent<Image>();

        float timer = 0.2f;

        // Fade to black
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);
            yield return null;
        }

        // Teleport
        other.transform.position = destinationObject.transform.position;

        // Wait before fading out
        yield return new WaitForSeconds(delayBeforeFade);

        // Fade back to transparent
        timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            panelImage.color = new Color(panelImage.color.r, panelImage.color.g, panelImage.color.b, alpha);
            yield return null;
        }

        isTeleporting = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!isTeleporting && other.gameObject.layer == LayerMask.NameToLayer("Player") && destinationObject != null)
        {
            StartCoroutine(TeleportWithFade(other));
        }
    }

    public void SetDestination(GameObject newDestination)
    {
        destinationObject = newDestination;
    }
}

