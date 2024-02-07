using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Zenject;

public class PlaceZone : Place
{
    [SerializeField] private List<Place> _placesBuyWith;
    [SerializeField] private float _cameraSlideDuration;
    [SerializeField] private float _buyDelay = 0.5f;
    
    private List<Place> OrderedPlacesWithCameraSlide =>  _placesBuyWith.Where(x=>x.NeedToChangeCameraAtBuy).OrderBy(x => Vector3.Distance(_player.transform.position, x.transform.position)).ToList();

    [Inject] private Player _player;
    protected override void OnBuy()
    {
        StartCoroutine(Buy());
    }

    private IEnumerator Buy()
    {
        foreach (var place in _placesBuyWith.Where(x=>x.NeedToChangeCameraAtBuy == false))
        {
            place.Buy();
        }
        foreach (var place in OrderedPlacesWithCameraSlide)
        {
            place.ShowCamera();
            yield return new WaitForSeconds(_cameraSlideDuration);
            place.Buy();
            yield return new WaitForSeconds(_buyDelay);
            place.HideCamera();
        }

        
    }
}
