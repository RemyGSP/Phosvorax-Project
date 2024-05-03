using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class StatueInteract : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactableTooltip;
    [SerializeField] private GameObject particles;
    private bool isUsed;
    [SerializeField] private GameObject glowingHead;
    [SerializeField] private Material nonGlowingMaterial;
    public void Interact(PlayerInteract player)
    {
        if (!isUsed)
        {
            isUsed = true;
            gameObject.GetComponent<CrystalDrop>().Drop();
            glowingHead.GetComponent<MeshRenderer>().sharedMaterial = nonGlowingMaterial;
            particles.SetActive(true);
            interactableTooltip.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        interactableTooltip.SetActive(false);
        isUsed = false;
        particles.SetActive(false);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isUsed)
        {
            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerInteract>().AddInteractable(this);

                interactableTooltip.SetActive(true);
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!isUsed)
        {

            if (other.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerInteract>().RemoveInteractable(this);
                interactableTooltip.SetActive(false);
            }

        }
    }

    public void StopInteract()
    {
    }
}
