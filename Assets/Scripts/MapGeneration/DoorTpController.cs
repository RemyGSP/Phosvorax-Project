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
        anim.SetBool("puenteon", true);
        //pogramar espera para que no pueda pasar antes de que suba el peunte.
        exitWall.SetActive(true);
    }
    public void TpClose()
    {
        anim.SetBool("puenteon", false);
        exitWall.SetActive(false);
    }
}
