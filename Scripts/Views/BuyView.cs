using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class BuyView : View
{
    [SerializeField] private Place _place;
    [SerializeField] private BuyViewAction _buyViewAction = BuyViewAction.HideOnBuy;
    
    [SerializeField] private UnityEvent _showEvents;
    [SerializeField] private UnityEvent _hideEvents;


    public override void Show()
    {
        base.Show();
        _showEvents?.Invoke();
    }

    public override void Hide()
    {
        base.Hide();
        _hideEvents?.Invoke();
    }

    private void Start()
    {
        if (_place.IsBought)
        {
            if(_buyViewAction != BuyViewAction.ShowOnBuy)
                Hide();
            else
                Show();
            return;
        }
        
        if (_place.IsBought == false)
        {
            _place.Bought += OnPlaceBought;
            
            if (_buyViewAction == BuyViewAction.HideOnBuy)
            {
                Show();
                return;
            }
        }

        if (_place.IsAvailableToBuy == false)
        {
            if (_buyViewAction == BuyViewAction.HideOnAvailable)
                Show();
            else
                Hide();
            _place.AvailableToBuy += OnAvailableToBuy;
        }
        else
        {
            if (_buyViewAction == BuyViewAction.HideOnAvailable)
            {
                Hide();
            }

            if (_buyViewAction == BuyViewAction.ShowOnAvailable)
            {
                Show();
            }
            
            if (_buyViewAction == BuyViewAction.ShowOnBuy)
            {
                Hide();
            }
            else
            {
                Show();
            }

        }
    }

    private void OnAvailableToBuy()
    {
        _place.AvailableToBuy -= OnAvailableToBuy;
        if (_buyViewAction == BuyViewAction.HideOnAvailable)
        {
            Hide();
        }
        if (_buyViewAction == BuyViewAction.ShowOnAvailable)
        {
            Show();
        }
        _place.Bought += OnPlaceBought;
    }
    
    private void OnPlaceBought()
    {
        _place.Bought -= OnPlaceBought;
        
        if(_buyViewAction != BuyViewAction.ShowOnBuy)
            Hide();
        else
            Show();
    }

}

public enum BuyViewAction
{
    HideOnBuy,
    HideOnAvailable,
    ShowOnBuy,
    ShowOnAvailable
}

