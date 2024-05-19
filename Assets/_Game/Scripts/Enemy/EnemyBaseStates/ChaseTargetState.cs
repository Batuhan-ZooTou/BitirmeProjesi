using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class ChaseTargetState : IState
    {
        Enemy _agent;

        public ChaseTargetState(Enemy agent)
        {
            _agent = agent;
        }
        public void OnEnter()
        {
            Debug.Log("Chasing",_agent.gameObject);
        }
        
        public void OnExit()
        {

        }
        public void Update()
        {
            if (_agent.currentDestination!=null)
            {
                _agent.movement.ContinueMovement();
                _agent.movement.MoveTowardsTarget(_agent.currentDestination.position, _agent.currentLookTarget.position);
            }
        }
        public void FixedUpdate()
        {
        }
    }
}