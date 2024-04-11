using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Managers;

namespace _Game.Scripts.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        public enum AirMovement
        {
            Jump,
            Fall,
            Land,
        }
        // animation IDs
        #region
        private int _DirectionX => Animator.StringToHash("DirectionX");
        private int _DirectionY => Animator.StringToHash("DirectionY");
        private int _animIDGrounded => Animator.StringToHash("Grounded");
        private int _animIDJump => Animator.StringToHash("Jump");
        private int _animIDFreeFall => Animator.StringToHash("FreeFall");
        private int _animIDMotionSpeed => Animator.StringToHash("AnimationSpeed");
        #endregion
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;
        [SerializeField]private Player player;
        public void SetMovement(float directionX, float directionY, bool isRunning, float animSpeed)
        {
            if (isRunning)
            {
                _animator.SetFloat(_DirectionX, directionX*2, 0.05f, Time.deltaTime);
                _animator.SetFloat(_DirectionY, directionY*2, 0.05f, Time.deltaTime);
            }
            else
            {
                _animator.SetFloat(_DirectionX, directionX, 0.05f, Time.deltaTime);
                _animator.SetFloat(_DirectionY, directionY, 0.05f, Time.deltaTime);
            }
            _animator.SetFloat(_animIDMotionSpeed, animSpeed);
        }
        public void SetAirMovement(AirMovement airMovement, bool value)
        {
            switch (airMovement)
            {
                case AirMovement.Jump:
                    _animator.SetBool(_animIDJump, value);

                    break;
                case AirMovement.Fall:
                    _animator.SetBool(_animIDFreeFall, value);
                    break;
                case AirMovement.Land:
                    _animator.SetBool(_animIDGrounded, value);
                    break;
            }
        }
        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                SoundManager.Instance.PlayRandomSoundOnGroup(SoundType.Walk);
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                SoundManager.Instance.PlayOneShotSound(SoundType.Land);
            }
        }
    }
}