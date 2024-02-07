using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public class RequestsReceiver : MonoBehaviour
{
    [SerializeField] private RequestHint _requestHintPrefab;
    [SerializeField] private Transform _requestHintParent;
    
    [Inject] private SignalHub _signalHub;
    
    private List<Request> _currentRequests = new List<Request>();
    private List<RequestHint> _requestHints = new List<RequestHint>();


    private void Start()
    {
        _signalHub.Get<Signals.NewCleaningRequest>().On(OnRequestMade);
        _signalHub.Get<Signals.NewLimitedInTimeRequest>().On(OnNewLimitedInTimeRequest);
        _signalHub.Get<Signals.EndRequest>().On(OnRequestEnd);
    }

    private void OnRequestMade(Request request)
    {
        _currentRequests.Add(request);
    }

    private void OnNewLimitedInTimeRequest(LimitedInTimeRequest request)
    {
        if (_requestHints.Any(x => x.Item.ItemId == request.RequiredItem.ItemId) == false)
        {
            var requestHint = Instantiate(_requestHintPrefab, _requestHintParent);
            _requestHints.Add(requestHint);
            requestHint.AddRequest(request);
            return;
        }

        var hint = _requestHints.Find(x => request.RequiredItem.ItemId == x.Item.ItemId);
        hint.AddRequest(request);
    }

    private void OnRequestEnd(Request request)
    {
        _currentRequests.Remove(request);
    }
}
