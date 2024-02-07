using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameAnalyticsSDK.Setup;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

[RequireComponent(typeof(BoxCollider))]
public class Reception : MonoBehaviour, Interactable
{
    [SerializeField] private ReceptionQueue _receptionQueue;
    [SerializeField] private MoneyStack _moneyStack;
    [SerializeField] private CheckInHelper _helper;
    [SerializeField] private List<InteractablePlace> _interactablePlaces = new List<InteractablePlace>();
    [SerializeField] private MessagePopup _messagePopup;

    [SerializeField] private InteractablePlayerZone _checkInZone;
    [SerializeField] private GameObject _highlight;

    [Inject] private UpgradesController _upgradesController;
    
    private Guest _guestToCheckIn;

    public Guest GuestToCheckIn => _guestToCheckIn;
    public UnityAction<Guest> GuestArrived;
    public ReceptionQueue Queue => _receptionQueue;

    private void OnEnable()
    {
        _checkInZone.ProgressChanged += OnProgressChanged;
    }

    private void Update()
    {
        if(_guestToCheckIn != null && _helper.IsEmployed == false)
            _highlight.SetActive(true);
        else
            _highlight.SetActive(false);
    }

    private void OnProgressChanged(float progress)
    {
        if(_guestToCheckIn == null)
            return;
        if (progress > 0)
        {
            _guestToCheckIn.StartWaitToCheckIn();
        }
        else
        {
            _guestToCheckIn.StopWaitToCheckIn();
        }
    }

    public void TryToCheckIn(Guest guest)
    {
        _guestToCheckIn = guest;
        guest.WalkToExit += OnGuestMoveToExit;
        GuestArrived?.Invoke(guest);
    }

    private void OnGuestMoveToExit(Guest guest)
    {
        guest.WalkToExit -= OnGuestMoveToExit;
        _guestToCheckIn = null;
    }
    
    public bool CanCheckIn(bool showMessages = false)
    {
        bool availablePlaces = _interactablePlaces.Any(place => place.CanCheckIn());
        bool guestToCheckIn = _guestToCheckIn != null;
        
        if (showMessages)
        {
            if(availablePlaces == false)
                _messagePopup.Show(Messages.NoAvailablePlaces); 
        }

        return availablePlaces && guestToCheckIn;
    }
    
    public void CheckIn(bool showMessage = true)
    {
        if (CanCheckIn(showMessage) == false)
        {
            return;
        }

        if(_guestToCheckIn.CanPopulate() == false)
            return;
        
        var placesToCheckIn = _interactablePlaces.FindAll(place => place.CanCheckIn());
        //var placeToCheckIn = placesToCheckIn[Random.Range(0, placesToCheckIn.Count)];
        var placeToCheckIn = placesToCheckIn.First();
        
        placeToCheckIn.Reserve(_guestToCheckIn);
        _guestToCheckIn.Populate(placeToCheckIn);
        var checkInPrice = _upgradesController.GetValue(UpgradeIds.CheckInPrice);
        _moneyStack.Add(checkInPrice);
        _guestToCheckIn = null;
    }

    public void Interact(InteractableCharacter character)
    {
        CheckIn();
    }

    public bool CanInteract(InteractableCharacter player)
    {
        return _helper.IsEmployed == false && _guestToCheckIn != null;
    }
}
