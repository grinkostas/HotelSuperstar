using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using NepixCore.Game.API;
using Zenject;

public class MoneyNode : StackSize
{
    [SerializeField] private float _amount;
    [SerializeField] private float _duration = 0.3f;

    [Inject] private IHapticService _hapticService;
    [Inject] private SignalHub _signalHub;
    public float Amount => _amount;

    public void Claim(Player player)
    {
        StartCoroutine(MoveWithFollow(player.MoneyPoint));
        transform.DOScale(Vector3.zero, _duration);
        _signalHub.Get<Signals.EarnMoney>().Dispatch(_amount);
        Destroy(gameObject, _duration);
    }

    private IEnumerator MoveWithFollow(Transform transformToFollow)
    {
        float wastedTime = 0;
        float progress = 0.0f;
        Vector3 startPosition = transform.position;
        while (wastedTime <= _duration)
        {
            progress = wastedTime / _duration;
            var delta = transformToFollow.position - startPosition;
            transform.position = startPosition + delta * progress;
            wastedTime += Time.deltaTime;
            yield return null;
        };
        _hapticService.Selection();
    }

}
