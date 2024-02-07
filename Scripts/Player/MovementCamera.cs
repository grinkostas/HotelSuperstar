using System;
using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;

public class MovementCamera : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private CinemachineVirtualCamera _stayCamera;
    [SerializeField] private CinemachineVirtualCamera _movingCamera;

    private void OnEnable()
    {
        _playerMovement.IsMovingChanged += OnIsMovingChange;
    }

    private void OnDisable()
    {
        _playerMovement.IsMovingChanged -= OnIsMovingChange;
    }


    private void OnIsMovingChange(bool isMoving)
    {
        if (isMoving == false)
        {
            _stayCamera.gameObject.SetActive(true);
            _movingCamera.gameObject.SetActive(false);
        }
        else
        {
            _stayCamera.gameObject.SetActive(false);
            _movingCamera.gameObject.SetActive(true);
        }
    }
}
