using UnityEngine;
using System.Collections;

public class DoorTpController : MonoBehaviour
{
    [SerializeField] private GameObject destinationObject;
    [SerializeField] private GameObject exitWall;
    [SerializeField] private GameObject model;
    private bool isTeleporting = false;
    private Animator anim;

    private void Start(){
        anim = model.GetComponent<Animator>();
    }

    public void SetDestination(GameObject newDestination)
    {
        destinationObject = newDestination;

    }
 

    private void OnTriggerEnter(Collider other)
    {
        if (!isTeleporting && other.gameObject.layer == LayerMask.NameToLayer("Player") && destinationObject != null)
        {
            StartCoroutine(Teleport(other));
            //decirle a la otra puerta que el player esta en su sala, decirle a tu sala que el player ya no esta
        }
    }

    private IEnumerator Teleport(Collider other)
    {
        isTeleporting = true;

        // Teleport
        other.transform.position = destinationObject.transform.position;

        // Esperar un frame antes de permitir otra teleportación
        yield return null;

        isTeleporting = false;
    }



    public void TpOpen()
    {
        if (!anim.GetBool("puenteon"))
        {
            anim.SetBool("puenteon", true);
            // Pausa durante un segundo
            StartCoroutine(ActivateExitWallAfterDelay(1.0f));
        }
    }

    private IEnumerator ActivateExitWallAfterDelay(float delay)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(delay);
        // Activar la pared de salida
        exitWall.SetActive(true);
    }
    public void TpClose()
    {
        if (anim.GetBool("puenteon"))
        {
        anim.SetBool("puenteon", false);
        exitWall.SetActive(true);
        }
        
    }
}
