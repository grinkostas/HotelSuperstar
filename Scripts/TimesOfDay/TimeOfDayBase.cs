using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public abstract class TimeOfDayBase : MonoBehaviour
{
    [SerializeField] private List<DayPhase> _actionDayPhases;
    [Inject] private DayTimeChanger _dayTimeChanger;

    private void OnEnable()
    {
        _dayTimeChanger.PhaseChanged += OnPhaseChanged;
    }

    private void OnDisable()
    {
        _dayTimeChanger.PhaseChanged -= OnPhaseChanged;
    }

    private void OnPhaseChanged(DayPhase phase)
    {
        if (_actionDayPhases.Contains(phase))
            OnSuitablePhase();
        else
            OnNotSuitablePhase();
    }

    protected abstract void OnSuitablePhase();
    protected abstract void OnNotSuitablePhase();
}
