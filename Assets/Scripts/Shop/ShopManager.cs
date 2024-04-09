using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

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
        this.healthLevel = healthLevel;
        PlayerReferences.instance.GetPlayer().gameObject.GetComponent<HealthBehaviour>().LevelUpHp();
    }

    public void SetSpeedLevel(int speedLevel)
    {
        this.speedLevel = speedLevel;
    }

    public void SetDamageLevel(int damageLevel)
    {
        this.damageLevel = damageLevel;
    }


    [CustomEditor(typeof(ShopManager))]
    public class ExampleScriptEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Set Health Level"))
            {
                ShopManager.instance.SetHealthLevel(ShopManager.instance.GetHealthLevel() + 1);
            }
            if (GUILayout.Button("Set Damage Level"))
            {
                ShopManager.instance.SetDamageLevel(ShopManager.instance.GetDamageLevel()+ 1);
            }
            if (GUILayout.Button("Set Speed Level"))
            {
                ShopManager.instance.SetSpeedLevel(ShopManager.instance.GetSpeedLevel() + 1);
            }
        }
    }


}
