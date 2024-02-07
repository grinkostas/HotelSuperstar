using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine.Events;

public class PlayerMovement : Movement
{
    [SerializeField] private Player _player;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private Rigidbody _rigidbody;
    
    [SerializeField] private DynamicJoystick _joystick;
    
    [SerializeField] private Transform _model;
    [SerializeField] private AnimationCurve _speedAnimationCurve;
    [SerializeField] private AnimationCurve _moveAnimationCurve;

    
    private bool _isMovable = true;
    private bool _isMoving = false;
    private bool _overrideInput = false;


    public UnityAction<bool> IsMovingChanged;
    public Vector3 CurrentInput { get; private set; }
    public CharacterController Controller => _characterController;
    public bool IsMoving => _isMoving;

    private void Update()
    {
        if (_overrideInput)
        {
            _characterController.enabled = false;
            return; 
        }
        Move();
    }

    private void FixedUpdate()
    {
        if (_overrideInput)
        {
            _characterController.enabled = false;
        }
    }


    private void Move()
    {
        if (_isMovable == false)
            return;
        
        GetCurrentInput();
        MovingStateChange(CurrentInput);
        Rotate(CurrentInput);
        Move(CurrentInput);
    }

    private void GetCurrentInput()
    {
        float horizontal = _joystick.Horizontal;
        float vertical = _joystick.Vertical;

        CurrentInput = new Vector3(
            CalculateAxisInput(horizontal, vertical), 
            0,
            CalculateAxisInput(vertical, horizontal));
            
        if(CurrentInput == Vector3.zero) CurrentInput = GetMoveInput();
    }

    private float CalculateAxisInput(float axisToCalculate, float secondAxis)
    {
        if (Mathf.Abs(axisToCalculate) <= 0.05f)
        {
            return 0.0f;
        }
        
        if (Mathf.Abs(axisToCalculate) >= Mathf.Abs(secondAxis))
            return axisToCalculate < 0 ? -1.0f : 1.0f;

        return axisToCalculate / Mathf.Abs(secondAxis);
    }

    public void Move(Vector3 move)
    {
        if (_characterController.isGrounded == false)
            move.y -= 1;

        float speed = SpeedMultipliersAggregated * Speed * Time.deltaTime;
        
        _characterController.Move(move * speed);
    }

    private Vector3 GetMoveInput()
    {
        return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }
    
    private void MovingStateChange(Vector3 move)
    {
        if (move == Vector3.zero)
        {
            if(_isMoving == true)
                IsMovingChanged?.Invoke(false);
            _isMoving = false;
        }
        else
        {
            if(_isMoving == false)
                IsMovingChanged?.Invoke(true);
            _isMoving = true;
        }
    }
    
    public void Rotate(Vector3 move)
    {
        if (move == Vector3.zero)
            return;
        Quaternion rotation = Quaternion.LookRotation(move, Vector3.up);
        Rotate(rotation);
    }


    public void Rotate(Quaternion rotation)
    {
        _model.transform.rotation = rotation;
    }
    public void Rotate(Transform payMoneyPoint)
    {
        var delta = payMoneyPoint.transform.position - _model.transform.position;
        delta.y = 0;
        Rotate(delta);
    }
    
    public void Move(Transform transform)
    {
        StartOverrideAllMovement();
        var playerTransform = _characterController.transform;
        playerTransform.position = transform.position;
        playerTransform.rotation = transform.rotation;
        StopOverrideAllMovement();
    }

    public void MoveWithAnimation(Vector3 destination,  float duration, bool canInteract = true, float speedRatio = 0.6f)
    {
        _overrideInput = true;
        CurrentInput = new Vector3(0, 0,speedRatio);
        if(canInteract == false)
            _player.DisableInteractForSeconds(this, duration);
        
        StartCoroutine(Moving(destination, duration, speedRatio));
    }

    private IEnumerator Moving(Vector3 destination, float duration, float maxSpeedRation)
    {
        _characterController.enabled = false;
        var startPosition = _characterController.transform.position;
        var delta = destination - startPosition;
        float wastedTime = 0.0f;
        while (wastedTime < duration)
        {
            yield return null;
            wastedTime += Time.deltaTime;
            float progress = wastedTime / duration;

            float curveProgress = _moveAnimationCurve.Evaluate(progress);
            _characterController.transform.position = startPosition + delta * curveProgress;
           
            CurrentInput = new Vector3(0, 0,_speedAnimationCurve.Evaluate(progress) * maxSpeedRation);
        }
        CurrentInput = Vector3.zero;
        _characterController.enabled = true;
        _overrideInput = false;
    }

    public void StartOverrideAllMovement()
    {
        _boxCollider.enabled = false;
        _rigidbody.detectCollisions = false;
        _characterController.enabled = false;
    }
    
    public void StopOverrideAllMovement()
    {
        _boxCollider.enabled = true;
        _rigidbody.detectCollisions = true;
        _characterController.enabled = true;
    }
}
