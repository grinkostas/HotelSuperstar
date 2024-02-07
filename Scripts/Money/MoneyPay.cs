using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class MoneyPay : View
{
    [Inject] private Player _player;

    private void OnEnable()
    {
        _player.Animator.PayingMoneyChanged += OnPayingMoneyChanged;
        Hide();
    }

    private void OnDisable()
    {
        _player.Animator.PayingMoneyChanged -= OnPayingMoneyChanged;
    }

    private void OnPayingMoneyChanged(bool isPaying)
    {
        Model.gameObject.SetActive(isPaying);
    }
}
