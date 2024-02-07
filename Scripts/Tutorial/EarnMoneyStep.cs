using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class EarnMoneyStep : TutorialStep
{
    [SerializeField] private float _moneyToEarn;
    [Inject] private SignalHub _signalHub;
    private float _earnedMoney = 0.0f;
    
    protected override void OnEnter()
    {
        _signalHub.Get<Signals.EarnMoney>().On(OnEarned);
    }

    private void OnEarned(float earned)
    {
        _earnedMoney += earned;
        if (_earnedMoney >= _moneyToEarn)
        {
            NextStep();
        }
    }
}
