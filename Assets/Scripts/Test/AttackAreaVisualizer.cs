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
    public void DrawAttackArea(float attackOffset, float attackRange)
    {
        areaFeedback.enabled = true;
        areaFeedback.rectTransform.sizeDelta = new Vector2(attackRange, attackRange);
        currentCastLimits = new Vector3(attackRange, 0, attackRange);
        DrawCursorIndicator(50f, 50f);
        CalculateRadiusInWorldCoordinates();

    }


    //Dandole el height y width activa el canvas de ra
    public void DrawLongRangeAbilityArea(float wideRange, float longRange)
    {
        areaFeedback.enabled = true;
        areaFeedback.rectTransform.sizeDelta = new Vector2(wideRange, longRange);
        currentCastLimits = new Vector3(wideRange, 0, longRange);
        DrawCursorIndicator(50f, 50f);
        CalculateRadiusInWorldCoordinates();

    }

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
        Debug.Log("Siguiendo");
        Vector3 cursorDir = PlayerReferences.instance.GetMouseTargetDir() - PlayerReferences.instance.GetPlayerCoordinates();
        cursorIndicator.transform.position = GetCursorPositionInsideBounds(cursorDir);
    }

    private Vector3 GetCursorPositionInsideBounds(Vector3 targetDir)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(areaFeedback.rectTransform, new Vector2(currentCastLimits.x, currentCastLimits.z), Camera.main, out Vector3 worldPoint);
        Vector3 circleCenterWorldPosition = areaFeedback.rectTransform.position;
        Vector3 aux = Vector3.zero;
        Vector3 playerPos = PlayerReferences.instance.GetPlayerCoordinates();
        aux = new Vector3(Mathf.Clamp(targetDir.x, -currentAreaRadius,currentAreaRadius), 0, Mathf.Clamp(targetDir.z,-currentAreaRadius,  currentAreaRadius));
        Debug.Log("Area Radius = " + currentAreaRadius + " Center Position = " + circleCenterWorldPosition + " Clamped Position = " + aux + " Cursor Position = " + targetDir);
        return aux + playerPos;
    }
     
    private void CalculateRadiusInWorldCoordinates()
    {
        Vector3[] corners = new Vector3[4];
        RectTransformUtility.ScreenPointToWorldPointInRectangle(areaFeedback.rectTransform,new Vector2(areaFeedback.rectTransform.rect.x, areaFeedback.rectTransform.rect.y),Camera.main,out corners[0]);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(areaFeedback.rectTransform, new Vector2(areaFeedback.rectTransform.rect.x, areaFeedback.rectTransform.rect.yMax), Camera.main, out corners[1]);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(areaFeedback.rectTransform,new Vector2(areaFeedback.rectTransform.rect.xMax, areaFeedback.rectTransform.rect.yMax),Camera.main,out corners[2]);
        RectTransformUtility.ScreenPointToWorldPointInRectangle(areaFeedback.rectTransform,new Vector2(areaFeedback.rectTransform.rect.xMax, areaFeedback.rectTransform.rect.y),Camera.main,out corners[3]);

        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];

        currentAreaRadius = Vector3.Distance(bottomLeft, topRight) * 0.5f;
    }
}

