using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class TutorialInstaller : MonoInstaller
{
    [SerializeField] private Tutorial _tutorial;
    [Header("Arrows")] 
    [SerializeField] private PlayerArrow _playerArrow;
    [SerializeField] private AboveArrow _aboveArrowPrefab;
    public override void InstallBindings()
    {
        Container.Bind<Tutorial>().FromInstance(_tutorial);
        Container.Bind<PlayerArrow>().FromInstance(_playerArrow);
        Container.Bind<AboveArrow>().FromInstance(_aboveArrowPrefab);
    }
}
