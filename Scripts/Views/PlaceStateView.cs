using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class PlaceStateView : View
{
    [SerializeField] private PlaceState _placeState;
    [SerializeField] private UnityEvent _events;

    private void OnEnable()
    {
        _placeState.HideView += Hide;
        _placeState.ShowView += Show;
    }

    public override void Show()
    {
        base.Show();
        _events.Invoke();
    }

    private void Start()
    {
        Hide();
    }

    private void OnDisable()
    {
        _placeState.HideView -= Hide;
        _placeState.ShowView -= Show;
    }

   
}
