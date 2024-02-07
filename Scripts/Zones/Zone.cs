using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public abstract class Zone<TCharacter> : ZoneBase, IProgressible where TCharacter : InteractableCharacter
{
    [SerializeField] private bool _multipleInteract = true;

    private int _currentInteractCount = 0;
    
    private bool _activated = true;
    private TCharacter _interactableCharacter;
    private float _timeInZone = 0;
    private bool _isCharacterInside = false;
    private bool _waitNextInteract = false;
    public GameObject GameObject => gameObject;

    private float Progress => _timeInZone / InteractTime;

    private float TimeInZone
    {
        get => _timeInZone;
        set
        {
            _timeInZone = value;
            ProgressChanged?.Invoke(Progress);
        }
    }

    protected TCharacter InteractableCharacter => _interactableCharacter;
    

    public abstract float InteractTime { get; }
    public bool IsCharacterInside => _isCharacterInside;
    
    public UnityAction<float> ProgressChanged { get; set; }

    private void Update()
    {
        TryInteract();
    }

    private void TryInteract()
    {
        if(_waitNextInteract)
            return;
        
        if (_multipleInteract == false && _currentInteractCount > 0)
            return;

        if (_isCharacterInside == false && _timeInZone > 0)
        {
            TimeInZone = Mathf.Clamp(_timeInZone - Time.deltaTime, 0, InteractTime);
            return;
        }
        
        if (_activated == false || _interactableCharacter == null || CanInteract(_interactableCharacter) == false)
            return;
        
        TimeInZone += Time.deltaTime;
        if (_timeInZone < InteractTime)
            return;
        _currentInteractCount++;
        TimeInZone = 0;
        OnInteract(_interactableCharacter);
    }
    
    

    protected abstract bool CanInteract(TCharacter character);
    protected abstract void OnInteract(TCharacter character);

    protected virtual void OnPlayerExit(TCharacter character) {}
    
    protected virtual void OnPlayerEnter(TCharacter character) {}

    private void OnTriggerEnter(Collider other)
    {   
        if (other.TryGetComponent(out TCharacter character))
        {
            _interactableCharacter = character;
            _isCharacterInside = true;
            OnPlayerEnter(character);
            CharacterEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out TCharacter character))
        {
            _waitNextInteract = false;
            _currentInteractCount = 0;
            _interactableCharacter = null;
            _isCharacterInside = false;
            OnPlayerExit(character);
            CharacterExit?.Invoke();
        }
    }

    public void WaitNextInteract() => _waitNextInteract = true;
    
    public void Enable()
    {
        _activated = true;
        gameObject.SetActive(true);
    }
    
    public void Disable()
    {
        _activated = false;
        _interactableCharacter = null;
        TimeInZone = 0;
        _isCharacterInside = false;
        gameObject.SetActive(false);
    }

}
