using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine.Events;
using Zenject;

public abstract class TutorialStep : MonoBehaviour
{
    [SerializeField, HideIf(nameof(_isLastStep))] private TutorialStep _nextStep;
    [SerializeField] private bool _isLastStep;
    [SerializeField] private Transform _destination;

    [Inject] private AboveArrow _aboveArrowPrefab;
    [Inject] protected PlayerArrow PlayerArrow;
    [Inject] private DiContainer _container;
    [Inject] protected Tutorial Tutorial;
    
    private GameObject _currentArrow;

    private bool _entered = false;
    
    public Transform Destination => _destination;

    public UnityAction Entered;
    
    public void Enter()
    {
        if(Tutorial.IsRunning == false)
            return;
        if(_entered)
            return;
        _entered = true;
        Entered?.Invoke();
        ShowArrows();
        OnEnter();
    }

    protected abstract void OnEnter();
    private void ShowArrows()
    {
        PlayerArrow.Show();
        
        Debug.Log(this);
        PlayerArrow.SetDestination(Destination);
        if(_currentArrow == null)
            _currentArrow = _container.InstantiatePrefab(_aboveArrowPrefab, Destination);
    }
    protected void NextStep()
    {
        DestroyAboveArrow();
        if(_isLastStep)
            return;
        
        _nextStep.Enter();
        gameObject.SetActive(false);
    }
    
    protected void DestroyAboveArrow()
    {
        if(_currentArrow != null)
            Destroy(_currentArrow); 
    }
}
