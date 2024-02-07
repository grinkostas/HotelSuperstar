using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.Events;
using Zenject;

public class ZoomView : View, IAnimatedView
{
    [SerializeField] private float _startDelay;
    [SerializeField] private float _animationDuration;
    
    [SerializeField] private bool _zoomOnEnable;
    [SerializeField] private bool _zoomOnDisable;

    [SerializeField] private Vector3 _zoomInScale = Vector3.one;
    [SerializeField] private Vector3 _zoomOutScale = Vector3.zero;

    [Inject] private Timer _timer;
    
    public UnityAction Animated { get; set; }

    private void OnEnable()
    {
        if(_zoomOnEnable)
            Show();
    }

    private void OnDisable()
    {
        if (_zoomOnDisable)
        {
            base.Show();
            Hide();
        }
    }
    [Button("Show")]
    public override void Show()
    {
        //if(!IsHidden) return;
        Model.transform.localScale = _zoomOutScale;
        base.Show();
        _timer.ExecuteWithDelay( ZoomIn, _startDelay);
    }
    
    [Button("Hide")]
    public override void Hide()
    {
        _timer.ExecuteWithDelay( ZoomOut, _startDelay);
    }

    private void ZoomIn()
    {
        Model.transform.DOScale(_zoomInScale, _animationDuration);
    }
    
    private void ZoomOut()
    {
        Model.transform.DOScale(_zoomOutScale, _animationDuration);
        _timer.ExecuteWithDelay(base.Hide, _animationDuration);
    }


}
