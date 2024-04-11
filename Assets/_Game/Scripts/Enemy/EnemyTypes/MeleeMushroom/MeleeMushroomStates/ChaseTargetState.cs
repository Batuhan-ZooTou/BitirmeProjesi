using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Game.Scripts.Enemy
{
    public class ChaseTargetState : IState
    {
        MeleeMushroomAgent _agent;
        Transform _destination;
        public ChaseTargetState(MeleeMushroomAgent agent, Transform destination)
        {
            _agent = agent;
            _destination = destination;
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
            _agent.movement.MoveTowardsTarget(_destination.position);

        }
        public void FixedUpdate()
        {
        }
    }
}