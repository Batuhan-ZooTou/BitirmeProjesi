using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Dialogue;
using _Game.Scripts.Managers;

namespace _Game.Scripts.Player
{
    public class PlayerDialogueSpeaker : DialogueSpeaker
    {
        [SerializeField]private Player player;
        public override void CheckIfParticipationToDialogue(DialogueScriptableObject currentDialogue, Transform playerMovePosition)
        {
            DialogueManager.Instance.currentDialogParticipants.Add(this);
            transform.position = playerMovePosition.position;
            transform.rotation = Quaternion.LookRotation(playerMovePosition.forward, Vector3.up);
            player.playerAnimationController.ChangeRigWeight(0);
            if (currentDialogue.sentenceScriptableObjects[0].speaker == speaker)
            {
                currentSentence = currentDialogue.sentenceScriptableObjects[0];
                StartSpeech();
            }
        }
        public override void OnDialogueEnded()
        {
            player.playerAnimationController.ChangeRigWeight(1);
        }
    }
}