using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TutorialUpgradesPopup : Popup
{
    [SerializeField] private GameObject _model;
    [SerializeField] private Button _upgradeButton;
    
    protected override GameObject ObjectToShow => _model;

    private void OnEnable()
    {
        _upgradeButton.onClick.AddListener(Hide);
    }
}
