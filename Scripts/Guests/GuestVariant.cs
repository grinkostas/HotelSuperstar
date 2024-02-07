using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GuestVariant : MonoBehaviour
{
    [SerializeField] private NpcAnimator _animator;
    [SerializeField] private PlaceType _placeType;
    [SerializeField] private float _yRotation;
    [SerializeField] private List<GameObject> _ammo;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;
    [SerializeField] private float _hipsScale;

    public float HipsScale => _hipsScale;
    public Transform LeftHand => _leftHand;
    public Transform RightHand => _rightHand;

    public List<GameObject> Ammo => _ammo;
    public NpcAnimator Animator => _animator;
    public PlaceType PlaceType => _placeType;
    
    
}
