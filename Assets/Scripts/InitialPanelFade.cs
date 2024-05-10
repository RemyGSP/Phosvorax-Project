using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitialPanelFade : MonoBehaviour
{
    public float delayBeforeFade = 2f; // Tiempo de espera antes de comenzar a desvanecerse
    public float fadeDuration = 2f; // Duración de la transición en segundos

    private Image panelImage;
    private float currentAlpha = 1f; // Opacidad actual del panel
    private float timer = 0f; // Temporizador para la transición
    private bool delayCompleted = false; // Indicador de si el retraso ha terminado

    void Start()
    {
        // Obtener el componente Image del panel
        panelImage = GetComponent<Image>();

        // Iniciar el retraso
        Invoke("CompleteDelay", delayBeforeFade);
    }

    void Update()
    {
        if (!delayCompleted)
            return;

        // Actualizar el temporizador
        timer += Time.deltaTime;

        // Calcular el nuevo valor de opacidad
        currentAlpha = 1f - (timer / fadeDuration);

        // Aplicar el nuevo valor de opacidad al panel
        Color newColor = panelImage.color;
        newColor.a = Mathf.Clamp01(currentAlpha);
        panelImage.color = newColor;

        // Destruir este script cuando la opacidad llegue a 0
        if (currentAlpha <= 0f)
        {
            Destroy(this);
        }
    }

    // Método para marcar que el retraso ha terminado
    void CompleteDelay()
    {
        delayCompleted = true;
    }
}