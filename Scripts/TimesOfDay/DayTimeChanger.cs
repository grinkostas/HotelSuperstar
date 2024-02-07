using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Zenject;

public class DayTimeChanger : MonoBehaviour
{
    [SerializeField] private List<DayPhase> _dayPhasesOrder;
    [SerializeField] private float _phaseDuration;

    [Inject] private Timer _timer;
    
    private int _currentPhaseIndex = -1;
    
    public UnityAction<DayPhase> PhaseChanged;

    private void Start()
    {
        NextPhase();
    }
    
    public void NextPhase()
    {
        _currentPhaseIndex++;
        if (_currentPhaseIndex >= _dayPhasesOrder.Count)
            _currentPhaseIndex = 0;
        
        PhaseChanged?.Invoke(_dayPhasesOrder[_currentPhaseIndex]);
        _timer.ExecuteWithDelay(NextPhase, _phaseDuration);
    }

    
    
}
