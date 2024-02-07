using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DropButton : View
{
    [SerializeField] private Stack _stack;

    private void OnEnable()
    {
        _stack.StackChanged += Actualize;
    }

    private void OnDisable()
    {
        _stack.StackChanged -= Actualize;
    }

    private void Start()
    {
        Actualize();
    }


    private void Actualize()
    {
        if(_stack.ItemsCount > 0)
            Show();
        else
            Hide();
    }
}
