using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Zenject;

public class EmployView : MonoBehaviour
{
    [SerializeField] private Helper _helper;
    [SerializeField] private Animator _animator;
    
    [SerializeField] private GameObject _particlePrefab;
    [SerializeField] private float _particleShowDuration;
    [SerializeField] private float _startMoveDelay;
    [SerializeField] private Transform _particlePoint;
    
    [SerializeField, AnimatorParam(nameof(_animator))] private string _employEmotionTrigger;
    [Inject] private Timer _timer;
    private void OnEnable()
    {
        _helper.Employed += OnEmployed;
    }

    private void OnEmployed()
    {
        _helper.Movement.Freeze();
        _animator.SetTrigger(_employEmotionTrigger);
        SpawnParticles();
        _timer.ExecuteWithDelay(_helper.Movement.UnFreeze, _startMoveDelay);
    }

    private void SpawnParticles()
    {
        var spawnedParticle = Instantiate(_particlePrefab, _particlePoint);
        Destroy(spawnedParticle, _particleShowDuration);
    }
}
