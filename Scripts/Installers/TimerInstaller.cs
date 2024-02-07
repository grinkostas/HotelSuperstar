using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class TimerInstaller : MonoInstaller
{
    [SerializeField] private Timer _timer;
    public override void InstallBindings()
    {
        Container.Bind<Timer>().FromInstance(_timer);
    }
}
