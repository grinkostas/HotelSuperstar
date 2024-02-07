using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class RequestStep : TutorialStep
{
    [SerializeField] private Request _request;
    [SerializeField] private ActionPlace _actionPlace;

    [Inject] private Player _player;
    protected override void OnEnter()
    {
        MakeRequest();
        _player.ZoneDestination = _request.InteractableCharacterZone;
        _request.End += OnRequestEnd;
    }

    private void MakeRequest()
    {
        if(_request.IsRequested)
            return;
        _request.MakeRequest();
    }

    private void OnRequestEnd(Request request)
    {
        _player.ZoneDestination = null;
        NextStep();
    }
}
