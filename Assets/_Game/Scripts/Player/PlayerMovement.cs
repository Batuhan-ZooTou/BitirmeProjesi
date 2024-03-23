using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Managers;
using UnityEngine.InputSystem;

namespace _Game.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;
        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        [Header("Prefabs References")]
        [SerializeField]private CharacterController _controller;
        [SerializeField] private Player player;
        private GameObject _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main.gameObject;
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;
            //Subscribe to Input Actions

        }
        private void OnEnable()
        {
            
        }
        private void OnDisable()
        {

        }
        private void Update()
        {
            JumpAndGravity();
            GroundedCheck();
            Move();
        }
        public void Move()
        {
            // normalise input direction
            Vector3 inputDirection = new Vector3(player.inputManager.moveInput.x, 0.0f, player.inputManager.moveInput.y);
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = player.inputManager.isSprinting ? SprintSpeed : MoveSpeed;
            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;


            // if there is no input, set the target speed to 0
            if (player.inputManager.moveInput == Vector2.zero)
            {
                targetSpeed = 0.0f;
            }
            else
            {
                // if there is a move input rotate player when the player is moving
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +_mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);
                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }
            // a reference to player direction
            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - 0.1f || currentHorizontalSpeed > targetSpeed + 0.1f)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed,Time.deltaTime * SpeedChangeRate);
                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }
            // move the player
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            // update animator if using character
            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;
            player.playerAnimationController.SetMovement(_animationBlend,1);
        }
        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;
                player.playerAnimationController.SetAirMovement(PlayerAnimationController.AirMovement.Jump, false);
                player.playerAnimationController.SetAirMovement(PlayerAnimationController.AirMovement.Fall, false);
                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (player.inputManager.isJumped && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
                    player.playerAnimationController.SetAirMovement(PlayerAnimationController.AirMovement.Jump, true);

                }
                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;
                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    player.playerAnimationController.SetAirMovement(PlayerAnimationController.AirMovement.Fall, true);
                }
                // if we are not grounded, do not jump
                player.inputManager.isJumped = false;
            }
            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }
        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            // update animator if using character
            player.playerAnimationController.SetAirMovement(PlayerAnimationController.AirMovement.Land, Grounded);

        }
        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }
    }
}