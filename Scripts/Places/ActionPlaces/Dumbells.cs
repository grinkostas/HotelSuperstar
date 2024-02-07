using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using NaughtyAttributes;

public class Dumbells : SpecefiedAnimationPlace
{
    [SerializeField] private GameObject _dumbellPrefab;
    [SerializeField, HideIf(nameof(_bothHands))] private HandSide _handSide;
    [SerializeField] private bool _bothHands;

    private List<GameObject> _spawnedDumbells = new List<GameObject>();

    protected override void OnPlaceGuest(Guest guest)
    {
        base.OnPlaceGuest(guest);
        SpawnDumbells(guest);
    }

    protected override void OnRemoveGuest(Guest guest)
    {
        base.OnRemoveGuest(guest);
        _spawnedDumbells.ForEach(x=> Destroy(x));
        _spawnedDumbells.Clear();
    }

    private void SpawnDumbells(Guest guest)
    {
        var hands = GetHands(guest);
        Debug.Log($"Hands {hands.Count}");
        foreach (var hand in hands)
        {
            Debug.Log("Spawned Dumbbell");
            var dumbell = Instantiate(_dumbellPrefab, hand);
            dumbell.transform.GetChild(0).localScale = Vector3.one * 1.0f / guest.Model.Current.HipsScale;
            _spawnedDumbells.Add(dumbell);
        }
    }
    
    private List<Transform> GetHands(Guest guest)
    {
        List<Transform> hands = new List<Transform>();
        if (_bothHands == false)
        {
            var hand = _handSide == HandSide.Left ? guest.Model.Current.LeftHand : guest.Model.Current.RightHand;
            hands.Add(hand);
        }
        else
        {
            hands.Add(guest.Model.Current.LeftHand);
            hands.Add(guest.Model.Current.RightHand);
        }
        return hands;
    }
    
    private enum HandSide
    {
        Left, 
        Right
    }
}
