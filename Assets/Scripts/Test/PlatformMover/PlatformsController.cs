using UnityEngine;
using System.Collections;
using System.Linq;

public class PlatformsController : MonoBehaviour
{
    [SerializeField] private MovingPlatform[] platformList;
    [SerializeField] private PlatfomrSwitch[] switchesList;
    private bool platformsAreMoving;
    private static PlatformsController instance;


    public static PlatformsController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlatformsController>();
            }
            return instance;
        }
    }

    void Start()
    {
        foreach (MovingPlatform platform in platformList)
        {
            platform.InitializeStartingPosition();
        }
        foreach (PlatfomrSwitch button in switchesList)
        {
            button.InitializeButtonArray();
        }
    }
    public void ArePlatformsMoving()
    {
        

    }
   


    public void StartPlatformsMovement(int switchIndex)
    {
        string switchIndexStr = switchIndex.ToString();
        if (switchIndexStr.Length == 1)
        {
            foreach (MovingPlatform platform in platformList)
            {
                platform.PlatfomrMotionStarter(switchesList[switchIndex].PlatfromCustomDirection);
            }
        }
        else
        {
            int primerDigito = int.Parse(switchIndexStr[0].ToString());
            int otrosDigitos = int.Parse(switchIndexStr.Substring(1));
            switchesList[primerDigito].MultiswitchVerification(otrosDigitos);
            if (switchesList[primerDigito].MultiswitchVerification(otrosDigitos))
            {
                foreach (MovingPlatform platform in platformList)
                {
                    platform.PlatfomrMotionStarter(switchesList[primerDigito].PlatfromCustomDirection);      
                }
                switchesList[primerDigito].ArrayToZero();
            }

        }

    }
}
