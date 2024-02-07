using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

public class Barbell : SpecefiedAnimationPlace
{
    [Header("Barbell")]
    [SerializeField] private Transform _barbelStartPosition;
    [SerializeField] private Transform _barbell;

    private bool _guestInside = false;
    
    protected override void OnPlaceGuest(Guest guest)
    {
        base.OnPlaceGuest(guest);
        _guestInside = true;
        var handsCenterPosition = Vector3.Lerp(guest.Model.Current.LeftHand.position, guest.Model.Current.RightHand.position, 0.5f);
        _barbell.position = handsCenterPosition;
        _barbell.SetParent(guest.Model.Current.LeftHand, true);
        StartCoroutine(BarbellStabilization());
    }

    private IEnumerator BarbellStabilization()
    {
        while (_guestInside)
        {
            _barbell.localPosition = Vector3.zero;
            _barbell.rotation = Quaternion.identity;
            yield return null;
        }
    }
    
    protected override void OnRemoveGuest(Guest guest)
    {
        _guestInside = false;
        base.OnRemoveGuest(guest);
        _barbell.SetParent(_barbelStartPosition, true);
        _barbell.position = _barbelStartPosition.position;
    }
}
