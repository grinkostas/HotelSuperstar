using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using NepixCore.Game.API;
using UnityEngine.Events;
using Zenject;

public class Stack : MonoBehaviour
{
    [SerializeField] private Transform _startStackPoint;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _duration;
    [SerializeField] private float _yOffset;
    [SerializeField] private Movement _movement;
    [SerializeField] private float _walkWithItemsSpeedMultiplier = 0.6f;

    private List<Item> _items = new List<Item>();

    [Inject] private IHapticService _hapticService;
    [Inject] private Timer _timer;
    public int ItemsCount => _items.Count;
    public UnityAction StackChanged;
    private Vector3 _itemLocalPosition = Vector3.zero;
   

    public void Put(Item item)
    {
        if(_items.Count == 0)
            _movement.ApplyMultiplayer(this, _walkWithItemsSpeedMultiplier);
        
        _timer.ExecuteWithDelay(()=>_hapticService.Selection(), _duration);
        StartCoroutine(AnimateItem(item, _itemLocalPosition, parent:_startStackPoint));
        AddToStack(item);
    }

    public void InstantiateInStack(Item itemPrefab)
    {
        var item = Instantiate(itemPrefab, _startStackPoint);
        AddToStack(item);
    }

    private void AddToStack(Item item)
    {
        _items.Add(item);
        StackChanged?.Invoke();
        item.transform.localPosition = _startStackPoint.transform.position + _itemLocalPosition;
        item.transform.localRotation = Quaternion.identity;
        _itemLocalPosition += item.StackSize.Size.y * Vector3.up;
    }

    private IEnumerator AnimateItem(Item item, Vector3 destinationPosition, bool localPosition = true, Transform parent = null, bool destroyOnEnd = false)
    {
        float progress = 0;
        float wastedTime = 0;
        if(parent != null)
            item.transform.SetParent(parent);
        else
            item.transform.SetParent(transform);
        var itemTransform = item.transform;
        Vector3 startPosition = localPosition ? itemTransform.localPosition : itemTransform.position;
        Vector3 moveDelta = destinationPosition - startPosition;
        Vector3 destination = startPosition + moveDelta;
        
        while (progress <= 1)
        {
            
            wastedTime += Time.deltaTime;
            progress = wastedTime / _duration;
            var position = Vector3.Lerp(startPosition, destination, progress);
            var yOffset = _yOffset * _curve.Evaluate(progress) * Vector3.up;
            var currentPosition = position + yOffset;
            if (localPosition)
                item.transform.localPosition = currentPosition;
            else
                item.transform.position = currentPosition;
            yield return null;
        }
        
        if(destroyOnEnd)
            Destroy(item.gameObject);
    }

    public bool CanUseItem(Item item)
    {
        if (item == null)
            return false;
        return _items.Count > 0 && _items.Find(x=> x!= null && x.ItemId == item.ItemId) != null;
    }
    
    public Item TryTake(Item item, Transform destination = null)
    {
        if (CanUseItem(item) == false)
            return null;
        
        var items = _items.Where(x => x.ItemId == item.ItemId);
        if (items.Any() == false)
            return null;

        var takedItem = items.Last();
        _items.Remove(takedItem);
        
        if(_items.Count == 0)
            _movement.RemoveMultiplayer(this, _walkWithItemsSpeedMultiplier);
        
        StartCoroutine(AnimateItem(takedItem, destination.transform.position,false, destination, true));
        
        _timer.ExecuteWithDelay(()=>_hapticService.Selection(), _duration);
        StackChanged?.Invoke();
        Rebuild();
        return takedItem;
    }
    
    private void Rebuild()
    {
        _itemLocalPosition = Vector3.zero;
        foreach (var item in _items)
        {
            item.transform.SetParent(_startStackPoint);
            var delta = item.StackSize.Size.y * Vector3.up;
            item.transform.localPosition = _itemLocalPosition;
            _itemLocalPosition += delta;
        }
    }

    public void DropAll()
    {
        while (_items.Count != 0)
        {
            DropItem();
        }

        Rebuild();
    }

    public void DropItems(int count)
    {
        if (count <= 0)
            return;
        
        for (int i = 0; i < count; i++)
            DropItem();
        
        
        Rebuild();
        
    }
    
    public void DropItem()
    {
        var item = _items[^1];
        
        Destroy(item.gameObject);
        _items.Remove(item);

        _hapticService.Selection();
        StackChanged?.Invoke();
        if(_items.Count == 0)
            _movement.RemoveMultiplayer(this, _walkWithItemsSpeedMultiplier);
        
            
    }
    
  
}
