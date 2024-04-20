using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitColorFeedback : MonoBehaviour
{
    [SerializeField] private Material changeMaterial;
    [SerializeField] private GameObject visuals;
    [SerializeField] private GameObject[] visualsMultiple;
    private Material previousMaterial;
    private bool isCurrentlyOnFeedback;
    [SerializeField] private float firstHalfPercentage;
    [SerializeField] private float secondHalfPercentage;
    [SerializeField] private bool notUniqueMaterial;
    private void Start()
    {
        changeMaterial = Instantiate(changeMaterial);
        previousMaterial = visuals.GetComponent<SkinnedMeshRenderer>().material;
    }
    // Update is called once per frame
    public void PlayHitFeedback(float timeToVanish)
    {
        //List<Material> materials = new List<Material>();
        if (!isCurrentlyOnFeedback)
        {
            if (!notUniqueMaterial)
            {

            StartCoroutine(_ChangeMaterialOverTime(timeToVanish));
            }
            else
            {

            }
        }
        //materials.Add(previousMaterial);
        //materials.Add(changeMaterial);
        //visuals.GetComponent<MeshRenderer>().SetMaterials(materials);
    }

    private IEnumerator _ChangeMaterialOverTime(float timeToVanish)
    {
        isCurrentlyOnFeedback = true;
        List<Material> materials = new List<Material>();
        materials.Add(previousMaterial);
        materials.Add(Instantiate(changeMaterial));
        SkinnedMeshRenderer meshRenderer = visuals.GetComponent<SkinnedMeshRenderer>();
        meshRenderer.materials = materials.ToArray();
        float duration;
        if (firstHalfPercentage == 0)
        {
            duration = timeToVanish / 1.5f; // Half the time to vanish for fade in and fade out
        }
        else
        {
            duration  = timeToVanish / (1 / firstHalfPercentage);
        }
        float alpha = 1;
        float emissionIntensity = 10;
        Color color = Color.white;
        color.a = alpha;
        meshRenderer.materials[1].color = color;

        Color emissionColor = Color.white; // Assuming initial emission color is white
        emissionColor *= emissionIntensity;
        meshRenderer.materials[1].SetColor("_EmissionColor", emissionColor);

        yield return new WaitForSeconds(duration);

        if (firstHalfPercentage == 0)
        {
            duration = timeToVanish / 2.5f; // Half the time to vanish for fade in and fade out
        }
        else
        {
            duration = timeToVanish / (1 / secondHalfPercentage);
        }
        Color finalColor = Color.red;
        finalColor.a = 1f;
        meshRenderer.materials[1].color = finalColor;

        Color finalEmissionColor = Color.red;
        finalEmissionColor *= 1f;
        meshRenderer.materials[1].SetColor("_EmissionColor", finalEmissionColor);
        alpha = 1;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            
            emissionIntensity = 10;

            color = Color.red;
            if (alpha == 0)
            {
                alpha = 1f;
                meshRenderer.materials[1].EnableKeyword("_EMISSION");
            }
            else
            {
                alpha = 0;
                meshRenderer.materials[1].DisableKeyword("_EMISSION");
            }
            color.a = alpha;
            meshRenderer.materials[1].color = color;
            emissionColor = Color.red;
            emissionColor *= emissionIntensity;
            meshRenderer.materials[1].SetColor("_EmissionColor", emissionColor);

            elapsedTime += Time.deltaTime + 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        // Ensure final values are set
        Color finalColorFadeOut = meshRenderer.materials[1].color;
        finalColorFadeOut.a = 0f;
        meshRenderer.materials[1].color = finalColorFadeOut;

        Color finalEmissionColorFadeOut = meshRenderer.materials[1].GetColor("_EmissionColor");
        finalEmissionColorFadeOut *= 0f;
        meshRenderer.materials[1].SetColor("_EmissionColor", finalEmissionColorFadeOut);
        isCurrentlyOnFeedback = false;
    }

    private IEnumerator _ChangeMultipleMaterialOverTime(float timeToVanish)
    {
        
        isCurrentlyOnFeedback = true;
        List<Material> materials = new List<Material>();
        materials.Add(previousMaterial);
        materials.Add(Instantiate(changeMaterial));
        SkinnedMeshRenderer meshRenderer = visualsMultiple[0].GetComponent<SkinnedMeshRenderer>();

        meshRenderer.materials = materials.ToArray();
        foreach (GameObject visual in visualsMultiple)
        {
            //visual.GetComponent<SkinnedMeshRenderer>().sharedMaterial =
        }
        float duration;
        if (firstHalfPercentage == 0)
        {
            duration = timeToVanish / 1.5f; // Half the time to vanish for fade in and fade out
        }
        else
        {
            duration = timeToVanish / (1 / firstHalfPercentage);
        }
        float alpha = 1;
        float emissionIntensity = 10;
        Color color = Color.white;
        color.a = alpha;
        meshRenderer.materials[1].color = color;

        Color emissionColor = Color.white; // Assuming initial emission color is white
        emissionColor *= emissionIntensity;
        meshRenderer.materials[1].SetColor("_EmissionColor", emissionColor);

        yield return new WaitForSeconds(duration);

        if (firstHalfPercentage == 0)
        {
            duration = timeToVanish / 2.5f; // Half the time to vanish for fade in and fade out
        }
        else
        {
            duration = timeToVanish / (1 / secondHalfPercentage);
        }
        Color finalColor = Color.red;
        finalColor.a = 1f;
        meshRenderer.materials[1].color = finalColor;

        Color finalEmissionColor = Color.red;
        finalEmissionColor *= 1f;
        meshRenderer.materials[1].SetColor("_EmissionColor", finalEmissionColor);
        alpha = 1;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {

            emissionIntensity = 10;

            color = Color.red;
            if (alpha == 0)
            {
                alpha = 1f;
                meshRenderer.materials[1].EnableKeyword("_EMISSION");
            }
            else
            {
                alpha = 0;
                meshRenderer.materials[1].DisableKeyword("_EMISSION");
            }
            color.a = alpha;
            meshRenderer.materials[1].color = color;
            emissionColor = Color.red;
            emissionColor *= emissionIntensity;
            meshRenderer.materials[1].SetColor("_EmissionColor", emissionColor);

            elapsedTime += Time.deltaTime + 0.05f;
            yield return new WaitForSeconds(0.05f);
        }

        // Ensure final values are set
        Color finalColorFadeOut = meshRenderer.materials[1].color;
        finalColorFadeOut.a = 0f;
        meshRenderer.materials[1].color = finalColorFadeOut;

        Color finalEmissionColorFadeOut = meshRenderer.materials[1].GetColor("_EmissionColor");
        finalEmissionColorFadeOut *= 0f;
        meshRenderer.materials[1].SetColor("_EmissionColor", finalEmissionColorFadeOut);
        isCurrentlyOnFeedback = false;
    }
}
