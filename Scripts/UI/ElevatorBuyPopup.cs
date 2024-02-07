using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class ElevatorBuyPopup : BuyPopup
{
    [SerializeField] private GameObject _objectToShow;
    [SerializeField] private TMP_Text _durationText;
    [SerializeField] private Elevator _elevator;
    [SerializeField] private ElevatorLiftPopup _liftPopup;
    protected override GameObject ObjectToShow => _objectToShow;

    private void Start()
    {
        _durationText.text = _elevator.BoughtTime.ToString();
    }

    protected override void OnSuccessfulBuy()
    {
        Hide();
        _liftPopup.Show();
    }
}
