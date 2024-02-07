using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine.Events;
using Zenject;

public abstract class InteractableZone<TCharacter> :  Zone<TCharacter> where TCharacter: InteractableCharacter
{
    
    [SerializeField] private bool _valueFromUpgrade;
    [SerializeField, ShowIf(nameof(_valueFromUpgrade))] private UpgradeSelect _upgradeSelect;
    [SerializeField, HideIf(nameof(_valueFromUpgrade))] private float _interactTime = 0.5f;
    
    [SerializeField, RequireInterface(typeof(Interactable))]
    private GameObject _interactableObject;
    
    [Inject] private UpgradesController _upgradesController;


    public UnityAction Interacted;
    
    public override float InteractTime
    {
        get
        {
            if (_valueFromUpgrade == false)
                return _interactTime;
            
            return _upgradesController.GetValue(_upgradeSelect);
        }
    }

    private Interactable _interactable;

    private void Start()
    {
        _interactable = _interactableObject.GetComponent<Interactable>();
    }

    protected override bool CanInteract(TCharacter player)
    {
        return (player.ZoneDestination == null || player.ZoneDestination == this) && _interactable.CanInteract(player) && player.CanInteract;
    }

    protected override void OnInteract(TCharacter player)
    {
        _interactable.Interact(player);
        Interacted?.Invoke();
    }
}
