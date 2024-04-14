using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Managers;
using Sirenix.OdinInspector;
using System;
using System.Linq;
using Cinemachine;
using TMPro;

namespace _Game.Scripts.Dialogue
{
    public enum DialogueSpeakers
    {
        Player,
        NpcChef,
    }
    public class DialogueSpeaker : MonoBehaviour
    {
        public CinemachineVirtualCamera speakerCamera;
        public TextMeshProUGUI dialogueText;
        public GameObject dialogueHolder;
        public SentenceScriptableObject currentSentence;
        public DialogueSpeakers speaker;
        public bool isTalking;
        private void Start()
        {
            DialogueManager.OnDialogueStarted += CheckIfParticipationToDialogue;
            DialogueManager.OnDialogueEnded += OnDialogueEnded;
        }
        private void OnDisable()
        {
            DialogueManager.OnDialogueStarted -= CheckIfParticipationToDialogue;
            DialogueManager.OnDialogueEnded -= OnDialogueEnded;
        }
        public virtual void StartSpeech()
        {
            DialogueManager.Instance.currentSpeaker = this;
            DialogueManager.Instance.StartCoroutine(DialogueManager.Instance.WaitSentenceToEnd(currentSentence.speakTime));
            CameraManager.Instance.SwitchCamera(speakerCamera, true);
            dialogueHolder.SetActive(true);
            dialogueText.SetText(currentSentence.sencetence);
        }
        public virtual void OnDialogueEnded()
        {

        }
        public virtual void EndSpeech()
        {
            isTalking = false;
            dialogueHolder.SetActive(false);
            dialogueText.SetText("");
        }
        public virtual void CheckIfParticipationToDialogue(DialogueScriptableObject currentDialogue,Transform playerMovePosition)
        {
            if (currentDialogue.dialogueSpeakers.Contains(speaker))
            {
                DialogueManager.Instance.currentDialogParticipants.Add(this);
                if (currentDialogue.sentenceScriptableObjects[0].speaker == speaker)
                {
                    currentSentence = currentDialogue.sentenceScriptableObjects[0];
                    StartSpeech();
                }
            }
        }
    }
}