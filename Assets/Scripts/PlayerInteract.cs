using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    //Aqui se pondran por orden los interactables que esten cerca y cuando pulses la E para interactuar ejecutara el metodo Interact del interactable mas cercano
    private List<IInteractable> interactableList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddInteractable(IInteractable interactable)
    {
        interactableList.Add(interactable);
    }

    public void RemoveInteractable(IInteractable interactable)
    {
        interactableList.Remove(interactable);
    }

    public void TryInteract() 
    {
        interactableList.First().Interact(this);
    }
}
