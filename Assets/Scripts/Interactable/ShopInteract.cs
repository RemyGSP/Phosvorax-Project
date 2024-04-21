using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteract : MonoBehaviour, IInteractable
{

    [SerializeField] private GameObject shopCanvas;
    [SerializeField] private GameObject interactableTooltip;
    [SerializeField] private GameObject tendero;
    public void Interact(PlayerInteract player)
    {
        shopCanvas.SetActive(true);
        tendero.GetComponent<Animator>().SetBool("isOpen",true);
        PlayerReferences.instance.canMove = false;
        PlayerReferences.instance.GetPlayer().GetComponent<StateMachine>().enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        interactableTooltip.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerInteract>().AddInteractable(this);

            interactableTooltip.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerInteract>().RemoveInteractable(this);
            interactableTooltip.SetActive(false);
        }

    }

    public void StopInteract()
    {
        shopCanvas.SetActive(false);
        PlayerReferences.instance.canMove = true;
        tendero.GetComponent<Animator>().SetBool("isOpen", false);
        PlayerReferences.instance.GetPlayer().GetComponent<StateMachine>().enabled = true;
        GetComponent<ShopController>().RestartState();
    }
}
