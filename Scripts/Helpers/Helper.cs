using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class Helper : InteractableCharacter, IBuyable
{
    [Header("General Helper Setteings")]
    [SerializeField] private NpcMovement _movement;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private bool _employAtStart = false;
    [SerializeField] private bool _limitedEmployTime = true;
    [SerializeField, ShowIf(nameof(_limitedEmployTime))] private float _duration;
    [SerializeField, HideIf(nameof(_employAtStart))] private float _price;
    public float Price => _price;
    public bool IsEmployed { get; protected set; }

    public UnityAction Employed;
    public UnityAction Dismissed;
    
    public Transform StartPoint => _startPosition;
    public NavMeshAgent Agent => _movement.Agent;
    public NpcMovement Movement => _movement;

    private void Start()
    {
        if(_employAtStart)
            Employ();
    }

    public void Employ()
    {
        IsEmployed = true;
        Employed?.Invoke();
        if(_limitedEmployTime)
            StartCoroutine(EmployTimer());
        OnEmploy();
    }

    public void Dismiss()
    {
        IsEmployed = false;
        StopCoroutine(EmployTimer());
        Agent.SetDestination(_startPosition.position);
        Dismissed?.Invoke();
        OnDismiss();
    }
    
    private IEnumerator EmployTimer()
    {
        yield return new WaitForSeconds(_duration);
        Dismiss();
    }
    
    protected abstract void OnEmploy();
    protected abstract void OnDismiss();
    public void Buy()
    {
        Employ();
    }
}
