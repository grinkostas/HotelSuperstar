using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Shop : MonoBehaviour
{
    [SerializeField] private Balance _balance;
    public Balance Balance => _balance;
    
    public bool Buy(IBuyable place)
    {
        if(_balance.CanBuy(place) == false)
            return false;
        
        _balance.Spend(place.Price);
        
        place.Buy();
        return true;
    }

    public static bool IsBought(string placeId)
    {
        return ES3.Load(placeId, new PlaceSave()).Bought;
    }
    
    public bool CanBuy(IBuyable place) => _balance.CanBuy(place);
}
