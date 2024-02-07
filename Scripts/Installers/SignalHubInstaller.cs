using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class SignalHubInstaller : MonoInstaller
{
    [SerializeField] private SignalHub _signalHub;
    public override void InstallBindings()
    {
        Container.Bind<SignalHub>().FromInstance(_signalHub);
    }
}
