using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBestAbilityToUse : MonoBehaviour
{
    [SerializeField] Abilities[] ability;
    [SerializeField] int[] abilityPoints;
    [SerializeField] float distanceToBeConsideredFarAway;
    private Vector3 playerPos;
    private bool canDecide = true;

    void Start()
    {
        abilityPoints = new int[ability.Length];
    }

    void Update()
    {
        playerPos = PlayerReferences.instance.GetPlayerCoordinates();

        if (canDecide)
        {
            StartCoroutine(SelectBestAbility());
            canDecide = false;
        }
    }

    private IEnumerator SelectBestAbility()
    {
        yield return new WaitForSeconds(1f);
        canDecide = true;
        float distance = Vector3.Distance(transform.position, playerPos);
        if (distance < distanceToBeConsideredFarAway)
        {
            SortAbilityOnMinRange(playerPos);
            SortAbilityOnMinDuration();
            IncrementPointsForAreaAbilities();
        }
        else
        {
            SortAbilityOnMaxRange(playerPos);
            SortAbilityOnMaxDuration();
        }

        GetBestAbility();
    }

    private void SortAbilityOnMinRange(Vector3 playerPos)
    {
        // Copiar el arreglo de puntos de habilidades para evitar modificar el original
        int[] tempPoints = new int[abilityPoints.Length];
        abilityPoints.CopyTo(tempPoints, 0);

        // Ordenar el arreglo de puntos de habilidades por la distancia a playerPos
        System.Array.Sort(tempPoints, (x, y) =>
        {
            float distanceX = Vector3.Distance(transform.position, playerPos);
            float distanceY = Vector3.Distance(transform.position, playerPos);
            return distanceX.CompareTo(distanceY);
        });

        // Encontrar la habilidad más cercana
        int index = System.Array.IndexOf(abilityPoints, tempPoints[0]);
        Abilities closestAbility = ability[index];
        Debug.Log("Closest Ability: " + closestAbility.name);

        // Incrementar puntos de la habilidad más cercana
        abilityPoints[index]++;
    }

    private void SortAbilityOnMaxRange(Vector3 playerPos)
    {
        // Copiar el arreglo de puntos de habilidades para evitar modificar el original
        int[] tempPoints = new int[abilityPoints.Length];
        abilityPoints.CopyTo(tempPoints, 0);

        // Ordenar el arreglo de puntos de habilidades por la distancia a playerPos en orden inverso
        System.Array.Sort(tempPoints, (x, y) =>
        {
            float distanceX = Vector3.Distance(transform.position, playerPos);
            float distanceY = Vector3.Distance(transform.position, playerPos);
            return distanceY.CompareTo(distanceX);
        });

        // Encontrar la habilidad más lejana
        int index = System.Array.IndexOf(abilityPoints, tempPoints[0]);
        Abilities farthestAbility = ability[index];
        Debug.Log("Farthest Ability: " + farthestAbility.name);

        // Incrementar puntos de la habilidad más lejana
        abilityPoints[index]++;
    }

    private void SortAbilityOnMaxDuration()
    {
        // Copiar el arreglo de puntos de habilidades para evitar modificar el original
        int[] tempPoints = new int[abilityPoints.Length];
        abilityPoints.CopyTo(tempPoints, 0);

        // Ordenar el arreglo de puntos de habilidades por la duración de las habilidades en orden inverso
        System.Array.Sort(tempPoints, (x, y) =>
        {
            float durationX = ability[x].duration;
            float durationY = ability[y].duration;
            return durationY.CompareTo(durationX);
        });

        // Encontrar la habilidad con la mayor duración
        int index = System.Array.IndexOf(abilityPoints, tempPoints[0]);
        Abilities longestDurationAbility = ability[index];
        Debug.Log("Ability with Longest Duration: " + longestDurationAbility.name);

        // Incrementar puntos de la habilidad con la mayor duración
        abilityPoints[index]++;
    }

    private void SortAbilityOnMinDuration()
    {
        // Copiar el arreglo de puntos de habilidades para evitar modificar el original
        int[] tempPoints = new int[abilityPoints.Length];
        abilityPoints.CopyTo(tempPoints, 0);

        // Ordenar el arreglo de puntos de habilidades por la duración de las habilidades
        System.Array.Sort(tempPoints, (x, y) =>
        {
            float durationX = ability[x].duration;
            float durationY = ability[y].duration;
            return durationX.CompareTo(durationY);
        });

        // Encontrar la habilidad con la menor duración
        int index = System.Array.IndexOf(abilityPoints, tempPoints[0]);
        Abilities shortestDurationAbility = ability[index];
        Debug.Log("Ability with Shortest Duration: " + shortestDurationAbility.name);

        // Incrementar puntos de la habilidad con la menor duración
        abilityPoints[index]++;
    }

    private void IncrementPointsForAreaAbilities()
    {
        for (int i = 0; i < ability.Length; i++)
        {
            // Verificar si la habilidad actual es una habilidad de rango
            if (ability[i].range > 0)
            {
                // Incrementar puntos para habilidades de rango
                abilityPoints[i]++;
            }
        }
    }


    private void GetBestAbility()
    {
        // Crear un arreglo de índices para mantener el orden original de las habilidades
        int[] indexes = new int[ability.Length];
        for (int i = 0; i < ability.Length; i++)
        {
            indexes[i] = i;
        }

        // Ordenar el arreglo de índices según la cantidad de puntos de habilidades
        System.Array.Sort(indexes, (x, y) => abilityPoints[y].CompareTo(abilityPoints[x]));

        // Crear una lista temporal para almacenar las habilidades ordenadas por puntos
        List<Abilities> sortedAbilities = new List<Abilities>();
        foreach (int index in indexes)
        {
            sortedAbilities.Add(ability[index]);
        }

        // Reasignar las habilidades en orden de puntos al arreglo original
        for (int i = 0; i < ability.Length; i++)
        {
            ability[i] = sortedAbilities[i];
        }
    }

    public void SetCanDecide(bool aux)
    {
            canDecide = aux;
    }
}

[System.Serializable]
public class Abilities
{
    public float range;
    public bool isAreaAbility;
    public float duration;

    // el nombre es mas que nada para no confundir las habilidades
    public string name;

    public Abilities(float range, bool isAreaAbility, float duration, string name)
    {
        this.range = range;
        this.isAreaAbility = isAreaAbility;
        this.duration = duration;
        this.name = name;
    }
}
