using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReceptionQueue : MonoBehaviour
{
    [SerializeField] private GuestSpawner _spawner;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Vector3 _positionDelta;
    [SerializeField] private int _maxGuestsInQueue = 5;

    private List<Guest> _guestInQueue = new List<Guest>();

    public int GuestsCount => _guestInQueue.Count;
    public Vector3 NextPoint => GetQueuePoint(_guestInQueue.Count);
    
    private void OnEnable()
    {
        _spawner.GuestSpawned += OnGuestSpawned;
    }

    private void OnDisable()
    {
        _spawner.GuestSpawned -= OnGuestSpawned;
    }

    private void OnGuestSpawned(Guest guest)
    {
        AddGuest(guest);
    }

    public void AddGuest(Guest guest)
    {
        _guestInQueue.Add(guest);
        guest.Populated += RemoveGuestFromQueue;
        guest.WalkToExit += RemoveGuestFromQueue;
        if (_guestInQueue.Count > _maxGuestsInQueue)
            _guestInQueue[0].MoveExit();
    }

    private void RemoveGuestFromQueue(Guest guest)
    {
        guest.Populated -= RemoveGuestFromQueue;
        guest.WalkToExit -= RemoveGuestFromQueue;
        _guestInQueue.Remove(guest);
        ActualizeQueue();
    }

    private Vector3 GetQueuePoint(int number)
    {
        return _startPoint.position + _positionDelta * number;
    }
    
    private void ActualizeQueue()
    {
        for (int i = 0; i < _guestInQueue.Count; i++)
        {
            _guestInQueue[i].Movement.Agent.SetDestination(GetQueuePoint(i));
        }
    }
}
