using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDoorAfterEntering : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject bridge;
    void Start()
    {
        
    }

    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            door.SetActive(true);
            bridge.SetActive(false);

        }
    }
}
