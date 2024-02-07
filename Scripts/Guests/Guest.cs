
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK.Setup;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine.AI;
using UnityEngine.Events;
using Zenject;

public class Guest : MonoBehaviour, IProgressible
{
    [SerializeField] private NpcMovement _movement;
    [SerializeField] private GameObject _angryEmojiView;
    [SerializeField] private List<GameObject> _ammo;

    [SerializeField] private GuestModelSwapper _modelSwapper;

    [Inject] private UpgradesController _upgradesController;
    [Inject] protected Reception Reception;
    [Inject] private ExitPoint _exit;
    
    private InteractablePlace _destination;

    private bool _reservedPlace = false;
    private bool _located = false;
    private bool _kicked = false;
    private bool _timerLocked = false;
    
    private bool _waitToCheckIn;


    public bool WasLocated => _located;
    public NpcMovement Movement => _movement;
    public GuestModelSwapper Model => _modelSwapper;
    
    
    public UnityAction<float> ProgressChanged { get; set; }

    public UnityAction<Guest> WalkToExit;
    public UnityAction<Guest> Populated;
    public UnityAction Located;
    public UnityAction<InteractablePlace> CheckedIn;

    public UnityAction<Guest> StartCheckIn;


    private void OnEnable()
    {
        GoToReception();
    }

    public void MakeEndlessPatience()
    {
        _timerLocked = true;
    }

    protected void GoToReception()
    {
        _movement.Agent.SetDestination(Reception.Queue.NextPoint);
    }
    
    private void Start()
    {
        if(_angryEmojiView != null)
            _angryEmojiView.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Reception reception))
        {
            if(_reservedPlace == false)
                StartCoroutine(TryCheckIn());
        }

        if (other.TryGetComponent(out InteractableState state))
        {
            EnterState(state);
        }

        if (other.TryGetComponent(out GuestSpawner spawner))
        {
            if(_kicked)
                Destroy(gameObject);
        }
    }

    public void EnterState(InteractableState state)
    {
        if(_located)
            return;
        if (state.TryEnter(this))
        {
            Located?.Invoke();
            _located = true;
        }
    }
    
    
    private IEnumerator TryCheckIn()
    {
        StartCheckIn?.Invoke(this);
        Reception.TryToCheckIn(this);
        float patience = _upgradesController.GetModel(UpgradeIds.GuestCheckInPatience).CurrentValue;
        float wastedTime = 0.0f;
        while (wastedTime < patience)
        {
            yield return null;
            if(_waitToCheckIn == false && _timerLocked == false)
                wastedTime += Time.deltaTime;
            ProgressChanged?.Invoke(1-(wastedTime/patience));
        }
        NoAvailablePlace();
    }

    public void StartWaitToCheckIn()
    {
        _waitToCheckIn = true;
    }

    public void StopWaitToCheckIn()
    {
        _waitToCheckIn = false;
    }
    public bool CanPopulate()
    {
        return !_kicked;
    }
    
    public void Populate(InteractablePlace place)
    {
        _reservedPlace = true;
        _destination = place;
        Populated?.Invoke(this);
        _movement.Agent.SetDestination(place.transform.position);
        CheckedIn?.Invoke(place);
    }

    public void AngryEmotion()
    {
        if(_angryEmojiView != null)
            _angryEmojiView.SetActive(true);
    }

    public void HideAngryEmotion()
    {
        if(_angryEmojiView != null)
            _angryEmojiView.SetActive(false);
    }
    
    private void NoAvailablePlace()
    {
        if(_reservedPlace)
            return;
        AngryEmotion();
        MoveExit();
    }

    public void MoveExit()
    {
        _movement.Agent.enabled = true;
        WalkToExit?.Invoke(this);
        _movement.Agent.SetDestination(_exit.transform.position);
        _kicked = true;
    }

    public void HideAmmo()
    {
        _ammo.ForEach(x=> x.SetActive(false));
    }

    public void ShowAmmo()
    {
        _ammo.ForEach(x=> x.SetActive(true));
    }

    public void SwapAmmo(List<GameObject> ammo)
    {
        _ammo = ammo;
    }

}
