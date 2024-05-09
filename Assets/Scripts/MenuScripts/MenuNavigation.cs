using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject transitionAnimation;
    [SerializeField] private GameObject[] menus;
    private int currentMenu;
    private List<int> previousMenus;
    [SerializeField] private InputAction goBackAction;
    [SerializeField] private bool closable;
    private bool canChangeMenu = true;

    void Start()
    {
        previousMenus = new List<int>();
        currentMenu = 0;
        ResetPreviousMenus();
        goBackAction.performed += GoBack;
    }

    private void OnEnable()
    {
        goBackAction.Enable();
    }

    private void OnDisable()
    {
        goBackAction.Disable();
    }

    public void ChangeMenu(int menu)
    {
        if (canChangeMenu)
        {
            // Activa el GameObject de transici�n y comienza su animaci�n
            transitionAnimation.SetActive(true);
            StartCoroutine(AnimateTransitionAndChangeMenu(menu));
        }
    }

    IEnumerator AnimateTransitionAndChangeMenu(int menu)
    {

        yield return new WaitForSeconds(1f);
        // Cambia al nuevo men�
        menus[currentMenu].SetActive(false);
        menus[menu].SetActive(true);
        previousMenus.Add(currentMenu);
        currentMenu = menu;
        ResetPreviousMenus();

        // Espera hasta que la animaci�n de transici�n termine
        yield return new WaitForSeconds(transitionAnimation.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

        // Desactiva el GameObject de transici�n una vez que la animaci�n haya terminado
        transitionAnimation.SetActive(false);
    }

    public int SearchLastMenu()
    {
        return previousMenus.LastOrDefault();
    }

    private void ResetPreviousMenus()
    {
        previousMenus.Clear();
    }

    public void GoBack(InputAction.CallbackContext action)
    {
        if (canChangeMenu && SearchLastMenu() > 0)
        {
            transitionAnimation.SetActive(true);
            StartCoroutine(AnimateTransitionAndGoBack());
        }
        else if (canChangeMenu && SearchLastMenu() == 0 && closable)
        {
            transitionAnimation.SetActive(true);
            StartCoroutine(AnimateTransitionAndClose());
        }
    }

    IEnumerator AnimateTransitionAndGoBack()
    {
        yield return new WaitForSeconds(transitionAnimation.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        transitionAnimation.SetActive(false);

        menus[currentMenu].SetActive(false);
        menus[SearchLastMenu()].SetActive(true);
        currentMenu = SearchLastMenu();
        previousMenus.RemoveAt(previousMenus.Count - 1);
        ResetPreviousMenus();
    }

    IEnumerator AnimateTransitionAndClose()
    {
        yield return new WaitForSeconds(transitionAnimation.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        transitionAnimation.SetActive(false);

        menus[currentMenu].SetActive(false);
    }
}
