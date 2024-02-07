using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class DayPhaseSkybox : TimeOfDayBase
{
    [SerializeField] private Material _skybox;
    [SerializeField] private float _duration = 0.5f;

    protected override void OnSuitablePhase()
    {
        ChangeSkybox();
    }

    private void ChangeSkybox()
    {
        var currentMaterial = RenderSettings.skybox;
        var colorPropertyName1 = currentMaterial.shader.GetPropertyName(0);
        var colorPropertyName2 = currentMaterial.shader.GetPropertyName(1);
        Animate(colorPropertyName1);
        Animate(colorPropertyName2);
    }

    private void Animate(string propertyName)
    {
        var currentMaterial = RenderSettings.skybox;
        StartCoroutine(Animations.ColorFadeRoutine(
            currentMaterial.GetColor(propertyName),
            _skybox.GetColor(propertyName),
            (color) => RenderSettings.skybox.SetColor(propertyName, color),
            _duration
        ));
    }
    
    

    protected override void OnNotSuitablePhase()
    {
    }
}
