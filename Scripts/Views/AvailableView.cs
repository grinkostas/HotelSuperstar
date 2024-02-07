using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AvailableView : View
{
    [SerializeField] private Place _place;

    private void Start()
    {
        if (_place.IsAvailableToBuy)
        {
            Hide();
            return;
        }
        else
        {
            _place.AvailableToBuy += OnAvailableToBuy;
        }
    }

    private void OnAvailableToBuy()
    {
        _place.AvailableToBuy -= OnAvailableToBuy;
        Hide();
    }
}
