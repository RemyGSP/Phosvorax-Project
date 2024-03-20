using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGenerator : MonoBehaviour
{
    public GameObject laser;
    public GameObject laserOriginParticle;


    public void ActivateLaser()
    {
        laser.SetActive(true);
        laserOriginParticle.SetActive(true);
    }

    public void DeactivateLaser()
    {
        laser.SetActive(false);
        laserOriginParticle.SetActive(false);
    }

    public bool GetLaserState()
    {
        return laser.activeSelf;
    }
}
