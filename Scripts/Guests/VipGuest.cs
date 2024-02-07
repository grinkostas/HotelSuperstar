using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VipGuest : MonoBehaviour
{
    [SerializeField] private Guest _guest;
    [SerializeField] private MoneyDropper _moneyDropper;
    private void OnEnable()
    {
        _guest.CheckedIn += OnGuestCheckedIn;
        _guest.Located += OnGuestPopulated;
        _guest.WalkToExit += OnGuestWalkToExit;
    }

    private void OnDisable()
    {
        _guest.CheckedIn -= OnGuestCheckedIn;
        _guest.Located -= OnGuestPopulated;
        _guest.WalkToExit -= OnGuestWalkToExit;
    }

    private void OnGuestCheckedIn(InteractablePlace place)
    {
        _moneyDropper.StartDrop();
    }

    private void OnGuestPopulated()
    {
        _moneyDropper.StopDrop();
    }

    private void OnGuestWalkToExit(Guest guest)
    {
        if(guest.WasLocated) _moneyDropper.StartDrop();
    }
}
