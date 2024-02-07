using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LookAtCamera : MonoBehaviour
{
    [SerializeField] private GameObject _model;
    private Quaternion _rotation;

    private void OnEnable()
    {
        if (Camera.main != null)
        {
            _model.transform.LookAt(Camera.main.transform);
        }
    }

    private void Start()
    {
        _rotation = Camera.main.transform.rotation;
    }

    private void Update()
    {
        _model.transform.rotation = _rotation;
    }
}
