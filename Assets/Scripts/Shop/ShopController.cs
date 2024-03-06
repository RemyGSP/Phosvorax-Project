using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//El script lo que hace es al empezar chequea el nivel que tiene el jugador el cual esta guardado en un singleton llamado ShopManager y entonces deja el menu preparado para poder ser usado
//Para poder confirmar los cambios habra que darle a un boton de compra que ejecutara el ConfirmPurchase y entonces mandara los datos al singleton ShopManager
//TODA LA INFORMACION DE LAS MEJORAS COMPRADAS SE SACA DE ShopManager, TODAS LAS VARIABLES DE NIVEL EN ESTE SCRIPT SON PARA CONTROLAR PRECIOS Y COSAS VISUALES
public class ShopController : MonoBehaviour
{
    [SerializeField] private Image leveledUp;
    [SerializeField] private Image notLevelUp;
    [SerializeField] private GameObject healthSquareContainer;
    [SerializeField] private GameObject damageSquareContainer;
    [SerializeField] private GameObject speedSquareContainer;
    [SerializeField] private TextMeshProUGUI healthCost;
    [SerializeField] private TextMeshProUGUI speedCost;
    [SerializeField] private TextMeshProUGUI damageCost;
    [SerializeField] private TextMeshProUGUI totalCost;
    [SerializeField] private Button purchaseButton;
    private int healthlvlCost;
    private int speedlvlCost;
    private int damagelvlCost;
    private int currentCost;
    private int healthLevel;
    private int damageLevel;
    private int speedLevel;
    Image[] healthQuares;
    Image[] damageSquares;
    Image[] speedSquares;
    void Start()
    {
        currentCost = 0;
        damageLevel = ShopManager.instance.GetDamageLevel();
        healthLevel = ShopManager.instance.GetHealthLevel();
        speedLevel = ShopManager.instance.GetSpeedLevel();
        healthlvlCost = (int)Mathf.Floor(ShopManager.instance.GetHealthLevel() * 1.75f * 50 + 50);
        damagelvlCost = (int)Mathf.Floor(ShopManager.instance.GetDamageLevel() * 1.75f * 50 + 50);
        speedlvlCost = (int)Mathf.Floor(ShopManager.instance.GetSpeedLevel() * 1.75f * 50 + 50);
        healthCost.text = healthlvlCost.ToString();
        damageCost.text = damagelvlCost.ToString();
        speedCost.text = speedlvlCost.ToString();
        speedSquares = healthSquareContainer.GetComponentsInChildren<Image>();
        damageSquares = damageSquareContainer.GetComponentsInChildren<Image>();
        healthQuares = healthSquareContainer.GetComponentsInChildren<Image>();
        Debug.Log(damageSquares.Length);
        for (int i = 0; i < ShopManager.instance.GetHealthLevel(); i++)
        {
            healthQuares[i] = leveledUp;
        }
        for (int i = 0; i < ShopManager.instance.GetSpeedLevel(); i++)
        {
            speedSquares[i] = leveledUp;
        }
        for (int i = 0; i < ShopManager.instance.GetDamageLevel(); i++)
        {
            damageSquares[i] = leveledUp;
        }

    }

    //Añado el costo de cada mejora al carrito y cuando se confirme la compra si es que el jugador tiene suficiente dinero podra realizar la compra
    public void AddToBasket(int cost)
    {
        currentCost += cost;
        totalCost.text = currentCost.ToString();
        if (currentCost > CrystalController.instance.GetCrystalAmount())
        {
            totalCost.color = Color.red;
            purchaseButton.interactable = false;
        }
        else
        {
            totalCost.color = Color.white;
            purchaseButton.interactable = true;
        }
    }


    //Los siguientes metodos se dedican a controlar cuanto compras y a iluminar los cuadrados que marcan los niveles
    public void BuyHealth()
    {
        if (healthLevel < healthQuares.Length)
        {
            healthQuares[healthLevel] = leveledUp;
            healthLevel++;
            AddToBasket((int)Mathf.Floor(healthLevel * 1.75f * 50 + 50));
        }
    }
    public void BuySpeed()
    {
        if (speedLevel < speedSquares.Length)
        {
            speedSquares[speedLevel] = leveledUp;
            speedLevel++;
            AddToBasket((int)Mathf.Floor(speedLevel * 1.75f * 50 + 50));
        }
    }

    public void BuyDamage()
    {
        if (damageLevel < damageSquares.Length)
        {
            damageSquares[damageLevel] = leveledUp;
            damageLevel++;
            AddToBasket((int)Mathf.Floor(damageLevel * 1.75f * 50 + 50));
        }
    }

    public void ReduceDamage()
    {
        if (damageLevel > ShopManager.instance.GetDamageLevel() && damageLevel > 0)
        {
            damageSquares[damageLevel] = notLevelUp;
            damageLevel--;
            AddToBasket(-(int)Mathf.Floor(damageLevel++ * 1.75f * 50 + 50));

        }

    }

    public void ReduceSpeed()
    {
        if (speedLevel > ShopManager.instance.GetSpeedLevel() && speedLevel > 0)
        {
            speedSquares[speedLevel] = notLevelUp;
            speedLevel--;
            AddToBasket(-(int)Mathf.Floor(speedLevel++ * 1.75f * 50 + 50));
        }

    }

    public void ReduceHealth()
    {
        if (healthLevel > ShopManager.instance.GetHealthLevel() && healthLevel > 0)
        {
            healthQuares[healthLevel] = notLevelUp;
            healthLevel--; // Decrement healthLevel
            AddToBasket(-(int)Mathf.Floor(healthLevel++ * 1.75f * 50 + 50));
            Debug.Log(healthLevel);
        }

    }
    public void ConfirmPurchase()
    {

        if (currentCost < CrystalController.instance.GetCrystalAmount())
        {
            CrystalController.instance.ReduceCrystals(currentCost);
            ShopManager.instance.SetSpeedLevel(speedLevel);
            ShopManager.instance.SetDamageLevel(damageLevel);
            ShopManager.instance.SetHealthLevel(healthLevel);
        }
    }


    public void CancelPurchase()
    {
        speedLevel = ShopManager.instance.GetSpeedLevel();
        damageLevel = ShopManager.instance.GetDamageLevel();
        healthLevel = ShopManager.instance.GetHealthLevel();
    }

}
