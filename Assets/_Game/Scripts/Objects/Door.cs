using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using Sirenix.OdinInspector;
namespace _Game.Scripts.Objects
{
    public enum DoorState
    {
        Open,
        Close,
    }
    [Serializable]
    public class DoorReferencePositions
    {
        [HorizontalGroup] [SerializeField] public Transform openTransform;
        [HorizontalGroup] [SerializeField] public Transform closeTransform;
    }
    [Serializable]
    public class DoorReferenceRotations
    {
        [HorizontalGroup] [SerializeField] public Transform openOutwardsTransform;
        [HorizontalGroup] [SerializeField] public Transform openInwardsTransform;
        [HorizontalGroup] [SerializeField] public Transform closeTransform;
    }
    public class Door : MonoBehaviour, IInteractable
    {
        [field: SerializeField] public KeyCode InteractKey { get; set; }
        [field: SerializeField] public bool canInteract { get; set; }
        [field: SerializeField] public TextMeshPro InteractionText { get; set; }
        [field: SerializeField] public string title { get; set; }
        [field: SerializeField] public string info { get; set; }
        [field: SerializeField] public InteractionState interactionState { get; set; }
        [SerializeField] protected DoorState doorState=DoorState.Close;

        public virtual void Start()
        {
            canInteract = true;
        }
        public void DeHighlight()
        {
            interactionState = InteractionState.DeHighlighted;
        }
        public void Highlight()
        {
            if (interactionState == InteractionState.DeHighlighted)
            {
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
                canInteract = true;
            }
        }
        public void DisAllowInteraction()
        {
            if (interactionState == InteractionState.Interactable)
            {
                interactionState = InteractionState.DeHighlighted;
                Highlight();
            }
        }
        public virtual void Interact(Transform interactor)
        {
            if (interactionState == InteractionState.Interactable)
            {
                canInteract = false;
                DeHighlight();
                switch (doorState)
                {
                    case DoorState.Open:
                        CloseDoor();
                        break;
                    case DoorState.Close:
                        OpenDoor();
                        break;
                }
            }
        }
        public virtual void SetInteractablty(bool state)
        {
            canInteract = state;
        }
        public virtual void OpenDoor()
        {
            doorState = DoorState.Open;
        }
        public virtual void CloseDoor()
        {
            doorState = DoorState.Close;
        }
    }
}