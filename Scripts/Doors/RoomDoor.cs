using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;

public class RoomDoor : Door
{
    private Vector3 _openedRotation = Vector3.up * 90;
    private Vector3 _closedRotation = Vector3.zero;
    private Vector3 _halfOpenedRotation = Vector3.up * 45;

    private Button _button;
    
    public override void Open()
    {
        Model.DORotate(_openedRotation, AnimationDuration);
    }

    public override void Close()
    {
        Model.DORotate(_closedRotation, AnimationDuration);
    }

    public void HalfOpen()
    {
        Model.DORotate(_halfOpenedRotation, AnimationDuration);
    }

    public void GuestEnter()
    {
        StartCoroutine(GuestEntering());
    }

    private IEnumerator GuestEntering()
    {
        Open();
        yield return new WaitForSeconds(AnimationDuration * 1.5f);
        Close();
    }

    private IEnumerator RoomToRepair()
    {
        Open();
        yield return new WaitForSeconds(AnimationDuration);
        HalfOpen();
    }
}
