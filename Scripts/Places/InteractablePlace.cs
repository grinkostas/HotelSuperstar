using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine.Events;

public class InteractablePlace : Place
{
    [SerializeField] private InteractableState _interactableState;
    [SerializeField] private PlaceType _placeType;

    public PlaceType Type => _placeType;

    private void Start()
    {
        if (IsBought)
        {
            _interactableState.EnterPhase();
        }
    }
    
    protected override void OnBuy()
    {
        _interactableState.EnterPhase();
    }
    
    public void Reserve(Guest guest)
    {
        _interactableState.Reserve(guest);
    }
    
    public bool CanCheckIn()
    {
        return _interactableState.IsInteractable;
    }


    
}
