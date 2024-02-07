
using UnityEngine;

public class GradientProxy : IEvaluatable<Color>
{
    private Gradient _gradient;
    public GradientProxy(Gradient gradient) => _gradient = gradient;
    public Color Evaluate(float t) => _gradient.Evaluate(t);
}
