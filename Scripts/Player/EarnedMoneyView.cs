using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Zenject;

public class EarnedMoneyView : View
{
    [SerializeField] private TMP_Text _earnedMoneyText;
    [SerializeField] private float _waitToNextEarn;
    [Header("Animation")] 
    [SerializeField] private float _scaleAnimationDuration;
    
    [Inject] private SignalHub _signalHub;

    private float _currentEarnedMoney = 0.0f;
    private float _currentWaitTime = 0.0f;
    
    
    private bool _calculateWaitTime;
    private bool _showed = false;

    private void Update()
    {
        if(_calculateWaitTime)
            _currentWaitTime += Time.deltaTime;
        if (_currentWaitTime >= _waitToNextEarn)
            StopWait();
    }

    private void OnEnable()
    {
        Hide();
        _signalHub.Get<Signals.EarnMoney>().On(OnEarnedMoney);
    }

    public override void Show()
    {
        if(_showed == false) 
            Model.transform.DOScale(Vector3.one, _scaleAnimationDuration);
        _showed = true;
    }

    public override void Hide()
    {
        _showed = false;
        Model.transform.DOScale(Vector3.zero, _scaleAnimationDuration);
    }

    private void OnEarnedMoney(float earnedMoney)
    {
        if(_showed == false)
            Show();
        _currentWaitTime = 0.0f;
        _calculateWaitTime = true;
        _currentEarnedMoney += earnedMoney;
        _earnedMoneyText.text = $"+{_currentEarnedMoney}";
    }

    private void StopWait()
    {
        _calculateWaitTime = false;
        _currentEarnedMoney = 0.0f;
        _currentWaitTime = 0.0f;
        Hide();
    }

    
}
