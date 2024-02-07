using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.AI;

public class NpcMovement : Movement
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private NpcAnimator _animator;
    [SerializeField] private bool _haveStack;
    [SerializeField] private Transform _rig;
    
    [SerializeField, ShowIf(nameof(_haveStack))]
    private Stack _stack;
    
    
    private bool _forcedAnimation;

    private bool _rotateRequire = false;
    private Vector3 _lookAt;
    public NavMeshAgent Agent => _agent;
    public NpcAnimator Animator => _animator;

    private void OnEnable()
    {
        if(SpeedModel != null)
            SpeedModel.Upgraded += ActualizeSpeed;
        ActualizeSpeed();
    }

    private void OnDisable()
    {
        if(SpeedModel != null)
            SpeedModel.Upgraded -= ActualizeSpeed;
    }

    private void ActualizeSpeed()
    {
        _agent.speed = Speed;
    }
    private void Update()
    {
        if(_rig != null)
            _rig.transform.localPosition = Vector3.zero;
        WalkCheck();
        if(_haveStack == false)
            return;
        if (_stack.ItemsCount > 0)
        {
            _animator.Animator.SetBool("HaveItemsInHand", true);
        }
        else
        {
            _animator.Animator.SetBool("HaveItemsInHand", false);
        }
    }

    private void WalkCheck()
    {
        if(_forcedAnimation)
            return;

        if (_agent.velocity == Vector3.zero)
        {
            _animator.Stop();
            Rotate();
        }
        else
            _animator.Walk();
    }


    private void Rotate()
    {
        if(_rotateRequire == false)
            return;

        _agent.transform.DOLocalRotate(_lookAt, 0.2f);
    }
    
    public void LookAtDestination(Vector3 lookAngle)
    {
        _lookAt = lookAngle;
        _rotateRequire = true;
    }

    public void Freeze()
    {
        Agent.isStopped = true;
        _forcedAnimation = true;
    }

    public void UnFreeze()
    {
        Agent.isStopped = false;
        _forcedAnimation = false;
    }
    
    public void ForceWalk()
    {
        _forcedAnimation = true;
        _animator.Walk();
    }

    public void SwapAnimator(NpcAnimator animator)
    {
        _animator = animator;
    }

    public void SwapRig(Transform rig)
    {
        _rig = rig;
    }

    public void ForceStop()
    {
        _forcedAnimation = false;
        _animator.Stop();
        Rotate();
    }

    protected override void OnAppliedMultiplayer()
    {
        _agent.speed = Speed * SpeedMultipliersAggregated;
    }

    protected override void OnRemovedMultiplayer()
    {
        _agent.speed = Speed * SpeedMultipliersAggregated;
    }
}
