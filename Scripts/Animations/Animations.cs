using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public static class Animations
{
    public static IEnumerator ColorFadeRoutine(Color startColor, Color destinationColor, UnityAction<Color> actualizeColor,  float duration)
    {
        var gradientProxy = new GradientProxy(GetGradient(startColor, destinationColor));
        return Animate(gradientProxy, actualizeColor, duration);
    }
    public static void ColorFade(MonoBehaviour sender, Color startColor, Color destinationColor, UnityAction<Color> actualizeColor,  
        float duration)
        => sender.StartCoroutine(ColorFadeRoutine(startColor, destinationColor, actualizeColor, duration));
    
    
    public static IEnumerator ValueFadeRoutine(float startValue, float endValue, UnityAction<float> actualize, float duration)
    {
        var curve = AnimationCurve.EaseInOut(0.0f, startValue, 1.0f, endValue);
        var curveProxy = new CurveProxy(curve);
        return Animate(curveProxy, actualize, duration);
    }
    public static void ValueFade(MonoBehaviour sender, float startValue, float endValue, UnityAction<float> actualize,
        float duration)
        => sender.StartCoroutine(ValueFadeRoutine(startValue, endValue, actualize, duration));



    private static IEnumerator Animate<T>(IEvaluatable<T> evaluatable, UnityAction<T> actualizeAction, float duration)
    {
        float wastedTime = 0.0f;
        while (wastedTime <= duration)
        {
            wastedTime += Time.deltaTime;
            actualizeAction(evaluatable.Evaluate(wastedTime / duration));
            yield return null;
        }
    }

    public static Gradient GetGradient(Color startColor, Color destinationColor)
    {
        var gradient = new Gradient();
        var colorKeys = new GradientColorKey[2];
        colorKeys[0].color = startColor;
        colorKeys[0].time = 0.0f;
        colorKeys[1].color = destinationColor;
        colorKeys[1].time = 1.0f;
        gradient.colorKeys = colorKeys;
        return gradient;
    }
}
