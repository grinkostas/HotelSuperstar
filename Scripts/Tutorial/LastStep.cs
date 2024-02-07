using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LastStep : TutorialStep
{
    protected override void OnEnter()
    { 
        Tutorial.End();
    }
}
