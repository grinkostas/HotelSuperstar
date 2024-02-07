using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public abstract class Request : MonoBehaviour, Interactable
{
    [SerializeField] private bool _requestOnStart;
    [SerializeField] private Item _requiredItem;
    [SerializeField] protected InteractableCharacterZone _interactableCharacterZone;
    [SerializeField] private Transform _makeRequestPoint;
    [SerializeField] protected Transform UseItemPoint;
    [Inject] protected SignalHub SignalHub;

    private bool _interactable = true;
    
    public Transform MakeRequestPoint => _makeRequestPoint;
    public InteractableCharacterZone InteractableCharacterZone => _interactableCharacterZone;
    public virtual float Reward => 0;
    public Item RequiredItem
    {
        get => _requiredItem;
        protected set => _requiredItem = value;
    }
    
    public UnityAction Requested;
    public UnityAction<Request> Done;
    public UnityAction<Request> End;
    
    public RequestStatus Status { get; private set; }
    public bool IsRequested { get; private set; }
    
    private void Awake()
    {
        _interactableCharacterZone.Disable();
    }

    private void Start()
    {
        if(_requestOnStart)
            MakeRequest();
    }

    protected abstract void OnUseItem();

    public void Interact(InteractableCharacter character)
    {
        if (character.TryUseItem(RequiredItem, UseItemPoint))
        {
            OnUseItem();
        }  
    }

    public bool CanInteract(InteractableCharacter player)
    {
        return IsRequested && player.CanUseItem(RequiredItem) && _interactable;
    }

    public void MakeRequest()
    {
        if(IsRequested)
            return;
        
        Requested?.Invoke();
        IsRequested = true;
        EnableInteract();
        _interactableCharacterZone.Enable();
        OnRequest();
    }

    public void Cancel(bool changeStatus = true)
    {
        IsRequested = false;
        _interactableCharacterZone.Disable();
        if (changeStatus)
        {
            Status = RequestStatus.Canceled;
            End?.Invoke(this);
        }
        SignalHub.Get<Signals.EndRequest>().Dispatch(this);
        OnCancel();
    }

    protected void Complete()
    {
        Cancel(false);
        Status = RequestStatus.Done;
        End?.Invoke(this);
        OnDone();
        Done?.Invoke(this);
    }

    protected void DisableInteract()
    {
        _interactable = false;
    }

    protected void EnableInteract()
    {
        _interactable = true;
    }

    protected abstract void OnRequest();
    protected virtual void OnCancel() {}
    protected virtual void OnDone() {}

}
