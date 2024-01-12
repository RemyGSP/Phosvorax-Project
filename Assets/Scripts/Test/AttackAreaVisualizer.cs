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
    private Vector3 currentCastLimits;
    private void Update()
    {
        if (isCasting)
        {

        }
    }
    public void DrawAttackArea(float attackOffset, float attackRange)
    {
        areaFeedback.enabled = true;
        areaFeedback.rectTransform.sizeDelta = new Vector2(attackRange, attackRange);
        currentCastLimits = new Vector3(attackRange,0,attackRange);
    }


    //Dandole el height y width activa el canvas de ra
    public void DrawLongRangeAbilityArea(float wideRange, float longRange)
    {
        areaFeedback.enabled = true;
        areaFeedback.rectTransform.sizeDelta =  new Vector2(wideRange,longRange);
        currentCastLimits = new Vector3(wideRange, 0, longRange);
    }

    public void DrawCursorIndicator(float cursorWidth, float cursorHeight)
    {
        cursorIndicator.SetActive(true);
        cursorIndicator.GetComponent<UnityEngine.UI.Image>().rectTransform.sizeDelta = new Vector2(cursorWidth, cursorHeight);
    }
    public void UndrawAttackArea()
    {
        areaFeedback.enabled = false;
    }
    private void ManageCursor()
    {
        Vector3 cursorDir = PlayerReferences.instance.GetMouseTargetDir();
        //if (cursorDir <)
    }

}

