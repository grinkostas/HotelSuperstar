using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.Events;

public class CheckInHelper : Helper
{
    [SerializeField] private Transform _activePosition;
    [SerializeField] private Reception _reception;

    [SerializeField] private float _startCheckInDelay;
    [SerializeField] private float _checkInDelay;

    [SerializeField] private Renderer _porter;
    [SerializeField] private Material _availableToBuyMaterial;
    [SerializeField] private Material _actionMaterial;

    protected override void OnEmploy()
    {
        StartCoroutine(CheckIn(_startCheckInDelay));
        Agent.SetDestination(_activePosition.position);
        Movement.LookAtDestination(new Vector3());
        _porter.material = _actionMaterial;
        StartCoroutine(CheckIn());
    }

    protected override void OnDismiss()
    {
        Movement.LookAtDestination(new Vector3());
        _porter.material = _availableToBuyMaterial;
    }
    

    private IEnumerator CheckIn(float additionalWait = 0)
    {
        while (IsEmployed)
        {
            yield return new WaitForSeconds(_checkInDelay + additionalWait);
            if(_reception.GuestToCheckIn != null)
                _reception.CheckIn(false);
        }
    }
    
    
}
