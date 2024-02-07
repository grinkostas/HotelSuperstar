

[System.Serializable]
public class UpgradeProperty
{
    public float StartValue;
    public float Step;
    
    public Formula Formula;
    
    public float Calculate(int level)
    {
        float result = StartValue;
        switch (Formula)
        {
            case Formula.Plus:
                result = StartValue + Step * level;
                break;
            case Formula.Exponent:
                result = UnityEngine.Mathf.Pow(Step, level) * StartValue;
                break;
            case Formula.Minus:
                result = StartValue - Step * level;
                break;
        }

        return result;
    }
}
