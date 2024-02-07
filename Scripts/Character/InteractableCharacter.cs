using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class InteractableCharacter : MonoBehaviour
{
    [SerializeField] private Stack _stack;
    [SerializeField] private UpgradeSelect _handCapacity;
    [Inject] private UpgradesController _upgradesController;
    private List<Item> _itemsInHands = new List<Item>();
    private List<object> _interactDisablers = new List<object>();

    public InteractableCharacterZone ZoneDestination { get; set; }
    public Stack Stack
    {
        get => _stack;
        protected set => _stack = value;
    }
    public int Capacity => (int)_upgradesController.GetValue(_handCapacity);
    public bool CanTakeItem => Capacity > _itemsInHands.Count;
    public bool CanInteract { get; private set; }

    private void Awake()
    {
        CanInteract = true;
    }

    public bool CanUseItem(Item item)
    {
        return Stack.CanUseItem(item);
    }
    
    public bool TryUseItem(Item item, Transform itemDestination = null)
    {
        var takenItem = Stack.TryTake(item, itemDestination);
        if (takenItem == null)
            return false;

        _itemsInHands.Remove(takenItem);
        OnUseItem();
        
        return true;
    }

    protected virtual void OnUseItem()
    {
    }
    
    public bool TryToGiveItem(Item item)
    {
        if(CanTakeItem == false)
            return false;
        
        _itemsInHands.Add(item);
        Stack.Put(item);
        return true;
    }

    public void DropItems()
    {
        Stack.DropAll();
        _itemsInHands.Clear();
    }


    public void DisableInteractForSeconds(object sender, float duration)
    {
        StartCoroutine(DisablingInteract(sender, duration));
    }

    private IEnumerator DisablingInteract(object sender, float duration)
    {
        _interactDisablers.Add(sender);
        DisableInteract();
        yield return new WaitForSeconds(duration);
        _interactDisablers.Remove(sender);
        if(_interactDisablers.Count == 0)
            EnableInteract();
    }


    public void DisableInteract()
    {
        CanInteract = false;
    }
    public void EnableInteract()
    {
        CanInteract = true;
    }
    
}
