using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public class MoneyStack : MonoBehaviour
{
    [SerializeField] private MoneyNode _moneyNodePrefab;
    [SerializeField] private float _moneyGiveDelay;
    [SerializeField] private float _moneySpawnDelay;

    [SerializeField] private Vector2Int _stackSize;
    [SerializeField] private Transform _startPoint;

    private float _currentAmount;
    private List<MoneyNode> _moneyModels = new List<MoneyNode>();
    private Vector3Int _currentIndexes = Vector3Int.zero;

    private bool _playerInside = false;
    private Player _player;

    [Inject] private DiContainer _diContainer;
    [Inject] private Shop _shop;

    private float _takenMoney = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _takenMoney = 0;
            _playerInside = true;
            _player = player;
            StartCoroutine(GiveMoney());
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _playerInside = false;
            if(_takenMoney > 0)
                _shop.Balance.ShowEarnAnimation(_takenMoney);
            StopCoroutine(GiveMoney());
        }
    }

    private IEnumerator GiveMoney()
    {
        while (_playerInside)
        {
            TakeLast();
            yield return new WaitForSeconds(_moneyGiveDelay); 
        }
        
    }

    private void TakeLast()
    {
        if (_moneyModels.Count == 0)
        {
            if (_takenMoney > 0)
            {
                _shop.Balance.ShowEarnAnimation(_takenMoney);
                
                _takenMoney = 0;
            }
            return;
        }

        DecreaseIndexes();
        var money = _moneyModels.Last();
        _moneyModels.Remove(money);
        if(_moneyModels.Count == 0)
            _currentIndexes = Vector3Int.zero;
        
        money.Claim(_player);
        _takenMoney += money.Amount;
    }


    public void Add(float amount)
    {
        int modelsCount = (int)amount / (int)_moneyNodePrefab.Amount;
        if (modelsCount <= 0)
            return;

        StartCoroutine(SpawningMoney(modelsCount));
    }

    private IEnumerator SpawningMoney(int count)
    {
        
        for (int i = 0; i < count; i++)
        {
            SpawnStackOfMoney();
            yield return new WaitForSeconds(_moneySpawnDelay);
        }
    }
    
    
    private void SpawnStackOfMoney()
    {
        var node = _diContainer.InstantiatePrefab(_moneyNodePrefab, _startPoint);
        var money = node.GetComponent<MoneyNode>();
        _moneyModels.Add(money);
        money.transform.localPosition = GetStackPoint();
        IncreaseIndexes();
    }

    private void IncreaseIndexes()
    {
        if (_currentIndexes.x + 1 < _stackSize.x )
        {
            _currentIndexes += Vector3Int.right;
        }
        else 
        {
            if (_currentIndexes.z + 1 < _stackSize.y )
            {
                _currentIndexes.z += 1;
            }
            else
            {
                _currentIndexes.z = 0;
                _currentIndexes.y += 1;
            }
            _currentIndexes.x = 0;
        }
    }

    private void DecreaseIndexes()
    {
        _currentAmount = Mathf.Clamp(_currentAmount - (int)_moneyNodePrefab.Amount, 0, int.MaxValue);
        if (_currentIndexes.z - 1 > 0)
        {
            _currentIndexes.z -= 1;
            return;
        }

        if (_currentIndexes.x - 1 > 0)
        {
            _currentIndexes.z = _stackSize.y - 1;
            _currentIndexes.x -= 1;
            return;
        }

        _currentIndexes.y = Mathf.Clamp(_currentIndexes.y -1, 0, Int32.MaxValue);
        _currentIndexes.x = _stackSize.x - 1;
        _currentIndexes.z = _stackSize.y - 1;

    }
    
    private Vector3 GetStackPoint() 
    {
        var size = _currentIndexes;
        Vector3 positionDelta = _moneyNodePrefab.Center + _moneyNodePrefab.Size;
        Vector3 delta = Vector3.Scale(positionDelta, size);
        return delta;

    }
}
