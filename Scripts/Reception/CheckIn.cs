using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.UI;

public class CheckIn : View
{
    [SerializeField] private List<GameObject> _hideOnShow;
    [SerializeField] private Button _checkInButton;
    [SerializeField] private Reception _reception;


    private void OnEnable()
    {
        _reception.GuestArrived += OnGuestArrived;
    }

    private void OnDisable()
    {
        _reception.GuestArrived -= OnGuestArrived;
    }

    public override void Show()
    {
        base.Show();
        ActualizeView();
        _hideOnShow.ForEach(x=> x.SetActive(false));
        ActualizeView();
    }

    public override void Hide()
    {
        base.Hide();
        _hideOnShow.ForEach(x=> x.SetActive(true));
    }
    
    private void OnGuestArrived(Guest guest)
    {
        EnableToCheckIn();
    }

    public void CheckInButtonClick()
    {
        _reception.CheckIn();
        DisableCheckIn();
        //ActualizeView();
    }
    
    private void ActualizeView()
    {
        if (IsHidden)
            return;

        if (_reception.CanCheckIn())
        {
            EnableToCheckIn();
            return;
        }
        
        DisableCheckIn();
    }

    private void EnableToCheckIn()
    {
        _checkInButton.interactable = true;
    }

    private void DisableCheckIn()
    {
        _checkInButton.interactable = false;
    }
}
