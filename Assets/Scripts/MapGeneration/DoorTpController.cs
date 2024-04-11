using UnityEngine;
using System.Collections;

public class DoorTpController : MonoBehaviour
{
    public GameObject destinationObject;
    private bool isTeleporting = false;

    private IEnumerator Teleport(Collider other)
    {
        isTeleporting = true;

        // Teleport
        other.transform.position = destinationObject.transform.position;

        // Esperar un frame antes de permitir otra teleportación
        yield return null;

        isTeleporting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTeleporting && other.gameObject.layer == LayerMask.NameToLayer("Player") && destinationObject != null)
        {
            StartCoroutine(Teleport(other));
        }
    }

    public void SetDestination(GameObject newDestination)
    {
        destinationObject = newDestination;
    }
}
