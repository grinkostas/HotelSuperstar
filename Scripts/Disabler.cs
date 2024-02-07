using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class Disabler : MonoBehaviour
{
    [SerializeField] private float _delay;

    [Inject] private Timer _timer;

    private void OnEnable()
    {
        _timer.ExecuteWithDelay(() => gameObject.SetActive(false), _delay);
    }
}
