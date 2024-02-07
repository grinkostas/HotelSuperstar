using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class HintView : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    [Inject] private Player _player;

    private void OnEnable()
    {
        _player.Movement.IsMovingChanged += OnPlayerMove;
    }

    private void OnPlayerMove(bool isMoving)
    {
        if(isMoving == false) return;
        _model.SetActive(false);
    }
}
