using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class DayTimeChangerInstaller : MonoInstaller
{
    [SerializeField] private DayTimeChanger _dayTimeChanger;
    
    public override void InstallBindings()
    {
        Container.Bind<DayTimeChanger>().FromInstance(_dayTimeChanger);
    }
}
