using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

public class SpecefiedAnimationPlace : ActionPlace
{
    [SerializeField] private Guest _guestPrefab;
    [Header("Anims")]
    [SerializeField, ShowIf(nameof(ShowTriggers)), AnimatorParam(nameof(Animator))] private string _actionTrigger;
    [SerializeField, ShowIf(nameof(ShowTriggers)), AnimatorParam(nameof(Animator))] private string _stopActionTrigger;

    private bool ShowTriggers => _guestPrefab != null; 
    private Animator Animator => _guestPrefab.Movement.Animator.Animator;
    
    protected override void OnPlaceGuest(Guest guest)
    {
        guest.HideAmmo();
        guest.Model.Current.Animator.Animator.SetTrigger(_actionTrigger);
    }

    protected override void OnRemoveGuest(Guest guest)
    {
        guest.Model.Current.Animator.Animator.SetTrigger(_stopActionTrigger);
        guest.ShowAmmo();
        guest.MoveExit();
    }
}
