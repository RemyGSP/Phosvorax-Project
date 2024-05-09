using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cinematicaplayer : MonoBehaviour
{
    // Evento definido como UnityEvent
    public UnityEvent OnTimerFinished;

    public float timerDuration = 5f; // Duración del temporizador en segundos
    private float currentTimer = 0f; // Temporizador actual

    private void Start()
    {
        StartTimer();
    }

    private void Update()
    {
        // Actualizar el temporizador cada frame
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0)
            {
                currentTimer = 0;
                OnTimerFinished.Invoke(); // Invocar el evento cuando el temporizador llega a cero
            }
        }
    }

    public void StartTimer()
    {
        currentTimer = timerDuration; // Iniciar el temporizador
    }
}
