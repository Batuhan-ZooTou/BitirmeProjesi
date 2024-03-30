using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum InteractionState
{
    DeHighlighted,
    Highlighted,
    Interactable,
}
public interface IInteractable
{
    public InteractionState interactionState { get; set; }
    public KeyCode InteractKey { get; set; }
    public TextMeshPro InteractionText { get; set; }
    public bool canInteract { get; set; }
    public string title { get; set; }
    public string info { get; set; }

    public void Highlight();
    public void DeHighlight();
    public void AllowInteraction();
    public void DisAllowInteraction();
    public void Interact();
}
