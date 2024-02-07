using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayPhaseLight : TimeOfDayBase
{
    [SerializeField] private Light _light;
    [SerializeField] private Color _destinationColor;
    [SerializeField] private float _intensity;
    [SerializeField] private float _duration;
    
    protected override void OnSuitablePhase()
    {
        StartCoroutine(Animations.ColorFadeRoutine(_light.color, _destinationColor, ActualizeColor, _duration));
        StartCoroutine(Animations.ValueFadeRoutine(_light.intensity, _intensity, ActualizeIntensity, _duration));
    }

    private void ActualizeColor(Color color) => _light.color = color;
    private void ActualizeIntensity(float intensity) => _light.intensity = intensity;

    protected override void OnNotSuitablePhase()
    {
    }
}
