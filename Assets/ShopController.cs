using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//El script lo que hace es al empezar chequea el nivel que tiene el jugador el cual esta guardado en un singleton llamado ShopManager y entonces deja el menu preparado para poder ser usado
//Para poder confirmar los cambios habra que darle a un boton de compra que ejecutara el ConfirmPurchase y entonces mandara los datos al singleton ShopManager
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
    void Start()
    {
        currentCost = 0;
        damageLevel = ShopManager.instance.GetDamageLevel();
        healthLevel = ShopManager.instance.GetHealthLevel();
        speedLevel = ShopManager.instance.GetSpeedLevel();  
        healthlvlCost = (int) Mathf.Floor(ShopManager.instance.GetHealthLevel() * 1.75f * 50 + 50) ;
        damagelvlCost = (int)Mathf.Floor(ShopManager.instance.GetDamageLevel() * 1.75f * 50 + 50);
        speedlvlCost =  (int)Mathf.Floor(ShopManager.instance.GetSpeedLevel() * 1.75f * 50 + 50);
        healthCost.text = healthlvlCost.ToString();
        damageCost.text = damagelvlCost.ToString();
        speedCost.text = speedlvlCost.ToString();
        Image[] healthsQuares = healthSquareContainer.GetComponentsInChildren<Image>();
        Image[] damageSquares = healthSquareContainer.GetComponentsInChildren<Image>();
        Image[] speedSquares = healthSquareContainer.GetComponentsInChildren<Image>();
        for (int i = 0; i < ShopManager.instance.GetHealthLevel(); i++)
        {
            healthsQuares[i] = leveledUp;
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

    public void BuyHealth()
    {
        AddToBasket((int)Mathf.Floor(ShopManager.instance.GetHealthLevel() * 1.75f * 50 + 50));
        healthLevel++;
    }
    public void BuySpeed()
    {
        AddToBasket((int)Mathf.Floor(ShopManager.instance.GetSpeedLevel() * 1.75f * 50 + 50));
        speedLevel++;
    }

    public void BuyDamage()
    {
        AddToBasket((int)Mathf.Floor(ShopManager.instance.GetDamageLevel() * 1.75f * 50 + 50));
        damageLevel++;
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
