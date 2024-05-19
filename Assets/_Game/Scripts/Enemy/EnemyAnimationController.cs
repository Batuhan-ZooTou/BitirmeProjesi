using _Game.Scripts.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class EnemyAnimationController : CoreAnimationController
    {
        // animation IDs
        #region
        private int _DirectionX => Animator.StringToHash("DirectionX");
        private int _DirectionY => Animator.StringToHash("DirectionY");
        private int _animIDMotionSpeed => Animator.StringToHash("AnimationSpeed");
        #endregion
        
        [SerializeField] private Enemy enemy;
        public void SetMovement(float directionX, float directionY, bool isRunning, float animSpeed)
        {
            if (isRunning)
            {
                animator.SetFloat(_DirectionX, directionX * 2, 0.05f, Time.deltaTime);
                animator.SetFloat(_DirectionY, directionY * 2, 0.05f, Time.deltaTime);
            }
            else
            {
                animator.SetFloat(_DirectionX, directionX, 0.05f, Time.deltaTime);
                animator.SetFloat(_DirectionY, directionY, 0.05f, Time.deltaTime);
            }
            animator.SetFloat(_animIDMotionSpeed, animSpeed);
        }
    }
}