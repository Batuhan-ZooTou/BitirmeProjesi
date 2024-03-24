using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collectable : MonoBehaviour, IInteractable
{
    [field: SerializeField] public KeyCode InteractKey { get; set; }
    [field: SerializeField] public bool canInteract { get; set;}
    [field: SerializeField] public TextMeshPro InteractionText { get; set; }
    [field: SerializeField] public string title { get; set; }
    [field: SerializeField] public string info { get; set; }
    [field: SerializeField] public InteractionState interactionState { get; set; }

    private void Start()
    {
        InteractionText.SetText("");
        InteractionText.gameObject.SetActive(false);
        canInteract = true;
    }
    public void DeHighlight()
    {
        interactionState = InteractionState.DeHighlighted;
        InteractionText.gameObject.SetActive(false);
    }
    public void Highlight()
    {
        if (interactionState==InteractionState.DeHighlighted)
        {
            InteractionText.gameObject.SetActive(true);
            InteractionText.SetText(title);
            interactionState = InteractionState.Highlighted;
        }
    }
    public void AllowInteraction()
    {
        if (!canInteract)
            return;
        if (interactionState == InteractionState.Highlighted)
        {
            interactionState = InteractionState.Interactable;
            InteractionText.SetText(title + "\n" + InteractKey.ToString() + ": " + info);
            canInteract = true;
        }
    }
    public void Interact()
    {
        if (interactionState == InteractionState.Interactable)
        {
            canInteract = false;
            DeHighlight();
        }
    }

}
