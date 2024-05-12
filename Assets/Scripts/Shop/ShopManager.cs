using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance { get; private set; }
    private int healthLevel;
    private int damageLevel;
    private int speedLevel;
    // Start is called before the first frame update
    void Start()
    {
        healthLevel = 0;
        damageLevel = 0;
        speedLevel = 0;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("El singleton ShopManager: " + gameObject.name);
            Destroy(gameObject);
        }

    }

    public int GetHealthLevel()
    {
        return healthLevel;
    }
    public int GetDamageLevel()
    {
        return damageLevel;
    }
    public int GetSpeedLevel()
    {
        return speedLevel;
    }

    public void SetHealthLevel(int healthLevel)
    {
        if (this.healthLevel < healthLevel)
        {
            PlayerReferences.instance.GetPlayer().gameObject.GetComponent<HealthBehaviour>().LevelUpHp();

        }
        this.healthLevel = healthLevel;

    }

    public void SetSpeedLevel(int speedLevel)
    {
        this.speedLevel = speedLevel;
    }

    public void SetDamageLevel(int damageLevel)
    {
        this.damageLevel = damageLevel;
    }



}
