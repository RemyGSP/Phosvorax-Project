using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour
{

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
        }
        return aux;
        }
    }
