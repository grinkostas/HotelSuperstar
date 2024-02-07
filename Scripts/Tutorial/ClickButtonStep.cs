using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine.UI;
using Zenject;

public class ClickButtonStep : TutorialStep
{
    [SerializeField] private Button _buttonToClick;
    [SerializeField] private GameObject _model;
    [SerializeField] private float _enterDelay;

    [Inject] private Timer _timer;
    
    protected override void OnEnter()
    {
        PlayerArrow.Hide();
        DestroyAboveArrow();
        _timer.ExecuteWithDelay(EnterAction, _enterDelay);
    }

    private void EnterAction()
    {
        _model.SetActive(true);
        _buttonToClick.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _model.SetActive(false);
        NextStep();
    }
    
    
}
