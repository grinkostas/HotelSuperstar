using System;
using UnityEngine;
using UnityEngine.Events;


public abstract class Dispenser : Place, Interactable 
{
    [SerializeField] private Item _item;
    [SerializeField] private Transform _givePoint;
    [SerializeField] private InteractableCharacterZone _interactableCharacterZone;

    [SerializeField] private Shaker _shaker;
    
    private bool _isPlayerInside = false;
    public InteractableCharacterZone InteractableCharacterZone => _interactableCharacterZone;

    public Transform GivePoint => _givePoint;
    public Item Item => _item;
    public UnityAction TookItem;
    
    protected abstract bool Takeable();
    protected abstract Item Take();

    private bool TryTake(out Item item)
    {
        item = null;
        if (Takeable() == false)
            return false;

        item = Take();
        TookItem?.Invoke();
        return true;
    }

    public void Interact(InteractableCharacter character)
    {
        if (TryTake(out Item item))
        {
            _shaker.Shake();
            
            character.TryToGiveItem(item);
        }
    }

    public bool CanInteract(InteractableCharacter character) => character.CanTakeItem && Takeable();
 
}
