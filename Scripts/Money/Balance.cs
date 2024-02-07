using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class Balance : MonoBehaviour
{
    [SerializeField] private TMP_Text _earnedMoneyViewPrefab;
    [SerializeField] private RectTransform _earnedMoneyViewParent;
    [SerializeField] private Transform _balanceUiPoint;
    [SerializeField] private float _destroyTime = 0.9f;
    
    [Inject] private SignalHub _signalHub;
    
    private float _amount;
    public float Amount { 
        get => _amount;
        private set => _amount = Mathf.Ceil(value);
    }

    public Transform UiPoint => _balanceUiPoint;
    
    public UnityAction Changed;

    private void Start()
    {
        Amount = ES3.Load(SaveId.Balance, 0.0f);
        Changed?.Invoke();
        
        _signalHub.Get<Signals.EarnMoney>().On(Earn);
    }

    public void Earn(float amount)
    {
        if(amount < 0)
            return;
        Amount += amount;
        BalanceChanged();
    }

    public void ShowEarnAnimation(float earnAmount)
    {
        var text = Instantiate(_earnedMoneyViewPrefab, _earnedMoneyViewParent);
        text.text = "+" + Mathf.Ceil(earnAmount);
        Destroy(text.gameObject, _destroyTime);
    }
    
    public void Spend(float amount)
    {
        if(amount < 0)
            return;
        Amount = Mathf.Clamp(Amount - amount, 0.0f, float.MaxValue);
        BalanceChanged();
    }

    public bool CanBuy(IBuyable place)
    {
        return Amount >= place.Price;
    }

    private void BalanceChanged()
    {
        ES3.Save(SaveId.Balance, Amount);
        Changed?.Invoke();
    }
    
    
}
