using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Clouds : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private float _simulateTime;

    private void OnEnable()
    {
        _particleSystem.Simulate(_simulateTime);
        _particleSystem.Play();
    }
}
