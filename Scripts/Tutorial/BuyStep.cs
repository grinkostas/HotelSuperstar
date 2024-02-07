using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuyStep : TutorialStep
{
    [SerializeField] private GameObject _buyView;
    [SerializeField] private Place _placeToBuy;
    private bool _bought = false;
    protected override void OnEnter()
    {
        _placeToBuy.gameObject.SetActive(true);
        _buyView.gameObject.SetActive(true);
        if(_placeToBuy.IsBought == false)
            _placeToBuy.Bought += OnPlaceBought;
        else
            OnPlaceBought();
    }

    private void OnPlaceBought()
    {
        _placeToBuy.Bought -= OnPlaceBought;
        _bought = true;
        NextStep();
    }
}
