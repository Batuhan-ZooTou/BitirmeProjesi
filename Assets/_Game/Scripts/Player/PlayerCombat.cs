using _Game.Scripts.Managers;
using _Game.Scripts.Player.Guns;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Game.Scripts.Player
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField]private BasicGun gun;
        void Start()
        {
            player.inputManager = InputManager.Instance;
            player.inputManager.playerInputAsset.Player.Shot.started += Shot;

        }
        private void OnDisable()
        {
            player.inputManager.playerInputAsset.Player.Shot.started -= Shot;
        }
        void Update()
        {

        }
        public void Shot(InputAction.CallbackContext context)
        {
            gun.Shot();
            //if (gun.isSingleShot)
            //{
            //    playerInput.isShoting = false;
            //}
        }
    }
}