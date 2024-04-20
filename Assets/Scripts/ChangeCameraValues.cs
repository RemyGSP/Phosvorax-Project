using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraValues : MonoBehaviour
{

    [SerializeField] private int newOrthoSize;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Camera.main.transform.parent.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = newOrthoSize;
        }
    }

}
