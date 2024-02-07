using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class GuestInstaller : MonoInstaller
{
    [SerializeField] private GuestSpawner _spawner;
    [SerializeField] private Reception _reception;
    [SerializeField] private ExitPoint _exit;
    public override void InstallBindings()
    {
        Container.Bind<ExitPoint>().FromInstance(_exit);
        Container.Bind<Reception>().FromInstance(_reception);
        Container.Bind<GuestSpawner>().FromInstance(_spawner);
    }
}
