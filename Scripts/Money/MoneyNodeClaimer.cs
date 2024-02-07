using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class MoneyNodeClaimer : MonoBehaviour
{
    [SerializeField] private MoneyNode _moneyNode;
    [SerializeField] private float _pickupDelay;

    [Inject] private Player _player;

    private bool _isPlayerInside;
    private bool _claimed;
    private float _wastedTime = 0.0f;
    

    private void Update()
    {
        if (_wastedTime < _pickupDelay) _wastedTime += Time.deltaTime;
        else if (_isPlayerInside) TryToTake();
    }

    private void TryToTake()
    {
        if(_wastedTime < _pickupDelay) return;
        if(_claimed) return;
        _moneyNode.Claim(_player);
        _claimed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _isPlayerInside = true;
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _isPlayerInside = false;
        }
    }
}
