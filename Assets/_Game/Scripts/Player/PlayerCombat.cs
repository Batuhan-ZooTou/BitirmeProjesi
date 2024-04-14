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
        [SerializeField] private LayerMask mouseColliderLayerMask = new LayerMask();
        [SerializeField] public Transform lookPosition;
        [SerializeField] private float lookPositionSpeed;
        [SerializeField] private float defaultLookRange;
        void Start()
        {
            player.inputManager = InputManager.Instance;
            player.inputManager.playerInputAsset.Player.Shot.performed += Shot;
            player.inputManager.playerInputAsset.Player.Reload.started += Reload;


        }
        private void OnDisable()
        {
            player.inputManager.playerInputAsset.Player.Shot.performed -= Shot;
            player.inputManager.playerInputAsset.Player.Reload.started -= Reload;
        }
        void Update()
        {
            if (!player.inputManager.inputDisabled)
            {
                Vector2 screenPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Ray ray = player.mainCamera.ScreenPointToRay(screenPoint);
                if (Physics.Raycast(ray, out RaycastHit raycastHit, defaultLookRange, mouseColliderLayerMask))
                {
                    lookPosition.position = Vector3.Lerp(lookPosition.position, raycastHit.point, lookPositionSpeed * Time.deltaTime);
                }
                else
                {
                    lookPosition.position = Vector3.Lerp(lookPosition.position, ray.GetPoint(defaultLookRange), lookPositionSpeed * Time.deltaTime);
                }
                //Debug.Log(Vector3.Dot((lookPosition.position - gun.muzzlePosition.position).normalized, gun.muzzlePosition.forward));
                //Debug.DrawRay(ray.GetPoint(0), (lookPosition.position- ray.GetPoint(0)).normalized* defaultLookRange, Color.green);
                //Debug.DrawRay(gun.muzzlePosition.position, (lookPosition.position - gun.muzzlePosition.position).normalized * defaultLookRange, Color.red);
            }
        }
        public void Shot(InputAction.CallbackContext context)
        {
            gun.Shot();
            //if (gun.isSingleShot)
            //{
            //    playerInput.isShoting = false;
            //}
        }
        public void Reload(InputAction.CallbackContext context)
        {
            gun.SetAction(ScriptableObjects.GunActionState.Reloading);
        }
    }
}