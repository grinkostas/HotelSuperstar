using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float _delay;
    private void OnEnable()
    {
        Destroy(gameObject, _delay);
    }
}
