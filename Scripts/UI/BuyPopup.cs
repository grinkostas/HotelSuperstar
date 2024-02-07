using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Zenject;

public abstract class BuyPopup : Popup
{
    [SerializeField, RequireInterface(typeof(IBuyable))] private GameObject _objectToBuy;
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _priceText;

    private IBuyable _itemToBuy;

    [Inject] private Shop _shop;

    private void OnEnable()
    {
        _itemToBuy = _objectToBuy.GetComponent<IBuyable>();
        _buyButton.onClick.AddListener(BuyButtonClick);
    }

    private void OnDisable()
    {
        _buyButton.onClick.RemoveListener(BuyButtonClick);
    }


    private void BuyButtonClick()
    {
        if (_shop.Buy(_itemToBuy))
        {
            OnSuccessfulBuy();
        }
    }

    protected abstract void OnSuccessfulBuy();
    
    public override void Show()
    {
        base.Show();
        _buyButton.interactable = _shop.CanBuy(_itemToBuy);
        _priceText.text = _itemToBuy.Price.ToString();
    }
}
