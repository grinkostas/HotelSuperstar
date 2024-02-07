using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Transform _model;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Vector3 _rotateAxis;

    private void Update()
    {
        _model.transform.rotation = Quaternion.Euler(_model.transform.rotation.eulerAngles + _rotateAxis * _rotationSpeed * Time.deltaTime);
    }
}
