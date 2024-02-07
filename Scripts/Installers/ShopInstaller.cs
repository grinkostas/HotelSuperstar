using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class ShopInstaller : MonoInstaller
{
    [SerializeField] private Shop _shop;
    [SerializeField] private Balance _balance;

    public override void InstallBindings()
    {
        Container.Bind<Shop>().FromInstance(_shop);
        Container.Bind<Balance>().FromInstance(_balance);
    }
}
