using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class CombatState : IState
    {
        protected Enemy _agent;

        public CombatState(Enemy agent)
        {
            _agent = agent;
        }
        public void Update()
        {
            _agent.movement.MoveTowardsTarget(_agent.currentDestination.position, _agent.currentLookTarget.position);
        }
        public void FixedUpdate()
        {
        }

        public void OnEnter()
        {
            Debug.Log("Attacking", _agent.gameObject);
        }

        public void OnExit()
        {

        }
    }
}