using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        private int _animIDSpeed => Animator.StringToHash("Speed");
        private int _animIDGrounded => Animator.StringToHash("Grounded");
        private int _animIDJump => Animator.StringToHash("Jump");
        private int _animIDFreeFall => Animator.StringToHash("FreeFall");
        private int _animIDMotionSpeed => Animator.StringToHash("MotionSpeed");
        #endregion
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;
        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;
        [SerializeField]private Player player;
        public void SetMovement(float value, float animSpeed)
        {
            _animator.SetFloat(_animIDSpeed, value);
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
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(transform.position), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(transform.position), FootstepAudioVolume);
            }
        }

    }
}