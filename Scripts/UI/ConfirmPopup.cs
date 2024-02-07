using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ConfirmPopup : MonoBehaviour
{
    public static ConfirmPopup Instance;
    private Action _currentAction = null;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    public void Show(Action action)
    {
        _currentAction = action;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Confirm()
    {
        if (_currentAction != null)
            _currentAction();
        Hide();
    }

    public void Decline()
    {
        _currentAction = null;
        Hide();
    }
    
}
