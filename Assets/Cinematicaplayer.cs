using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Cinematicaplayer : MonoBehaviour
{
    // Evento definido como UnityEvent
    public UnityEvent OnTimerFinished;

    public float timerDuration = 5f; // Duración del temporizador en segundos
    private float currentTimer = 0f; // Temporizador actual

    [SerializeField] private InputAction skipAction; // Acción de entrada para saltar la cinemática

    private void OnEnable()
    {
        skipAction.Enable();
    }

    private void OnDisable()
    {
        skipAction.Disable();
    }

    private void Start()
    {
        StartTimer();
        skipAction.performed += SkipCinematic;
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

    private void SkipCinematic(InputAction.CallbackContext context)
    {
        // Se activa cuando se realiza la acción de saltar la cinemática
        OnTimerFinished.Invoke(); // Invocar el evento para saltar la cinemática
    }
}

