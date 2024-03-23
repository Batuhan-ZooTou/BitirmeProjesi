using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Extensions;
using UnityEngine.InputSystem;

namespace _Game.Scripts.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public PlayerInputAsset playerInputAsset;
        [Header("Character Input Values")]
        public Vector2 moveInput;
        public Vector2 lookInput;
        public bool isSprinting;
        public bool isJumped;
        public bool isShoting;
        public bool isAiming;

        protected override void Awake()
        {
            base.Awake();
            playerInputAsset = new PlayerInputAsset();
            playerInputAsset.Player.Enable();
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void OnEnable()
        {
            playerInputAsset.Player.Jump.started += Jump;
            playerInputAsset.Player.Sprint.started += Sprint;
            playerInputAsset.Player.Sprint.canceled += Sprint;
            playerInputAsset.Player.Shot.started += Shot;
            playerInputAsset.Player.Shot.canceled += Shot;
            playerInputAsset.Player.Aim.started += Aim;
            playerInputAsset.Player.Aim.canceled += Aim;
        }
        private void OnDisable()
        {
            playerInputAsset.Player.Jump.started -= Jump;
            playerInputAsset.Player.Sprint.started -= Sprint;
            playerInputAsset.Player.Sprint.canceled -= Sprint;
            playerInputAsset.Player.Shot.started -= Shot;
            playerInputAsset.Player.Shot.canceled -= Shot;
            playerInputAsset.Player.Aim.started -= Aim;
            playerInputAsset.Player.Aim.canceled -= Aim;
        }
        private void Update()
        {
            moveInput=playerInputAsset.Player.Move.ReadValue<Vector2>();
            lookInput = playerInputAsset.Player.Look.ReadValue<Vector2>();
        }
        public void Jump(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                isJumped = true;
            }
        }
        public void Sprint(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                isSprinting = true;
            }
            if (context.canceled)
            {
                isSprinting = false;
            }
        }
        public void Shot(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                isShoting = true;
            }
            if (context.canceled)
            {
                isShoting = false;
            }
        }
        public void Aim(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                isAiming = true;
            }
            if (context.canceled)
            {
                isAiming = false;
            }
        }
    }
}