using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class GuestVariantFabric : MonoBehaviour
{
    [SerializeField] private List<GuestVariant> _guestVariants;

    public GuestVariant GetVariant(InteractablePlace place)
    {
        return _guestVariants.Find(x=> x.PlaceType == place.Type);
    }

    
}
