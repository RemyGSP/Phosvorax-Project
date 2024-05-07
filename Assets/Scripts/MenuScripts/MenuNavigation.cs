using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuNavigation : MonoBehaviour
{
    [SerializeField] private GameObject[] menus;
    private int currentMenu;
    private List<int> previousMenus;
    [SerializeField] private InputAction goBackAction;
    [SerializeField] private bool closable;
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
        menus[currentMenu].SetActive(false);
        menus[menu].SetActive(true);
        previousMenus.Add(currentMenu);
        currentMenu = menu;

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
            previousMenus[SearchLastMenu() -1 ] = -1;
        }
        if (SearchLastMenu() == 0 && closable)
        {
            menus[currentMenu].SetActive(false);
        }
    }

}
