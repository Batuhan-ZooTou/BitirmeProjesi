using _Game.Scripts.Dialogue;
using _Game.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using _Game.Scripts.Managers;
using _Game.Scripts.Helper;

namespace _Game.Scripts.Objects
{
    public class NpcChef : MonoBehaviour ,IInteractable
    {
        [field: SerializeField] public KeyCode InteractKey { get; set; }
        [field: SerializeField] public bool canInteract { get; set; }
        [field: SerializeField] public TextMeshPro InteractionText { get; set; }
        [field: SerializeField] public string title { get; set; }
        [field: SerializeField] public string info { get; set; }
        [field: SerializeField] public InteractionState interactionState { get; set; }
        public DialogueSpeaker dialogueSpeaker;
        public List<DialogueScriptableObject> dialoguesAssigned=new List<DialogueScriptableObject>();
        public Transform talkPoint;
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
            if (interactionState == InteractionState.DeHighlighted)
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
        public void DisAllowInteraction()
        {
            if (interactionState == InteractionState.Interactable)
            {
                interactionState = InteractionState.DeHighlighted;
                Highlight();
            }
        }
        public void Interact()
        {
            if (interactionState == InteractionState.Interactable)
            {
                if (CanStartDialogue())
                {
                    canInteract = false;
                    DeHighlight();
                    DialogueManager.Instance.StartDialogue(dialoguesAssigned[0], talkPoint);
                }
            }
        }
        public bool CanStartDialogue()
        {
            if (dialoguesAssigned.Count>0)
            {
                return true;
            }
            return false;
        }

    }
}