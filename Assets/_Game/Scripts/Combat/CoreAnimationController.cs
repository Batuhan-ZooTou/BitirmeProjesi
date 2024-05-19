using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Combat
{
    public class CoreAnimationController : MonoBehaviour
    {
        [SerializeField]protected Animator animator;
        public virtual void SetAnimator(bool state)
        {
            animator.enabled = state;
        }
    }
}