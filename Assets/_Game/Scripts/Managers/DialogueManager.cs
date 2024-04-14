using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Extensions;
using _Game.Scripts.Dialogue;
using _Game.Scripts.Interfaces;
using System;
using Sirenix.OdinInspector;

namespace _Game.Scripts.Managers
{
    
    public class DialogueManager : Singleton<DialogueManager>
    {
        public List<DialogueScriptableObject> dialogues=new List<DialogueScriptableObject>();
        public List<DialogueSpeaker> currentDialogParticipants = new List<DialogueSpeaker>();
        public DialogueSpeaker currentSpeaker;
        public DialogueScriptableObject currentDialogue;
        private int currentDialogueIndex;
        public static Action<DialogueScriptableObject,Transform> OnDialogueStarted;
        public static Action OnDialogueEnded;
        public void StartDialogue(DialogueScriptableObject dialogue, Transform playerMovePosition)
        {
            OnDialogueStarted?.Invoke(dialogue, playerMovePosition);
            InputManager.Instance.DisableInput();
            currentDialogue = dialogue;
        }
        public IEnumerator WaitSentenceToEnd(float time)
        {
            yield return new WaitForSeconds(time);
            NextDialogue();
        }
        [Button]
        public void NextDialogue()
        {
            currentSpeaker.EndSpeech();
            currentDialogueIndex++;
            if (currentDialogueIndex>=currentDialogue.sentenceScriptableObjects.Count)
            {
                EndDialogue();
                return;
            }
            foreach (var nextSpeaker in currentDialogParticipants)
            {
                if (nextSpeaker.speaker == currentDialogue.sentenceScriptableObjects[currentDialogueIndex].speaker)
                {
                    nextSpeaker.currentSentence = currentDialogue.sentenceScriptableObjects[currentDialogueIndex];
                    nextSpeaker.StartSpeech();
                    return;
                }
            }
        }
        public void EndDialogue()
        {
            OnDialogueEnded?.Invoke();
            currentDialogueIndex = 0;
            currentDialogParticipants.Clear();
            InputManager.Instance.EnableInput();
            CameraManager.Instance.SwitchToDefaultCam();
        }
        
    }
}