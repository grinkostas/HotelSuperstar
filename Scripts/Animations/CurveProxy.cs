
using UnityEngine;

public class CurveProxy : IEvaluatable<float>
{
    private AnimationCurve _animationCurve;
    public CurveProxy(AnimationCurve curve) => _animationCurve = curve;
    public float Evaluate(float t) => _animationCurve.Evaluate(t);
}
