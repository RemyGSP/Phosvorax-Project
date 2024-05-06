using UnityEngine;
using System.Collections;
using Cinemachine;
using System.Linq.Expressions;

public class DoorTpController : MonoBehaviour
{
    [SerializeField] private GameObject destinationObject;
    [SerializeField] private GameObject exitWall;
    [SerializeField] private GameObject model;
    private bool isTeleporting = false;
    private Animator anim;

    private RooomController roomController;

    private void Start()
    {
        anim = model.GetComponent<Animator>();
        roomController = transform.parent.GetComponent<RooomController>();
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
            StartCoroutine(FadeInAndOut(0.4f));

        }
    }

    private IEnumerator FadeInAndOut(float time)
    {
        Camera.main.transform.parent.GetComponent<CinemachineStoryboard>().m_ShowImage = true;
        yield return new WaitForSeconds(time);
        Camera.main.transform.parent.GetComponent<CinemachineStoryboard>().m_ShowImage = false;
    }
        private IEnumerator Teleport(Collider other)
    {
        isTeleporting = true;

        // Teleport
        other.transform.position = destinationObject.transform.position;
        Camera.main.transform.position = destinationObject.transform.position;

        InformToSetPlayerInRoom(false);
        destinationObject.GetComponent<DoorTpController>().InformToSetPlayerInRoom(true);

        yield return null;

        isTeleporting = false;
    }


    public void InformToSetPlayerInRoom(bool ipir){
        roomController.SetIsPlayerInRoom(ipir);
    }


    public void TpOpen()
    {
        if (!anim.GetBool("puenteon"))
        {
            anim.SetBool("puenteon", true);
            // Llama a la función ActivateExitWall después de 2 segundos
            Invoke("ActivateExitWall", 2.8f);
        }
    }

    private void ActivateExitWall()
    {
        exitWall.SetActive(false);
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