using System.Collections;
using UnityEngine;

public class LaserGenerator : MonoBehaviour
{
    public GameObject laser;
    public GameObject laserOriginParticle;
    public GameObject laserOriginPoint;
    [SerializeField] private GameObject laserFinalPoint;
    private Material laserMaterial;
    [SerializeField] private float duration;
    LineRenderer lineRenderer;
    float originalLineZ = 10;

    private void Start()
    {
        lineRenderer = laser.GetComponentInChildren<LineRenderer>();
        laserMaterial = lineRenderer.material;
        if (laserMaterial == null)
        {
            Debug.LogError("Material not found on laser object!");
        }
    }
    public void ActivateLaser()
    {
        laser.SetActive(true);
        if(laserOriginParticle != null)
        {
            laserOriginParticle.SetActive(true);
        }
        
        StartCoroutine(ChangeScrollValueOverTime(2.5f, 0f));
    }

    public void DeactivateLaser()
    {
        laser.SetActive(false);

        if(laserOriginParticle != null)
        {
            laserOriginParticle.SetActive(false);
        }
        
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

    public void ChangeLaserValue(float startScrollValue, float targetScrollValue)
    {
        StartCoroutine(ChangeScrollValueOverTime(startScrollValue, targetScrollValue));
    }


    public Vector3 getFinalPointCoordinates()
    {
        return laserFinalPoint.transform.position;
    }

    public Vector3 getInitialPointCoordinates()
    {
        return laserOriginPoint.transform.position;
    }

    internal void SetLineRenderFinalDistance(float newLineRenderPointPositionZ)
    {
        Vector3 temp = lineRenderer.GetPosition(1);
        if (newLineRenderPointPositionZ == -1)
        {
            temp.z = originalLineZ;
        }
        else
        {
            temp.z = newLineRenderPointPositionZ;
        }
        lineRenderer.SetPosition(1, temp);
    }
}
