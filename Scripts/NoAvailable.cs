using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NoAvailable : MonoBehaviour, Interactable
{
    [SerializeField] private View _noAvailablePopup;
    public void Interact(InteractableCharacter character)
    {
        _noAvailablePopup.Show();
    }

    public bool CanInteract(InteractableCharacter player)
    {
        return true;
    }
}
