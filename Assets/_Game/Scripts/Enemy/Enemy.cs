using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace _Game.Scripts.Enemy
{
    public class Enemy : MonoBehaviour
    {
        public StateMachine stateMachine;
        public EnemyMovement movement;
        public EnemyHealth health;
        public EnemyAnimationController animationController;
        public NavMeshAgent navMeshAgent;
        public Transform currentDestination;
        public Transform currentLookTarget;
        public Collider hitbox;

        public virtual void Awake()
        {
            stateMachine = new StateMachine();
        }
    }
}