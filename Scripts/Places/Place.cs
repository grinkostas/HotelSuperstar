using System;
using Cinemachine;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Events;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

[RequireComponent(typeof(BoxCollider))]
public class Place : MonoBehaviour, IBuyable
{
    [SerializeField, Dropdown(nameof(Ids))] 
    private string _id;
    
    [SerializeField] private bool _isBought;
    [HideIf(nameof(_isBought)), SerializeField] private bool _buyOutside;
    [HideIf(nameof(_buyOutside)), SerializeField] private float _price = 0;
    [HideIf(nameof(_buyOutside)), SerializeField] private Place _requiredPlaceToBuy;

    [SerializeField] private bool _changeCameraAtBuy;
    [SerializeField, ShowIf(nameof(_changeCameraAtBuy))] private CinemachineVirtualCamera _buyCamera;
    [SerializeField] private bool _saveBuyInAnalytics;
    private string[] Ids = PlaceId.All;
    public bool IsAvailableToBuy => _buyOutside == false && (_requiredPlaceToBuy == null || _requiredPlaceToBuy.IsBought);

    public bool NeedToChangeCameraAtBuy => _changeCameraAtBuy;
    public bool IsBought => _isBought;
    public string Id => _id;
    public float Price => _price;
    public UnityAction Bought;
    public UnityAction AvailableToBuy;

    private void Awake()
    {
        if (_isBought == false)
            _isBought = GetSave().Bought;
        
        if (_isBought == false && IsAvailableToBuy == false && _requiredPlaceToBuy != null)
            _requiredPlaceToBuy.Bought += OnRequirePlaceBuy;
    }

    private void OnRequirePlaceBuy()
    {
        if(IsBought == false)
            AvailableToBuy?.Invoke();
    }

    public void Buy()
    {
        if(_isBought)
            return;
        
        if(_saveBuyInAnalytics)
        {
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, _id, "Bought");
        }
        
        _isBought = true;
        Save();
        Bought?.Invoke();
        OnBuy();
    }

    public void Save()
    {
        ES3.Save(_id, new PlaceSave(_isBought));
    }

    public void Save(int buyProgress)
    {
        ES3.Save(_id, new PlaceSave(_isBought, buyProgress));
    }
    
    public PlaceSave GetSave()
    {
        return GetSave(_id);
    }

    public static PlaceSave GetSave(string id)
    {
        return ES3.Load(id, new PlaceSave());
    }

    protected virtual void OnBuy()
    {
    }
    
    public void ShowCamera()
    {
        _buyCamera.gameObject.SetActive(true);
    }

    public void HideCamera()
    {
        _buyCamera.gameObject.SetActive(false);
    }


}

