using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class LaserGenerator : MonoBehaviour
{
    public GameObject laser;
    public GameObject laserOriginParticle;
    private Material laserMaterial;
    [SerializeField] private float duration;

    private void Start()
    {
        laserMaterial = laser.GetComponent<Renderer>().material;
        if (laserMaterial == null)
        {
            Debug.LogError("Material not found on laser object!");
        }
    }
    public void ActivateLaser()
    {
        laser.SetActive(true);
        laserOriginParticle.SetActive(true);
        StartCoroutine(ChangeScrollValueOverTime(2.5f,0f));
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

    private IEnumerator ChangeScrollValueOverTime(float startScrollValue, float targetScrollValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newScrollValue = Mathf.Lerp(startScrollValue, targetScrollValue, elapsedTime / duration);
            laserMaterial.SetFloat("_Dissolve", newScrollValue);
            elapsedTime += Time.deltaTime; // Incrementar elapsedTime
            yield return null;
        }
    }

    public float GetLaserLength()
    {
        Debug.Log("Laser lenght: "+ laser.GetComponentInChildren<Renderer>().bounds.size.z);
        return (laser.GetComponentInChildren<Renderer>().bounds.size.z) / 1f;
    }

}
