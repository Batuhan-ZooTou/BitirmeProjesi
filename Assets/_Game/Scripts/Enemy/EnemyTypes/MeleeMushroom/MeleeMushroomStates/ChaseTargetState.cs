using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class ChaseTargetState : IState
    {
        MeleeMushroomAgent _agent;

        public ChaseTargetState(MeleeMushroomAgent agent)
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
            _agent.movement.MoveTowardsTarget(_agent.currentDestination.position, _agent.currentLookTarget.position);
        }
        public void FixedUpdate()
        {
        }
    }
}