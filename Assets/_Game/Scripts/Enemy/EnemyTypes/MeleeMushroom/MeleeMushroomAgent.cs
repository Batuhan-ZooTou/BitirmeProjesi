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
            At(idle,chaseTarget, new FuncPredicate(() => HasTarget()));
            Any(die, new FuncPredicate(() => IsDead()));
            stateMachine.SetState(idle);

        }
        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
        public bool HasTarget()
        {
            return currentDestination != null;
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