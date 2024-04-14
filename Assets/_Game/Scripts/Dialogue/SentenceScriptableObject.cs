using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Interfaces;
using _Game.Scripts.Managers;

namespace _Game.Scripts.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogues", menuName = "Dialogues/Sentence")]
    public class SentenceScriptableObject : ScriptableObject
    {
        public DialogueSpeakers speaker;
        public int index;
        public float speakTime;
        public string sencetence;
    }
}