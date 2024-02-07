using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Door : MonoBehaviour
{
    [SerializeField] protected Transform Model;
    [SerializeField] private float _animationDuration;

    public float AnimationDuration => _animationDuration;

    public abstract void Open();
    public abstract void Close();

}
