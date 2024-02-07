using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UpgradesShopItemsInstaller : MonoInstaller
{
    [SerializeField] private List<UpgradeShopItem> _upgradeShopItems;
    
    public override void InstallBindings()
    {
        Container.Bind<List<UpgradeShopItem>>().FromInstance(_upgradeShopItems).NonLazy();
        foreach (var upgradeShopItem in _upgradeShopItems)
        {
            Container.BindInstance(upgradeShopItem);
        }
    }
}