using UnityEngine;

public interface Interactable
{ 
    void Interact(InteractableCharacter character);
    bool CanInteract(InteractableCharacter player);
}
