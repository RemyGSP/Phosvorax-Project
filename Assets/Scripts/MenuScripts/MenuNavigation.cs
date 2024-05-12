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
    private bool canUseAnimation = true;

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
        if (canChangeMenu && canUseAnimation && transitionAnimation)
        {
            transitionAnimation.SetActive(false);
            transitionAnimation.SetActive(true);
            StartCoroutine(AnimateTransitionAndChangeMenu(menu));
            canUseAnimation = false;
        }
        else
        {
            ChangeMenuWithoutAnimation(menu);
        }
    }

    private void ChangeMenuWithoutAnimation(int menu)
    {
        menus[currentMenu].SetActive(false);
        menus[menu].SetActive(true);
        previousMenus.Add(currentMenu);
        currentMenu = menu;
        ResetPreviousMenus();
        canUseAnimation = true;
    }
    IEnumerator AnimateTransitionAndChangeMenu(int menu)
    {

        yield return new WaitForSeconds(1f);
        // Cambia al nuevo menú
        menus[currentMenu].SetActive(false);
        menus[menu].SetActive(true);
        previousMenus.Add(currentMenu);
        currentMenu = menu;
        ResetPreviousMenus();
        canUseAnimation = true;

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
        if (SearchLastMenu() > 0)
        {
            Debug.Log(SearchLastMenu());
            menus[currentMenu].SetActive(false);
            menus[SearchLastMenu() - 1].SetActive(true);
            currentMenu = SearchLastMenu() - 1;
            previousMenus.Remove(previousMenus.LastOrDefault());
            previousMenus[SearchLastMenu() - 1] = -1;
        }
        if (SearchLastMenu() == 0 && closable)
        {
            menus[currentMenu].SetActive(false);
        }
    }
}
