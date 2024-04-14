using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using _Game.Scripts.Managers;
using _Game.Scripts.Dialogue;
namespace _Game.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerMovement playerMovement;
        public PlayerAnimationController playerAnimationController;
        public PlayerHealth playerHealth;
        public PlayerCombat playerCombat;
        public PlayerCamera playerCamera;
        public PlayerDialogueSpeaker playerDialgueSpeaker;
        public Camera mainCamera;

        [HideInInspector]public InputManager inputManager;
        private void Start()
        {
            inputManager = InputManager.Instance;
            mainCamera = Camera.main;

        }
    }
}