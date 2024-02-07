using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StackSize : MonoBehaviour
{
    [SerializeField] private Vector3 _size;
    [SerializeField] private Vector3 _center;
    
    public Vector3 Size => _size;
    public Vector3 Center => _center;
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position+_center, _size);
    }
    
}
