using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraValues : MonoBehaviour
{

    [SerializeField] private int newOrthoSize;
    [SerializeField] private CinemachineVirtualCamera bossCamera;
    private CinemachineVirtualCamera playerCamera;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerCamera = Camera.main.transform.parent.GetComponent<CinemachineVirtualCamera>();
            playerCamera.Priority = 10;
            bossCamera.Priority = 11;
        }
    }

    private void OnTriggerExit(Collider other)
    {

    }

}
