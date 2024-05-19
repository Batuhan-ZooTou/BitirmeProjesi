using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using _Game.Scripts.Extensions;
using Sirenix.OdinInspector;

namespace _Game.Scripts.Enemy
{
    public class MeleeMushroomAgent : Enemy
    {
        public override void Awake()
        {
            base.Awake();
            var idle = new IdleState(this);
            var chaseTarget = new ChaseTargetState(this);
            var die = new DeathState(this);
            var attack = new CombatState(this);
            At(idle,chaseTarget, new FuncPredicate(() => HasTarget()));
            At(attack, chaseTarget, new FuncPredicate(() => !IsTargetInReach()));
            Any(die, new FuncPredicate(() => IsDead()));
            Any(attack, new FuncPredicate(() => IsTargetInReach()));
            stateMachine.SetState(idle);

        }
        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
        public bool HasTarget()
        {
            currentDestination = targetSearchZone.GetTarget();
            currentLookTarget = currentDestination;
            return currentDestination != null;
        }
        public bool IsTargetInReach()
        {
            if (HasTarget())
            {
                return combat.CanReachToTarget();
            }
            return false;
        }
        public bool IsDead()
        {
            return health.isDead;
        }
        void Update()
        {
            stateMachine.Update();
        }

        void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
    }
}