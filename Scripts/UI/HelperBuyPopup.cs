using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;

public class HelperBuyPopup : BuyPopup
{
    [SerializeField] private GameObject _model;
    [SerializeField] private InteractablePlayerZone _playerZone;
    protected override GameObject ObjectToShow => _model;

    public UnityAction Hired;

    protected override void OnSuccessfulBuy()
    {
        Hired?.Invoke();
        Hide();
    }

    public override void Hide()
    {
        base.Hide();
        _playerZone.WaitNextInteract();
    }
}
