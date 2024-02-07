using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Zenject;

public class ZoneInteractStep : TutorialStep
{
    [SerializeField] private bool _isPlayerZone;
    [SerializeField, HideIf(nameof(_isPlayerZone))] private InteractableCharacterZone _interactableCharacterZone;
    [SerializeField, ShowIf(nameof(_isPlayerZone))] private InteractablePlayerZone _interactablePlayerZone;

    [Inject] private Player _player;

    
    protected override void OnEnter()
    {
        _player.EnableInteract();
        Subscribe();
    }

    private void Subscribe()
    {
        if (_isPlayerZone)
        {
            _interactablePlayerZone.gameObject.SetActive(true);
            _interactablePlayerZone.Interacted += OnInteracted;
        }
        else
        {
            _player.ZoneDestination = _interactableCharacterZone;
            _interactableCharacterZone.gameObject.SetActive(true);
            _interactableCharacterZone.Interacted += OnInteracted;
        }
    }

    private void UnSubscribe()
    {
        if (_isPlayerZone)
            _interactablePlayerZone.Interacted -= OnInteracted;
        else
            _interactableCharacterZone.Interacted -= OnInteracted;
    }

    private void OnInteracted()
    {
        UnSubscribe();
        _player.ZoneDestination = null;
        NextStep();
    }
}
