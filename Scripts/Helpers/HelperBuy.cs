using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HelperBuy : MonoBehaviour, Interactable
{
    [SerializeField] private Helper _helper;
    [SerializeField] private HelperBuyPopup _employPopup;
    [SerializeField] private List<InteractablePlayerZone> _interactablePlayerZone;
    [SerializeField] private List<GameObject> _hideOnEmploy;

    
    public void Interact(InteractableCharacter character)
    {        
        _employPopup.Show();
        _employPopup.Hired += OnHired;
    }

    private void OnHired()
    {
        _employPopup.Hired -= OnHired;
        _interactablePlayerZone.ForEach(x=> x.Disable());
        _hideOnEmploy.ForEach(x=> x.SetActive(false));
        _helper.Dismissed += OnDismissed;
    }
    
    public bool CanInteract(InteractableCharacter player)
    {
        return !_helper.IsEmployed;
    }
    
    private void OnDismissed()
    {
        _helper.Dismissed -= OnDismissed;
        _interactablePlayerZone.ForEach(x=> x.Enable());
        
        _hideOnEmploy.ForEach(x=> x.SetActive(true));
    }
}
