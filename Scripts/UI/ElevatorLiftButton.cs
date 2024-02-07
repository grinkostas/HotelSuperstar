using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ElevatorLiftButton : MonoBehaviour
{
    [SerializeField] private Floor _floor;

    private Button _button;

    public UnityAction<Floor> OnClick;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.interactable = _floor.IsAvailableToElevate;
        if (_floor.IsAvailableToElevate == false)
            _floor.Bought += OnFloorBought;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }
    
    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        OnClick?.Invoke(_floor);
    }
    private void OnFloorBought()
    {
        _floor.Bought -= OnFloorBought;
        _button.interactable = true;
    }
    
    

}
