using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public class UpgradeShopPopup : Popup
{
    [SerializeField] private Shop _shop;
    [SerializeField] private List<UpgradeShopItem> _upgradeShopItems;
    [SerializeField] private UpgradeShopItemView _upgradeShopItemViewPrefab;
    [SerializeField] private Transform _shopItemsParent;
    [SerializeField] private GameObject _model;

    [Inject] private UpgradesController _upgradesController;
    [Inject] private DiContainer _container;
    private List<UpgradeShopItemView> _upgradeShopItemViews = new List<UpgradeShopItemView>();

    protected override GameObject ObjectToShow => _model;

    public override void Show()
    {
        if(_upgradeShopItemViews.Count == 0)
            CreateViews();
        base.Show();
    }

    private void CreateViews()
    {
        foreach (var shopItem in _upgradeShopItems)
        {
            var viewObject = _container.InstantiatePrefab(_upgradeShopItemViewPrefab, _shopItemsParent);
            var view = viewObject.GetComponent<UpgradeShopItemView>();
            view.Initialize(shopItem);
            _upgradeShopItemViews.Add(view);
        }
    }
}
