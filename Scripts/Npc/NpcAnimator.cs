using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

public class NpcAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [AnimatorParam(nameof(_animator))] 
    [SerializeField] protected string _walkingParam;

    public virtual void Walk() => SetBool(_walkingParam);
    public Animator Animator => _animator;
    
    public virtual void Stop()
    {
        _animator.SetBool(_walkingParam, false);
    }

    private void SetBool(string param)
    {
        Stop();
        _animator.SetBool(param, true);
    }
    


}
