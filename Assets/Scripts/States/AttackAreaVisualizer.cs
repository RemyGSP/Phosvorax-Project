using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AttackAreaVisualizer : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image areaFeedback;
    [SerializeField] GameObject cursorIndicator;
    private bool isCasting;
    private float currentAreaRadius;
    private Vector3 currentCastLimits;
    private Vector3 lastPosition;



    private void Start()
    {
        isCasting = false;
    }
    private void Update()
    {
        if (isCasting)
        {
            ManageCursor();
        }
    }

    //Esto cambia el width y el height de la imagen que contiene el area del ataque
    public void DrawAttackArea(float attackOffset, float attackRange)
    {
        areaFeedback.enabled = true;
        areaFeedback.rectTransform.sizeDelta = new Vector2(attackRange, attackRange);
        currentCastLimits = new Vector3(attackRange, 0, attackRange);
        DrawCursorIndicator(50f, 50f);
        CalculateRadiusInWorldCoordinates();

    }


    public void DrawLineRangeAbilityArea(float wideRange, float longRange)
    {
        areaFeedback.enabled = true;
        areaFeedback.rectTransform.sizeDelta = new Vector2(wideRange, longRange);
        currentCastLimits = new Vector3(wideRange, 0, longRange);
        DrawCursorIndicator(50f, longRange);
        CalculateRadiusInWorldCoordinates();

    }

    //Activa y le da width y height a la imagen que contiene el cursor 
    public void DrawCursorIndicator(float cursorWidth, float cursorHeight)
    {
        cursorIndicator.SetActive(true);
        isCasting = true;
        cursorIndicator.GetComponent<UnityEngine.UI.Image>().rectTransform.sizeDelta = new Vector2(cursorWidth, cursorHeight);
    }
    public void UndrawAttackArea()
    {
        areaFeedback.enabled = false;
    }

    private void ManageCursor()
    {
        Vector3 cursorDir = PlayerReferences.instance.GetMouseTargetDir() - PlayerReferences.instance.GetPlayerCoordinates();
        cursorIndicator.transform.position = GetCursorPositionInsideBounds(cursorDir);
        lastPosition = cursorIndicator.transform.position - PlayerReferences.instance.GetPlayerCoordinates();
    }

    //Esto busca la posicion sin pasarse fuera del circulo del area
    public Vector3 GetCursorPositionInsideBounds(Vector3 targetDir)
    {
        Vector3 aux = Vector3.zero;
        Vector3 playerPos = PlayerReferences.instance.GetPlayerCoordinates();

        float distanceFromCenter = Vector3.Distance(Vector3.zero, targetDir);

        // Check if the cursor is outside the circle
        if (distanceFromCenter > currentAreaRadius)
        {
            // Move the cursor to the nearest point on the circle
            aux = targetDir.normalized * currentAreaRadius;
        }
        else
        {
            aux = targetDir;
        }
        aux.y = 0.1f;
        return aux + playerPos;
    }


    //Esto crea cuatro puntos y consigue el radio del circulo
    private void CalculateRadiusInWorldCoordinates()
    {
        Vector3[] corners = new Vector3[4];
        areaFeedback.rectTransform.GetWorldCorners(corners);
        float width = Vector3.Distance(corners[0], corners[3]);
        float height = Vector3.Distance(corners[0], corners[1]);
        currentAreaRadius = Mathf.Min(width, height) * 0.5f;

    }

    public void ActivateArea()
    {
        areaFeedback.gameObject.SetActive(true);
        cursorIndicator.gameObject.SetActive(true);
    }

    public void DeactivateArea()
    {
        areaFeedback.gameObject.SetActive(false);
        cursorIndicator.gameObject.SetActive(false);
    }
}

