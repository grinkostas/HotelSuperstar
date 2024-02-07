using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CleaningStep : ZoneInteractStep
{
    [SerializeField] private ActionState _actionState;
    protected override void OnEnter()
    {
        base.OnEnter();
        _actionState.ExitPhase();
    }
    
}
