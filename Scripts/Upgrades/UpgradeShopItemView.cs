using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class UpgradeShopItemView : MonoBehaviour, IBuyable
{
    [SerializeField] private Image _icon;
    [SerializeField] private UpgradeView _upgradeViewPrefab;
    [SerializeField] private Transform _upgradeViewParent;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _nameText;

    [SerializeField] private List<Button> _buyButtons;
    [SerializeField] private GameObject _unavailableModel;

    [SerializeField] private bool _initOnStart;
    [SerializeField, ShowIf(nameof(_initOnStart))] private UpgradeShopItem _shopItem;

    [Inject] private UpgradesController _upgradesController;
    [Inject] private SignalHub _signalHub;
    [Inject] private Shop _shop;
    
    private UpgradeShopItem _upgradeShopItem;

    public UpgradeShopItem ShopItem => _upgradeShopItem;
    private List<UpgradeView> _upgradeViews = new List<UpgradeView>();
    public float Price => _upgradeShopItem.GetPrice(_upgradesController);
    
    public void Initialize(UpgradeShopItem upgradeShopItem)
    {
        _icon.sprite = upgradeShopItem.Icon;
        _upgradeShopItem = upgradeShopItem;
        _nameText.text = _upgradeShopItem.name;
        foreach (var upgrade in _upgradeShopItem.Upgrades)
        {
            var view = Instantiate(_upgradeViewPrefab, _upgradeViewParent);
            view.Initialize(_upgradesController.GetModel(upgrade));
            _upgradeViews.Add(view);
            view.Actualize();
        }
        
        Actualize();
    }

    private void OnEnable()
    {
        if(_initOnStart)
            Initialize(_shopItem);
        
        foreach (var button in _buyButtons)
            if (button != null)
                button.interactable = _upgradeShopItem.IsAvailable;
        
        _unavailableModel.SetActive(!_upgradeShopItem.IsAvailable);
    }

    public void Actualize()
    {
        _priceText.text = _upgradeShopItem.GetPrice(_upgradesController).ToString();

        _upgradeViews.ForEach(x=>x.Actualize());
        
        if (_upgradeViews.Any(x => x.IsMax == false) == false)
        {
            _buyButtons.ForEach(x=>x.interactable = false);
            _priceText.text = "Max";
        }
    }

    public void BuyButton()
    {
        if(_upgradeShopItem.CanLevelUp(_upgradesController) == false)
            return;
        
        _shop.Buy(this);
        Actualize();
    }

    public void Buy()
    {
        foreach (var model in _upgradeShopItem.GetModels(_upgradesController))
        {
            _signalHub.Get<Signals.UpgradeLevelUp>().Dispatch(model.Upgrade);
        }
    }

    
}
