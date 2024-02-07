using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ActionPlace : MonoBehaviour
{
    [SerializeField] protected Transform GuestEnterPoint;
    [SerializeField] protected Vector3 RotationOnEnd;

    private Vector3 _previousPosition;
    private Transform _previousParent;
    public virtual void PlaceGuest(Guest guest)
    {
        guest.Movement.Agent.enabled = false;
        _previousPosition = guest.transform.localPosition;
        _previousParent = guest.transform.parent;
        guest.transform.SetParent(GuestEnterPoint);
        guest.transform.localPosition = Vector3.zero;
        guest.transform.localRotation = Quaternion.identity;
        OnPlaceGuest(guest);
        guest.HideAmmo();
    }

    public virtual void RemoveGuest(Guest guest)
    {
        guest.transform.SetParent(_previousParent);
        
        guest.transform.localPosition = _previousPosition;
        guest.transform.localRotation = Quaternion.Euler(RotationOnEnd);
        guest.Movement.Agent.enabled = true;
        OnRemoveGuest(guest);
        
        guest.ShowAmmo();
    }

    protected virtual void OnPlaceGuest(Guest guest)
    {
    }

    protected virtual void OnRemoveGuest(Guest guest)
    {
    }
}
