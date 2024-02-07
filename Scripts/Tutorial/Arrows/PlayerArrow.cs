using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerArrow : View
{
    [SerializeField] private float _hideDistance;
    [SerializeField] private bool _lookAt = true;
    
    private Transform _target = null;

    private void Update()
    {
        if(_target == null)
            return;
        Rotate();
        HideOnClose();
    }

    private void Rotate()
    {
        if(_lookAt)
            Model.transform.LookAt(_target);
    }

    private void HideOnClose()
    {
        var distance = Vector3.Distance(Model.transform.position, _target.position);
        
        if (distance < _hideDistance)
            Hide();
        else
            Show();
        
    }

    public void SetDestination(Transform target)
    {
        _target = target;
    }

    public void ReceiveDestination()
    {
        _target = null;
        Hide();
    }
}
