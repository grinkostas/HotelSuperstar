using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu]
public class Upgrade : ScriptableObject
{
    public string Name;
    public UpgradeSelect UpgradeSelect;
    public UpgradeProperty Property;
    public int MaxLevel;

    public float GetValue(int level) => Property.Calculate(Mathf.Clamp(level, 0, MaxLevel));

    public string Id => UpgradeSelect.Text;
}

public enum Formula
{
    Plus,
    Exponent, 
    Minus
}
