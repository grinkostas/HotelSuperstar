using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InvertedPlaceStateView : View
{
    [SerializeField] private PlaceState _placeState;

    private void OnEnable()
    {
        _placeState.HideView += Show;
        _placeState.ShowView += Hide;
    }
    
    private void OnDisable()
    {
        _placeState.HideView -= Show;
        _placeState.ShowView -= Hide;
    }
}
