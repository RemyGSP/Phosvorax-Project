using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlatfomrSwitch
{
    [SerializeField] private GameObject[] switchObject;
    [SerializeField] public Vector3 PlatfromCustomDirection;
    private int[] NumberofSwitchpressed;

    public void InitializeButtonArray()
    {
        NumberofSwitchpressed = new int[switchObject.Length];
    }

    public bool MultiswitchVerification(int buttonid)
    {
        SwitchListEditor(buttonid);
        for (int i = 0; i < NumberofSwitchpressed.Length; i++)
        {
            if (NumberofSwitchpressed[i] != 1)
            {
                return false;
            }
        }
    return true; 
    }
    public void ArrayToZero(){
        for (int i = 0; i < NumberofSwitchpressed.Length; i++)
        {
            NumberofSwitchpressed[i] = 0;
        }
    }

     private void SwitchListEditor(int buttonid)
    {
        NumberofSwitchpressed[buttonid] = 1;
    }
    
}


