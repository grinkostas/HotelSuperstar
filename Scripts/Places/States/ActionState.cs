using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

public class ActionState : PlaceState
{
    [SerializeField] private float _interactableTime;
    [SerializeField] private ActionPlace _actionPlace;
    [SerializeField] private InteractableState _interactableState; 
    [SerializeField] private List<LimitedInTimeRequest> _requests;
    [Space]
    [SerializeField] private float _timeToTryRequest;
    [SerializeField] [Range(0,1)] private float _requestChance;

    [Inject] private Tutorial _tutorial;
    
    private LimitedInTimeRequest _currentRequest;
    private float _interactTime = 0;
    private bool _isStateActive = false;

    private bool _isRequested
    {
        get
        {
            bool result = false;
            _requests.ForEach(x=> result = result || x.IsRequested);
            return result;
        }
    }

    private bool Available => _interactTime <= _interactableTime;
    
    public LimitedInTimeRequest LastRequest { get; private set; }
    
    private void Update()
    {
        if(_isStateActive == false)
            return;
        
        if(_isRequested == false && _tutorial.IsRunning == false)
            _interactTime += Time.deltaTime;
        
        if(Available == false)
            ExitPhase();
    }

    public override void OnEnterPhase()
    {
        _isStateActive = true;
        _interactTime = 0;
        
        StartCoroutine(Request());
        _actionPlace.PlaceGuest(_interactableState.CurrentGuest);
    }


    private IEnumerator Request()
    {
        yield return new WaitForSeconds(_timeToTryRequest);
        if (TryRequest() == false)
            yield return Request();
    }

    private bool TryRequest()
    {
        int random = Random.Range(0, 101);
        if(_requestChance * 100 < random)
            return false;
        
        float availableTime = _interactableTime - _interactTime;
        var availableRequests = _requests.FindAll(x => 1.0f < availableTime && x.CanRequest());
        
        if(availableRequests.Count == 0)
            return false;
        
        var assignment = availableRequests[Random.Range(0, availableRequests.Count)];
        if (availableRequests.Count == 0) assignment = availableRequests[0];
        Request(assignment);
        return true;
    }

    private void Request(LimitedInTimeRequest request)
    {
        LastRequest = request;
        _currentRequest = request;
        _currentRequest.Done += OnRequestEnd;
        _currentRequest.End += OnRequestEnd;
        _currentRequest.MakeRequest();
        HideView?.Invoke();
    }

    private void OnRequestEnd(Request request)
    {
        if (_currentRequest != null)
        {
            _currentRequest.Done -= OnRequestEnd;
            _currentRequest.End -= OnRequestEnd;
        }
        _currentRequest = null;
        ShowView?.Invoke();
    }

    public override void OnExitPhase()
    {
        StopCoroutine(Request());
        _isStateActive = false;
        _actionPlace.RemoveGuest(_interactableState.CurrentGuest);
    }
}
