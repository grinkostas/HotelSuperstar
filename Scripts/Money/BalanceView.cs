using System;
using UnityEngine;
using TMPro;

using System.Collections;
using System.Collections.Generic;

public class BalanceView : MonoBehaviour
{
    [SerializeField] private Balance _balance;
    [SerializeField] private TMP_Text _text;
    private void OnEnable()
    {
        _balance.Changed += OnBalanceChanged;
    }

    private void OnBalanceChanged()
    {
        _text.text = ((int)Mathf.Ceil(_balance.Amount)).ToString();
    }


}
