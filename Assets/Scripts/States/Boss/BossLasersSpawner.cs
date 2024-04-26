using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLasersSpawner : MonoBehaviour
{
    public GameObject laser;
    public GameObject laser2;


    LineRenderer lineRenderer;
    LineRenderer lineRenderer2;
    private Material laserMaterial;
    private Material laser2Material;

    private Collider laserCollider;
    private Collider laser2Collider;


    [Header("Lasers Spawn Values")]
    [SerializeField] float startScrollValue;
    [SerializeField] float endScrollValue;
    [SerializeField] float duration;
    void Start()
    {
        lineRenderer = laser.GetComponentInChildren<LineRenderer>();
        lineRenderer2 = laser.GetComponentInChildren<LineRenderer>();

        laserCollider = laser.GetComponentInChildren<Collider>();
        laser2Collider = laser.GetComponentInChildren<Collider>();
    }

    public void ActivateLasers()
    {
        laser.SetActive(true);
        laser2.SetActive(true);
        laserCollider.enabled = true;
        laser2Collider.enabled = true;

        StartCoroutine(ChangeScrollValueOverTime(startScrollValue, endScrollValue));
    }

    private IEnumerator ChangeScrollValueOverTime(float startScrollValue, float targetScrollValue)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float newScrollValue = Mathf.Lerp(startScrollValue, targetScrollValue, elapsedTime / duration);
            laserMaterial.SetFloat("_Dissolve", newScrollValue);
            laser2Material.SetFloat("_Dissolve", newScrollValue);
            elapsedTime += Time.deltaTime; // Incrementar elapsedTime
            yield return null;
        }
    }

    public void DeactivateLasers()
    {
        laser.SetActive(false);
        laser2.SetActive(false);
        laserCollider.enabled = false;
        laser2Collider.enabled = false;
    }

    public bool isLaser1Activated()
    {
        return laser.activeSelf;
    }

    public bool isLaser2Activated()
    {
        return laser2.activeSelf;
    }
}
