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
    [CreateAssetMenu(fileName = "Dialogues", menuName = "Dialogues/NewDialogue")]
    public class DialogueScriptableObject : ScriptableObject
    {
        public string dialogueId;
        public bool disposeAfter;
        public List<DialogueSpeakers> dialogueSpeakers = new List<DialogueSpeakers>();
        public List<SentenceScriptableObject> sentenceScriptableObjects = new List<SentenceScriptableObject>();
        [Button]
        public void GetSpeakers()
        {
            foreach (var sentence in sentenceScriptableObjects)
            {
                if (dialogueSpeakers.Any(any=>any == sentence.speaker))
                {
                    return;
                }
                dialogueSpeakers.Add(sentence.speaker);
            }
        }
    }
}