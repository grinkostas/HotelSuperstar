using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class ElevatorDoor : Door
{
    [SerializeField] private Transform _openPosition;
    [SerializeField] private Transform _closePosition;
    
    public override void Open()
    {
        Model.DOMove(_openPosition.position, AnimationDuration);
    }

    public override void Close()
    {
        Model.DOMove(_closePosition.position, AnimationDuration);
    }
}
