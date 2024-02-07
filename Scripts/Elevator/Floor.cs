using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

public class Floor : Place
{
    [SerializeField] private Transform _elevatorPoint;
    [SerializeField] private Transform _elevatePoint;
    [SerializeField] private ElevatorDoor _leftDoor;
    [SerializeField] private ElevatorDoor _rightDoor;
    [SerializeField] private float _doorsOpenDelay;
    [SerializeField] private float _playerLeaveDuration;
    
    public bool IsAvailableToElevate => IsBought;
    public Transform ElevatePoint => _elevatePoint;

    public void Elevate(PlayerMovement movement)
    {
        StartCoroutine(Elevating(movement));
    }

    private IEnumerator Elevating(PlayerMovement movement)
    {
        movement.Move(_elevatorPoint);
        movement.Rotate(_elevatePoint);
        movement.MoveWithAnimation(_elevatorPoint.position, _doorsOpenDelay + _leftDoor.AnimationDuration, false);
        
        yield return new WaitForSeconds(_doorsOpenDelay);
        _leftDoor.Open();
        _rightDoor.Open();
        yield return new WaitForSeconds(_leftDoor.AnimationDuration);
        
        movement.MoveWithAnimation(_elevatePoint.position, _playerLeaveDuration, false);
        yield return new WaitForSeconds(_playerLeaveDuration);
        _leftDoor.Close();
        _rightDoor.Close();
    }
    

    protected override void OnBuy()
    {
    }
}
