using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using Zenject;

public class Generator : Dispenser, IProgressible
{
    [SerializeField] private UpgradeSelect _productionTimeSelect;
    [SerializeField] private UpgradeSelect _capacitySelect;
    [SerializeField] private Transform _spawnPoint;

    [Inject] private UpgradesController _upgradesController;
    
    private bool _isSpawning = false;
    private float ProductTime => _upgradesController.GetValue(_productionTimeSelect);
    private int Capacity => (int)CapacityModel.CurrentValue;
    
    private List<Item> _generatedItems = new List<Item>();

    public int GeneratedCount => _generatedItems.Count;
    public UpgradeModel CapacityModel => _upgradesController.GetModel(_capacitySelect);
    public UnityAction<float> ProgressChanged { get; set; }
    public UnityAction ItemsCountChanged;
    private void Start()
    {
        if(IsBought)
            StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        
        _isSpawning = true;
        float wastedTime = 0;
        while (wastedTime <= ProductTime)
        {
            wastedTime += Time.deltaTime;
            ProgressChanged?.Invoke(wastedTime/ProductTime);
            yield return null;
        }

        var item = Instantiate(Item);
        item.transform.position = _spawnPoint.position;
        _generatedItems.Add(item);
        
        ProgressChanged?.Invoke(0);
        ItemsCountChanged?.Invoke();
        
        if (_generatedItems.Count < Capacity)
        {
            yield return Spawn();
        }
        else
        {
            _isSpawning = false;
        }

        yield return null;
    }
    
    protected override bool Takeable()
    {
        return _generatedItems.Count > 0;
    }

    protected override Item Take()
    {
        var item = _generatedItems.Last();
        _generatedItems.Remove(item);
        ItemsCountChanged?.Invoke();

        if(_isSpawning == false)
            StartCoroutine(Spawn());
        return item;
    }

    protected override void OnBuy()
    {
        StartCoroutine(Spawn());
    }

}
