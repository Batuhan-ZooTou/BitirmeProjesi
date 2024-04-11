using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        // animation IDs
        #region
        private int _DirectionX => Animator.StringToHash("DirectionX");
        private int _DirectionY => Animator.StringToHash("DirectionY");
        private int _animIDMotionSpeed => Animator.StringToHash("AnimationSpeed");
        #endregion
        [SerializeField] private Animator _animator;
        public Animator Animator => _animator;
        [SerializeField] private Enemy enemy;
        public void SetMovement(float directionX, float directionY, bool isRunning, float animSpeed)
        {
            if (isRunning)
            {
                _animator.SetFloat(_DirectionX, directionX * 2, 0.05f, Time.deltaTime);
                _animator.SetFloat(_DirectionY, directionY * 2, 0.05f, Time.deltaTime);
            }
            else
            {
                _animator.SetFloat(_DirectionX, directionX, 0.05f, Time.deltaTime);
                _animator.SetFloat(_DirectionY, directionY, 0.05f, Time.deltaTime);
            }
            _animator.SetFloat(_animIDMotionSpeed, animSpeed);
        }
    }
}