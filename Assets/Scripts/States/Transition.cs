using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Transition {
    [SerializeField] Decision decisionToBeMade;
    [SerializeField] States onDecisionTrueExitState;
    [SerializeField] States onDecisionFalseExitState;

    public States GetExitState() {
        return decisionToBeMade.Decide() ? onDecisionTrueExitState : onDecisionFalseExitState;
    }
}