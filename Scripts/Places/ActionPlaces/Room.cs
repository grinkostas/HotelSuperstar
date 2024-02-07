using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.AI;
using Zenject;

public class Room : ActionPlace
{
    [SerializeField] private RoomDoor _roomDoor;
    [SerializeField] private float _rotationDuration = 0.1f;
    [SerializeField] private float _closeDoorDelay;
    [SerializeField] private Transform _requestPoint;
    [SerializeField] private ActionState _actionState;
    [Inject] private Timer _timer;
    private Guest _currentGuest;
    private Vector3 _startPosition;
    
    public override void PlaceGuest(Guest guest)
    {
        _currentGuest = guest;
        _startPosition = guest.transform.position;
        StartCoroutine(PopulateGuest(guest));
    }

    public override void RemoveGuest(Guest guest)
    {
        StartCoroutine(KickGuest(guest));
        _currentGuest = null;
    }

    public void WaitForRequest()
    {
        StartCoroutine(StartRequest(_currentGuest, _requestPoint.position, true));
    }

    private IEnumerator StartRequest(Guest guest, Vector3 destination, bool stopAtEnd = false)
    {
        _roomDoor.HalfOpen();
        yield return new WaitForSeconds(_roomDoor.AnimationDuration);
        MoveGuest(guest, destination, stopAtEnd);
        _currentGuest.Movement.Animator.Animator.SetTrigger("WaitForRequest");
        yield return new WaitForSeconds(_closeDoorDelay);
    }

    public void RequestEnd()
    {
        StartCoroutine(GuestReturnAfterRequest());
    }

    private IEnumerator GuestReturnAfterRequest()
    {
        _currentGuest.Movement.Animator.Animator.SetTrigger("StopWaitForRequest");
        yield return new WaitForSeconds(0.2f);
        if (_actionState.LastRequest.Status == RequestStatus.Canceled)
        {
            _currentGuest.AngryEmotion();
            yield return new WaitForSeconds(1.5f);
            _currentGuest.HideAngryEmotion();
        }
        _currentGuest.transform.DORotate(new Vector3(0, 0, 0), _rotationDuration);
        yield return new WaitForSeconds(_rotationDuration);
        yield return PopulateGuest(_currentGuest);
    }

    private IEnumerator PopulateGuest(Guest guest)
    {
        yield return MovingGuest(guest, GuestEnterPoint.position);
        _roomDoor.Close();
        guest.Movement.ForceStop();
        guest.transform.DORotate(RotationOnEnd, _rotationDuration);
        yield return new WaitForSeconds(0.5f);
        _currentGuest.HideAmmo();
    }
    
    private IEnumerator KickGuest(Guest guest)
    {
        _currentGuest.ShowAmmo();
        yield return MovingGuest(guest, _startPosition);
        guest.MoveExit();
        _roomDoor.HalfOpen();
    }
    
    private IEnumerator MovingGuest(Guest guest, Vector3 destination, bool stopAtEnd = false)
    {
        yield return OpenDoor();
        MoveGuest(guest, destination, stopAtEnd);
        yield return new WaitForSeconds(_closeDoorDelay);
    }
    private IEnumerator OpenDoor()
    {
        _roomDoor.Open();
        yield return new WaitForSeconds(_roomDoor.AnimationDuration);
    }

    private void MoveGuest(Guest guest, Vector3 destination, bool stopAtEnd = false)
    {
        guest.Movement.ForceWalk();
        guest.Movement.Agent.enabled = false;
        guest.transform.DOMove(destination, _closeDoorDelay);
        if(stopAtEnd)
            _timer.ExecuteWithDelay(_currentGuest.Movement.ForceStop, _closeDoorDelay);
    }

}
