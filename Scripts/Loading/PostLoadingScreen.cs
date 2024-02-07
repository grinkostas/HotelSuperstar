using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine.UI;
using Zenject;

public class PostLoadingScreen : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _hideDelay;
    [SerializeField] private Slider _slider;
    [SerializeField, Range(0, 1)] private float _startSliderValue;
    
    [Inject] private Timer _timer;

    private void Start()
    {
        Animations.ValueFade(this, _startSliderValue, 1.0f, value => _slider.value = value, _hideDelay);
        _timer.ExecuteWithDelay(Hide, _hideDelay);
    }

    
    private void Hide()
    {
        Animations.ValueFade(this, 1, 0, Hide, _animationDuration);
    }

    private void Hide(float progress)
    {
        _canvasGroup.alpha = progress;
    }
}
