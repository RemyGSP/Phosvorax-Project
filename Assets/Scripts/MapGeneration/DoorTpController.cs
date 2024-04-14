using UnityEngine;
using System.Collections;

public class DoorTpController : MonoBehaviour
{
    [SerializeField] private GameObject destinationObject;
    [SerializeField] private GameObject exitWall;
    [SerializeField] private GameObject model;
    private bool isTeleporting = false;
    private bool TpState;
    private Animator anim;

    private void Start(){
        anim = model.GetComponent<Animator>();
        TpState = true;
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
        if (!TpState)
        {
        anim.SetBool("puenteon", true);
        //poner espera antes de quitar la barrera para no curzar cunado no esta listo
        exitWall.SetActive(false);
        TpState = true;
        }
        
    }
    public void TpClose()
    {
        if (TpState)
        {
        anim.SetBool("puenteon", false);
        exitWall.SetActive(true);
        TpState = false;
        }
        
    }
}
