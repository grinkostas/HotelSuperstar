using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.AI;

public class RequestHelper : Helper
{
    [SerializeField] private DispenserManager _dispenserManager;
    [SerializeField] private SignalHub _signalHub;
    [SerializeField] private Vector3 _rotationAtReturn;
    private List<Request> _requests = new List<Request>();
    private Request _currentRequest = null;
    private Dispenser _currentDispenser = null;

    private bool _isMakingRequest = false;
    private Direction _currentDirection = Direction.Afk;
    
    internal enum Direction
    {
        Afk,
        MovingToItem,
        MovingToRequest
    }


    private void OnEnable()
    {
        _signalHub.Get<Signals.NewCleaningRequest>().On(OnNewRequest);
        _signalHub.Get<Signals.RequestCompetedSignal>().On(OnRequestCompleted);
        
    }


    private void OnNewRequest(Request request)
    {
        if(_requests.Contains(request) == false)
            _requests.Add(request);
        if(IsEmployed == false)
            return;
        NextRequest();
    }

    private void OnRequestCompleted(Request request)
    {
        if (_requests.Contains(request))
            _requests.Remove(request);
        if (request == _currentRequest)
        {
            CompleteRequest();
        }
    }

    
    
    private void NextRequest()
    {
        if(_isMakingRequest)
            return;

        if (_requests.Count == 0 || IsEmployed == false)
        {
            Agent.SetDestination(StartPoint.position);
            Movement.LookAtDestination(_rotationAtReturn);
            return;
        }

        
        var availableRequests = _requests.FindAll(x=> x.IsRequested);

        if(availableRequests.Count == 0)
            return;

        Request request = availableRequests[0];
        if (Stack.ItemsCount > 0)
        {
            var requestWithHavingItems = availableRequests.Find(x => Stack.CanUseItem(x.RequiredItem));
            if (requestWithHavingItems != null)
            {
                request = requestWithHavingItems;
            }
            else if (Capacity <= Stack.ItemsCount)
            {
                DropItems();
            }
            
        }
        
        MakeRequest(request);
    }

    private void MakeRequest(Request request)
    {
        _currentRequest = request;
        _isMakingRequest = true;
        if (Stack.CanUseItem(request.RequiredItem))
        {
            MoveToRequest(request);
            return;
        }
        
        MoveToItem(request);
    }

    private void CompleteRequest()
    {
        _requests.Remove(_currentRequest);
        _currentRequest = null;
        _currentDispenser = null;
        _currentDirection = Direction.Afk;
        _isMakingRequest = false;
        Agent.SetDestination(StartPoint.position);
        
        if(IsEmployed == false)
            return;
        
        NextRequest();
    }


    private void MoveToRequest(Request request)
    {
        Agent.SetDestination(request.MakeRequestPoint.position);
        _currentDirection = Direction.MovingToRequest;
        ZoneDestination = request.InteractableCharacterZone;
    }

    private void MoveToItem(Request request)
    {
        _currentDirection = Direction.MovingToItem;
        _currentDispenser = _dispenserManager.GetItemDispenser(request.RequiredItem);
        Agent.SetDestination(_currentDispenser.GivePoint.position);
        ZoneDestination = _currentDispenser.InteractableCharacterZone;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out InteractableCharacterZone zone))
        {
            if (_currentDirection == Direction.MovingToItem)
            {
                if(_currentDispenser.InteractableCharacterZone != zone)
                    return;
                StartCoroutine(WaitItemAndMakeRequest(zone, _currentRequest));
            }
            else if (_currentDirection == Direction.MovingToRequest)
            {
                if(_currentRequest.InteractableCharacterZone != zone)
                    return;
                StartCoroutine(MakeRequest(zone));
            }
        }
    }

    private IEnumerator WaitItemAndMakeRequest(InteractableCharacterZone zone, Request request)
    {
        yield return new WaitForSeconds(zone.InteractTime * Capacity);
        MoveToRequest(request);
    }

    private IEnumerator MakeRequest(InteractableCharacterZone zone)
    {
        yield return new WaitForSeconds(zone.InteractTime);
        CompleteRequest();
    }
    protected override void OnEmploy()
    {
        NextRequest();
    }

    protected override void OnDismiss()
    {
        _isMakingRequest = false;
        DropItems();
    }
}
