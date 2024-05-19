using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Game.Scripts.Managers;
using UnityEngine.Animations.Rigging;
using DG.Tweening;
using _Game.Scripts.Combat;

namespace _Game.Scripts.Player
{
    public class PlayerAnimationController : CoreAnimationController
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
        [SerializeField]private Player player;
        [SerializeField]private Rig rig;
        public void SetMovement(float directionX, float directionY, bool isRunning, float animSpeed)
        {
            if (isRunning)
            {
                animator.SetFloat(_DirectionX, directionX*2, 0.05f, Time.deltaTime);
                animator.SetFloat(_DirectionY, directionY*2, 0.05f, Time.deltaTime);
            }
            else
            {
                animator.SetFloat(_DirectionX, directionX, 0.05f, Time.deltaTime);
                animator.SetFloat(_DirectionY, directionY, 0.05f, Time.deltaTime);
            }
            animator.SetFloat(_animIDMotionSpeed, animSpeed);
        }
        public void ChangeRigWeight(float target)
        {
            DOVirtual.Float(rig.weight, target, 1, (float current) =>
            {
                rig.weight = current;
            });
        }
        public void SetAirMovement(AirMovement airMovement, bool value)
        {
            switch (airMovement)
            {
                case AirMovement.Jump:
                    animator.SetBool(_animIDJump, value);

                    break;
                case AirMovement.Fall:
                    animator.SetBool(_animIDFreeFall, value);
                    break;
                case AirMovement.Land:
                    animator.SetBool(_animIDGrounded, value);
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