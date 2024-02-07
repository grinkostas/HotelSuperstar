using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DayPhaseCamera : TimeOfDayBase
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Color _destinationColor;
    [SerializeField] private float _transitionDuration;

    protected override void OnSuitablePhase()
    {
        StartCoroutine(Animations.ColorFadeRoutine(_camera.backgroundColor, _destinationColor, ActualizeCameraColor, _transitionDuration));
    }

    private void ActualizeCameraColor(Color color) => _camera.backgroundColor = color;

    protected override void OnNotSuitablePhase()
    {
    }
}
