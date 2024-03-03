using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrystalController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI crystalDisplay;
    [SerializeField] private Animator crystalAnimator;
    private int crystalAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCrystalAmount()
    {
        return crystalAmount;
    }

    /// <summary>
    /// Dale un valor para sumar al la cantidad de moneda que tiene el jugador
    /// </summary>
    /// <param name="crystalAmount"></param>
    public void AddCrystals(int crystalAmount)
    {
        this.crystalAmount += crystalAmount;
        crystalDisplay.text = crystalAmount.ToString();
        UpdateDisplayAnimation();
    }

    /// <summary>
    /// Resta cristales a la cantidad de cristales 
    /// y devuelve true si es capaz de restar o false si no es capaz de restar
    /// </summary>
    /// <param name="crystalAmount"></param>
    /// <returns></returns>
    public bool ReduceCrystals(int crystalAmount)
    {
        bool aux = false;
        if (this.crystalAmount - crystalAmount < 0)
        {
            aux = true;
            this.crystalAmount -= crystalAmount;
            crystalDisplay.text = crystalAmount.ToString();
            UpdateDisplayAnimation();
        }
        return aux;
    }

    public void UpdateDisplayAnimation()
    {
        crystalAnimator.speed = 1 + (0.1f * crystalAmount);
    }
}
