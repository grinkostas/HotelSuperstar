using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using NepixCore.Game.API;
using Zenject;

public class Popup : MonoBehaviour
{
    [SerializeField] private bool _pauseOnShow = true;
    [SerializeField, ShowIf(nameof(_pauseOnShow))] protected PauseManager PauseManager;
    private float _previousTimeScale;
    [Inject] private IHapticService _hapticService;
    protected virtual GameObject ObjectToShow => gameObject;
    public virtual void Show()
    {
        ObjectToShow.SetActive(true);
        _hapticService.Selection();
        if(_pauseOnShow)
            PauseManager.Pause(this);
    }

    public virtual void Hide()
    {
        ObjectToShow.SetActive(false);
        if(_pauseOnShow)
            PauseManager.Resume(this);
    }
}
