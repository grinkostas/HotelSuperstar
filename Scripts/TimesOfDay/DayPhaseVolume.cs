using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class DayPhaseVolume : TimeOfDayBase
{
    [SerializeField] private Volume _volume;
    [SerializeField] private float _duration;

    protected override void OnSuitablePhase()
    {
        StartCoroutine(Animations.ValueFadeRoutine(0, 1, (value) => _volume.weight = value, _duration));
    }

    protected override void OnNotSuitablePhase()
    {
        StartCoroutine(Animations.ValueFadeRoutine(1, 0, (value) => _volume.weight = value, _duration));
    }
}
