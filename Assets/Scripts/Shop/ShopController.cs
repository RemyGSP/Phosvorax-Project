using System;
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
    [SerializeField] private Sprite leveledUpSprite;
    [SerializeField] private Sprite notLevelUpSprite;
    [SerializeField] private TextMeshProUGUI totalCost;
    [SerializeField] private Button purchaseButton;

    private int currentCost;


    [SerializeField] Upgradeable[] upgradeables;


    public enum UpgradeableType { HEALTH, DAMAGE, SPEED };

    [System.Serializable]
    public class Upgradeable
    {
        public int[] levelCosts;
        public UpgradeableType type;
        [SerializeField] GameObject squareContainer;
        [SerializeField] protected TextMeshProUGUI upgradeCostUI;
        [SerializeField] protected int upgradeCost;
        protected int currentLevel;
        protected Image[] levelSquares;
        public int GetLevel()
        {
            return currentLevel;
        }

        public void SetLevel(int newLevel)
        {
            currentLevel = newLevel;
        }

        public void FillInfo(Sprite leveledUpSprite, Sprite notLeveledUpSprite)
        {
            float specificCost = 0;
            switch (type)
            {
                case UpgradeableType.HEALTH:
                    specificCost = ShopManager.instance.GetHealthLevel();
                    currentLevel = ShopManager.instance.GetHealthLevel();
                    break;
                case UpgradeableType.DAMAGE:
                    specificCost = ShopManager.instance.GetDamageLevel();
                    currentLevel = ShopManager.instance.GetDamageLevel();
                    break;
                case UpgradeableType.SPEED:
                    specificCost = ShopManager.instance.GetSpeedLevel();
                    currentLevel = ShopManager.instance.GetSpeedLevel();
                    break;
            }

            float commmonCost = 1.75f * 50 + 50;
            upgradeCost = (int)Mathf.Floor(specificCost * commmonCost);
            upgradeCostUI.text = upgradeCost.ToString();
            levelSquares = squareContainer.GetComponentsInChildren<Image>();

            for (int i = 0; i < levelSquares.Length; i++)
            {
                if (i <= currentLevel)
                    levelSquares[i].sprite = leveledUpSprite;
                else
                {
                    levelSquares[i].sprite = notLeveledUpSprite;
                }

            }

        }

        public Image[] GetSquares()
        {
            return levelSquares;
        }
    }

    void Start()
    {
        currentCost = 0;


        for (int i = 0; i < upgradeables.Length; i++)
        {
            upgradeables[i].FillInfo(leveledUpSprite, notLevelUpSprite);
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


    public void BuyUpgrade(UpgradeableType upgradeableType)
    {
        BuyUpgrade((int)upgradeableType);
    }

    public void BuyUpgrade(int upgradeIndex)
    {
        Upgradeable upgradeable = upgradeables[upgradeIndex];
        int level = upgradeable.GetLevel();
        Debug.Log(upgradeable.GetLevel());
        if (level < upgradeable.GetSquares().Length)
        {
            AddToBasket((int)Mathf.Floor(level * 1.75f * 50 + 50));

            upgradeable.GetSquares()[level].sprite = leveledUpSprite;
            upgradeable.SetLevel(level + 1);
        }
    }

    public void ReduceUpgrade(int upgradeIndex)
    {
        Upgradeable upgradeable = upgradeables[upgradeIndex];
        int level = upgradeable.GetLevel();
        Debug.Log(upgradeable.GetLevel());
        if ( level > ShopManager.instance.GetDamageLevel() && level > 0)
        {
            AddToBasket(-(int)Mathf.Floor((level - 1) * 1.75f * 50 + 50));
            upgradeable.GetSquares()[level - 1].sprite = notLevelUpSprite;
            upgradeable.SetLevel(level - 1);
            
        }
    }


    public void ConfirmPurchase()
    {

        if (currentCost < CrystalController.instance.GetCrystalAmount())
        {
            CrystalController.instance.ReduceCrystals(currentCost);
            currentCost = 0;
            totalCost.text = currentCost.ToString();
            totalCost.color = Color.white;
            ShopManager.instance.SetSpeedLevel(upgradeables[1].GetLevel());
            ShopManager.instance.SetDamageLevel(upgradeables[2].GetLevel());
            ShopManager.instance.SetHealthLevel(upgradeables[0].GetLevel());
        }
    }


    public void CancelPurchase()
    {

        upgradeables[0].SetLevel(ShopManager.instance.GetHealthLevel());
        upgradeables[1].SetLevel(ShopManager.instance.GetSpeedLevel());
        upgradeables[2].SetLevel(ShopManager.instance.GetDamageLevel());
    }

    public void RestartState()
    {
        currentCost = 0;
        totalCost.text = currentCost.ToString();
        totalCost.color = Color.white;
        upgradeables[0].SetLevel(ShopManager.instance.GetHealthLevel());
        upgradeables[1].SetLevel(ShopManager.instance.GetSpeedLevel());
        upgradeables[2].SetLevel(ShopManager.instance.GetDamageLevel());
        Debug.Log(ShopManager.instance.GetHealthLevel());
        Debug.Log(ShopManager.instance.GetSpeedLevel());
        Debug.Log(ShopManager.instance.GetDamageLevel());
        for (int i = 0; i < upgradeables.Length; i++)
        {
            Upgradeable upgradeable = upgradeables[i];
            int level = upgradeable.GetLevel();
            for (int a = 0; a < upgradeable.GetSquares().Length; a++)
            {
                if (upgradeable.GetSquares()[a].sprite == leveledUpSprite && a >= ShopManager.instance.GetDamageLevel())
                {
                    upgradeable.GetSquares()[a].sprite = notLevelUpSprite;
                }
            }
        }
    }

}
