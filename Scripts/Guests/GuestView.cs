using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public partial class GuestView : View
{
    [SerializeField] private List<GuestViewTransition> _transitions;
    [SerializeField] private Guest _guest;

    
    private void OnEnable()
    {
        foreach (var transition in _transitions)
            GetGuestActionReference(transition.Condition) += GetViewAction(transition.Action);
        
    }
    
    private void OnDisable()
    {
        foreach (var transition in _transitions)
            GetGuestActionReference(transition.Condition) -= GetViewAction(transition.Action);
    }
    
    private UnityAction<Guest> GetViewAction(GuestViewAction viewAction) => viewAction == GuestViewAction.Show ? Show : Hide;
    
    private ref UnityAction<Guest> GetGuestActionReference(GuestViewCondition condition)
    {
        switch (condition)
        {
            case GuestViewCondition.StartCheckIn: return ref _guest.StartCheckIn; 
            case GuestViewCondition.CheckedIn: return ref _guest.Populated;
            default: return ref _guest.WalkToExit;
        }
    }
   
    
    
    private void Show(Guest guest) => Show();
    private void Hide(Guest guest) => Hide();
    

    
}
