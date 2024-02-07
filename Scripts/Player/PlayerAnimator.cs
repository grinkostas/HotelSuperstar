using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using NepixCore.Game.API;
using UnityEngine.Events;
using Zenject;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Stack _hands;
    [SerializeField] private float _speedRatioWithItems = 0.8f;
    
    [AnimatorParam(nameof(_animator)), SerializeField] 
    private string _runningParameter;
    
    [AnimatorParam(nameof(_animator)), SerializeField] 
    private string _itemsInHandsParameter;

    [Header("Money Pay")] 
    [SerializeField] private Transform _moneySpawnStartPoint;
    [SerializeField] private GameObject _moneyBillPrefab;
    [SerializeField] private float _startAnimationDelay;
    [SerializeField] private float _moneyBillSpawnInterval;
    [SerializeField] private float _oneBillMoveDuration;

    [Inject] private Shop _shop;
    [Inject] private Player _player;
    [Inject] private Timer _timer;
    
    private Coroutine _coroutine;
    private bool _payingMoney = false;
    
    [AnimatorParam(nameof(_animator)), SerializeField] 
    private string _moneyPayParameter;

    [Inject] private IHapticService _hapticService;
    private float SpeedRatioLimit => IsItemsInHand ? _speedRatioWithItems : 1.0f;
    
    private bool IsItemsInHand => _hands.ItemsCount > 0;
    private bool _currentIdleParam = true;


    public UnityAction<bool> PayingMoneyChanged;

    private void Update()
    {
        Actualize();
    }

    private void Actualize()
    {
        UpdateAnimator();
    }
    

    private void UpdateAnimator()
    {
        var inputMagnitude =  Mathf.Clamp(_playerMovement.CurrentInput.magnitude, 0.0f, SpeedRatioLimit);
        _animator.SetFloat(_runningParameter, inputMagnitude);

        _animator.SetBool(_itemsInHandsParameter, IsItemsInHand);
    }

    public void PayMoney(Transform payMoneyPoint)
    {
        if(_coroutine == null)
            _coroutine = StartCoroutine(Paying(payMoneyPoint));
        _animator.SetBool(_moneyPayParameter, _shop.Balance.Amount > 1.0f);
    }
    
    private IEnumerator Paying(Transform payMoneyPoint)
    {
        yield return new WaitForSeconds(_startAnimationDelay);
        PayingMoneyChanged?.Invoke(true);
        _player.DropItems();
        while (_animator.GetBool(_moneyPayParameter) && _playerMovement.IsMoving == false)
        {
            if (_shop.Balance.Amount < 1.0f)
            {
                yield break;
            }
            _playerMovement.Rotate(payMoneyPoint);
            SpawnMoney(payMoneyPoint);
            yield return new WaitForSeconds(_moneyBillSpawnInterval);
        }

        StopPayMoney();
    }

    
    private void SpawnMoney(Transform payMoneyPoint)
    {
        var money = Instantiate(_moneyBillPrefab, _moneySpawnStartPoint);
        money.transform.LookAt(payMoneyPoint);
        money.transform.DOMove(payMoneyPoint.position, _oneBillMoveDuration);
        money.transform.DOShakeRotation(_oneBillMoveDuration);
        _timer.ExecuteWithDelay(()=>_hapticService.Selection(), _oneBillMoveDuration);
        Destroy(money, _oneBillMoveDuration);
    }
    
    public void StopPayMoney()
    {
        _coroutine = null;
        _animator.SetBool(_moneyPayParameter, false);
        PayingMoneyChanged?.Invoke(false);
    }
    
    
}
