using System;

[Serializable]
public class PlaceSave
{
    public bool Bought;
    public int BuyProgress;
    public bool IsDefault { get; private set; }

    public PlaceSave()
    {
        Bought = false;
        BuyProgress = 0;
        IsDefault = true;
    }

    public PlaceSave(bool bought, int buyProgress = 0)
    {
        Bought = bought;
        IsDefault = false;
        BuyProgress = buyProgress;
    }
}