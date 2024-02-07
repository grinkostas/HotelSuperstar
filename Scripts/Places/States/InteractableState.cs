using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class InteractableState : PlaceState
{
    [SerializeField] private Transform _interactPoint;

    private Guest _guest;
    
    public Transform InteractPoint => _interactPoint;
    public Guest CurrentGuest => _guest;
    public bool IsInteractable { get; private set; }

    public void Reserve(Guest guest)
    {
        _guest = guest;
        IsInteractable = false;
    }
    
    public bool TryEnter(Guest guest)
    {
        if (guest == _guest)
        {
            ExitPhase();
            return true;
        }
        return false;
        
    }
    
    public override void OnEnterPhase()
    {
        IsInteractable = true; 
    }

    public override void OnExitPhase()
    {
        IsInteractable = false;
    }
}
