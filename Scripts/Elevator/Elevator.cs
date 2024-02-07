using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Elevator : MonoBehaviour, Interactable, IBuyable
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private float _price;
    [SerializeField] private float _boughtTime;

    [SerializeField] private Popup _buyPopup;
    [SerializeField] private Popup _liftPopup;
    [SerializeField] private int _maxHaveInStackItems;
    
    private bool _isAvailable = false;
    private InteractableCharacter _currentInteractableCharacter;

    public float Price => _price;
    public float BoughtTime => _boughtTime;
    
    
    public void Elevate(Floor floor)
    {
        int itemsToDrop = _currentInteractableCharacter.Stack.ItemsCount - _maxHaveInStackItems;
        _currentInteractableCharacter.Stack.DropItems(itemsToDrop);
        
        floor.Elevate(_playerMovement);
    }
    
    public void Interact(InteractableCharacter character)
    {
        _currentInteractableCharacter = character;
        if(_isAvailable)
            _liftPopup.Show();
        else
            _buyPopup.Show();
    }

    public bool CanInteract(InteractableCharacter character)
    {
        return true;
    }

    public void Buy()
    {
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        _isAvailable = true;
        yield return new WaitForSeconds(_boughtTime);
        _isAvailable = false;
    }
    
    
}
