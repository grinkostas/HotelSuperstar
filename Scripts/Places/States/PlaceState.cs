using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public abstract class PlaceState : MonoBehaviour
{
    [SerializeField] protected PlaceState NextState;

    public UnityAction ShowView;
    public UnityAction HideView;


    public void EnterPhase()
    {
        ShowView?.Invoke();
        OnEnterPhase();
    }

    public abstract void OnEnterPhase();

    public void ExitPhase()
    {
        OnExitPhase();
        HideView?.Invoke();
        NextState.EnterPhase();
    }

    public abstract void OnExitPhase();

}
