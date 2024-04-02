using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour
{  
    // Update is called once per frame
    void Update()
    {
        if (PrefabRoomInstancier.isMapGenerated)
        {
            Destroy(gameObject);
        }
    }
}
