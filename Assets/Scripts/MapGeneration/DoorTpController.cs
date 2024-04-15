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

    public void TpOpen()
    {
        if (!anim.GetBool("puenteon"))
        {
        anim.SetBool("puenteon", true);
        //poner espera antes de quitar la barrera para no curzar cunado no esta listo
        exitWall.SetActive(false);
        }
        
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
