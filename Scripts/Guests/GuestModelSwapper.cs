using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Zenject;

public class GuestModelSwapper : MonoBehaviour
{
    [SerializeField] private Guest _guest;
    [SerializeField] private Transform _particlePoint;
    [SerializeField] private GuestVariant _startVariant;
    [SerializeField] private GameObject _spawnParticle;
    [SerializeField] private Transform _parent;
    [SerializeField] private GuestVariantFabric _fabric;
    [Inject] private Timer _timer;
    private GuestVariant _nextGuestVariant;

    private GuestVariant _currentVariant;

    public GuestVariant Current => _currentVariant;
    
    private void OnEnable()
    {
        _currentVariant = _startVariant;
        _guest.CheckedIn += OnCheckedIn;
    }
    
    private void OnCheckedIn(InteractablePlace place)
    {
        _nextGuestVariant = _fabric.GetVariant(place);
        if(_nextGuestVariant == null)
            return;

        _guest.Movement.Freeze();
        _startVariant.transform.DOScale(Vector3.zero, 0.2f);
        SpawnChangeParticle();
        _timer.ExecuteWithDelay(SpawnModel, 0.2f);
        
        
    }

    private void SpawnModel()
    {
        var spawnedGuestVariant = Instantiate(_nextGuestVariant, _startVariant.transform.position, _startVariant.transform.rotation, _parent);
        _currentVariant = spawnedGuestVariant;
        _guest.Movement.SwapAnimator(spawnedGuestVariant.Animator);
        _guest.Movement.SwapRig(spawnedGuestVariant.transform);
        _guest.SwapAmmo(spawnedGuestVariant.Ammo);

        var variantTransform = spawnedGuestVariant.transform;
        var startScale = variantTransform.localScale;
        variantTransform.localScale = Vector3.zero;
        variantTransform.DOScale(startScale, 0.5f);
        
        
        
        Destroy(_startVariant.gameObject);
        _timer.ExecuteWithDelay( _guest.Movement.UnFreeze, 0.2f);
    }
    
    private void SpawnChangeParticle()
    {
        var particle = Instantiate(_spawnParticle, _particlePoint.position, Quaternion.identity);
        Destroy(particle, 2.0f);
    }
}
