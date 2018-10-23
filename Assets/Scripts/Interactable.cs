using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interactable {

    bool IsNPC();
    bool IsInteractable();
    KeyCode GetKeyInteract();
    string GetInteractableText();
    void Interact();
}
