using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class RequestView : View
{
    [SerializeField] private List<Request> _requests;
    [SerializeField] private UnityEvent _eventsOnStart;
    [SerializeField] private UnityEvent _eventsOnEnd;

    private void OnEnable()
    {
        foreach (var request in _requests)
        {
            request.End += OnRequestEnd;
            request.Requested += Show;
        }
    }


    public override void Show()
    {
        base.Show();
        _eventsOnStart.Invoke();
    }

    private void Start()
    {
        Hide();
    }

    private void OnDisable()
    {
        foreach (var request in _requests)
        {
            request.End -= OnRequestEnd;
            request.Requested -= Show;
        }
    }

    private void OnRequestEnd(Request request)
    {
        _eventsOnEnd.Invoke();
        Hide();
    }
    
    
}
