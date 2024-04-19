using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetBestAbilityToUse : MonoBehaviour
{
    public Abilities[] abilities;
    [SerializeField] float distanceToBeConsideredFarAway;
    private Vector3 playerPos;
    private bool canDecide = true;

    void Start()
    {
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

    }

    private void SortAbilityOnMinRange(Vector3 playerPos)
    {


        System.Array.Sort(abilities, (x, y) =>
        {
            return x.range.CompareTo(y.range);
        });

        int index = System.Array.IndexOf(abilities, abilities[0]);
        Abilities closestAbility = abilities[index];

        abilities[index].points++;
    }

    private void SortAbilityOnMaxRange(Vector3 playerPos)
    {
        System.Array.Sort(abilities, (x, y) =>
        {
            return y.range.CompareTo(x.range);
        });

        int index = System.Array.IndexOf(abilities, abilities[0]);
        Abilities farthestAbility = abilities[index];

        abilities[index].points++;
    }

    private void SortAbilityOnMaxDuration()
    {

        System.Array.Sort(abilities, (x, y) =>
        {
            return y.duration.CompareTo(x.duration);
        });


        int index = System.Array.IndexOf(abilities, abilities[0]);
        Abilities longestDurationAbility = abilities[index];

        abilities[index].points++;
    }

    private void SortAbilityOnMinDuration()
    {

        System.Array.Sort(abilities, (x, y) =>
        {
            return x.duration.CompareTo(y.duration);
        });

        int index = System.Array.IndexOf(abilities, abilities[0]);
        Abilities shortestDurationAbility = abilities[index];

        abilities[index].points++;
    }

    private void IncrementPointsForAreaAbilities()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            if (abilities[i].range > 0)
            {
                abilities[i].points++;
            }
        }
    }

    public void ResetArrays()
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            abilities[i].points = 0;
        }
    }

    public void SetCanDecide(bool aux)
    {
        canDecide = aux;
    }

    public Abilities[] getAbilityArrayWithPoints()
    {
        {

            // Copiar el array de habilidades original para no modificar el original
            Abilities[] sortedAbilities = abilities.Clone() as Abilities[];

            // Ordenar el array de habilidades basado en los puntos de mayor a menor
            System.Array.Sort(sortedAbilities, (x, y) => y.points.CompareTo(x.points));

            return sortedAbilities;
        }
    }

    public Abilities[] getAbilityArray()
    {
        return abilities;

    }
}

[System.Serializable]
public class Abilities
{
    public float range;
    public bool isAreaAbility;
    public float duration;
    public int points;
    public int abilityIndex;
    // el nombre es mas que nada para no confundir las habilidades
    public string name;
    public int index;

    public Abilities(float range, bool isAreaAbility, float duration, string name)
    {
        this.range = range;
        this.isAreaAbility = isAreaAbility;
        this.duration = duration;
        this.name = name; 
    }
}
