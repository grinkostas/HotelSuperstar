using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Shaker : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _oneShakeDuration;
    [SerializeField] private float _shakeDelta;
    [SerializeField] private Vector3 _shakeDirection;
    [SerializeField] private float _shakeCount;

    private bool _isShaking = false;

    private Vector3 _startPosition;
    
    private void Awake()
    {
        _startPosition = _target.position;
    }

    public void Shake()
    {
        if (_isShaking == false)
            StartCoroutine(Shaking());
    }

    private IEnumerator Shaking()
    {
        _isShaking = true;
        int direction = 1;
        for (int i = 1; i < _shakeCount; i++)
        {
            _target.DOMove(_startPosition + (_shakeDirection * (direction * _shakeDelta)), _oneShakeDuration);
            direction *= -1;
            yield return new WaitForSeconds(_oneShakeDuration);
        }

        _target.DOMove(_startPosition, _oneShakeDuration / 2);

        _isShaking = false;
    }
}
