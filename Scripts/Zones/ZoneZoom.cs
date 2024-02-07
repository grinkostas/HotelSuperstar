using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameAnalyticsSDK.Setup;

public class ZoneZoom : MonoBehaviour
{
    [SerializeField] private ZoneBase _zone;
    [SerializeField] private GameObject _objectToZoom;
    [SerializeField] private Vector3 _scaleMultiplayer;
    [SerializeField] private float _duration;

    public Vector3 _startScale2 = Vector3.zero;
    
    private void OnEnable()
    {
        _zone.CharacterEnter += OnCharacterEnter;
        _zone.CharacterExit += OnCharacterExit;
        if (_startScale2 != Vector3.zero && _startScale2 != Vector3.negativeInfinity)
            _objectToZoom.transform.localScale = _startScale2;
    }

    private void Start()
    {
        _startScale2 = _objectToZoom.transform.localScale;
    }

    private void OnDisable()
    {
        _zone.CharacterEnter += OnCharacterEnter;
        _zone.CharacterExit += OnCharacterExit;
    }


    private void OnCharacterEnter()
    {
        if(_startScale2 == Vector3.zero) return;
        _objectToZoom.transform.DOScale(Vector3.Scale(_startScale2, _scaleMultiplayer), _duration);
    }

    private void OnCharacterExit()
    {
        if(_startScale2 == Vector3.zero) return;
        _objectToZoom.transform.DOScale(_startScale2, _duration);
    }
}
