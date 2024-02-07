using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialStepView : View
{
    [SerializeField] private TutorialStep _step;

    private void OnEnable()
    {
        _step.Entered += OnStepEntered;
    }

    private void OnDisable()
    {
        _step.Entered -= OnStepEntered;
    }

    private void OnStepEntered()
    {
        Show();
    }
}
